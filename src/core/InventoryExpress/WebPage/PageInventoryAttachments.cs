using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using System;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("InventoryAttachments")]
    [Title("inventoryexpress:inventoryexpress.inventory.attachment.label")]
    [Segment("attachments", "inventoryexpress:inventoryexpress.inventory.attachment.display")]
    [Path("/Details")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("attachment")]
    public sealed class PageInventoryAttachments : PageWebApp, IPageInventory
    {
        ///// <summary>
        ///// Liefert oder setzt das Inventar
        ///// </summary>
        //private Inventory Inventory { get; set; }

        ///// <summary>
        ///// Liefert oder setzt die Anhänge
        ///// </summary>
        //private List<InventoryAttachment> Attachments { get; } = new List<InventoryAttachment>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryAttachments()
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

            var guid = context.Request.GetParameter("InventoryID")?.Value;
            var table = new ControlTable()
            {
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three),
            };

            table.AddColumn(this.I18N("inventoryexpress.media.form.file.label"));
            table.AddColumn(this.I18N("inventoryexpress.media.size.label"));
            table.AddColumn(this.I18N("inventoryexpress.media.updatedate.label"));
            table.AddColumn(this.I18N("inventoryexpress.media.action.label"));

            lock (ViewModel.Instance.Database)
            {
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
                var attachments = ViewModel.Instance.InventoryAttachments.Where(x => x.InventoryId == inventory.Id);

                var items = from Attachment in attachments
                            join Media in ViewModel.Instance.Media
                            on Attachment.MediaId equals Media.Id
                            where Attachment.InventoryId == inventory.Id
                            select new { Attachment, Media };

                var createDelteMordalForm = new Func<string, ControlModalFormularConfirmDelete>((guid) =>
                {
                    var form = new ControlModalFormularConfirmDelete("del_" + guid)
                    {
                        Header = this.I18N("inventoryexpress.media.file.delete.label"),
                        Content = new ControlFormularItemStaticText() { Text = this.I18N("inventoryexpress.media.file.delete.description") },
                        RedirectUri = context.Uri
                    };

                    form.Confirm += (s, e) =>
                    {
                        if (inventory != null)
                        {
                            lock (ViewModel.Instance.Database)
                            {
                                var item = (from Attachment in attachments
                                            join Media in ViewModel.Instance.Media
                                            on Attachment.MediaId equals Media.Id
                                            where Attachment.InventoryId == inventory.Id &&
                                                  Media.Guid == guid
                                            select new { Attachment, Media }).FirstOrDefault();

                                var filename = item.Media?.Name;

                                ViewModel.Instance.InventoryAttachments.Remove(item.Attachment);
                                ViewModel.Instance.Media.Remove(item.Media);

                                ViewModel.Instance.SaveChanges();

                                var journal = new InventoryJournal()
                                {
                                    InventoryId = inventory.Id,
                                    Action = "inventoryexpress.journal.action.inventory.attachment.del",
                                    Created = DateTime.Now,
                                    Guid = Guid.NewGuid().ToString()
                                };

                                ViewModel.Instance.InventoryJournalParameters.Add(new InventoryJournalParameter()
                                {
                                    InventoryJournal = journal,
                                    Name = "inventoryexpress.media.form.file.label",
                                    OldValue = filename,
                                    NewValue = "🗑",
                                    Guid = Guid.NewGuid().ToString()
                                });

                                ViewModel.Instance.InventoryJournals.Add(journal);
                                ViewModel.Instance.SaveChanges();
                            }
                        }
                    };

                    return form;
                });

                foreach (var row in items)
                {
                    table.AddRow(new Control[]
                    {
                        new ControlLink() { Text = row.Media.Name, Uri = context.Uri.Root.Append("media").Append(row.Media.Guid) },
                        new ControlText() { /*Text = string.Format(new FileSizeFormatProvider() { Culture = Culture }, "{0:fs}", row.Media.Data.Length)*/ },
                        new ControlText() { Text = row.Media.Updated.ToString(Culture.DateTimeFormat.ShortDatePattern + " " + Culture.DateTimeFormat.ShortTimePattern) },
                        new ControlButtonLink()
                        {
                            Icon = new PropertyIcon(TypeIcon.TrashAlt),
                            Modal = new PropertyModal(TypeModal.Modal, createDelteMordalForm(row.Media.Guid))
                        }
                    });
                }
            }

            context.VisualTree.Content.Preferences.Add(table);
        }
    }
}
