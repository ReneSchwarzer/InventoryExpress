using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    /// Eine Seite, die zu Bearbeitunng oder Neuerstellung eines Inventars eingesetzt wird
    /// </summary>
    public sealed partial class PageInventoryItemEdit : Page
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryItemEdit()
        {
            this.InitializeComponent();

            ProgressBar.DataContext = ViewModel.Instance;
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
                InventoryID.IsEnabled = false;
            }
            else
            {
                var resourceLoader = ResourceLoader.GetForCurrentView();
                Titel.Text = resourceLoader.GetString("PageInventoryItemAddLabel/Text");

                DataContext = new Model.Inventory();
            }

            ManufacturerComboBox.ItemsSource = ViewModel.Instance.Manufacturers;
            LocationComboBox.ItemsSource = ViewModel.Instance.Locations;
            GLAccountComboBox.ItemsSource = ViewModel.Instance.GLAccounts;
            CostCenterComboBox.ItemsSource = ViewModel.Instance.CostCenters;
            SupplierComboBox.ItemsSource = ViewModel.Instance.Suppliers;
            StateComboBox.ItemsSource = ViewModel.Instance.States;

            var parents = new List<Inventory>();
            parents.Add(new Inventory());
            parents.AddRange(from x in ViewModel.Instance.Inventorys where x != DataContext select x);
            ParentComboBox.ItemsSource = parents;

            foreach (var v in Model.ViewModel.Instance.Templates)
            {
                TemplateComboBox.Items.Add(v);
            }

            Update();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Inventar gespeichert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnSaveAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var inventory = DataContext as Model.Inventory;
            if (inventory != null)
            {
                if (string.IsNullOrWhiteSpace(inventory.Name))
                {
                    MessageDialog msg = new MessageDialog
                    (
                        resourceLoader.GetString("MsgCompulsoryStatement/Text"),
                        resourceLoader.GetString("MsgTitleHint/Text")
                    );
                    await msg.ShowAsync();

                    return;
                }
                else if (!Model.ViewModel.Instance.Inventorys.Contains(inventory) &&
                          Model.ViewModel.Instance.Inventorys.Find(f => f != null && !string.IsNullOrWhiteSpace(f.Name) && f.Equals(inventory.Name)) != null)
                {
                    MessageDialog msg = new MessageDialog
                    (
                        resourceLoader.GetString("MsgUniqe/Text"),
                        resourceLoader.GetString("MsgTitleHint/Text")
                    );
                    await msg.ShowAsync();

                    return;
                }

                IsEnabled = false;
                ButtonBar.Visibility = Visibility.Collapsed;

                inventory.Commit(true);
                
                IsEnabled = true;
                ButtonBar.Visibility = Visibility.Visible;
                
                if (!ViewModel.Instance.Inventorys.Contains(inventory))
                {
                    ViewModel.Instance.Inventorys.Add(inventory);
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
            var inventory = DataContext as Inventory;
            if (inventory != null)
            {
                // Daten verwerfen
                inventory.Rollback();
            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Vorlage vom Benutzer geändert wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        /// <summary>
        /// Aktualisieren
        /// </summary>
        private void Update()
        {
            var inventory = DataContext as Inventory;
            if (inventory != null && inventory.Template != null)
            {
                // Leere Attribute löschen
                var attributes = (from x in inventory.Attributes
                                  where !string.IsNullOrWhiteSpace(x.Value)
                                  select x).ToList();

                // Template-Attribute übernehmen
                foreach (var v in inventory.Template.Attributes)
                {
                    if (attributes.Find
                        (
                            f =>
                            f.Name.Equals
                            (
                                v.Name,
                                StringComparison.OrdinalIgnoreCase
                            )
                        ) == null)
                    {
                        attributes.Add(new AttributeTextValue()
                        {
                            Name = v.Name,
                            Tag = v.Tag,
                            Memo = v.Memo
                        });
                    }
                }

                inventory.Attributes = attributes;
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Inventar gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnDeleteAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var inventory = DataContext as Model.Inventory;
            var exist = await ApplicationData.Current.LocalFolder.TryGetItemAsync(inventory.ID + ".inventory");

            if (inventory != null && exist != null)
            {
                MessageDialog msg = new MessageDialog
                (
                    resourceLoader.GetString("MsgDelInventoryAsk/Text"),
                    resourceLoader.GetString("MsgTitleDel/Text")
                );
                msg.Commands.Add(new UICommand(resourceLoader.GetString("MsgYes/Text"), async c =>
                {
                    // Daten löschen
                    Model.ViewModel.Instance.Inventorys.Remove(inventory);

                    // Datei löschen
                    var file = await ApplicationData.Current.LocalFolder.GetFileAsync(inventory.ID + ".inventory");
                    await file.DeleteAsync(StorageDeleteOption.PermanentDelete);

                    Frame.Navigate(typeof(PageMain));
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
        /// Wird aufgerufen, wenn ein Bild geladen werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnOpenImageAsync(object sender, RoutedEventArgs e)
        {
            var inventory = DataContext as Model.Inventory;

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                inventory.ImageBase64 = await Item.ConvertToIBase64Async(file, 300);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Bild entfernt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnRemoveImage(object sender, RoutedEventArgs e)
        {
            var inventory = DataContext as Model.Inventory;

            inventory.ImageBase64 = null;
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich der Status der Checkbox ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnCheck(object sender, RoutedEventArgs e)
        {
            DerecognitionDatePicker.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich der Status der Checkbox ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnUnCheck(object sender, RoutedEventArgs e)
        {
            DerecognitionDatePicker.Visibility = Visibility.Collapsed;
        }
    }
}
