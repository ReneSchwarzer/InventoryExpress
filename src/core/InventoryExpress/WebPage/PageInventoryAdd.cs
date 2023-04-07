﻿using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebUri;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [Id("InventoryAdd")]
    [Title("inventoryexpress:inventoryexpress.inventory.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.inventory.add.label")]
    [ContextPath("/")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageInventoryAdd : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularInventory Form { get; } = new ControlFormularInventory("inventory")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryAdd()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.ProcessFormular += ProcessFormular;
            Form.RedirectUri = ContextPath;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Neues Inventarobjekt erstellen und speichern
            var inventory = new WebItemEntityInventory();
            Form.Apply(inventory, e.Context.Culture);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateInventory(inventory);

                transaction.Commit();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.inventory.notification.add"),
                    new ControlLink()
                    {
                        Text = inventory.Name,
                        Uri = new UriRelative(ViewModel.GetInventoryUri(inventory.Id))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(inventory.Image),
                durability: 10000
            );
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
