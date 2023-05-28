﻿using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExTitle("inventoryexpress:inventoryexpress.manufacturer.edit.label")]
    [WebExSegmentGuid("ManufacturerId", "inventoryexpress:inventoryexpress.manufacturer.edit.display", WebExSegmentGuidAttribute.Format.Simple)]
    [WebExContextPath("/")]
    [WebExParent(typeof(PageManufacturers))]
    [WebExModule(typeof(Module))]
    [WebExContext("general")]
    [WebExContext("manufactureredit")]
    public sealed class PageManufacturerEdit : PageWebApp, IPageManufacturer
    {
        /// <summary>
        /// Returns the form
        /// </summary>
        private ControlFormularManufacturer Form { get; } = new ControlFormularManufacturer("manufacturer")
        {
        };

        /// <summary>
        /// Liefert oder setzt den Hersteller
        /// </summary>
        private WebItemEntityManufacturer Manufacturer { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PageManufacturerEdit()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Invoked when the form is about to be populated.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            Form.ManufacturerName.Value = Manufacturer?.Name;
            Form.Description.Value = Manufacturer?.Description;
            Form.Address.Value = Manufacturer?.Address;
            Form.Zip.Value = Manufacturer?.Zip;
            Form.Place.Value = Manufacturer?.Place;
            Form.Tag.Value = Manufacturer?.Tag;
        }

        /// <summary>
        /// Fired when the form is about to be processed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Herstellerobjekt ändern und speichern
            Manufacturer.Name = Form.ManufacturerName.Value;
            Manufacturer.Description = Form.Description.Value;
            Manufacturer.Address = Form.Address.Value;
            Manufacturer.Zip = Form.Zip.Value;
            Manufacturer.Place = Form.Place.Value;
            Manufacturer.Tag = Form.Tag.Value;
            Manufacturer.Updated = DateTime.Now;

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateManufacturer(Manufacturer);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.manufacturer.notification.edit"),
                    new ControlLink()
                    {
                        Text = Manufacturer.Name,
                        Uri = ViewModel.GetManufacturerUri(Manufacturer.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: Manufacturer.Image,
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

            var Guid = context.Request.GetParameter("ManufacturerId")?.Value;
            Manufacturer = ViewModel.GetManufacturer(Guid);

            Uri.Display = Manufacturer.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}