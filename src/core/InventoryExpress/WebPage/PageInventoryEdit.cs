using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("InventoryEdit")]
    [Title("inventoryexpress:inventoryexpress.inventory.edit.label")]
    [Segment("edit", "inventoryexpress:inventoryexpress.inventory.edit.display")]
    [Path("/InventoryDetails")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("inventoryedit")]
    //[Cache]
    public sealed class PageInventoryEdit : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Formular
        /// </summary>
        private readonly ControlFormularInventory Form = new("inventory")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryEdit()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.RedirectUri = Uri.Take(-1);
            Form.InitializeFormular += OnInitializeFormular;
            Form.FillFormular += OnFillFormular;
            Form.ProcessFormular += OnProcessFormular;
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnInitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initial befüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnFillFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);

            Form.Fill(inventory, e.Context.Culture);
        }

        /// <summary>
        /// Wird ausgelöst, wenn die Formulareingaben verarbeitet werden sollen
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnProcessFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);
            Form.Apply(inventory, e.Context.Culture);

            using var transaction = ViewModel.BeginTransaction();

            ViewModel.AddOrUpdateInventory(inventory);

            transaction.Commit();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.inventory.notification.edit"),
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
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            // Attribute im Formular erstellen
            context.VisualTree.Content.Primary.Add(Form);
            context.Uri.Display = context.Request.GetParameter("InventoryID")?.Value.Split('-').LastOrDefault();
        }
    }
}
