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

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryItem()
        {
            this.InitializeComponent();

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

            // Initalize common helper class and register for printing
            PrintHelper = new PrintHelper(this);
            PrintHelper.RegisterForPrinting();

            // Initialize print content for this scenario
            PrintHelper.PreparePrintContent(new PageInventoryItemPrint() { DataContext = inventory });
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
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
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
            var inventory = FlipView.SelectedItem as Model.Inventory;
            if (inventory != null)
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
            var senderElement = sender as FrameworkElement;
            if (senderElement != null)
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
        private void OnNavigateToHelpPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageInventoryItemHelp), DataContext);
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
    }
}
