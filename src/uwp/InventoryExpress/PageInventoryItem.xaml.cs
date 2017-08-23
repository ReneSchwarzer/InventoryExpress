using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu dieser Seite gewechselt wird
        /// </summary>
        /// <param name="e">Der Auslöser des Events</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = e.Parameter;
            var inventory = e.Parameter as Model.Inventory;

            var currentView = SystemNavigationManager.GetForCurrentView();

            ProgressRing.Visibility = Visibility.Visible;
            ProgressRing.IsActive = true;
            ButtonBar.Visibility = Visibility.Collapsed;
            IsEnabled = false;

            ProgressRing.Visibility = Visibility.Collapsed;
            ProgressRing.IsActive = false;
            ButtonBar.Visibility = Visibility.Visible;
            IsEnabled = true;

            // Initalize common helper class and register for printing
            PrintHelper = new PrintHelper(this);
            PrintHelper.RegisterForPrinting();

            // Initialize print content for this scenario
            PrintHelper.PreparePrintContent(new PageInventoryItemPrint());
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
            Frame.Navigate(typeof(PageInventoryItemEdit), DataContext);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das aktuelle Konto zum Favorieten gemacht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnLike(object sender, RoutedEventArgs e)
        {
            var inventory = DataContext as Model.Inventory;
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
            Frame.Navigate(typeof(PageAscription), DataContext);
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
