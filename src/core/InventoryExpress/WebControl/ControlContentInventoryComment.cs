using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.ContentSecondary)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlContentInventoryComment : ControlPanel, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlContentInventoryComment()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Info);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            lock (ViewModel.Instance.Database)
            {
                var id = context.Page.GetParamValue("InventoryID");
                var inventory = ViewModel.Instance.Inventories.OrderByDescending(x => x.Created).Where(x => x.Guid.Equals(id)).FirstOrDefault();

                var list = new ControlList()
                {
                    Layout = TypeLayoutList.Flush,
                    Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.Five, PropertySpacing.Space.None)
                };

                foreach (var comment in ViewModel.Instance.InventoryComments.Where(x => x.InventoryId == inventory.Id))
                {
                    list.Add(new ControlListItem(new ControlTimelineComment()
                    {
                        Post = comment.Comment,
                        Timestamp = comment.Created,
                        Likes = -1
                    }));
                }

                var form = new ControlFormularComment("form_comment")
                {
                    RedirectUri = context.Uri
                };

                form.ProcessFormular += (s, e) =>
                        {
                            if (!string.IsNullOrWhiteSpace(form.Comment.Value))
                            {
                                ViewModel.Instance.InventoryComments.Add(new InventoryComment()
                                {
                                    Inventory = inventory,
                                    Comment = form.Comment.Value,
                                    Guid = Guid.NewGuid().ToString()
                                });

                                ViewModel.Instance.SaveChanges();
                            }
                        };

                list.Add(new ControlListItem(form));
                Content.Add(list);

                return base.Render(context);
            }
        }
    }
}
