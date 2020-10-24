using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebExpress.Plugins;

namespace InventoryExpress.Model
{
    public class ViewModel
    {
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
        /// Liefert die aktuelle Zeit
        /// </summary>
        public string Now => DateTime.Now.ToString("dd.MM.yyyy<br>HH:mm:ss");

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public IPluginContext Context { get; set; }

        /// <summary>
        /// Liefert oder setzt die Inventarelemente
        /// </summary>
        public List<Inventory> Inventorys { get; set; }

        /// <summary>
        /// Liefert oder setzt die Vorlagen
        /// </summary>
        public List<Template> Templates { get; set; }

        /// <summary>
        /// Liefert oder setzt die Attribute
        /// </summary>
        public List<Attribute> Attributes { get; set; }

        /// <summary>
        /// Liefert oder setzt die Standorte
        /// </summary>
        public List<Location> Locations { get; set; }

        /// <summary>
        /// Liefert oder setzt die Lieferanten
        /// </summary>
        public List<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Liefert oder setzt die Hersteller
        /// </summary>
        public List<Manufacturer> Manufacturers { get; set; }

        /// <summary>
        /// Liefert oder setzt die Kostenstellen
        /// </summary>
        public List<CostCenter> CostCenters { get; set; }

        /// <summary>
        /// Liefert oder setzt die Zustände
        /// </summary>
        public List<State> States { get; set; }

        /// <summary>
        /// Liefert oder setzt die Sachkonten
        /// </summary>
        public List<GLAccount> GLAccounts { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        private ViewModel()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public void Init()
        {
            Inventorys = new List<Inventory>();
            Templates = new List<Template>();
            Attributes = new List<Attribute>();
            Locations = new List<Location>();
            Suppliers = new List<Supplier>();
            Manufacturers = new List<Manufacturer>();
            CostCenters = new List<CostCenter>();
            States = new List<State>();
            GLAccounts = new List<GLAccount>();

            if (Directory.Exists(Path.Combine(Context.AssetBaseFolder, "data")))
            {
                Load();
            }
            else
            {
                // Datenverzeichnis erstellen
                Directory.CreateDirectory(Path.Combine(Context.AssetBaseFolder, "data"));
            }
        }

        /// <summary>
        /// Lade die Daten
        /// </summary>
        public void Load()
        {
            var files = Directory.GetFiles(Path.Combine(Context.AssetBaseFolder, "data"));


            // 2. Sachkonten laden
            foreach (var file in from x in files
                                 where Path.GetExtension(x).Equals(".glaccount")
                                 select x)
            {
                GLAccounts.Add(GLAccount.Factory(file));
            }

            // 4. Lieferanten laden
            foreach (var file in from x in files
                                 where Path.GetExtension(x).Equals(".supplier")
                                 select x)
            {
                Suppliers.Add(Supplier.Factory(file));
            }


            // 6. Kostenstellen laden
            foreach (var file in from x in files
                                 where Path.GetExtension(x).Equals(".costcenter")
                                 select x)
            {
                CostCenters.Add(CostCenter.Factory(file));
            }

            // 7. Attribute laden
            foreach (var file in from x in files
                                 where Path.GetExtension(x).Equals(".attribute")
                                 select x)
            {
                Attributes.Add(Attribute.Factory(file));
            }

            // 8. Vorlagen laden
            foreach (var file in from x in files
                                 where Path.GetExtension(x).Equals(".template")
                                 select x)
            {
                Templates.Add(Template.Factory(file));
            }

            // 9. Inventar laden
            foreach (var file in from x in files
                                 where Path.GetExtension(x).Equals(".inventory")
                                 select x)
            {
                Inventorys.Add(Inventory.Factory(file));
            }

            Inventorys = Inventorys.OrderBy(x => x.Name).ToList();
            Templates = Templates.OrderBy(x => x.Name).ToList();
            Attributes = Attributes.OrderBy(x => x.Name).ToList();
            Suppliers = Suppliers.OrderBy(x => x.Name).ToList();
            CostCenters = CostCenters.OrderBy(x => x.Name).ToList();
            GLAccounts = GLAccounts.OrderBy(x => x.Name).ToList();
        }
    }
}