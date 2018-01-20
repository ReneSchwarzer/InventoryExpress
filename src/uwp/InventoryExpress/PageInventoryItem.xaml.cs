using InventoryExpress.Model;
using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace InventoryExpress
{
    /// <summary>
    /// Eine Seite, die eine Übersicht eines Inventars anzeigt / druckt.
    /// </summary>
    public sealed partial class PageInventoryItem : Page
    {
        private PrintHelper PrintHelper { get; set; }
        private PageInventoryItemPrint PageInventoryItemPrint { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryItem()
        {
            this.InitializeComponent();
            ProgressBar.DataContext = ViewModel.Instance;

            FlipView.ItemsSource = ViewModel.Instance.FilteredInventorys;
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu dieser Seite gewechselt wird
        /// </summary>
        /// <param name="e">Der Auslöser des Events</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = ViewModel.Instance;
            var inventory = e.Parameter as Model.Inventory;
            FlipView.SelectedItem = inventory;

            var currentView = SystemNavigationManager.GetForCurrentView();
            PageInventoryItemPrint = new PageInventoryItemPrint();

            // Initalize common helper class and register for printing
            PrintHelper = new PrintHelper(this);
            PrintHelper.RegisterForPrinting();

            // Initialize print content for this scenario
            PrintHelper.PreparePrintContent(PageInventoryItemPrint);
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
        /// Wird aufgerufen, wenn das aktuelle Konto geändert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToHomePage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageMain), FlipView.SelectedItem);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das aktuelle Konto geändert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToEditPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageInventoryItemEdit), FlipView.SelectedItem);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das aktuelle Konto zum Favorieten gemacht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnLike(object sender, RoutedEventArgs e)
        {
            if (FlipView.SelectedItem is Model.Inventory inventory)
            {
                inventory.Commit(true);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Passwort in die Zwischenablage kopiert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnPasswordToClipboard(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement senderElement)
            {
                // In Tag-Eigenschaft befindet sich der Wert
                var tag = senderElement.Tag.ToString();

                var data = new DataPackage();
                data.SetText(tag);

                Clipboard.SetContent(data);
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn auf ein Listenientrag gehalten wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnHoldingOnItem(object sender, HoldingRoutedEventArgs e)
        {
            var senderElement = sender as FrameworkElement;
            var flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);

            if (flyoutBase != null)
            {
                flyoutBase.ShowAt(senderElement);
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn auf ein Listenientrag mit der rechten Maustaste gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnRightTappedOnItem(object sender, RightTappedRoutedEventArgs e)
        {
            var senderElement = sender as FrameworkElement;
            var flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);

            if (flyoutBase != null)
            {
                flyoutBase.ShowAt(senderElement);
            }
        }
       
        /// <summary>
        /// Wird aufgerufen, wenn zur Hilfe gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToAscriptionPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageAscription), FlipView.SelectedItem);
        }

        /// <summary>
        /// Wird aufgerufen, wenn gedruckt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        async private void OnPrintButtonClick(object sender, RoutedEventArgs e)
        {
            PageInventoryItemPrint.DataContext = FlipView.SelectedItem;

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

        /// <summary>
        /// Wird aufgerufen, wenn sich das Item der FlipView geändert hat
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Like.DataContext = FlipView.SelectedItem;
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
        /// Wird aufgerufen, wenn zur den Zuständen gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToStatePage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageState), FlipView.SelectedItem);
        }

        /// <summary>
        /// Wird aufgerufen, wenn zur den Vorlagen gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToTemplatePage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageTemplate), FlipView.SelectedItem);
        }
    }
}
