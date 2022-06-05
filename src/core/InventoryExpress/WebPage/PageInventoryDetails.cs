using InventoryExpress.Model;
using System.Collections.Generic;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("InventoryDetails")]
    [Title("inventoryexpress:inventoryexpress.details.label")]
    [SegmentGuid("InventoryID", "inventoryexpress:inventoryexpress.details.label")]
    [Path("/")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("inventorydetails")]
    public sealed class PageInventoryDetails : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Die Beschreibung der Inventargegenstandes
        /// </summary>
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

            var guid = context.Request.GetParameter("InventoryID")?.Value;
            var classes = new List<string>() { "w-100" };
            var inventory = ViewModel.GetInventory(guid);

            var mediaItems = ViewModel.GetInventoryAttachments(inventory)
                                      .Where(x => x.Name.ToLower().EndsWith(".jpg") || x.Name.ToLower().EndsWith(".png") || x.Name.ToLower().EndsWith(".svg"));

            context.Uri.Display = inventory?.Name;
            Title = inventory?.Name;
            Description.Text = inventory?.Description;

            context.VisualTree.Content.Primary.Add(new ControlPanelFlexbox(new ControlCarousel
            (
                mediaItems.Select(x => new ControlCarouselItem()
                {
                    Control = new ControlImage("id_" + x.Id) { Uri = context.Uri.Root.Append("media").Append(x.Id), Classes = classes }
                }).ToArray()
            )
            {
                Width = TypeWidth.SeventyFive
            })
            {
                Layout = TypeLayoutFlexbox.Default,
                Justify = TypeJustifiedFlexbox.Center
            });

            context.VisualTree.Content.Primary.Add(Description);
        }
    }
}
