using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;
using WebExpress.WebApp.WebControl;

namespace InventoryExpress.WebControl
{
    /// <summary>
    /// Modal zum Löschen eines Dokumentes. Wird von der Komponetne ControlMoreMediaDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("media")]
    public sealed class ControlContentMediaModalDelete : ControlModalFormConfirmDelete, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentMediaModalDelete()
           : base("del_media")
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Confirm += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    var guid = context.Page.GetParamValue("MediaID");
                    var inventoryGuid = context.Page.GetParamValue("InventoryID");
                    var media = ViewModel.Instance.Media.Where(x => x.Guid == guid).FirstOrDefault();
                    var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == inventoryGuid).FirstOrDefault();

                    if (media != null)
                    {
                        // Aus DB löschen
                        ViewModel.Instance.Media.Remove(media);
                        ViewModel.Instance.SaveChanges();

                        if (inventory != null)
                        {
                            var journal = new InventoryJournal()
                            {
                                InventoryId = inventory.Id,
                                Action = "inventoryexpress.journal.action.inventory.media.del",
                                Created = DateTime.Now,
                                Guid = Guid.NewGuid().ToString()
                            };

                            ViewModel.Instance.InventoryJournals.Add(journal);
                            ViewModel.Instance.SaveChanges();
                        }
                    }

                    ViewModel.Instance.SaveChanges();
                }
            };

            Header = context.Page.I18N("inventoryexpress.media.delete.label");
            Content = new ControlFormularItemStaticText() { Text = context.I18N("inventoryexpress.media.delete.description") };

            return base.Render(context);
        }
    }
}
