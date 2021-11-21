using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using System;
using System.IO;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Message;
using WebExpress.UI.Attribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebApp.WebControl;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    [Section(Section.HeadlineSecondary)]
    [Module("inventoryexpress")]
    [Context("attachment")]
    public sealed class ComponentHeadlineInventoryAttachmentAdd : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentHeadlineInventoryAttachmentAdd()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public void Initialization(IComponentContext context)
        {
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
                var guid = context.Request.GetParameter("InventoryID")?.Value;
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
                var form = new ControlModalFormFileUpload("add")
                {
                    Header = I18N(context.Culture, "inventoryexpress:inventoryexpress.media.file.add.label")
                };

                form.Upload += (s, e) =>
                {
                    if (context.Request.GetParameter(form.File.Name) is ParameterFile file)
                    {
                        // Anlage speichern
                        var stock = from a in ViewModel.Instance.InventoryAttachments
                                    join m in ViewModel.Instance.Media
                                    on a.MediaId equals m.Id
                                    where m.Name == file.Value
                                    select a;

                        if (stock.Count() == 0)
                        {
                            var media = new Media()
                            {
                                Name = file.Value,
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
                            ViewModel.Instance.InventoryAttachments.Add(attachment);
                            ViewModel.Instance.SaveChanges();

                            File.WriteAllBytes(Path.Combine(context.Application.AssetPath, media.Guid), file.Data);

                            var journal = new InventoryJournal()
                            {
                                InventoryId = inventory.Id,
                                Action = "inventoryexpress:inventoryexpress.journal.action.inventory.attachment.add",
                                Created = DateTime.Now,
                                Guid = Guid.NewGuid().ToString()
                            };

                            ViewModel.Instance.InventoryJournals.Add(journal);

                            ViewModel.Instance.InventoryJournalParameters.Add(new InventoryJournalParameter()
                            {
                                InventoryJournal = journal,
                                Name = "inventoryexpress:inventoryexpress.inventory.attachment.label",
                                OldValue = "🖳",
                                NewValue = media.Name,
                                Guid = Guid.NewGuid().ToString()
                            });

                            ViewModel.Instance.SaveChanges();
                        }
                    }
                };

                Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.media.file.add.label");
                Icon = new PropertyIcon(TypeIcon.Plus);
                BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
                Value = inventory?.Created.ToString(context.Page.Culture.DateTimeFormat.ShortDatePattern);

                Modal = form;

                return base.Render(context);
            }
        }
    }
}
