using InventoryExpress.Model;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.SidebarPreferences)]
    [Application("InventoryExpress")]
    [Context("manufectoredit")]
    public sealed class ControlSidebarManufactorMedia : ControlDropdown, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlSidebarManufactorMedia()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Page.GetParamValue("ManufactorID");
            var manufactur = ViewModel.Instance.Manufacturers.Where(x => x.Guid == guid).FirstOrDefault();

            var image = ViewModel.Instance.Media.Where(x => x.ID == manufactur.MediaID).Select(x => context.Page.Uri.Root.Append("media/" + x.Guid)).FirstOrDefault();

            Add(new ControlDropdownItemLink()
            {
                Text = context.Page.I18N("inventoryexpress.edit.label"),
                Uri = new UriRelative("#editimage"),
                Modal = new ControlModal
                (
                    "editimage",
                    context.Page.I18N("inventoryexpress.manufactor.edit.media.label"),
                    new ControlText()
                    {
                        Text = context.Page.I18N("inventoryexpress.manufactor.edit.media.description")
                    },
                    new ControlButton()
                    {
                        Text = context.Page.I18N("inventoryexpress.delete.label"),
                        Icon = new PropertyIcon(TypeIcon.PowerOff),
                        Margin = new PropertySpacingMargin(PropertySpacing.Space.One),
                        BackgroundColor = new PropertyColorButton(TypeColorButton.Danger),
                        OnClick = $"window.location.href = '{ context.Page.Uri.Append("del") }'"
                    }
                )
                {
                    Styles = new List<string>(new string[] { "z-index: 999999;" })
                }
            }); ;

            AddSeperator();

            Add(new ControlDropdownItemLink()
            {
                Text = context.Page.I18N("inventoryexpress.delete.label"),
                Icon = new PropertyIcon(TypeIcon.Trash),
                TextColor = new PropertyColorText(TypeColorText.Danger),
                Active = image == null ? TypeActive.Disabled : TypeActive.None,
                Modal = new ControlModal
                (
                    "deleteimage",
                    context.Page.I18N("inventoryexpress.manufactor.delete.media.label"),
                    new ControlText()
                    {
                        Text = context.Page.I18N("inventoryexpress.manufactor.delete.media.description")
                    },
                    new ControlButton()
                    {
                        Text = context.Page.I18N("inventoryexpress.delete.label"),
                        Icon = new PropertyIcon(TypeIcon.PowerOff),
                        Margin = new PropertySpacingMargin(PropertySpacing.Space.One),
                        BackgroundColor = new PropertyColorButton(TypeColorButton.Danger),
                        OnClick = $"window.location.href = '{ context.Page.Uri.Append("del") }'"
                    }
                )
            });

            Image = image == null ? context.Page.Uri.Root.Append("/assets/img/Logo.png") : image;
            Width = 150;
            Height = 150;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

            return base.Render(context);
        }
    }
}
