using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingCondition")]
    [Title("inventoryexpress:inventoryexpress.condition.label")]
    [Segment("condition", "inventoryexpress:inventoryexpress.condition.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.StarHalf)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.data.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("condition")]
    public sealed class PageSettingConditions : PageWebAppSetting
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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            var table = new ControlTable()
            {
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three),
            };

            table.AddColumn("inventoryexpress:inventoryexpress.condition.name.label");
            table.AddColumn("inventoryexpress:inventoryexpress.condition.description.label");
            table.AddColumn("inventoryexpress:inventoryexpress.condition.order.label");
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
                            Text = "inventoryexpress:inventoryexpress.condition.edit.label",
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
                                Text = "inventoryexpress:inventoryexpress.condition.delete.label",
                                TextColor = new PropertyColorText(TypeColorText.Muted),
                                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null)
                            }
                            :
                            new ControlLink()
                            {
                                Text = "inventoryexpress:inventoryexpress.condition.delete.label",
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

            visualTree.Content.Preferences.Add(table);
        }
    }
}
