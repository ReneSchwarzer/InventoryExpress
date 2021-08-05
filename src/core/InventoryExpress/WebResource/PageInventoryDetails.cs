using InventoryExpress.Model;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("Details")]
    [Title("inventoryexpress.details.label")]
    [SegmentGuid("InventoryID", "inventoryexpress.details.label")]
    [Path("/")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("inventorydetails")]
    public sealed class PageInventoryDetails : PageTemplateWebApp
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryDetails()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            lock (ViewModel.Instance.Database)
            {
                var id = GetParamValue("InventoryID");
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();

                Uri.Display = inventory?.Name;
                Title = inventory?.Name;
            }
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            lock (ViewModel.Instance.Database)
            {
                var id = GetParamValue("InventoryID");
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();
                var classes = new List<string>() { "w-100" };

                Content.Primary.Add(new ControlPanelFlexbox(new ControlCarousel
                (
                    (from attachment in ViewModel.Instance.InventoryAttachment
                     join media in ViewModel.Instance.Media
                     on attachment.MediaId equals media.Id
                     where attachment.InventoryId == inventory.Id &&
                     (media.Name.ToLower().EndsWith(".jpg") || media.Name.ToLower().EndsWith(".png") || media.Name.ToLower().EndsWith(".svg"))
                     select
                     (
                         new ControlCarouselItem()
                         {
                             Control = new ControlImage("id_" + media.Id) { Uri = Uri.Root.Append("media").Append(media.Guid), Classes = classes }
                         }
                     )).ToArray()
                )
                {
                    Width = TypeWidth.SeventyFive
                })
                {
                    Layout = TypeLayoutFlexbox.Default,
                    Justify = TypeJustifiedFlexbox.Center
                });

                Content.Primary.Add(new ControlText()
                {
                    Text = inventory?.Description,
                    Format = TypeFormatText.Paragraph,
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                });
            }
        }
    }
}
