using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    /// <summary>
    /// Modal zum Löschen einer Kostenstelle. Wird von der Komponetne ControlMoreCostCenterDelete aufgerufen
    /// </summary>
    [Section(Section.ContentSecondary)]
    [Module("inventoryexpress")]
    [Context("costcenteredit")]
    public sealed class ComponentContentCostCenterModalDelete : ComponentControlModalFormConfirmDelete
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContentCostCenterModalDelete()
           : base("del_costcenter")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Confirm += (s, e) =>
            {
                var guid = context.Request.GetParameter("CostCenterID")?.Value;
                var costcenter = ViewModel.GetCostCenter(guid);

                if (costcenter != null)
                {
                    ViewModel.DeleteCostCenter(guid);
                    ViewModel.Instance.SaveChanges();

                    NotificationManager.CreateNotification
                    (
                        e.Context.Request,
                        string.Format
                        (
                            I18N(e.Context, "inventoryexpress:inventoryexpress.costcenter.notification.delete"),
                            new ControlText()
                            {
                                Text = costcenter.Name,
                                Format = TypeFormatText.Span,
                                TextColor = new PropertyColorText(TypeColorText.Danger)
                            }.Render(e.Context).ToString().Trim()
                        )
                    );
                }
            };

            Header = I18N(context.Culture, "inventoryexpress:inventoryexpress.costcenter.delete.label");
            Content = new ControlFormularItemStaticText() { Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.costcenter.delete.description") };
            RedirectUri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
