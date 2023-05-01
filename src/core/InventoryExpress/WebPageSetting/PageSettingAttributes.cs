﻿using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [WebExID("SettingAttribute")]
    [WebExTitle("inventoryexpress:inventoryexpress.attribute.label")]
    [WebExSegment("attributes", "inventoryexpress:inventoryexpress.attribute.label")]
    [WebExContextPath("/Setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.Cubes)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.data.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    [WebExContext("attribute")]
    [WebExCache()]
    public sealed class PageSettingAttributes : PageWebAppSetting
    {
        /// <summary>
        /// Liefert oder setzt die Tabelle
        /// </summary>
        private ControlApiTable Table { get; set; } = new ControlApiTable()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingAttributes()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Table.RestUri = ResourceContext.ContextPath.Append("api/v1/attributes");
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            Table.OptionSettings.Icon = TypeIcon.Cog.ToClass();

            Table.OptionItems.Clear();
            Table.OptionItems.Add(new ControlApiTableOptionItem(InternationalizationManager.I18N(context, "inventoryexpress:inventoryexpress.edit.label"))
            {
                Icon = TypeIcon.Edit.ToClass(),
                Color = TypeColorText.Dark.ToClass(),
                Uri = "#",
                OnClick = $"new webexpress.ui.modalFormularCtrl({{ uri: '{context.ApplicationContext.ContextPath.Append("setting/attributes/edit/")}/' + item.id, size: 'large' }});"
            });

            Table.OptionItems.Add(new ControlApiTableOptionItem());

            Table.OptionItems.Add(new ControlApiTableOptionItem(InternationalizationManager.I18N(context, "inventoryexpress:inventoryexpress.delete.label"))
            {
                Icon = TypeIcon.Trash.ToClass(),
                Color = TypeColorText.Danger.ToClass(),
                Disabled = "return !item.isinuse;",
                OnClick = $"new webexpress.ui.modalFormularCtrl({{ uri: '{context.ApplicationContext.ContextPath.Append("setting/conditions/del/")}/' + item.id, size: 'small' }});"
            });

            context.VisualTree.Content.Preferences.Add(Table);
        }
    }
}
