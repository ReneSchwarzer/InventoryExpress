using System.Reflection;
using WebExpress;
using WebExpress.Plugins;

namespace InventoryExpress
{
    public class InventoryExpressFactory : PluginFactory
    {
        /// <summary>
        /// Liefert den Anwendungsnamen indem das Plugin aktiv ist. 
        /// </summary>
        public override string AppArtifactID => "org.WebExpress.Inventory";

        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        public override string ArtifactID => "Inventory";

        /// <summary>
        /// Liefert oder setzt die HerstellerID
        /// </summary>
        public override string ManufacturerID => "org.WebExpress";

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public override string Description => "Inventardatenbank";

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public override string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Liefert das Icon des Plugins
        /// </summary>
        public override string Icon => "Assets/img/Logo.png";

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
