using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
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
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var id = GetParamValue("InventoryID");
            var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();

            Title = inventory?.Name;

            Content.Primary.Add(new ControlImage()
            {
                //Source = new UriRelative("/data/" + inventory?.Image)

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
