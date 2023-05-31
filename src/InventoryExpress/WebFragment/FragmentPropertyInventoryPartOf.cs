using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.PropertyPrimary)]
    [WebExModule<Module>]
    [WebExContext("inventorydetails")]
    public sealed class FragmentPropertyInventoryPartOf : FragmentControlList
    {
        /// <summary>
        /// Das Erstellungsdatum
        /// </summary>
        private ControlAttribute ParentAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Link),
            Name = "inventoryexpress:inventoryexpress.inventory.parent.label"
        };

        /// <summary>
        /// Das Datum der letzten Änderung
        /// </summary>
        private ControlAttribute ChildAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Link),
            Name = "inventoryexpress:inventoryexpress.inventory.child.label"
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentPropertyInventoryPartOf()
        {
            Layout = TypeLayoutList.Flush;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var id = context.Request.GetParameter("InventoryId")?.Value;
            Items.Clear();

            //lock (ViewModel.Instance.Database)
            //{
            //    var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();
            //    var parent = ViewModel.Instance.Inventories.Where(x => x.Id.Equals(inventory.ParentId)).FirstOrDefault();
            //    var children = ViewModel.Instance.Inventories.Where(x => x.ParentId.Equals(inventory.Id));

            //    if (parent != null)
            //    {
            //        Add(new ControlListItem(ParentAttribute, new ControlLink()
            //        {
            //            Text = parent?.Name,
            //            Uri = context.Uri.Root.Append(parent.Guid),
            //        }));
            //    }

            //    if (children.Any())
            //    {
            //        var list = new List<Control> { ChildAttribute };

            //        foreach (var child in children)
            //        {
            //            list.Add(new ControlLink()
            //            {
            //                Text = child?.Name,
            //                Uri = context.Uri.Root.Append(child?.Guid)
            //            });
            //        }

            //        Add(new ControlListItem(list));
            //    }
            //}

            return base.Render(context);
        }
    }
}
