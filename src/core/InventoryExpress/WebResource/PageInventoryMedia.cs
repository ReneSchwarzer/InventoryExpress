using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("InventoryMedia")]
    [Title("inventoryexpress.inventory.media.label")]
    [Segment("media", "inventoryexpress.inventory.media.display")]
    [Path("/Details")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("media")]
    [Context("mediaedit")]
    [Context("inventoryedit")]
    public sealed class PageInventoryMedia : PageTemplateWebApp, IPageInventory
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
        public override void Initialization()
        {
            base.Initialization();

            Form = new ControlFormularMedia("media")
            {
                RedirectUri = Uri,
                BackUri = Uri.Take(-1)
            };

            var guid = GetParamValue("InventoryID");
            Inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
            Media = ViewModel.Instance.Media.Where(x => x.Id == Inventory.MediaId).FirstOrDefault();

            AddParam("MediaID", Media?.Guid, ParameterScope.Local);

            Form.InitializeFormular += (s, e) =>
            {
                
            };

            Form.Image.Validation += (s, e) =>
            {
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Content.Preferences.Add(new ControlImage()
            {
                Uri = Media != null ? Uri.Root.Append($"media/{Media.Guid}") : Uri.Root.Append("/assets/img/inventoryexpress.svg"),
                Width = 400,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
            });

            Content.Primary.Add(Form);

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

                if (GetParam(Form.Image.Name) is ParameterFile file)
                {
                    // Image speichern
                    if (Media == null)
                    {
                        Inventory.Media = new Media()
                        {
                            Name = file.Value,
                            Data = file.Data,
                            Tag = Form.Tag.Value,
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };

                        ViewModel.Instance.SaveChanges();

                        journal.Action = "inventoryexpress.journal.action.inventory.media.add";
                    }
                    else
                    {
                        // Image ändern
                        Media.Name = file.Value;
                        Media.Data = file.Data;
                        Media.Tag = Form.Tag.Value;
                        Media.Updated = DateTime.Now;

                        ViewModel.Instance.SaveChanges();

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
            };
        }
    }
}
