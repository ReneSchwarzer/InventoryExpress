using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.ContentSecondary)]
    [WebExOrder(int.MaxValue)]
    [WebExModule<Module>]
    [WebExContext("inventorydetails")]
    public sealed class FragmentContentInventoryComment : FragmentControlPanel
    {
        /// <summary>
        /// Das Kommentierungsformular
        /// </summary>
        private ControlFormularComment Form { get; } = new ControlFormularComment("B4F92D96-89C6-46DD-910E-1D5BF08EEF42");

        /// <summary>
        /// Die Liste
        /// </summary>
        private ControlList List { get; } = new ControlList()
        {
            Layout = TypeLayoutList.Flush,
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.Five, PropertySpacing.Space.None)
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentContentInventoryComment()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

            Content.Add(List);
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
            Form.RedirectUri = context.Uri;
            List.Items.Clear();

            var guid = context.Request.GetParameter("InventoryId")?.Value;
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
