﻿using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.UI.Controls;
using WebExpress.UI.Plugin;

namespace InventoryExpress.Controls
{
    public class ControlQuickCreateManufactor : ControlLink, IPluginComponentQuickCreateSecondary
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlQuickCreateManufactor()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            TextColor = new PropertyColorText(TypeColorText.Dark);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Text = "Hersteller";
            Uri = context.Page.Uri.Root.Append("manufactors/add");
            Active = context.Page is IPageManufactor ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.Industry);

            return base.Render(context);
        }

    }
}
