using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebHtml;
using WebExpress.WebPage;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    [Section(Section.SidebarPrimary)]
    [Module<Module>]
    [Scope<PageInventoryDetails>]
    [Scope<PageInventoryAttachments>]
    [Scope<PageInventoryJournal>]
    [Scope<PageInventoryEdit>]
    public sealed class FragmentSidebarInventoryTree : FragmentControlTree
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentSidebarInventoryTree()
        {
            Layout = TypeLayoutTree.TreeView;
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
            var guid = context.Request.GetParameter<ParameterInventoryId>()?.Value;
            var inventories = ViewModel.GetInventories().OrderBy(x => x.Name);

            foreach (var i in inventories)
            {
                var control = new ControlTreeItemLink(GetChildren(i, context))
                {
                    Text = i?.Name,
                    Layout = TypeLayoutTreeItem.TreeView,
                    Uri = context.ContextPath.Append(i.Guid),
                    Active = i.Guid == guid ? TypeActive.Active : TypeActive.None
                };

                control.Expand = control.IsAnyChildrenActive ? TypeExpandTree.Visible : TypeExpandTree.Collapse;

                Items.Add(control);
            }

            return base.Render(context);
        }

        /// <summary>
        /// Creates the child tree nodes.
        /// Works recursively.
        /// </summary>
        /// <param name="parent">The parent tree element.</param>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The child tree nodes.</returns>
        private ControlTreeItemLink[] GetChildren(WebItemEntityInventory parent, RenderContext context)
        {
            var guid = context.Request.GetParameter<ParameterInventoryId>()?.Value;
            var children = ViewModel.GetInventoryChildren(parent).OrderBy(x => x.Name);
            var childrenContols = new List<ControlTreeItemLink>();

            foreach (var i in children)
            {
                var control = new ControlTreeItemLink(GetChildren(i, context))
                {
                    Text = i?.Name,
                    Layout = TypeLayoutTreeItem.TreeView,
                    Uri = context.ContextPath.Append(i.Guid),
                    Active = i.Guid == guid ? TypeActive.Active : TypeActive.None
                };

                childrenContols.Add(control);

                control.Expand = control.IsAnyChildrenActive ? TypeExpandTree.Visible : TypeExpandTree.Collapse;
            }

            return childrenContols.ToArray();
        }
    }
}
