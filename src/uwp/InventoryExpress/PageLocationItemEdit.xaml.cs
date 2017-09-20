using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace InventoryExpress
{
    /// <summary>
    /// Eine Seite zum Bearbeiten oder zur Neuanlage von Standortn und deren Attribute
    /// </summary>
    public sealed partial class PageLocationItemEdit : Page
    {
        private PrintHelper PrintHelper { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocationItemEdit()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu dieser Seite gewechselt wird
        /// </summary>
        /// <param name="e">Der Auslöser des Events</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                DataContext = e.Parameter;
            }
            else
            {
                // aus PageAccountItemAddLabel.Text
                var resourceLoader = ResourceLoader.GetForCurrentView();
                Titel.Text = resourceLoader.GetString("PageLocationItemAddLabel/Text");

                DataContext = new Model.Location();
            }

            // Initalize common helper class and register for printing
            PrintHelper = new PrintHelper(this);
            PrintHelper.RegisterForPrinting();

            // Initialize print content for this scenario
            PrintHelper.PreparePrintContent(new PageLocationItemPrint());
        }

        /// <summary>
        /// Wird aufgerufen wenn der Benutzer die Seite verlässt
        /// </summary>
        /// <param name="e">Der Auslöser des Events</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (PrintHelper != null)
            {
                PrintHelper.UnregisterForPrinting();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Standort gespeichert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnSaveAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var Location = DataContext as Model.Location;
            if (Location != null)
            {
                if (string.IsNullOrWhiteSpace(Location.Name))
                {
                    MessageDialog msg = new MessageDialog
                    (
                        resourceLoader.GetString("MsgCompulsoryStatement/Text"),
                        resourceLoader.GetString("MsgTitleHint/Text")
                    );
                    await msg.ShowAsync();

                    return;
                }
                else if (!Model.ViewModel.Instance.Locations.Contains(Location) &&
                          Model.ViewModel.Instance.Locations.Find(f => f != null && !string.IsNullOrWhiteSpace(f.Name) && f.Equals(Location.Name)) != null)
                {
                    MessageDialog msg = new MessageDialog
                    (
                        resourceLoader.GetString("MsgUniqe/Text"),
                        resourceLoader.GetString("MsgTitleHint/Text")
                    );
                    await msg.ShowAsync();

                    return;
                }

                ProgressRing.Visibility = Visibility.Visible;
                ProgressRing.IsActive = true;
                IsEnabled = false;
                ButtonBar.Visibility = Visibility.Collapsed;

                Location.Commit(true);

                IsEnabled = true;
                ButtonBar.Visibility = Visibility.Visible;
                ProgressRing.Visibility = Visibility.Collapsed;
                ProgressRing.IsActive = false;

                if (!ViewModel.Instance.Locations.Contains(Location))
                {
                    ViewModel.Instance.Locations.Add(Location);
                }
            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Eingaben verworfen werden sollen
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnCancelAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var Location = DataContext as Location;
            if (Location != null)
            {
                // Daten verwerfen
                Location.Rollback();
            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Standort gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnDeleteAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var Location = DataContext as Location;
            var exist = await ApplicationData.Current.LocalFolder.TryGetItemAsync(Location.ID + ".Location");

            if (Location != null && exist != null)
            {
                MessageDialog msg = new MessageDialog
                (
                    resourceLoader.GetString("MsgDelAccountAsk/Text"),
                    resourceLoader.GetString("MsgTitleDel/Text")
                );
                msg.Commands.Add(new UICommand(resourceLoader.GetString("MsgYes/Text"), async c =>
                {
                    // Daten löschen
                    Model.ViewModel.Instance.Locations.Remove(Location);

                    // Datei löschen
                    var file = await ApplicationData.Current.LocalFolder.GetFileAsync(Location.ID + ".Location");
                    await file.DeleteAsync(StorageDeleteOption.PermanentDelete);

                    if (Frame.CanGoBack)
                    {
                        Frame.GoBack();
                    }
                }));
                msg.Commands.Add(new UICommand(resourceLoader.GetString("MsgNo/Text")));

                msg.DefaultCommandIndex = 0;
                msg.CancelCommandIndex = 1;

                await msg.ShowAsync();
            }
            else
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn zur Hilfe gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToHelpPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageLocationItemEditHelp), DataContext);
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Bild geladen werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnOpenImageAsync(object sender, RoutedEventArgs e)
        {
            var Location = DataContext as Location;

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                Location.ImageBase64 = await Item.ConvertToIBase64Async(file, 200);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Bild entfernt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnRemoveImage(object sender, RoutedEventArgs e)
        {
            var Location = DataContext as Location;

            Location.ImageBase64 = null;
        }

        /// <summary>
        /// Wird aufgerufen, wenn gedruckt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        async private void OnPrintButtonClick(object sender, RoutedEventArgs e)
        {
            if (Windows.Graphics.Printing.PrintManager.IsSupported())
            {
                try
                {
                    // Show print UI
                    await Windows.Graphics.Printing.PrintManager.ShowPrintUIAsync();

                }
                catch
                {
                    // Printing cannot proceed at this time
                    ContentDialog noPrintingDialog = new ContentDialog()
                    {
                        Title = "Printing error",
                        Content = "\nSorry, printing can' t proceed at this time.",
                        PrimaryButtonText = "OK"
                    };
                    await noPrintingDialog.ShowAsync();
                }
            }
            else
            {
                // Printing is not supported on this device
                ContentDialog noPrintingDialog = new ContentDialog()
                {
                    Title = "Printing not supported",
                    Content = "\nSorry, printing is not supported on this device.",
                    PrimaryButtonText = "OK"
                };
                await noPrintingDialog.ShowAsync();
            }
        }
    }
}
