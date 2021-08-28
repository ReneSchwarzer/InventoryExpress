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
    [ID("SettingAttribute")]
    [Title("inventoryexpress.attribute.label")]
    [Segment("attribute", "inventoryexpress.attribute.label")]
    [Path("/SettingGeneral")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.Cubes)]
    [SettingGroup("inventoryexpress.setting.data.label")]
    [SettingContext("inventoryexpress.setting.general.label")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("attribute")]
    public sealed class PageSettingAttributes : PageTemplateWebAppSetting
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingAttributes()
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
            table.AddColumn("");

            var attributes = new List<Attribute>();

            lock (ViewModel.Instance.Database)
            {
                attributes.AddRange(ViewModel.Instance.Attributes);
            }

            foreach (var attribute in attributes.OrderBy(x => x.Name))
            {
                var inuse = false;

                lock (ViewModel.Instance.Database)
                {
                    inuse = ViewModel.Instance.InventoryAttributes.Where(x => x.AttributeId == attribute.Id).Any() ||
                            ViewModel.Instance.TemplateAttributes.Where(x => x.AttributeId == attribute.Id).Any();
                }

                table.AddRow
                (
                    new ControlText() { Text = attribute.Name },
                    new ControlText() { Text = attribute.Description },
                    new ControlPanelFlexbox
                    (
                        new ControlLink()
                        {
                            Text = context.I18N("inventoryexpress.attribute.edit.label"),
                            Uri = new UriFragment(),
                            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                            Modal = new ControlModalFormularAttributeEdit(attribute.Guid) { Item = attribute }
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
                                Text = context.I18N("inventoryexpress.attribute.delete.label"),
                                TextColor = new PropertyColorText(TypeColorText.Muted),
                                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null)
                            }
                            :
                            new ControlLink()
                            {
                                Text = context.I18N("inventoryexpress.attribute.delete.label"),
                                TextColor = new PropertyColorText(TypeColorText.Danger),
                                Uri = new UriFragment(),
                                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                                Modal = new ControlModalFormularAttributeDelete(attribute.Guid) { Item = attribute }
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
