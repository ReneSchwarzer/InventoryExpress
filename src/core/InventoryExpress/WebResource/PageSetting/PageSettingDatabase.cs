using InventoryExpress.Model;
using System.IO;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebResource.PageSetting;

namespace InventoryExpress.WebResource.PageSetting
{
    [ID("SettingDatabase")]
    [Title("inventoryexpress.setting.database.label")]
    [Segment("database", "inventoryexpress.setting.database.label")]
    [Path("/SettingGeneral")]
    [SettingSection(SettingSection.Secondary)]
    [SettingIcon(TypeIcon.Database)]
    [SettingGroup("inventoryexpress.setting.system.label")]
    [SettingContext("inventoryexpress.setting.general.label")]
    [Module("InventoryExpress")]
    [Context("admin")]
    public sealed class PageSettingDatabase : PageTemplateWebAppSetting
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingDatabase()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            var providerName = ViewModel.Instance.Database.ProviderName;
            var dataSource = ViewModel.Instance.DataSource;
            var file = new FileInfo(dataSource);
            var fileSize = string.Format(new FileSizeFormatProvider() { Culture = Culture }, "{0:fs}", file.Exists ? file.Length : 0);

            var table = new ControlTable() { Striped = false };
            table.AddRow(new ControlText() { Text = this.I18N("inventoryexpress.setting.database.provider.label") }, new ControlText() { Text = providerName, Format = TypeFormatText.Code });
            table.AddRow(new ControlText() { Text = this.I18N("inventoryexpress.setting.database.datasource.label") }, new ControlText() { Text = dataSource, Format = TypeFormatText.Code });
            table.AddRow(new ControlText() { Text = this.I18N("inventoryexpress.setting.database.filesize.label") }, new ControlText() { Text = fileSize, Format = TypeFormatText.Code });

            Content.Primary.Add(new ControlText() { Text = this.I18N("inventoryexpress.setting.database.info.label"), TextColor = new PropertyColorText(TypeColorText.Info), Margin = new PropertySpacingMargin(PropertySpacing.Space.Two) });
            Content.Primary.Add(table);



            lock (ViewModel.Instance.Database)
            {
            }
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();
        }
    }
}
