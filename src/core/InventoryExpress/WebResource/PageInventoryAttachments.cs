using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("InventoryAttachments")]
    [Title("inventoryexpress.inventory.attachment.label")]
    [Segment("attachments", "inventoryexpress.inventory.attachment.display")]
    [Path("/Details")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("attachment")]
    public sealed class PageInventoryAttachments : PageTemplateWebApp, IPageInventory
    {
        /// <summary>
        /// Liefert oder setzt das Inventar
        /// </summary>
        private Inventory Inventory { get; set; }

        /// <summary>
        /// Liefert oder setzt die Anhänge
        /// </summary>
        private List<InventoryAttachment> Attachments { get; } = new List<InventoryAttachment>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryAttachments()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            var guid = GetParamValue("InventoryID");
            Inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
            Attachments.AddRange(ViewModel.Instance.InventoryAttachment.Where(x => x.InventoryId == Inventory.Id));
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var context = new RenderContext(this);

            var items = from Attachment in Attachments
                        join Media in ViewModel.Instance.Media
                        on Attachment.MediaId equals Media.Id
                        where Attachment.InventoryId == Inventory.Id
                        select new { Attachment, Media };

            var table = new ControlTable()
            {
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three),
            };

            table.AddColumn(context.I18N("inventoryexpress.media.form.file.label"));
            table.AddColumn(context.I18N("inventoryexpress.media.size.label"));
            table.AddColumn(context.I18N("inventoryexpress.media.updatedate.label"));
            table.AddColumn(context.I18N("inventoryexpress.media.action.label"));

            var CreateDelteMordalForm = new Func<string, ControlModalFormConfirmDelete>((guid) =>
            {
                var form = new ControlModalFormConfirmDelete("del_" + guid)
                {
                    Header = context.Page.I18N("inventoryexpress.media.file.delete.label"),
                    Content = new ControlFormularItemStaticText() { Text = context.Page.I18N("inventoryexpress.media.file.delete.description") },
                    RedirectUri = Uri
                };

                form.Confirm += (s, e) =>
                {
                    if (Inventory != null)
                    {
                        var item = (from Attachment in Attachments
                                    join Media in ViewModel.Instance.Media
                                    on Attachment.MediaId equals Media.Id
                                    where Attachment.InventoryId == Inventory.Id &&
                                          Media.Guid == guid
                                    select new { Attachment, Media }).FirstOrDefault();

                        var filename = item.Media?.Name;

                        ViewModel.Instance.InventoryAttachment.Remove(item.Attachment);
                        ViewModel.Instance.Media.Remove(item.Media);

                        ViewModel.Instance.SaveChanges();

                        var journal = new InventoryJournal()
                        {
                            InventoryId = Inventory.Id,
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
                };

                return form;
            });

            foreach (var row in items)
            {
                table.AddRow(new Control[]
                {
                    new ControlLink() { Text = row.Media.Name, Uri = Uri.Root.Append("media").Append(row.Media.Guid) },
                    new ControlText() { Text = string.Format(new FileSizeFormatProvider() { Culture = context.Culture }, "{0:fs}", row.Media.Data.Length) },
                    new ControlText() { Text = row.Media.Updated.ToString(context.Culture.DateTimeFormat.ShortDatePattern + " " + context.Culture.DateTimeFormat.ShortTimePattern) },
                    new ControlButtonLink()
                    {
                        Icon = new PropertyIcon(TypeIcon.TrashAlt),
                        Modal = CreateDelteMordalForm(row.Media.Guid)
                    }
                });
            }

            Content.Preferences.Add(table);
        }
    }
}
