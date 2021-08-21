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
    [Section(Section.HeadlineSecondary)]
    [Application("InventoryExpress")]
    [Context("condition")]
    public sealed class ControlHeadlineConditionAdd : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlHeadlineConditionAdd()
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
                
                Text = context.Page.I18N("inventoryexpress.condition.add.label");
                Icon = new PropertyIcon(TypeIcon.Plus);
                BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
                Value = inventory?.Created.ToString(context.Page.Culture.DateTimeFormat.ShortDatePattern);

                Modal = new ControlModalFormularCondition
                (
                    //"add",
                    //context.Page.I18N("inventoryexpress.condition.add.label")
                );

                return base.Render(context);
            }
        }
    }
}
