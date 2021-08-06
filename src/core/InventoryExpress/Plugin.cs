using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Plugin;

namespace InventoryExpress
{
    [ID("InventoryExpress")]
    [Name("plugin.name")]
    [Description("plugin.description")]
    [Icon("/assets/img/Logo.png")]
    public sealed class Plugin : IPlugin
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Plugin()
        {
        }

        /// <summary>
        /// Initialisierung des Plugins. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Initialization(IPluginContext context)
        {
            //ViewModel.Instance.Context = Context;
            ViewModel.Instance.Init();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Plugin mit der Arbeit beginnt. Der Aufruf von Run erfolgt nebenläufig.
        /// </summary>
        public void Run()
        {
            // <<<<<<<<<<<<<<<<<<<<<<<<<<< Nach dem Testen bitte löschen!!! 

            // >>>>>>>>>>>>>>>>>>>>>>>>>>>
        }

        /// <summary>
        /// Freigeben von nicht verwalteten Ressourcen, welche wärend der Verwendung reserviert wurden.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
