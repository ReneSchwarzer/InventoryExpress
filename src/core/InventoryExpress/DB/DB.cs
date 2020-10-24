using InventoryExpress.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryExpress.DB
{
    public class DB : DbContext
    {
        /// <summary>
        /// Instanz des DbContext
        /// </summary>
        private static DB _this = null;

        /// <summary>
        /// Liefert oder setzt die Zustände
        /// </summary>
        public DbSet<State> States { get; set; }

        /// <summary>
        /// Liefert oder setzt die Standorte
        /// </summary>
        public DbSet<Location> Locations { get; set; }

        /// <summary>
        /// Liefert oder setzt die Hersteller
        /// </summary>
        public DbSet<Manufacturer> Manufacturers { get; set; }

        /// <summary>
        /// Lifert die einzige Instanz der DB-Klasse
        /// </summary>
        public static DB Instance
        {
            get
            {
                if (_this == null)
                {
                    _this = new DB();
                }

                return _this;
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        private DB()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Assets/db/inventory.db;");
        }

        /// <summary>
        /// Alles aktualliesieren
        /// </summary>
        public void RefreshAll()
        {
            foreach (var entity in ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        /// <summary>
        /// Verwirft die Änderungen, die seit dem letzten Speichern durchgeführt worden
        /// </summary>
        public void Rollback()
        {
            if (ChangeTracker.HasChanges())
            {
                RefreshAll();
            }
        }
    }
}