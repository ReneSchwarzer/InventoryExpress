using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
            };
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu dieser Seite gewechselt wird
        /// </summary>
        /// <param name="e">Der Auslöser des Events</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ProgressRing.IsActive = true;
         
            DataContext = Model.ViewModel.Instance;

            ProgressRing.IsActive = false;

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
        /// Wird aufgerufen, wenn zur den Vorlagen gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToTemplatePage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageTemplate));
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
            Frame.Navigate(typeof(PageState));
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
    }
}
