using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("CostCenterMedia")]
    [Title("inventoryexpress:inventoryexpress.costcenter.media.label")]
    [Segment("media", "inventoryexpress:inventoryexpress.costcenter.media.display")]
    [Path("/CostCenter/CostCenterEdit")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("media")]
    [Context("mediaedit")]
    [Context("costcenteredit")]
    public sealed class PageCostCenterMedia : PageWebApp, IPageCostCenter
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularMedia Form { get; set; }

        /// <summary>
        /// Liefert oder setzt die Kostenstelle
        /// </summary>
        private CostCenter CostCenter { get; set; }

        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        private Media Media { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageCostCenterMedia()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form = new ControlFormularMedia("media");


        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            Form.RedirectUri = context.Uri;
            Form.BackUri = context.Uri.Take(-1);

            var guid = context.Request.GetParameter("CostCenterID")?.Value;
            lock (ViewModel.Instance.Database)
            {
                CostCenter = ViewModel.Instance.CostCenters.Where(x => x.Guid == guid).FirstOrDefault();
                Media = ViewModel.Instance.Media.Where(x => x.Id == CostCenter.MediaId).FirstOrDefault();
            }

            //AddParam("MediaID", Media?.Guid, ParameterScope.Local);

            visualTree.Content.Preferences.Add(new ControlImage()
            {
                Uri = Media != null ? context.Uri.Root.Append($"media/{Media.Guid}") : context.Uri.Root.Append("/assets/img/inventoryexpress.svg"),
                Width = 400,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
            });

            visualTree.Content.Primary.Add(Form);

            Form.Tag.Value = Media?.Tag;

            Form.Image.Validation += (s, e) =>
            {
            };

            Form.ProcessFormular += (s, e) =>
            {
                if (context.Request.GetParameter(Form.Image.Name) is ParameterFile file)
                {
                    // Image speichern
                    if (Media == null)
                    {
                        CostCenter.Media = new Media()
                        {
                            Name = file.Value,
                            Data = file.Data,
                            Tag = Form.Tag.Value,
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };
                    }
                    else
                    {
                        // Image ändern
                        Media.Name = file.Value;
                        Media.Data = file.Data;
                        Media.Tag = Form.Tag.Value;
                        Media.Updated = DateTime.Now;
                    }
                }

                if (Form.Tag.Value != Media?.Tag)
                {
                    CostCenter.Media.Tag = Form.Tag.Value;
                }

                lock (ViewModel.Instance.Database)
                {
                    ViewModel.Instance.SaveChanges();
                }
            };
        }
    }
}
