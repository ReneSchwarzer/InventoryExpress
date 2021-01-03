using InventoryExpress.Model;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.PropertyPrimary)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlPropertyInventoryPartOf : ControlList, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertyInventoryPartOf()
        {
            Layout = TypeLayoutList.Flush;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var id = context.Page.GetParamValue("InventoryID");
            var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();
            var parent = ViewModel.Instance.Inventories.Where(x => x.Id.Equals(inventory.ParentId)).FirstOrDefault();
            var children = ViewModel.Instance.Inventories.Where(x => x.ParentId.Equals(inventory.Id));

            if (parent != null)
            {
                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.I18N("inventoryexpress.inventory.parent.label"),
                    Icon = new PropertyIcon(TypeIcon.Link),
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                },
                new ControlLink()
                {
                    Text = parent?.Name,
                    Uri = context.Uri.Root.Append(parent.Guid),
                }
                ));
            }

            if (children.Count() > 0)
            {
                var list = new List<Control>();
                list.Add(new ControlAttribute()
                {
                    Name = context.I18N("inventoryexpress.inventory.child.label"),
                    Icon = new PropertyIcon(TypeIcon.Link),
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                });

                foreach (var child in children)
                {
                    list.Add(new ControlLink()
                    {
                        Text = child?.Name,
                        Uri = context.Uri.Root.Append(child?.Guid)
                    });
                }

                Add(new ControlListItem(list));
            }

            return base.Render(context);
        }
    }
}
