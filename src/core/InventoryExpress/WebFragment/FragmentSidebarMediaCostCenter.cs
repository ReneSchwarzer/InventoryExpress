﻿using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebAttribute;
using WebExpress.WebMessage;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [Section(Section.SidebarHeader)]
    [WebExModule("inventoryexpress")]
    [WebExContext("costcenteredit")]
    public sealed class FragmentSidebarMediaCostCenter : FragmentSidebarMedia
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentSidebarMediaCostCenter()
        {
            Form.Header = "inventoryexpress:inventoryexpress.costcenter.media.label";
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Upload-Ereignis ausgelöst wurde
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        protected override void OnUpload(object sender, FormularUploadEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.File.Name) as ParameterFile;
            var guid = e.Context.Request.GetParameter("CostCenterID")?.Value;
            var costCenter = ViewModel.GetCostCenter(guid);

            if (file != null)
            {
                using var transaction = ViewModel.BeginTransaction();

                ViewModel.AddOrUpdateMedia(costCenter, file);

                transaction.Commit();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(e.Context.Culture, "inventoryexpress:inventoryexpress.media.notification.edit"),
                    new ControlLink()
                    {
                        Text = costCenter.Name,
                        Uri = ViewModel.GetCostCenterUri(costCenter.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: ViewModel.GetMediaUri(costCenter.Media.Id),
                durability: 10000
            );
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Request.GetParameter("CostCenterID")?.Value;
            var costCenter = ViewModel.GetCostCenter(guid);

            Image.Uri = costCenter?.Image;

            return base.Render(context);
        }
    }
}
