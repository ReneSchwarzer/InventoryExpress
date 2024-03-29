﻿using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using WebExpress.Internationalization;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using WebExpress.WebScope;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.inventory.attachment.label")]
    [Segment("attachments", "inventoryexpress:inventoryexpress.inventory.attachment.display")]
    [ContextPath("/")]
    [Parent<PageInventoryDetails>]
    [Module<Module>]
    public sealed class PageInventoryAttachments : PageWebApp, IPageInventory, IScope
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageInventoryAttachments()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter<ParameterInventoryId>()?.Value;
            var inventory = ViewModel.GetInventory(guid);
            var attachments = ViewModel.GetInventoryAttachments(inventory);

            var table = new ControlTable()
            {
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three),
            };

            table.AddColumn("inventoryexpress:inventoryexpress.media.form.file.label");
            table.AddColumn("inventoryexpress:inventoryexpress.media.size.label");
            table.AddColumn("inventoryexpress:inventoryexpress.media.updatedate.label");
            table.AddColumn("inventoryexpress:inventoryexpress.media.action.label");

            foreach (var item in attachments)
            {
                table.AddRow(new Control[]
                {
                    new ControlLink() { Text = item.Name, Uri = context.Uri.ModuleRoot.Append("media").Append(item.Guid) },
                    new ControlText() { Text = string.Format(new FileSizeFormatProvider() { Culture = Culture }, "{0:fs}", item.Size) },
                    new ControlText() { Text = item.Updated.ToString(Culture.DateTimeFormat.ShortDatePattern + " " + Culture.DateTimeFormat.ShortTimePattern) },
                    new ControlButtonLink()
                    {
                        Icon = new PropertyIcon(TypeIcon.TrashAlt),
                        Modal = new PropertyModal(TypeModal.Modal, CreateDelteMordalForm(inventory, item, context))
                    }
                });
            }

            context.VisualTree.Content.Preferences.Add(table);
        }

        /// <summary>
        /// Erstellt einen modalen Dialog zum Löschen einer Anlage
        /// </summary>
        /// <param name="item">The attachment.</param>
        /// <param name="context">Der Renderkontext</param>
        /// <returns></returns>
        private ControlModalFormularConfirmDelete CreateDelteMordalForm(WebItemEntityInventory inventory, WebItemEntityMedia item, RenderContextWebApp context)
        {
            var form = new ControlModalFormularConfirmDelete("del_" + item.Id)
            {
                Header = "inventoryexpress:inventoryexpress.media.file.delete.label",
                Content = new ControlFormItemStaticText() { Text = "inventoryexpress:inventoryexpress.media.file.delete.description" },
                RedirectUri = context.Uri
            };

            form.Confirm += (s, e) =>
            {
                using var transaction = ViewModel.BeginTransaction();

                ViewModel.DeleteInventoryAttachments(item);
                ViewModel.AddInventoryJournal(inventory, new WebItemEntityJournal(new WebItemEntityJournalParameter()
                {
                    Name = "inventoryexpress.media.form.file.label",
                    OldValue = item.Name,
                    NewValue = "🗑"
                })
                {
                    Action = "inventoryexpress.journal.action.inventory.attachment.del"
                });

                transaction.Commit();
            };

            return form;
        }
    }
}
