﻿using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.SidebarPrimary)]
    [WebExModule(typeof(Module))]
    [WebExContext("inventorydetails")]
    [WebExContext("attachment")]
    [WebExContext("journal")]
    [WebExContext("inventoryedit")]
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
            var Guid = context.Request.GetParameter("InventoryId")?.Value;
            var inventories = ViewModel.GetInventories(new WqlStatement()).OrderBy(x => x.Name);

            foreach (var i in inventories)
            {
                var control = new ControlTreeItemLink(GetChildren(i, context))
                {
                    Text = i?.Name,
                    Layout = TypeLayoutTreeItem.TreeView,
                    Uri = context.ContextPath.Append(i.Id),
                    Active = i.Id == Guid ? TypeActive.Active : TypeActive.None
                };

                control.Expand = control.IsAnyChildrenActive ? TypeExpandTree.Visible : TypeExpandTree.Collapse;

                Items.Add(control);
            }

            return base.Render(context);
        }

        /// <summary>
        /// Erstellt die untergeordnenten Baumknoten
        /// Arbeitet Rekursiv
        /// </summary>
        /// <param name="parent">Das übergeordnete Baumelement</param>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns></returns>
        private ControlTreeItemLink[] GetChildren(WebItemEntityInventory parent, RenderContext context)
        {
            var Guid = context.Request.GetParameter("InventoryId")?.Value;
            var children = ViewModel.GetInventoryChildren(parent).OrderBy(x => x.Name);
            var childrenContols = new List<ControlTreeItemLink>();

            foreach (var i in children)
            {
                var control = new ControlTreeItemLink(GetChildren(i, context))
                {
                    Text = i?.Name,
                    Layout = TypeLayoutTreeItem.TreeView,
                    Uri = context.ContextPath.Append(i.Id),
                    Active = i.Id == Guid ? TypeActive.Active : TypeActive.None
                };

                childrenContols.Add(control);

                control.Expand = control.IsAnyChildrenActive ? TypeExpandTree.Visible : TypeExpandTree.Collapse;
            }

            return childrenContols.ToArray();
        }
    }
}