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
    [Id("SettingCondition")]
    [Title("inventoryexpress:inventoryexpress.condition.label")]
    [Segment("conditions", "inventoryexpress:inventoryexpress.condition.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.StarHalf)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.data.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("condition")]
    [Cache]
    public sealed class PageSettingConditions : PageWebAppSetting
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
        public PageSettingConditions()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Table.RestUri = context.Application.ContextPath.Append("api/v1/conditions");


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
                OnClick = $"new webexpress.ui.modalFormularCtrl({{ uri: '{context.Application.ContextPath.Append("setting/conditions/edit/")}/' + item.id, size: 'large' }});"
            });

            Table.OptionItems.Add(new ControlApiTableOptionItem());

            Table.OptionItems.Add(new ControlApiTableOptionItem(InternationalizationManager.I18N(context, "inventoryexpress:inventoryexpress.delete.label"))
            {
                Icon = TypeIcon.Trash.ToClass(),
                Color = TypeColorText.Danger.ToClass(),
                Disabled = "return !item.isinuse;",
                OnClick = $"new webexpress.ui.modalFormularCtrl({{ uri: '{context.Application.ContextPath.Append("setting/conditions/del/")}/' + item.id, size: 'small' }});"
            });


            //var table = new ControlTable()
            //{
            //    Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three),
            //};

            //table.AddColumn("inventoryexpress:inventoryexpress.condition.name.label");
            //table.AddColumn("inventoryexpress:inventoryexpress.condition.description.label");
            //table.AddColumn("inventoryexpress:inventoryexpress.condition.order.label");
            //table.AddColumn("");

            //var conditions = new List<Condition>();

            //lock (ViewModel.Instance.Database)
            //{
            //    conditions.AddRange(ViewModel.Instance.Conditions);
            //}

            //foreach (var condition in conditions.OrderBy(x => x.Grade))
            //{
            //    var inuse = false;

            //    lock (ViewModel.Instance.Database)
            //    {
            //        inuse = ViewModel.Instance.Inventories.Where(x => x.ConditionId == condition.Id).Any();
            //    }

            //    table.AddRow
            //    (
            //        new ControlText() { Text = condition.Name },
            //        new ControlText() { Text = condition.Description },
            //        new ControlText() { Text = condition.Grade.ToString() },
            //        new ControlPanelFlexbox
            //        (
            //            new ControlLink()
            //            {
            //                Text = "inventoryexpress:inventoryexpress.condition.edit.label",
            //                Uri = Uri.Root.Append("setting/conditions/" + condition.Guid),
            //                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
            //                Modal = new PropertyModal(TypeModal.Formular)
            //            },
            //            new ControlText()
            //            {
            //                Text = "|",
            //                TextColor = new PropertyColorText(TypeColorText.Muted)
            //            },
            //            (
            //                inuse ?
            //                new ControlText()
            //                {
            //                    Text = "inventoryexpress:inventoryexpress.condition.delete.label",
            //                    TextColor = new PropertyColorText(TypeColorText.Muted),
            //                    Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null)
            //                }
            //                :
            //                new ControlLink()
            //                {
            //                    Text = "inventoryexpress:inventoryexpress.condition.delete.label",
            //                    TextColor = new PropertyColorText(TypeColorText.Danger),
            //                    Uri = Uri.Root.Append("setting/conditions/" + condition.Guid),
            //                    Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
            //                    Modal = new PropertyModal(TypeModal.Formular)
            //                }
            //            )
            //        )
            //        {
            //            Align = TypeAlignFlexbox.Center,
            //            Layout = TypeLayoutFlexbox.Default,
            //            Justify = TypeJustifiedFlexbox.End
            //        }
            //    );
            //}

            context.VisualTree.Content.Preferences.Add(Table);
        }
    }
}
