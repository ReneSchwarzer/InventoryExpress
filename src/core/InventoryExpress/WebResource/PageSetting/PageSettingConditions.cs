using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebResource.PageSetting;

namespace InventoryExpress.WebResource.PageSetting
{
    [ID("SettingCondition")]
    [Title("inventoryexpress.condition.label")]
    [Segment("condition", "inventoryexpress.condition.label")]
    [Path("/SettingGeneral")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.StarHalf)]
    [SettingGroup("inventoryexpress.setting.data.label")]
    [SettingContext("inventoryexpress.setting.general.label")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("condition")]
    public sealed class PageSettingConditions : PageTemplateWebAppSetting
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingConditions()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var context = new RenderContext(this);

            var table = new ControlTable()
            {
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three),
            };

            table.AddColumn(context.I18N("inventoryexpress.condition.name.label"));
            table.AddColumn(context.I18N("inventoryexpress.condition.description.label"));
            table.AddColumn(context.I18N("inventoryexpress.condition.order.label"));
            table.AddColumn("");

            var conditions = new List<Condition>();

            lock (ViewModel.Instance.Database)
            {
                conditions.AddRange(ViewModel.Instance.Conditions);
            }

            foreach (var condition in conditions.OrderBy(x => x.Grade))
            {
                var inuse = false;

                lock (ViewModel.Instance.Database)
                {
                    inuse = ViewModel.Instance.Inventories.Where(x => x.ConditionId == condition.Id).Any();
                }

                table.AddRow
                (
                    new ControlText() { Text = condition.Name },
                    new ControlText() { Text = condition.Description },
                    new ControlText() { Text = condition.Grade.ToString() },
                    new ControlPanelFlexbox
                    (
                        new ControlLink()
                        {
                            Text = context.I18N("inventoryexpress.condition.edit.label"),
                            Uri = new UriFragment(),
                            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                            Modal = new ControlModalFormularConditionEdit(condition.Guid) { Item = condition }
                        },
                        new ControlText()
                        {
                            Text = "|",
                            TextColor = new PropertyColorText(TypeColorText.Muted)
                        },
                        (
                            inuse ?
                            new ControlText()
                            {
                                Text = context.I18N("inventoryexpress.condition.delete.label"),
                                TextColor = new PropertyColorText(TypeColorText.Muted),
                                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null)
                            }
                            :
                            new ControlLink()
                            {
                                Text = context.I18N("inventoryexpress.condition.delete.label"),
                                TextColor = new PropertyColorText(TypeColorText.Danger),
                                Uri = new UriFragment(),
                                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                                Modal = new ControlModalFormularConditionDelete(condition.Guid) { Item = condition }
                            }
                        )
                    )
                    {
                        Align = TypeAlignFlexbox.Center,
                        Layout = TypeLayoutFlexbox.Default,
                        Justify = TypeJustifiedFlexbox.End
                    }
                );
            }

            Content.Preferences.Add(table);
        }
    }
}
