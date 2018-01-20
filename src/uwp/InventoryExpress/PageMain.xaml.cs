using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
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
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class PageMain : Page
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageMain()
        {
            this.InitializeComponent();

            ViewModel.Instance.Loaded += (s, a) =>
            {
                DataContext = null;
                DataContext = ViewModel.Instance;
                ProgressBar.DataContext = ViewModel.Instance;
            };  
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu dieser Seite gewechselt wird
        /// </summary>
        /// <param name="e">Der Auslöser des Events</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = ViewModel.Instance;
        }

        /// <summary>
        /// Wird aufgerufen, wenn zur den Herstellern gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToManufacturerPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageManufacturer));
        }

        /// <summary>
        /// Wird aufgerufen, wenn zur den Herstellern gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToSupplierPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageSupplier));
        }

        /// <summary>
        /// Wird aufgerufen, wenn zur den Standorten gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToLocationPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageLocation));
        }

        /// <summary>
        /// Wird aufgerufen, wenn zur den Standorten gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToCostCenterPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageCostCenter));
        }

        /// <summary>
        /// Wird aufgerufen, wenn zur den Sachkonten gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToGLAccountPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageGLAccount));
        }

        /// <summary>
        /// Wird aufgerufen, wenn zur Hilfe gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToHelpPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageMainHelp), DataContext);
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein neues Konto erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToAddPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageInventoryItemEdit), null);
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Listenelement geöffnet werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(PageInventoryItem), e.ClickedItem);
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Daten importiert werden sollen
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnImportAsync(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".inv");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                await ViewModel.Instance.ImportAsync(file);

                // Neu laden
                await ViewModel.Instance.Load();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Daten exportiert werden sollen
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnExportAsync(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeChoices.Add("Inventar", new List<string>() { ".inv" });
            picker.SuggestedFileName = DateTime.Now.ToString("yyyyMMdd");
            picker.DefaultFileExtension = ".inv";

            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                await ViewModel.Instance.ExportAsync(file);
            }
        }
    }
}
