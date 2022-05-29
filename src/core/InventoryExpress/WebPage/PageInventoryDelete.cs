﻿using InventoryExpress.Model;
using System.IO;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebPage
{
    [ID("InventoryDelete")]
    [Title("inventoryexpress:inventoryexpress.inventory.delete.label")]
    [Segment("del", "inventoryexpress:inventoryexpress.inventory.delete.display")]
    [Path("/Inventory/InventoryEdit")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("Inventorydelete")]
    public sealed class PageInventoryDelete : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("inventory")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryDelete()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.RedirectUri = context.ContextPath;
            Form.InitializeFormular += OnInitializeFormular;
            Form.Confirm += OnConfirmFormular;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnInitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular bestätigt urde.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);
            using var transaction = ViewModel.BeginTransaction();

            ViewModel.DeleteInventory(guid);

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    I18N(Culture, "inventoryexpress:inventoryexpress.inventory.notification.delete"),
                    new ControlLink()
                    {
                        Text = inventory.Name,
                        Uri = new UriRelative(ViewModel.GetInventoryUri(inventory.ID))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(inventory.Image),
                durability: 10000
            );

            transaction.Commit();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}