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
    /// Eine Seite zum Bearbeiten oder zur Neuanlage von Vorlagen und deren Attribute
    /// </summary>
    public sealed partial class PageTemplateItemEdit : Page
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplateItemEdit()
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
                Titel.Text = resourceLoader.GetString("PageTemplateItemAddLabel/Text");

                DataContext = new Model.Template();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Vorlage gespeichert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnSaveAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var Template = DataContext as Model.Template;
            if (Template != null)
            {
                if (string.IsNullOrWhiteSpace(Template.Name))
                {
                    MessageDialog msg = new MessageDialog
                    (
                        resourceLoader.GetString("MsgCompulsoryStatement/Text"),
                        resourceLoader.GetString("MsgTitleHint/Text")
                    );
                    await msg.ShowAsync();

                    return;
                }
                else if (!Model.ViewModel.Instance.Templates.Contains(Template) &&
                          Model.ViewModel.Instance.Templates.Find(f => f != null && !string.IsNullOrWhiteSpace(f.Name) && f.Equals(Template.Name)) != null)
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

                Template.Commit(true);

                IsEnabled = true;
                ButtonBar.Visibility = Visibility.Visible;
                ProgressRing.Visibility = Visibility.Collapsed;
                ProgressRing.IsActive = false;

                if (!ViewModel.Instance.Templates.Contains(Template))
                {
                    ViewModel.Instance.Templates.Add(Template);
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
            var Template = DataContext as Template;
            if (Template != null)
            {
                // Daten verwerfen
                Template.Rollback();
            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Vorlage gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private async void OnDeleteAndNavigateBack(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var Template = DataContext as Template;
            var exist = await ApplicationData.Current.RoamingFolder.TryGetItemAsync(Template.ID + ".Template");

            if (Template != null && exist != null)
            {
                MessageDialog msg = new MessageDialog
                (
                    resourceLoader.GetString("MsgDelAccountAsk/Text"),
                    resourceLoader.GetString("MsgTitleDel/Text")
                );
                msg.Commands.Add(new UICommand(resourceLoader.GetString("MsgYes/Text"), async c =>
                {
                    // Daten löschen
                    Model.ViewModel.Instance.Templates.Remove(Template);

                    // Datei löschen
                    var file = await ApplicationData.Current.RoamingFolder.GetFileAsync(Template.ID + ".Template");
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
            Frame.Navigate(typeof(PageTemplateItemEditHelp), DataContext);
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

            flyoutBase.ShowAt(senderElement);
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

            flyoutBase.ShowAt(senderElement);
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Attribut hinzugefügt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnAddAttribute(object sender, RoutedEventArgs e)
        {
            var attribute = Attribute.SelectedItem as Model.Attribute;

            if (attribute != null)
            {
                var provider = DataContext as Model.Template;
                if (provider != null)
                {
                    // Daten übernehmen
                    var a = new List<Model.Attribute>(provider.Attributes);
                    a.Add(attribute);
                    provider.Attributes = a;
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Attribut gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventparameter</param>
        private void OnDeleteAttribute(object sender, RoutedEventArgs e)
        {
            var senderElement = sender as FrameworkElement;
            if (senderElement != null)
            {
                // In Tag-Eigenschaft befindet sich der Name des zu löschenden Attrinutes
                var tag = senderElement.Tag.ToString();

                var provider = DataContext as Model.Template;
                if (provider != null)
                {
                    // Daten löschen
                    var a = new List<Model.Attribute>(from x in provider.Attributes
                                                      where !x.Name.Equals(tag)
                                                      select x);
                    provider.Attributes = a;
                }
            }
        }
    }
}
