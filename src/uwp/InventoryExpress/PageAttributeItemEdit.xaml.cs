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
    /// Eine Seite zum Bearbeiten oder zur Neuanlage von Attributn und deren Attribute
    /// </summary>
    public sealed partial class PageAttributeItemEdit : Page
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageAttributeItemEdit()
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
                Titel.Text = resourceLoader.GetString("PageAttributeItemAddLabel/Text");

                DataContext = new Model.Attribute();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Attribut gespeichert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnSaveAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var Attribute = DataContext as Model.Attribute;
            if (Attribute != null && resourceLoader != null)
            {
                if (string.IsNullOrWhiteSpace(Attribute.Name))
                {
                    MessageDialog msg = new MessageDialog
                    (
                        resourceLoader.GetString("MsgCompulsoryStatement/Text"),
                        resourceLoader.GetString("MsgTitleHint/Text")
                    );
                    await msg.ShowAsync();

                    return;
                }
                else if (!Model.ViewModel.Instance.Attributes.Contains(Attribute) &&
                          Model.ViewModel.Instance.Attributes.Find(f => f != null && !string.IsNullOrWhiteSpace(f.Name) && f.Equals(Attribute.Name)) != null)
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

                Attribute.Commit(true);

                IsEnabled = true;
                ButtonBar.Visibility = Visibility.Visible;
                ProgressRing.Visibility = Visibility.Collapsed;
                ProgressRing.IsActive = false;

                if (!ViewModel.Instance.Attributes.Contains(Attribute))
                {
                    ViewModel.Instance.Attributes.Add(Attribute);
                }
            }

            if (Frame != null && Frame.CanGoBack)
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
            var Attribute = DataContext as Model.Attribute;
            if (Attribute != null)
            {
                // Daten verwerfen
                Attribute.Rollback();
            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Attribut gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnDeleteAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var Attribute = DataContext as Model.Attribute;
            var exist = await ApplicationData.Current.LocalFolder.TryGetItemAsync(Attribute.ID + ".Attribute");

            if (Attribute != null && exist != null)
            {
                MessageDialog msg = new MessageDialog
                (
                    resourceLoader.GetString("MsgDelAccountAsk/Text"),
                    resourceLoader.GetString("MsgTitleDel/Text")
                );
                msg.Commands.Add(new UICommand(resourceLoader.GetString("MsgYes/Text"), async c =>
                {
                    // Daten löschen
                    Model.ViewModel.Instance.Attributes.Remove(Attribute);

                    // Datei löschen
                    var file = await ApplicationData.Current.LocalFolder.GetFileAsync(Attribute.ID + ".Attribute");
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
            Frame.Navigate(typeof(PageAttributeItemEditHelp), DataContext);
        }
    }
}
