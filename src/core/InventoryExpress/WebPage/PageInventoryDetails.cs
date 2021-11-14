using InventoryExpress.Model;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("Details")]
    [Title("inventoryexpress:inventoryexpress.details.label")]
    [SegmentGuid("InventoryID", "inventoryexpress:inventoryexpress.details.label")]
    [Path("/")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("inventorydetails")]
    public sealed class PageInventoryDetails : PageWebApp
    {
        private ControlText Description { get; } = new ControlText()
        {
            Format = TypeFormatText.Paragraph,
            TextColor = new PropertyColorText(TypeColorText.Dark)
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryDetails()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var id = context.Request.GetParameter("InventoryID")?.Value;
            var classes = new List<string>() { "w-100" };

            lock (ViewModel.Instance.Database)
            {
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();
                var mediaItems = from attachment in ViewModel.Instance.InventoryAttachment
                                 join media in ViewModel.Instance.Media
                                 on attachment.MediaId equals media.Id
                                 where
                                    attachment.InventoryId == inventory.Id &&
                                    media != null &&
                                    (
                                        media.Name.ToLower().EndsWith(".jpg") ||
                                        media.Name.ToLower().EndsWith(".png") ||
                                        media.Name.ToLower().EndsWith(".svg")
                                    )
                                 select media;

                context.Uri.Display = inventory?.Name;
                Title = inventory?.Name;
                Description.Text = inventory?.Description;

                context.VisualTree.Content.Primary.Add(new ControlPanelFlexbox(new ControlCarousel
                (
                    mediaItems.Select(x => new ControlCarouselItem()
                    {
                        Control = new ControlImage("id_" + x.Id) { Uri = context.Uri.Root.Append("media").Append(x.Guid), Classes = classes }
                    }).ToArray()
                )
                {
                    Width = TypeWidth.SeventyFive
                })
                {
                    Layout = TypeLayoutFlexbox.Default,
                    Justify = TypeJustifiedFlexbox.Center
                });
            }

            context.VisualTree.Content.Primary.Add(Description);
        }
    }
}
