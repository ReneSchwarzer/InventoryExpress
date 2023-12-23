using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System.Linq;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Liefert alle Einstellungen
        /// </summary>
        /// <returns>Eine Aufzählung, welche die Einstellungen beinhaltet</returns>
        public static WebItemEntitySettings GetSettings()
        {
            lock (DbContext)
            {
                var settings = DbContext.Settings.Select(x => new WebItemEntitySettings(x)).FirstOrDefault();

                return settings;
            }
        }

        /// <summary>
        /// Fügt Einstellungen hinzu oder aktuallisiert diese
        /// </summary>
        /// <param name="settings">Die Einstellungen</param>
        public static void AddOrUpdateSettings(WebItemEntitySettings settings)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Settings.FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Setting()
                    {
                        Currency = settings.Currency
                    };

                    DbContext.Settings.Add(entity);
                    DbContext.SaveChanges();
                }
                else
                {
                    // Update
                    availableEntity.Currency = settings.Currency;
                    DbContext.SaveChanges();
                }
            }
        }
    }
}