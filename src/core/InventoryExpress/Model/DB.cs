using Microsoft.EntityFrameworkCore;

namespace InventoryExpress.Model
{
    public class DB : DbContext
    {
        /// <summary>
        /// Liefert oder setzt die Datenquelle
        /// </summary>
        public string DataSource { get; internal set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        internal DB()
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn die DB configuriert werden soll
        /// </summary>
        /// <param name="optionsBuilder">Der OptionsBuilder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={ DataSource };");
        }

        /// <summary>
        /// Modell erstellen
        /// </summary>
        /// <param name="modelBuilder">Der Modellbuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DB).Assembly);
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