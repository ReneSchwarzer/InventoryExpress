using InventoryExpress.Model;
using InventoryExpress.Parameter;
using System.Linq;
using WebExpress.WebApp.WebPage;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebResource;
using WebExpress.WebCore.WebScope;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.inventory.journal.label")]
    [Segment("journal", "inventoryexpress:inventoryexpress.inventory.journal.display")]
    [ContextPath("/")]
    [Parent<PageInventoryDetails>]
    [Module<Module>]
    public sealed class PageInventoryJournal : PageWebApp, IPageInventory, IScope
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

            var guid = context.Request.GetParameter<ParameterInventoryId>()?.Value;
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
