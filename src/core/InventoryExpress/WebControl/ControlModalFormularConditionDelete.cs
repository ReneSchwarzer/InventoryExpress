using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlModalFormularConditionDelete : ControlModalForm
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormularConditionDelete(string id = null)
            : base("delete_" + id)
        {
            Init();
        }

        /// <summary>
        /// Der zu löschende Status
        /// </summary>
        public Condition Item { get; set; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Formular.ProcessFormular += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    if (Item != null)
                    {
                        // Aus DB löschen
                        ViewModel.Instance.Conditions.Remove(Item);
                    }

                    ViewModel.Instance.SaveChanges();
                }

                lock (ViewModel.Instance.Database)
                {
                    var i = 1;
                    var order = ViewModel.Instance.Conditions.OrderBy(x => x.Grade).ToList();

                    // Neu sortieren
                    foreach (var o in order)
                    {
                        o.Grade = i++;
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
            Header = context.Page.I18N("inventoryexpress.condition.delete.header");

            Formular.SubmitButton.Text = context.Page.I18N("inventoryexpress.condition.delete.label");
            Formular.SubmitButton.Icon = new PropertyIcon(TypeIcon.TrashAlt);
            Formular.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Danger);
            Formular.RedirectUri = context.Uri;
            Formular.Add(new ControlFormularItemStaticText() { Text = string.Format(context.I18N("inventoryexpress.condition.delete.description"), $"{ Item.Grade } - { Item.Name}") });

            return base.Render(context);
        }
    }
}
