using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.SidebarPrimary)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    [Context("attachment")]
    [Context("journal")]
    [Context("inventoryedit")]
    public sealed class ComponentSidebarInventoryTree : ComponentControlTree
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentSidebarInventoryTree()
        {
            Layout = TypeLayoutTree.TreeView;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            //lock (ViewModel.Instance.Database)
            //{
            //    var guid = context.Request.GetParameter("InventoryID")?.Value;
            //    var inventories = ViewModel.Instance.Inventories.Where(x => x.ParentId == null).OrderBy(x => x.Name);

            //    Items.Clear();

            //    foreach (var i in inventories)
            //    {
            //        var control = new ControlTreeItemLink(GetChildren(i, context))
            //        {
            //            Text = i?.Name,
            //            Layout = TypeLayoutTreeItem.TreeView,
            //            Uri = context.Uri.Root.Append(i.Guid),
            //            Active = i.Guid == guid ? TypeActive.Active : TypeActive.None
            //        };

            //        control.Expand = control.IsAnyChildrenActive ? TypeExpandTree.Visible : TypeExpandTree.Collapse;

            //        Items.Add(control);
            //    }
            //}

            return base.Render(context);
        }

        /// <summary>
        /// Erstellt die untergeordnenten Baumknoten
        /// Arbeitet Rekursiv
        /// </summary>
        /// <param name="parent">Das übergeordnete Baumelement</param>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns></returns>
        private ControlTreeItemLink[] GetChildren(Inventory parent, RenderContext context)
        {
            var guid = context.Request.GetParameter("InventoryID")?.Value;
            //var children = ViewModel.GetInventories(new WqlStatement()).Where(x => x.ParentId == parent.Id).OrderBy(x => x.Name);
            var childrenContols = new List<ControlTreeItemLink>();

            //foreach (var i in children)
            //{
            //    var control = new ControlTreeItemLink(GetChildren(i, context))
            //    {
            //        Text = i?.Name,
            //        Layout = TypeLayoutTreeItem.TreeView,
            //        Uri = context.Uri.Root.Append(i.Guid),
            //        Active = i.Guid == guid ? TypeActive.Active : TypeActive.None
            //    };

            //    childrenContols.Add(control);

            //    control.Expand = control.IsAnyChildrenActive ? TypeExpandTree.Visible : TypeExpandTree.Collapse;
            //}

            return childrenContols.ToArray();
        }
    }
}
