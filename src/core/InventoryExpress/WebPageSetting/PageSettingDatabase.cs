using InventoryExpress.Model;
using System.IO;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingDatabase")]
    [Title("inventoryexpress:inventoryexpress.setting.database.label")]
    [Segment("database", "inventoryexpress:inventoryexpress.setting.database.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Secondary)]
    [SettingIcon(TypeIcon.Database)]
    [SettingGroup("webexpress.webapp:setting.group.system.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("admin")]
    public sealed class PageSettingDatabase : PageWebAppSetting
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

            var providerName = ViewModel.Instance.Database.ProviderName;
            var dataSource = ViewModel.Instance.DataSource;
            var file = new FileInfo(dataSource);
            var fileSize = string.Format(new FileSizeFormatProvider() { Culture = Culture }, "{0:fs}", file.Exists ? file.Length : 0);

            var table = new ControlTable() { Striped = false };
            table.AddRow(new ControlText() { Text = this.I18N("inventoryexpress.setting.database.provider.label") }, new ControlText() { Text = providerName, Format = TypeFormatText.Code });
            table.AddRow(new ControlText() { Text = this.I18N("inventoryexpress.setting.database.datasource.label") }, new ControlText() { Text = dataSource, Format = TypeFormatText.Code });
            table.AddRow(new ControlText() { Text = this.I18N("inventoryexpress.setting.database.filesize.label") }, new ControlText() { Text = fileSize, Format = TypeFormatText.Code });

            visualTree.Content.Primary.Add(new ControlText() { Text = this.I18N("inventoryexpress.setting.database.info.label"), TextColor = new PropertyColorText(TypeColorText.Info), Margin = new PropertySpacingMargin(PropertySpacing.Space.Two) });
            visualTree.Content.Primary.Add(table);
        }
    }
}
