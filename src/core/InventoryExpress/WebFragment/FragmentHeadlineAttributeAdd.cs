﻿using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.HeadlineSecondary)]
    [WebExModule(typeof(Module))]
    [WebExContext("attribute")]
    public sealed class FragmentHeadlineAttributeAdd : FragmentControlButtonLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentHeadlineAttributeAdd()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Text = "inventoryexpress:inventoryexpress.attribute.add.label";
            Icon = new PropertyIcon(TypeIcon.Plus);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Large);
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
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Uri = context.ApplicationContext.ContextPath.Append("setting/attributes/add/");

            return base.Render(context);
        }
    }
}