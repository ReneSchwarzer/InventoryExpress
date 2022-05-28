using InventoryExpress.Model;
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
    [ID("InventoryMedia")]
    [Title("inventoryexpress:inventoryexpress.inventory.media.label")]
    [Segment("media", "inventoryexpress:inventoryexpress.inventory.media.display")]
    [Path("/Details")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("media")]
    [Context("mediaedit")]
    [Context("inventoryedit")]
    public sealed class PageInventoryMedia : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularMedia Form { get; set; }

        /// <summary>
        /// Liefert oder setzt das Inventar
        /// </summary>
        private Inventory Inventory { get; set; }

        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        private Media Media { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryMedia()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            Form = new ControlFormularMedia("media")
            {
                RedirectUri = context.Uri
            };

            var guid = context.Request.GetParameter("InventoryID")?.Value;
            lock (ViewModel.Instance.Database)
            {
                Inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
                Media = ViewModel.Instance.Media.Where(x => x.Id == Inventory.MediaId).FirstOrDefault();
            }

            //AddParam("MediaID", Media?.Guid, ParameterScope.Local);

            Form.InitializeFormular += (s, e) =>
            {

            };

            Form.Image.Validation += (s, e) =>
            {
            };

            visualTree.Content.Preferences.Add(new ControlImage()
            {
                Uri = Media != null ? context.Uri.Root.Append($"media/{Media.Guid}") : context.Uri.Root.Append("/assets/img/inventoryexpress.svg"),
                Width = 400,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
            });

            visualTree.Content.Primary.Add(Form);

            Form.Tag.Value = Media?.Tag;

            var oldTag = Media?.Tag;

            Form.ProcessFormular += (s, e) =>
            {
                var journal = new InventoryJournal()
                {
                    InventoryId = Inventory.Id,
                    Action = "inventoryexpress.journal.action.inventory.media.edit",
                    Created = DateTime.Now,
                    Guid = Guid.NewGuid().ToString()
                };

                lock (ViewModel.Instance.Database)
                {
                    if (context.Request.GetParameter(Form.Image.Name) is ParameterFile file)
                    {
                        // Image speichern
                        if (Media == null)
                        {
                            Inventory.Media = new Media()
                            {
                                Name = file.Value,
                                Tag = Form.Tag.Value,
                                Created = DateTime.Now,
                                Updated = DateTime.Now,
                                Guid = Guid.NewGuid().ToString()
                            };

                            ViewModel.Instance.SaveChanges();

                            File.WriteAllBytes(Path.Combine(context.Application.AssetPath, Inventory.Media.Guid), file.Data);

                            journal.Action = "inventoryexpress.journal.action.inventory.media.add";
                        }
                        else
                        {
                            // Image ändern
                            Media.Name = file.Value;
                            Media.Tag = Form.Tag.Value;
                            Media.Updated = DateTime.Now;

                            ViewModel.Instance.SaveChanges();

                            File.WriteAllBytes(Path.Combine(context.Application.AssetPath, Media.Guid), file.Data);

                            ViewModel.Instance.InventoryJournalParameters.Add(new InventoryJournalParameter()
                            {
                                InventoryJournal = journal,
                                Name = "inventoryexpress.media.form.image.label",
                                OldValue = "...",
                                NewValue = "...",
                                Guid = Guid.NewGuid().ToString()
                            });
                        }
                    }

                    ViewModel.Instance.InventoryJournals.Add(journal);
                    ViewModel.Instance.SaveChanges();

                    if (Form.Tag.Value != Media?.Tag)
                    {
                        Inventory.Media.Tag = Form.Tag.Value;
                    }

                    if (!string.IsNullOrWhiteSpace(oldTag) && oldTag != Media?.Tag)
                    {
                        ViewModel.Instance.InventoryJournalParameters.Add(new InventoryJournalParameter()
                        {
                            InventoryJournal = journal,
                            Name = "inventoryexpress.inventory.tags.label",
                            OldValue = oldTag,
                            NewValue = Media?.Tag,
                            Guid = Guid.NewGuid().ToString()
                        });
                    }

                    ViewModel.Instance.SaveChanges();
                }
            };
        }
    }
}
