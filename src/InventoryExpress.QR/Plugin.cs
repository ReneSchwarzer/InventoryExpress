using WebExpress.WebAttribute;
using WebExpress.WebPlugin;

namespace InventoryExpress.QR
{
    [Name("plugin.name")]
    [Description("plugin.description")]
    [Icon("/assets/img/qr.svg")]
    [Dependency("InventoryExpress")]
    public sealed class Plugin : IPlugin
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Plugin()
        {
        }

        /// <summary>
        /// Initialization des Plugins. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Initialization(IPluginContext context)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Plugin mit der Arbeit beginnt. Der Aufruf von Run erfolgt nebenläufig.
        /// </summary>
        public void Run()
        {
        }

        /// <summary>
        /// Freigeben von nicht verwalteten Ressourcen, welche wärend der Verwendung reserviert wurden.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
