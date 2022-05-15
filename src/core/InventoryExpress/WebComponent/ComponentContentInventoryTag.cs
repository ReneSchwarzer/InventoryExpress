using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.ContentSecondary)]
    [Order(int.MinValue)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    public sealed class ComponentContentInventoryTag : ComponentControlPanelFlexbox
    {
        /// <summary>
        /// Die Liste mit den Tag-Links
        /// </summary>
        private ControlLinkList TagList { get; } = new ControlLinkList()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Tag)
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContentInventoryTag()
        {
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Null, PropertySpacing.Space.Five, PropertySpacing.Space.Two, PropertySpacing.Space.Two);
            Layout = TypeLayoutFlexbox.Default;
            Justify = TypeJustifiedFlexbox.End;
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);

            Content.Add(TagList);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            TagList.Links.Clear();

            lock (ViewModel.Instance.Database)
            {
                var id = context.Request.GetParameter("InventoryID")?.Value;
                var inventoryTags = from i in ViewModel.Instance.Inventories
                                    join it in ViewModel.Instance.InventoryTags on i.Id equals it.InventoryId
                                    join t in ViewModel.Instance.Tags on it.TagId equals t.Id
                                    where i.Guid.Equals(id)
                                    select new ControlLink(string.Empty) { Text = t.Label.ToLower() };

                TagList.Links.AddRange(inventoryTags);
            }

            return base.Render(context);
        }
    }
}
