using InventoryExpress.Model;
using WebExpress.WebAttribute;
using WebExpress.WebModule;

namespace InventoryExpress
{
    [WebExName("module.name")]
    [WebExDescription("module.description")]
    [WebExIcon("/assets/img/Logo.png")]
    [WebExAssetPath("")]
    [WebExContextPath("/")]
    [WebExApplication(typeof(Application))]
    public sealed class Module : IModule
    {
        /// <summary>
        /// The context of the module.
        /// </summary>
        private IModuleContext ModuleContext { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Module()
        {
        }

        /// <summary>
        /// Initialization of the module. Here, for example, managed resources can be loaded. 
        /// </summary>
        /// <param name="moduleContext">The context of the module that applies to the execution.</param>
        public void Initialization(IModuleContext moduleContext)
        {
            ModuleContext = moduleContext;

            var applicationContext = moduleContext.ApplicationContext;

            ViewModel.Initialization(applicationContext, ModuleContext);
        }

        /// <summary>
        /// Called when the module starts working. The call is concurrent.
        /// </summary>
        public void Run()
        {
            //ViewModel.Instance.Import(Path.Combine(@"C:\Users\rene_\OneDrive\Bilder\Desktop\Sandbox\inventory.zip"), Context.Application.AssetPath, i => { });
            //ViewModel.Instance.Export(Path.Combine(@"C:\Users\rene_\OneDrive\Bilder\Desktop\Sandbox\_inventory.zip"), Context.Application.AssetPath, i => { });
        }

        /// <summary>
        /// Release unmanaged resources that have been reserved during use.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
