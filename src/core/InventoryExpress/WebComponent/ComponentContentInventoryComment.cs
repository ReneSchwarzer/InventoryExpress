using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
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
    [Order(int.MaxValue)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    public sealed class ComponentContentInventoryComment : ComponentControlPanel
    {
        /// <summary>
        /// Das Kommentierungsformular
        /// </summary>
        private ControlFormularComment Form { get; } = new ControlFormularComment("form_comment");

        /// <summary>
        /// Die Liste
        /// </summary>
        private ControlList List { get; } = new ControlList()
        {
            Layout = TypeLayoutList.Flush,
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.Five, PropertySpacing.Space.None)
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContentInventoryComment()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

            Content.Add(List);
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
            Form.RedirectUri = context.Uri;
            List.Items.Clear();

            lock (ViewModel.Instance.Database)
            {
                var id = context.Request.GetParameter("InventoryID")?.Value;
                var inventory = ViewModel.Instance.Inventories.OrderByDescending(x => x.Created).Where(x => x.Guid.Equals(id)).FirstOrDefault();

                foreach (var comment in ViewModel.Instance.InventoryComments.Where(x => x.InventoryId == inventory.Id))
                {
                    List.Add(new ControlListItem(new ControlTimelineComment()
                    {
                        Post = comment.Comment,
                        Timestamp = comment.Created,
                        Likes = -1
                    }));
                }

                Form.ProcessFormular += (s, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(Form.Comment.Value))
                    {
                        lock (ViewModel.Instance.Database)
                        {
                            ViewModel.Instance.InventoryComments.Add(new InventoryComment()
                            {
                                Inventory = inventory,
                                Comment = Form.Comment.Value,
                                Guid = Guid.NewGuid().ToString()
                            });

                            ViewModel.Instance.SaveChanges();
                        }
                    }
                };
            }

            List.Add(new ControlListItem(Form));

            return base.Render(context);
        }
    }
}
