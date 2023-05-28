﻿using WebExpress.WebAttribute;
using WebExpress.WebModule;

namespace InventoryExpress.QR
{
    [WebExId("InventoryExpress.QR")]
    [WebExName("module.name")]
    [WebExDescription("module.description")]
    [WebExIcon("/assets/img/Logo.png")]
    [WebExAssetPath("/")]
    [WebExContextPath("/qr")]
    [WebExApplication("InventoryExpress")]
    public sealed class Module : IModule
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Module()
        {
        }

        /// <summary>
        /// Initialization des Moduls. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Initialization(IModuleContext context)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Modul mit der Arbeit beginnt. Der Aufruf erfolgt nebenläufig.
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