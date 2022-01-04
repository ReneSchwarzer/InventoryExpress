using InventoryExpress.Model;
using System.Collections.Generic;
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
    [Section(Section.PropertyPrimary)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    public sealed class ComponentPropertyInventoryPartOf : ComponentControlList
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
        /// Konstruktor
        /// </summary>
        public ComponentPropertyInventoryPartOf()
        {
            Layout = TypeLayoutList.Flush;
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
            var id = context.Request.GetParameter("InventoryID")?.Value;
            Items.Clear();

            lock (ViewModel.Instance.Database)
            {
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();
                var parent = ViewModel.Instance.Inventories.Where(x => x.Id.Equals(inventory.ParentId)).FirstOrDefault();
                var children = ViewModel.Instance.Inventories.Where(x => x.ParentId.Equals(inventory.Id));

                if (parent != null)
                {
                    Add(new ControlListItem(ParentAttribute, new ControlLink()
                    {
                        Text = parent?.Name,
                        Uri = context.Uri.Root.Append(parent.Guid),
                    }));
                }

                if (children.Any())
                {
                    var list = new List<Control> { ChildAttribute };

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
            }

            return base.Render(context);
        }
    }
}
