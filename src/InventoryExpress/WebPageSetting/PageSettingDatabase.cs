using InventoryExpress.Model;
using System.IO;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebScope;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [WebExTitle("inventoryexpress:inventoryexpress.setting.database.label")]
    [WebExSegment("database", "inventoryexpress:inventoryexpress.setting.database.label")]
    [WebExContextPath("/setting")]
    [WebExSettingSection(WebExSettingSection.Secondary)]
    [WebExSettingIcon(TypeIcon.Database)]
    [WebExSettingGroup("webexpress.webapp:setting.group.system.label")]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule<Module>]
    [WebExScope<ScopeAdmin>]
    public sealed class PageSettingDatabase : PageWebAppSetting
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageSettingDatabase()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;
            var dbInfo = ViewModel.GetDbInfo();

            var providerName = dbInfo.ProviderName;
            var dataSource = dbInfo.DataSource;
            var file = new FileInfo(dataSource);
            var fileSize = string.Format(new FileSizeFormatProvider() { Culture = Culture }, "{0:fs}", file.Exists ? file.Length : 0);

            var table = new ControlTable() { Striped = false };
            table.AddRow(new ControlText() { Text = this.I18N("inventoryexpress:inventoryexpress.setting.database.provider.label") }, new ControlText() { Text = providerName, Format = TypeFormatText.Code });
            table.AddRow(new ControlText() { Text = this.I18N("inventoryexpress:inventoryexpress.setting.database.datasource.label") }, new ControlText() { Text = dataSource, Format = TypeFormatText.Code });
            table.AddRow(new ControlText() { Text = this.I18N("inventoryexpress:inventoryexpress.setting.database.filesize.label") }, new ControlText() { Text = fileSize, Format = TypeFormatText.Code });

            visualTree.Content.Primary.Add(new ControlText() { Text = this.I18N("inventoryexpress:inventoryexpress.setting.database.info.label"), TextColor = new PropertyColorText(TypeColorText.Info), Margin = new PropertySpacingMargin(PropertySpacing.Space.Two) });
            visualTree.Content.Primary.Add(table);
        }
    }
}
