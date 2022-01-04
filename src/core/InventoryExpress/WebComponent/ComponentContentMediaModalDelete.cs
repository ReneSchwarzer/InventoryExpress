using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using System;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebApp.WebControl;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    /// <summary>
    /// Modal zum Löschen eines Dokumentes. Wird von der Komponetne ControlMoreMediaDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Module("inventoryexpress")]
    [Context("media")]
    public sealed class ComponentContentMediaModalDelete : ComponentControlModalFormConfirmDelete
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContentMediaModalDelete()
           : base("del_media")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);

            Header = "inventoryexpress:inventoryexpress.media.delete.label";
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
                    var guid = context.Request.GetParameter("MediaID")?.Value;
                    var inventoryGuid = context.Request.GetParameter("InventoryID")?.Value;
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


            Content = new ControlFormularItemStaticText() { Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.media.delete.description") };

            return base.Render(context);
        }
    }
}
