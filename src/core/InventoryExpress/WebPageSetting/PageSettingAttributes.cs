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
    [Id("SettingAttribute")]
    [Title("inventoryexpress:inventoryexpress.attribute.label")]
    [Segment("attributes", "inventoryexpress:inventoryexpress.attribute.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.Cubes)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.data.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("attribute")]
    [Cache()]
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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Table.RestUri = context.Application.ContextPath.Append("api/v1/attributes");
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
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
                OnClick = $"new webexpress.ui.modalFormularCtrl({{ uri: '{context.Application.ContextPath.Append("setting/attributes/edit/")}/' + item.id, size: 'large' }});"
            });

            Table.OptionItems.Add(new ControlApiTableOptionItem());

            Table.OptionItems.Add(new ControlApiTableOptionItem(InternationalizationManager.I18N(context, "inventoryexpress:inventoryexpress.delete.label"))
            {
                Icon = TypeIcon.Trash.ToClass(),
                Color = TypeColorText.Danger.ToClass(),
                Disabled = "return !item.isinuse;",
                OnClick = $"new webexpress.ui.modalFormularCtrl({{ uri: '{context.Application.ContextPath.Append("setting/conditions/del/")}/' + item.id, size: 'small' }});"
            });

            context.VisualTree.Content.Preferences.Add(Table);
        }
    }
}
