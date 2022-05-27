using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("InventoryJournal")]
    [Title("inventoryexpress:inventoryexpress.inventory.journal.label")]
    [Segment("journal", "inventoryexpress:inventoryexpress.inventory.journal.display")]
    [Path("/Details")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("journal")]
    public sealed class PageInventoryJournal : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Liefert oder setzt das Inventar
        /// </summary>
        private Inventory Inventory { get; set; }

        /// <summary>
        /// Liefert oder setzt die Journaleinträge
        /// </summary>
        private List<InventoryJournal> Journals { get; } = new List<InventoryJournal>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryJournal()
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

            var guid = context.Request.GetParameter("InventoryID")?.Value;
            lock (ViewModel.Instance.Database)
            {
                Inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
                if (Inventory != null)
                {
                    Journals.AddRange(ViewModel.Instance.InventoryJournals.Where(x => x.InventoryId == Inventory.Id));
                }
            }

            var list = new ControlList() { Layout = TypeLayoutList.Flush };

            foreach (var item in Journals.OrderByDescending(x => x.Created))
            {
                var param = new List<InventoryJournalParameter>();
                lock (ViewModel.Instance.Database)
                {
                    param.AddRange(ViewModel.Instance.InventoryJournalParameters.Where(x => x.InventoryJournalId == item.Id));
                }

                list.Add(new ControlListItem
                (
                    new ControlText() { Text = this.I18N(item.Action) },
                    new ControlText()
                    {
                        Text = item.Created.ToString(Culture.DateTimeFormat.ShortDatePattern + " " + Culture.DateTimeFormat.ShortTimePattern),
                        Format = TypeFormatText.Small,
                        TextColor = new PropertyColorText(TypeColorText.Secondary),
                        Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None)
                    },
                    new ControlPanel
                    (
                        "",
                        param.Select
                        (
                            x => new ControlPanelFlexbox
                            (
                                new ControlText()
                                {
                                    Text = $"{this.I18N(x.Name)?.Trim().TrimEnd(':')}:",
                                    Format = TypeFormatText.Span,
                                    TextColor = new PropertyColorText(TypeColorText.Default),
                                    Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None)
                                },
                                new ControlText()
                                {
                                    Text = x.OldValue,
                                    Format = TypeFormatText.Code,
                                    TextColor = new PropertyColorText(TypeColorText.Default),
                                    Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None)
                                },
                                new ControlText()
                                {
                                    Text = "=>",
                                    Format = TypeFormatText.Span,
                                    TextColor = new PropertyColorText(TypeColorText.Default),
                                    Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None)
                                }, new ControlText()
                                {
                                    Text = x.NewValue,
                                    Format = TypeFormatText.Code,
                                    TextColor = new PropertyColorText(TypeColorText.Default),
                                    Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None)
                                }
                            )
                            {
                                Layout = TypeLayoutFlexbox.Default,
                                Align = TypeAlignFlexbox.Center,
                                Justify = TypeJustifiedFlexbox.Start
                            }
                         )
                    )
                    {
                    }
                )
                {

                });
            }

            visualTree.Content.Preferences.Add(list);
        }
    }
}
