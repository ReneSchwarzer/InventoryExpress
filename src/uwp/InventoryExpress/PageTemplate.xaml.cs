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
    /// Eine Seite, die zur Verwaltung der Vorlagen eingesetzt wird
    /// </summary>
    public sealed partial class PageTemplate : Page
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplate()
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

            ProgressRing.IsActive = true;

            DataContext = Model.ViewModel.Instance;

            ProgressRing.IsActive = false;

            DataContext = ViewModel.Instance;
        }

        /// <summary>
        /// Wird aufgerufen, wenn zur Hilfe gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToHelpPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageTemplateHelp), DataContext);
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein neuer Vorlagen erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToAddPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageTemplateItemEdit), null);
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Listenelement geöffnet werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(PageTemplateItemEdit), e.ClickedItem);
        }

        /// <summary>
        /// Wird aufgerufen, wenn zur Startseite gewechselt werden soll
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
        /// Wird aufgerufen, wenn zu Hilfeseite gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToEditPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageTemplateItemEdit), DataContext);
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu Attributseite gewechselt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnNavigateToAttributePage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageAttribute), DataContext);
        }
    }
}
