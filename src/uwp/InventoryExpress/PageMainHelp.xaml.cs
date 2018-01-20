using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace InventoryExpress
{
    /// <summary>
    /// Die Hilfeseite mit Informationen über das Programm 
    /// </summary>
    public sealed partial class PageMainHelp : Page
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageMainHelp()
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

            var currentView = SystemNavigationManager.GetForCurrentView();

            DataContext = e.Parameter;

            Version.Text = string.Format
                (
                    "Version {0}.{1}.{2}.{3}",
                    Package.Current.Id.Version.Major,
                    Package.Current.Id.Version.Minor,
                    Package.Current.Id.Version.Build,
                    Package.Current.Id.Version.Revision
                );
        }

        /// <summary>
        /// Wird aufgerufen, wenn zur vorigen Seite gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToBackPage(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Beispieldaten bereitgestellt werden sollen
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnClickButtonAsync(object sender, RoutedEventArgs e)
        {
            ProgressRing.Visibility = Visibility.Visible;

            await ViewModel.Instance.CreateSample();

            await ViewModel.Instance.InitAsync();

            ProgressRing.Visibility = Visibility.Collapsed;
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
        /// Wird aufgerufen, wenn zur den Sachkonten gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToHomePage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageMain));
        }
    }
}
