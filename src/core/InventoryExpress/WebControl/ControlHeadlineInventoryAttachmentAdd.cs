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
using WebExpress.WebApp.WebControl;
using WebExpress.WebResource;

namespace InventoryExpress.WebControl
{
    [Section(Section.HeadlineSecondary)]
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
                var form = new ControlModalFormFileUpload("add") 
                { 
                    Header = context.Page.I18N("inventoryexpress.media.file.add.label") 
                };

                form.Upload += (s, e) =>
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

                            var journal = new InventoryJournal()
                            {
                                InventoryId = inventory.Id,
                                Action = "inventoryexpress.journal.action.inventory.attachment.add",
                                Created = DateTime.Now,
                                Guid = Guid.NewGuid().ToString()
                            };
                            
                            ViewModel.Instance.InventoryJournals.Add(journal);

                            ViewModel.Instance.InventoryJournalParameters.Add(new InventoryJournalParameter()
                            {
                                InventoryJournal = journal,
                                Name = "inventoryexpress.inventory.attachment.label",
                                OldValue = "🖳",
                                NewValue = media.Name,
                                Guid = Guid.NewGuid().ToString()
                            });
                                                        
                            ViewModel.Instance.SaveChanges();
                        }
                    }
                };

                Text = context.Page.I18N("inventoryexpress.media.file.add.label");
                Icon = new PropertyIcon(TypeIcon.Plus);
                BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
                Value = inventory?.Created.ToString(context.Page.Culture.DateTimeFormat.ShortDatePattern);

                Modal = form;

                return base.Render(context);
            }
        }
    }
}
