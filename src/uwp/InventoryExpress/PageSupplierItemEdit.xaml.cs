﻿using InventoryExpress.Model;
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
    /// Eine Seite zum Bearbeiten oder zur Neuanlage von Lieferantn und deren Attribute
    /// </summary>
    public sealed partial class PageSupplierItemEdit : Page
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierItemEdit()
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
                Titel.Text = resourceLoader.GetString("PageSupplierItemAddLabel/Text");

                DataContext = new Model.Supplier();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Lieferant gespeichert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnSaveAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var Supplier = DataContext as Model.Supplier;
            if (Supplier != null)
            {
                if (string.IsNullOrWhiteSpace(Supplier.Name))
                {
                    MessageDialog msg = new MessageDialog
                    (
                        resourceLoader.GetString("MsgCompulsoryStatement/Text"),
                        resourceLoader.GetString("MsgTitleHint/Text")
                    );
                    await msg.ShowAsync();

                    return;
                }
                else if (!Model.ViewModel.Instance.Suppliers.Contains(Supplier) &&
                          Model.ViewModel.Instance.Suppliers.Find(f => f != null && !string.IsNullOrWhiteSpace(f.Name) && f.Equals(Supplier.Name)) != null)
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

                Supplier.Commit(true);

                IsEnabled = true;
                ButtonBar.Visibility = Visibility.Visible;
                ProgressRing.Visibility = Visibility.Collapsed;
                ProgressRing.IsActive = false;

                if (!ViewModel.Instance.Suppliers.Contains(Supplier))
                {
                    ViewModel.Instance.Suppliers.Add(Supplier);
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
            var Supplier = DataContext as Supplier;
            if (Supplier != null)
            {
                // Daten verwerfen
                Supplier.Rollback();
            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Lieferant gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnDeleteAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var Supplier = DataContext as Supplier;
            var exist = await ApplicationData.Current.LocalFolder.TryGetItemAsync(Supplier.ID + ".Supplier");

            if (Supplier != null && exist != null)
            {
                MessageDialog msg = new MessageDialog
                (
                    resourceLoader.GetString("MsgDelAccountAsk/Text"),
                    resourceLoader.GetString("MsgTitleDel/Text")
                );
                msg.Commands.Add(new UICommand(resourceLoader.GetString("MsgYes/Text"), async c =>
                {
                    // Daten löschen
                    Model.ViewModel.Instance.Suppliers.Remove(Supplier);

                    // Datei löschen
                    var file = await ApplicationData.Current.LocalFolder.GetFileAsync(Supplier.ID + ".Supplier");
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
            Frame.Navigate(typeof(PageSupplierItemEditHelp), DataContext);
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Bild geladen werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnOpenImageAsync(object sender, RoutedEventArgs e)
        {
            var Supplier = DataContext as Supplier;

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                Supplier.ImageBase64 = await Item.ConvertToIBase64Async(file, 200);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Bild entfernt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnRemoveImage(object sender, RoutedEventArgs e)
        {
            var Supplier = DataContext as Supplier;

            Supplier.ImageBase64 = null;
        }
    }
}