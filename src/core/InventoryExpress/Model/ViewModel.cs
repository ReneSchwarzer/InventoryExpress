using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;
using WebExpress.WebModule;

namespace InventoryExpress.Model
{
    public partial class ViewModel : DB
    {
        /// <summary>
        /// Instanz des einzigen Modells
        /// </summary>
        public static string RootUri { get; private set; }

        /// <summary>
        /// Ermittelt die Uri des Anwendungsicons 
        /// </summary>
        public static string ApplicationIcon { get; private set; }

        /// <summary>
        /// Instanz des einzigen Modells
        /// </summary>
        private static ViewModel _this = null;

        /// <summary>
        /// Lifert die einzige Instanz der Modell-Klasse
        /// </summary>
        public static ViewModel Instance
        {
            get
            {
                if (_this == null)
                {
                    _this = new ViewModel();
                }

                return _this;
            }
        }

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public static IModuleContext Context { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        private ViewModel()
        {
            DataSource = "Assets/db/inventory.db";
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Initialization(IModuleContext context)
        {
            Context = context;
            ApplicationIcon = Context.Application.Icon.ToString();
            RootUri = context.ContextPath.ToString();

            Database.EnsureCreated();

            Database.Migrate();
        }


        /// <summary>
        /// Export der Daten
        /// </summary>
        /// <param name="fileName">Das Archive</param>
        /// <param name="dataPath">Das Verzeichnis, indem die Anhänge gespeichert werden</param>
        /// <param name="progress">Der Fortschritt</param>
        public void Export(string fileName, string dataPath, Action<int> progress)
        {
            ImportExport.Export(fileName, dataPath, progress);
        }

        /// <summary>
        /// Import der Daten
        /// </summary>
        /// <param name="fileName">Das Archive</param>
        /// <param name="dataPath">Das Verzeichnis, indem die Anhänge gespeichert werden</param>
        /// <param name="progress">Der Fortschritt</param>
        public void Import(string fileName, string dataPath, Action<int> progress)
        {
            lock (Database)
            {
                Database.BeginTransaction();

                Conditions.RemoveRange(Conditions);
                Locations.RemoveRange(Locations);
                Manufacturers.RemoveRange(Manufacturers);
                Suppliers.RemoveRange(Suppliers);
                LedgerAccounts.RemoveRange(LedgerAccounts);
                CostCenters.RemoveRange(CostCenters);
                Inventories.RemoveRange(Inventories);
                InventoryAttributes.RemoveRange(InventoryAttributes);
                InventoryAttachments.RemoveRange(InventoryAttachments);
                InventoryComments.RemoveRange(InventoryComments);
                InventoryJournals.RemoveRange(InventoryJournals);
                InventoryJournalParameters.RemoveRange(InventoryJournalParameters);
                InventoryTags.RemoveRange(InventoryTags);
                Templates.RemoveRange(Templates);
                TemplateAttributes.RemoveRange(TemplateAttributes);
                Attributes.RemoveRange(Attributes);
                Media.RemoveRange(Media);
                Tags.RemoveRange(Tags);

                Database.CommitTransaction();

                //Database.ExecuteSqlCommand("vacum;");

                SaveChanges();
            }

            ImportExport.Import(fileName, dataPath, progress);
        }
    }
}