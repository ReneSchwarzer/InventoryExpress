using System;
using WebExpress;
using WebExpress.Plugins;

namespace InventoryExpress
{
    public class InventoryExpressFactory : PluginFactory
    {
        /// <summary>
        /// Liefert den Dateinamen der Konfigurationsdatei
        /// </summary>
        public override string ConfigFileName => "inventoryexpress.config.xml";

        /// <summary>
        /// Erstellt eine neue Instanz eines Prozesszustandes
        /// </summary>
        /// <param name="context">Der Benutzer</param>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        /// <returns>Die Instanz des Prozesszustandes</returns>
        public override IPlugin Create(HttpServerContext context, string configFileName)
        {
            var plugin = Create<InventoryExpressPlugin>(context, configFileName);

            return plugin;
        }
    }
}
