using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
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

            var guid = context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);

            foreach (var comment in ViewModel.GetInventoryComments(inventory))
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
                    using var transaction = ViewModel.BeginTransaction();

                    ViewModel.AddInventoryComment(inventory, new WebItemEntityComment()
                    {
                        Comment = Form.Comment.Value
                    });

                    transaction.Commit();                   
                }
            };

            List.Add(new ControlListItem(Form));

            return base.Render(context);
        }
    }
}
