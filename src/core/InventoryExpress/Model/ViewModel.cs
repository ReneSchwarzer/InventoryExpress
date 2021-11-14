using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using WebExpress.Plugin;

namespace InventoryExpress.Model
{
    public class ViewModel : DB
    {
        /// <summary>
        /// Liefert oder setzt die Inventargegenstände
        /// </summary>
        public DbSet<Inventory> Inventories { get; set; }

        /// <summary>
        /// Liefert oder setzt die Attribute der Inventargegenstände
        /// </summary>
        public DbSet<InventoryAttribute> InventoryAttributes { get; set; }

        /// <summary>
        /// Liefert oder setzt die Datei-Anhänge der Inventargegenstände
        /// </summary>
        public DbSet<InventoryAttachment> InventoryAttachment { get; set; }

        /// <summary>
        /// Liefert oder setzt die Kommentare der Inventargegenstände
        /// </summary>
        public DbSet<InventoryComment> InventoryComments { get; set; }

        /// <summary>
        /// Liefert oder setzt das Journal der Inventargegenstände
        /// </summary>
        public DbSet<InventoryJournal> InventoryJournals { get; set; }

        /// <summary>
        /// Liefert oder setzt die Journal-Parameter der Inventargegenstände
        /// </summary>
        public DbSet<InventoryJournalParameter> InventoryJournalParameters { get; set; }

        /// <summary>
        /// Liefert oder setzt die Schlagwörter der Inventargegenstände
        /// </summary>
        public DbSet<InventoryTag> InventoryTag { get; set; }

        /// <summary>
        /// Liefert oder setzt die Zustände
        /// </summary>
        public DbSet<Condition> Conditions { get; set; }

        /// <summary>
        /// Liefert oder setzt die Standorte
        /// </summary>
        public DbSet<Location> Locations { get; set; }

        /// <summary>
        /// Liefert oder setzt die Hersteller
        /// </summary>
        public DbSet<Manufacturer> Manufacturers { get; set; }

        /// <summary>
        /// Liefert oder setzt die Lieferanten
        /// </summary>
        public DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Liefert oder setzt die Sachkonten
        /// </summary>
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }

        /// <summary>
        /// Liefert oder setzt die Kostenstellen
        /// </summary>
        public DbSet<CostCenter> CostCenters { get; set; }

        /// <summary>
        /// Liefert oder setzt die Vorlagen
        /// </summary>
        public DbSet<Template> Templates { get; set; }

        /// <summary>
        /// Liefert oder setzt die Vorlagenattribute
        /// </summary>
        public DbSet<TemplateAttribute> TemplateAttributes { get; set; }

        /// <summary>
        /// Liefert oder setzt die Attribute
        /// </summary>
        public DbSet<Entity.Attribute> Attributes { get; set; }

        /// <summary>
        /// Liefert oder setzt die Medien
        /// </summary>
        public DbSet<Media> Media { get; set; }

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Liefert oder setzt die Medien
        /// </summary>
        public DbSet<Setting> Settings { get; set; }

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
        public IPluginContext Context { get; set; }

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
        public void Initialization(IPluginContext context)
        {
            //Database.EnsureCreated();

            //Database.Migrate();
        }


        /// <summary>
        /// Export der Daten
        /// </summary>
        /// <param name="fileName">Das Archive</param>
        /// <returns></returns>
        public void Export(string fileName)
        {
            foreach(var v in CostCenters)
            {
                var xml = new XElement("costcenter");
                xml.Add(new XAttribute("version", 1));
                // Item
                xml.Add(new XElement("guid", v.Guid));
                xml.Add(new XElement("name", v.Name));
                xml.Add(new XElement("description", v.Description));
                xml.Add(new XElement("created", v.Created));
                xml.Add(new XElement("updated", v.Updated));
                xml.Add(new XElement("media", v.Media?.Guid));
                // ItemTag
                xml.Add(new XElement("tag", v.Tag));
            }


            //xml.Add(new XElement("image", new XCData(ImageBase64)));


            using var stream = File.OpenRead(fileName);
            using var archive = new ZipArchive(stream, ZipArchiveMode.Create);

            //foreach (var f in await ApplicationData.Current.LocalFolder.GetFilesAsync())
            //{
            //    var buffer = await FileIO.ReadBufferAsync(f);
            //    var entry = archive.CreateEntry(f.Name, CompressionLevel.Optimal);
            //    using (var entryStream = entry.Open())
            //    {
            //        using (var outputputStream = entryStream.AsOutputStream())
            //        {
            //            var b = WindowsRuntimeBufferExtensions.ToArray(buffer);
            //            await entryStream.WriteAsync(b, 0, b.Length);
            //        }
            //    }
            //}
        }
    }
}