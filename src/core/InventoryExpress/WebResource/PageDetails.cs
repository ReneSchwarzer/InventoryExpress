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
    [Context("details")]
    public sealed class PageDetails : PageTemplateWebApp
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageDetails()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            //ToolBar.Add(new ControlToolBarItemButton()
            //{
            //    Icon = new PropertyIcon(TypeIcon.Edit),
            //    Text = "Ändern",
            //    Title = "Bearbeiten",
            //    Uri = Uri.Root.Append("edit"),
            //    TextColor = new PropertyColorText(TypeColorText.White),
            //    //Modal = new ControlModalEdit(this)
            //},
            //new ControlToolBarItemButton()
            //{
            //    Icon = new PropertyIcon(TypeIcon.Print),
            //    Uri = Uri.Root.Append("print"),
            //    Title = "Drucken",
            //    Size = new PropertySizeText(TypeSizeText.Default),
            //    TextColor = new PropertyColorText(TypeColorText.White)
            //},
            //new ControlToolBarItemButton()
            //{
            //    Icon = new PropertyIcon(TypeIcon.TrashAlt),
            //    Title = "Löschen",
            //    Size = new PropertySizeText(TypeSizeText.Default),
            //    TextColor = new PropertyColorText(TypeColorText.White),
            //    Modal = new ControlModalDel()
            //    {

            //    }
            //});
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var id = GetParamValue("InventoryID");
            var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();

            Content.Content.Add(new ControlText()
            {
                Text = inventory?.Name,
                Format = TypeFormatText.H1,
                TextColor = new PropertyColorText(TypeColorText.Primary)
            });

            Content.Content.Add(new ControlAttribute()
            {
                Name = "Bild:",
                Icon = new PropertyIcon(TypeIcon.Image),
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            Content.Content.Add(new ControlImage()
            {
                //Source = new UriRelative("/data/" + inventory?.Image)

            });

            Content.Content.Add(new ControlAttribute()
            {
                Name = "Beschreibung:",
                Icon = new PropertyIcon(TypeIcon.Comment),
                TextColor = new PropertyColorText(TypeColorText.Dark)

            });

            Content.Content.Add(new ControlText()
            {
                Text = inventory?.Description,
                Format = TypeFormatText.Paragraph,
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });
        }
    }
}
