﻿using WebExpress.WebAttribute;
using WebExpress.WebPlugin;

namespace InventoryExpress.QR
{
    [WebExID("InventoryExpress.QR")]
    [WebExName("plugin.name")]
    [WebExDescription("plugin.description")]
    [WebExIcon("/assets/img/qr.svg")]
    [WebExDependency("InventoryExpress")]
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
