﻿using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.IO;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
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
            Edit = true
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
            Form.LocationName.Validation += LocationNameValidation;
            Form.Zip.Validation += ZipValidation;
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Neues Standortobjekt erstellen und speichern
            var location = new Location()
            {
                Name = Form.LocationName.Value,
                Description = Form.Description.Value,
                Address = Form.Address.Value,
                Zip = Form.Zip.Value,
                Place = Form.Place.Value,
                Building = Form.Building.Value,
                Room = Form.Room.Value,
                Tag = Form.Tag.Value,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Guid = Guid.NewGuid().ToString()
            };

            if (e.Context.Request.GetParameter(Form.Image.Name) is ParameterFile file)
            {
                if (location.Media == null)
                {
                    location.Media = new Media()
                    {
                        Name = file.Value,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Guid = Guid.NewGuid().ToString()
                    };
                }
                else
                {
                    location.Media.Name = file.Value;
                    location.Media.Updated = DateTime.Now;
                }

                File.WriteAllBytes(Path.Combine(e.Context.Application.AssetPath, "media", location.Media.Guid), file.Data);
            }

            lock (ViewModel.Instance.Database)
            {
                ViewModel.Instance.Locations.Add(location);
                ViewModel.Instance.SaveChanges();
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld Zip validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ZipValidation(object sender, ValidationEventArgs e)
        {
            if (e.Value != null && e.Value.Count() >= 10)
            {
                e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.location.validation.zip.tolong", Type = TypesInputValidity.Error });
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Feld LocationName validiert werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void LocationNameValidation(object sender, ValidationEventArgs e)
        {
            lock (ViewModel.Instance.Database)
            {
                if (e.Value.Length < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.location.validation.name.invalid", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.Locations.Where(x => x.Name.Equals(e.Value)).Any())
                {
                    e.Results.Add(new ValidationResult() { Text = "inventoryexpress:inventoryexpress.location.validation.name.used", Type = TypesInputValidity.Error });
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
            Form.BackUri = e.Context.Uri.Take(-1);
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
