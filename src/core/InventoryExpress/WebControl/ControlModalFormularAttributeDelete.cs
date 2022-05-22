using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlModalFormularAttributeDelete : ControlModalFormularConfirmDelete
    {
        /// <summary>
        /// Das zu löschende Attribut
        /// </summary>
        public Attribute Item { get; set; }

        /// <summary>
        /// Liefert den Text des Contentbereiches
        /// </summary>
        protected ControlFormularItemStaticText ContextText { get; } = new ControlFormularItemStaticText();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormularAttributeDelete(string id = null)
            : base("delete_" + id)
        {
            Header = "inventoryexpress:inventoryexpress.attribute.delete.header";
            Content = ContextText;
        }

        /// <summary>
        /// Löst das Confirm-Event aus
        /// </summary>
        /// <param name="context">Der Kontext</param>
        protected override void OnConfirm(RenderContextFormular context)
        {
            base.OnConfirm(context);

            lock (ViewModel.Instance.Database)
            {
                if (Item != null)
                {
                    // Aus DB löschen
                    ViewModel.Instance.Attributes.Remove(Item);
                }

                ViewModel.Instance.SaveChanges();
            }
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            ContextText.Text = string.Format(context.I18N("inventoryexpress:inventoryexpress.attribute.delete.description"), Item.Name);

            return base.Render(context);
        }
    }
}
