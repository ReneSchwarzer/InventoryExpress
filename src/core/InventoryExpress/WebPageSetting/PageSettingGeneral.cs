using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingGeneral")]
    [Title("inventoryexpress:inventoryexpress.setting.label")]
    [Segment("general", "inventoryexpress:inventoryexpress.setting.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Preferences)]
    [SettingIcon(TypeIcon.Wrench)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.general.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("admin")]
    public sealed class PageSettingGeneral : PageWebAppSetting
    {
        /// <summary>
        /// Das Einstellungsformular
        /// </summary>
        private ControlFormularSettings Form { get; } = new ControlFormularSettings("settings")
        {
            EnableCancelButton = false
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingGeneral()
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

            var setting = null as Setting;

            Form.RedirectUri = context.Uri;
            Form.BackUri = context.Uri.Take(-1);

            lock (ViewModel.Instance.Database)
            {
                setting = ViewModel.Instance.Settings.FirstOrDefault();
            }

            Form.FillFormular += (s, e) =>
            {
                Form.Currency.Value = setting?.Currency;
            };

            Form.ProcessFormular += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    if (setting == null)
                    {
                        ViewModel.Instance.Settings.Add(new Setting()
                        {
                            Currency = Form.Currency.Value
                        });
                    }
                    else
                    {
                        setting.Currency = Form.Currency.Value;
                    }

                    ViewModel.Instance.SaveChanges();
                }
            };

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
