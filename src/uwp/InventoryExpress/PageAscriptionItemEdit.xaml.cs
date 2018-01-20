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
    /// Eine Seite zum Bearbeiten oder zur Neuanlage von Zuschreibungn und deren Attribute
    /// </summary>
    public sealed partial class PageAscriptionItemEdit : Page
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageAscriptionItemEdit()
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

            ProgressBar.DataContext = ViewModel.Instance;

            if (e.Parameter is Inventory)
            {
                // aus PageAccountItemAddLabel.Text
                var resourceLoader = ResourceLoader.GetForCurrentView();
                Titel.Text = resourceLoader.GetString("PageAscriptionItemAddLabel/Text");

                DataContext = new Model.Ascription() { Parent = e.Parameter as Inventory };
            }
            else if (e.Parameter is Ascription)
            {
                DataContext = e.Parameter;
            }

            ManufacturerComboBox.ItemsSource = ViewModel.Instance.Manufacturers;
            SupplierComboBox.ItemsSource = ViewModel.Instance.Suppliers;
            StateComboBox.ItemsSource = ViewModel.Instance.States;

            foreach (var v in Model.ViewModel.Instance.Templates)
            {
                TemplateComboBox.Items.Add(v);
            }

            Update();

        }

        /// <summary>
        /// Wird aufgerufen, wenn der Zuschreibung gespeichert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnSaveAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var ascription = DataContext as Model.Ascription;
            var parent = ascription.Parent;

            IsEnabled = false;
            ButtonBar.Visibility = Visibility.Collapsed;

            ascription.Commit(true);

            IsEnabled = true;
            ButtonBar.Visibility = Visibility.Visible;

            if (!parent.Ascriptions.Contains(ascription))
            {
                parent.Ascriptions.Add(ascription);
            }

            parent.Commit(true);

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
            var Ascription = DataContext as Ascription;
            if (Ascription != null)
            {
                // Daten verwerfen
                Ascription.Rollback();
            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Zuschreibung gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnDeleteAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var ascription = DataContext as Ascription;

            if (ascription != null)
            {
                MessageDialog msg = new MessageDialog
                (
                    resourceLoader.GetString("MsgDelAscriptionAsk/Text"),
                    resourceLoader.GetString("MsgTitleDel/Text")
                );
                msg.Commands.Add(new UICommand(resourceLoader.GetString("MsgYes/Text"), c =>
                {
                    // Lösche
                    ascription.Parent.Ascriptions.Remove(ascription);
                    ascription.Parent.Commit(true);

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
            var inventory = DataContext as Ascription;
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
    }
}
