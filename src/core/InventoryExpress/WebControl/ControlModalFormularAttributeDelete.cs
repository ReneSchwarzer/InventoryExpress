using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlModalFormularAttributeDelete : ControlModalFormDelete
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormularAttributeDelete(string id = null)
            : base("delete_" + id)
        {
            Init();
        }

        /// <summary>
        /// Das zu löschende Attribut
        /// </summary>
        public Model.Attribute Item { get; set; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Delete += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    if (Item != null)
                    {
                        // Aus DB löschen
                        ViewModel.Instance.Attributes.Remove(Item);
                    }

                    ViewModel.Instance.SaveChanges();
                }
            };
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Header = context.Page.I18N("inventoryexpress.attribute.delete.header");
            Content = new ControlFormularItemStaticText() { Text = string.Format(context.I18N("inventoryexpress.attribute.delete.description"), Item.Name) };

            return base.Render(context);
        }
    }
}
