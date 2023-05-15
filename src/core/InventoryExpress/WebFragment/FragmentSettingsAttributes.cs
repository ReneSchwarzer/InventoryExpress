﻿using InventoryExpress.WebPageSetting;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.AppSettingsPrimary)]
    [WebExModule(typeof(Module))]
    public sealed class FragmentSettingsAttributes : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentSettingsAttributes()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);

            Text = "inventoryexpress:inventoryexpress.attribute.label";
            Uri = context.ModuleContext.ContextPath.Append("setting/attributes");
            Icon = new PropertyIcon(TypeIcon.Cubes);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageTemplate ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }

    }
}