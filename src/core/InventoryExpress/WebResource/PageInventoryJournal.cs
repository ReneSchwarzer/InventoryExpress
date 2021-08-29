using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("InventoryJournal")]
    [Title("inventoryexpress.inventory.journal.label")]
    [Segment("journal", "inventoryexpress.inventory.journal.display")]
    [Path("/Details")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("journal")]
    public sealed class PageInventoryJournal : PageTemplateWebApp, IPageInventory
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
        public override void Initialization()
        {
            base.Initialization();

            var guid = GetParamValue("InventoryID");
            lock (ViewModel.Instance.Database)
            {
                Inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
                if (Inventory != null)
                {
                    Journals.AddRange(ViewModel.Instance.InventoryJournals.Where(x => x.InventoryId == Inventory.Id));
                }
            }
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

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
                                    Text = $"{ this.I18N(x.Name)?.Trim().TrimEnd(':')}:",
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


            Content.Preferences.Add(list);
        }
    }
}
