using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;
using WebExpress.WebResource;

namespace InventoryExpress.WebControl
{
    [Section(Section.MoreSecondary)]
    [Application("InventoryExpress")]
    [Context("attachment")]
    public sealed class ControlHeadlineInventoryAttachmentAdd : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlHeadlineInventoryAttachmentAdd()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            lock (ViewModel.Instance.Database)
            {
                var guid = context.Page.GetParamValue("InventoryID");
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
                var form = new ControlFormularFileUpload("add") { EnableSubmitAndNextButton = false, EnableCancelButton = false, RedirectUri = Uri };
                
                form.ProcessFormular += (s, e) =>
                {
                    if ((context.Page as Resource).GetParam(form.File.Name) is ParameterFile file)
                    {
                        // Anlage speichern
                        var stock = from a in ViewModel.Instance.InventoryAttachment
                                    join m in ViewModel.Instance.Media
                                    on a.MediaId equals m.Id
                                    where m.Name == file.Value
                                    select a;

                        if (stock.Count() == 0)
                        {
                            var media = new Media()
                            {
                                Name = file.Value,
                                Data = file.Data,
                                Created = DateTime.Now,
                                Updated = DateTime.Now,
                                Guid = Guid.NewGuid().ToString()
                            };

                            var attachment = new InventoryAttachment()
                            {
                                InventoryId = inventory.Id,
                                Media = media,
                                Created = DateTime.Now,
                            };

                            ViewModel.Instance.Media.Add(media);
                            ViewModel.Instance.InventoryAttachment.Add(attachment);
                            ViewModel.Instance.SaveChanges();

                            context.Page.Redirecting(context.Uri);
                        }
                    }
                };

                Text = context.Page.I18N("inventoryexpress.media.file.add.label");
                Icon = new PropertyIcon(TypeIcon.Plus);
                BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
                Value = inventory?.Created.ToString(context.Page.Culture.DateTimeFormat.ShortDatePattern);

                Modal = new ControlModal
                (
                    "add",
                    context.Page.I18N("inventoryexpress.media.file.add.label"),
                    form
                );

                return base.Render(context);
            }
        }
    }
}
