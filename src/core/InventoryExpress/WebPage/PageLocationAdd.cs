﻿using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using System.IO;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("LocationAdd")]
    [Title("inventoryexpress:inventoryexpress.location.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.location.add.label")]
    [Path("/Location")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageLocationAdd : PageWebApp, IPageLocation
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularLocation Form { get; } = new ControlFormularLocation("location")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocationAdd()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.ProcessFormular += ProcessFormular;
            Form.RedirectUri = context.Application.ContextPath.Append("locations");
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Neues Herstellerobjekt erstellen und speichern
            var location = new WebItemEntityLocation()
            {
                Name = Form.LocationName.Value,
                Description = Form.Description.Value,
                Address = Form.Address.Value,
                Zip = Form.Zip.Value,
                Place = Form.Place.Value,
                Building = Form.Building.Value,
                Room = Form.Room.Value,
                Tag = Form.Tag.Value
            };

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateLocation(location);

                transaction.Commit();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.location.notification.add"),
                    new ControlLink()
                    {
                        Text = location.Name,
                        Uri = new UriRelative(ViewModel.GetLocationUri(location.Id))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(location.Image),
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

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
