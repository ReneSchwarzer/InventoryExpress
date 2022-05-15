﻿using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.IO;
using System.Linq;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("SupplierMedia")]
    [Title("inventoryexpress:inventoryexpress.supplier.media.label")]
    [Segment("media", "inventoryexpress:inventoryexpress.supplier.media.display")]
    [Path("/Supplier/SupplierEdit")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("media")]
    [Context("mediaedit")]
    [Context("supplieredit")]
    public sealed class PageSupplierMedia : PageWebApp, IPageSupplier
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularMedia Form { get; set; }

        /// <summary>
        /// Liefert oder setzt den Lieferanten
        /// </summary>
        private Supplier Supplier { get; set; }

        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        private Media Media { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierMedia()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form = new ControlFormularMedia("media");
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            var guid = context.Request.GetParameter("SupplierID")?.Value;
            lock (ViewModel.Instance.Database)
            {
                Supplier = ViewModel.Instance.Suppliers.Where(x => x.Guid == guid).FirstOrDefault();
                Media = ViewModel.Instance.Media.Where(x => x.Id == Supplier.MediaId).FirstOrDefault();
            }

            //AddParam("MediaID", Media?.Guid, ParameterScope.Local);

            visualTree.Content.Preferences.Add(new ControlImage()
            {
                Uri = Media != null ? context.Uri.Root.Append($"media/{Media.Guid}") : context.Uri.Root.Append("/assets/img/inventoryexpress.svg"),
                Width = 400,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
            });

            visualTree.Content.Primary.Add(Form);

            Form.RedirectUri = context.Uri;
            Form.BackUri = context.Uri.Take(-1);

            Form.Tag.Value = Media?.Tag;

            Form.Image.Validation += (s, e) =>
            {
                //if (e.Value.Count() < 1)
                //{
                //    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                //}
                //else if (!manufactur.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase) && ViewModel.Instance.Suppliers.Where(x => x.Name.Equals(e.Value)).Count() > 0)
                //{
                //    e.Results.Add(new ValidationResult() { Text = "Der Hersteller wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                //}
            };

            Form.ProcessFormular += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    if (context.Request.GetParameter(Form.Image.Name) is ParameterFile file)
                    {
                        // Image speichern
                        if (Media == null)
                        {
                            Supplier.Media = new Media()
                            {
                                Name = file.Value,
                                Tag = Form.Tag.Value,
                                Created = DateTime.Now,
                                Updated = DateTime.Now,
                                Guid = Guid.NewGuid().ToString()
                            };

                            File.WriteAllBytes(Path.Combine(context.Application.AssetPath, Supplier.Media.Guid), file.Data);
                        }
                        else
                        {
                            // Image ändern
                            Media.Name = file.Value;
                            Media.Tag = Form.Tag.Value;
                            Media.Updated = DateTime.Now;

                            File.WriteAllBytes(Path.Combine(context.Application.AssetPath, Media.Guid), file.Data);
                        }
                    }

                    if (Form.Tag.Value != Media?.Tag)
                    {
                        Supplier.Media.Tag = Form.Tag.Value;
                    }

                    ViewModel.Instance.SaveChanges();
                }
            };
        }
    }
}