using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingTemplateMedia")]
    [Title("inventoryexpress:inventoryexpress.template.media.label")]
    [Segment("media", "inventoryexpress:inventoryexpress.template.media.display")]
    [Path("/Setting/SettingTemplate/SettingTemplateEdit")]
    [Module("inventoryexpress")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Context("general")]
    [Context("media")]
    [Context("mediaedit")]
    public sealed class PageSettingTemplateMedia : PageWebAppSetting
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularMedia Form { get; set; }

        /// <summary>
        /// Liefert oder setzt die Vorlage
        /// </summary>
        private Template Template { get; set; }

        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        private Media Media { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingTemplateMedia()
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

            var guid = context.Request.GetParameter("TemplateID")?.Value;
            lock (ViewModel.Instance.Database)
            {
                Template = ViewModel.Instance.Templates.Where(x => x.Guid == guid).FirstOrDefault();
                Media = ViewModel.Instance.Media.Where(x => x.Id == Template.MediaId).FirstOrDefault();
            }

            //AddParam("MediaID", Media?.Guid, ParameterScope.Local);

            visualTree.Content.Preferences.Add(new ControlImage()
            {
                Uri = Media != null ? context.Uri.Root.Append($"media/{Media.Guid}") : context.Uri.Root.Append("/assets/img/inventoryexpress.svg"),
                Width = 400,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
            });

            visualTree.Content.Primary.Add(Form);

            Form.RedirectUri = context.Uri;
            Form.BackUri = context.Uri.Take(-1);

            Form.Tag.Value = Media?.Tag;

            Form.Image.Validation += (s, e) =>
            {
            };

            Form.ProcessFormular += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    if (context.Request.GetParameter(Form.Image.Name) is ParameterFile file)
                    {
                        // Image speichern
                        if (Media == null)
                        {
                            Template.Media = new Media()
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
                        Template.Media.Tag = Form.Tag.Value;
                    }

                    ViewModel.Instance.SaveChanges();
                }
            };
        }
    }
}
