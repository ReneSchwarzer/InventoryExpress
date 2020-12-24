using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("ManufactorDel")]
    [Title("inventoryexpress.manufactor.delete.label")]
    [Segment("del", "inventoryexpress.manufactor.delete.display")]
    [Path("/Manufactor/ManufactorEdit")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("manufectoredit")]
    public sealed class PageManufactorDelete : PageTemplateWebApp, IPageManufactor
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufactorDelete()
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

            var guid = GetParamValue("ManufactorID");
            var manufactur = ViewModel.Instance.Manufacturers.Where(x => x.Guid == guid).FirstOrDefault();
            var media = ViewModel.Instance.Media.Where(x => x.ID == manufactur.ID).FirstOrDefault();

            ViewModel.Instance.Manufacturers.Remove(manufactur);

            if (media != null)
            {
                //manufactur.MediaID = null;
                ViewModel.Instance.Media.Remove(media);
            }

            ViewModel.Instance.SaveChanges();

            Redirecting(Uri.Take(-2));
        }

        /// <summary>
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
