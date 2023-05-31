using WebExpress.WebAttribute;
using WebExpress.WebPlugin;

namespace InventoryExpress
{
    [WebExName("InventoryExpress")]
    [WebExDescription("plugin.description")]
    [WebExIcon("/assets/img/Logo.png")]
    [WebExDependency("webexpress.ui")]
    [WebExDependency("webexpress.webapp")]
    public sealed class Plugin : IPlugin
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Plugin()
        {
        }

        /// <summary>
        /// Initialization of the plugin. Here, for example, managed resources can be loaded. 
        /// </summary>
        /// <param name="context">The context that applies to the execution of the plugin.</param>
        public void Initialization(IPluginContext context)
        {
        }

        /// <summary>
        /// Invoked when the plugin starts working. The call to Run is concurrent.
        /// </summary>
        public void Run()
        {
        }

        /// <summary>
        /// Release unmanaged resources that have been reserved during use.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
