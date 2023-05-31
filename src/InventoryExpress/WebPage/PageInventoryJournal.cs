using InventoryExpress.Model;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExTitle("inventoryexpress:inventoryexpress.inventory.journal.label")]
    [WebExSegment("journal", "inventoryexpress:inventoryexpress.inventory.journal.display")]
    [WebExContextPath("/")]
    [WebExParent(typeof(PageInventoryDetails))]
    [WebExModule<Module>]
    [WebExContext("general")]
    [WebExContext("journal")]
    public sealed class PageInventoryJournal : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageInventoryJournal()
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

            var guid = context.Request.GetParameter("InventoryId")?.Value;
            var inventory = ViewModel.GetInventory(guid);
            var journals = ViewModel.GetInventoryJournals(inventory);

            var list = new ControlList() { Layout = TypeLayoutList.Flush };

            foreach (var item in journals)
            {
                list.Add(new ControlListItem
                (
                    new ControlText() { Text = InternationalizationManager.I18N(context, item.Action) },
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
                        item.Parameters.Select
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
                            })
                        )
                   )
                );
            }

            context.VisualTree.Content.Preferences.Add(list);
        }
    }
}
