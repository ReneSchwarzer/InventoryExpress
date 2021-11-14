﻿using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingAttribute")]
    [Title("inventoryexpress:inventoryexpress.attribute.label")]
    [Segment("attribute", "inventoryexpress:inventoryexpress.attribute.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.Cubes)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.data.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("attribute")]
    public sealed class PageSettingAttributes : PageWebAppSetting
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

            table.AddColumn(this.I18N("inventoryexpress:inventoryexpress.condition.name.label"));
            table.AddColumn(this.I18N("inventoryexpress:inventoryexpress.condition.description.label"));
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
                            Text = this.I18N("inventoryexpress:inventoryexpress.attribute.edit.label"),
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
                                Text = this.I18N("inventoryexpress:inventoryexpress.attribute.delete.label"),
                                TextColor = new PropertyColorText(TypeColorText.Muted),
                                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null)
                            }
                            :
                            new ControlLink()
                            {
                                Text = this.I18N("inventoryexpress:inventoryexpress.attribute.delete.label"),
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

            visualTree.Content.Preferences.Add(table);
        }
    }
}
