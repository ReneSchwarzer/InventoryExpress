using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;

namespace InventoryExpress.Model
{
    public class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Tritt ein, wenn die Daten geladen wurden
        /// </summary>
        public event EventHandler Loaded;

        /// <summary>
        /// Bestimmt ob das Viewmodell vollständig inizalisiert ist
        /// </summary>
        private Visibility _visibilityProgresBar = Visibility.Collapsed;
        public Visibility VisibilityProgresBar
        {
            get
            {
                return _visibilityProgresBar;
            }
            set
            {
                if (_visibilityProgresBar != value)
                {
                    _visibilityProgresBar = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VisibilityProgresBar"));
                }
            }
        } 

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Instanz des einzigen Modells
        /// </summary>
        private static ViewModel m_this = null;

        /// <summary>
        /// Konstruktor
        /// </summary>
        private ViewModel()
        {
            var init = InitAsync();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
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

            await Load();
            Loaded?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Beispieldaten bereitstellen
        /// </summary>
        /// <returns></returns>
        public async Task CreateSample()
        {
            var appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            // 1. Standorte kopieren
            var assets = await appInstalledFolder.GetFolderAsync("Assets\\locations");
            foreach (var file in from x in await assets.GetFilesAsync() select x)
            {
                try
                {
                    var item = await Model.Location.Factory(file);
                    var newName = item.ID + ".location";

                    // Kopieren
                    await file.CopyAsync
                    (
                        ApplicationData.Current.LocalFolder,
                        newName,
                        NameCollisionOption.ReplaceExisting
                    );
                }
                catch
                {

                }
            }

            // 2. Lieferanten kopieren
            assets = await appInstalledFolder.GetFolderAsync("Assets\\suppliers");
            foreach (var file in from x in await assets.GetFilesAsync() select x)
            {
                try
                {
                    var item = await Model.Supplier.Factory(file);
                    var newName = item.ID + ".supplier";

                    // Kopieren
                    await file.CopyAsync
                    (
                        ApplicationData.Current.LocalFolder,
                        newName,
                        NameCollisionOption.ReplaceExisting
                    );
                }
                catch
                {

                }
            }

            // 3. Hersteller kopieren
            assets = await appInstalledFolder.GetFolderAsync("Assets\\manufacturers");
            foreach (var file in from x in await assets.GetFilesAsync() select x)
            {
                try
                {
                    var item = await Model.Manufacturer.Factory(file);
                    var newName = item.ID + ".manufacturer";

                    // Kopieren
                    await file.CopyAsync
                    (
                        ApplicationData.Current.LocalFolder,
                        newName,
                        NameCollisionOption.ReplaceExisting
                    );
                }
                catch
                {

                }
            }

            // 4. Kostenstellen kopieren
            assets = await appInstalledFolder.GetFolderAsync("Assets\\costcenters");
            foreach (var file in from x in await assets.GetFilesAsync() select x)
            {
                try
                {
                    var item = await Model.CostCenter.Factory(file);
                    var newName = item.ID + ".costcenter";

                    // Kopieren
                    await file.CopyAsync
                    (
                        ApplicationData.Current.LocalFolder,
                        newName,
                        NameCollisionOption.ReplaceExisting
                    );
                }
                catch
                {

                }
            }

            // 5. Attribute kopieren
            assets = await appInstalledFolder.GetFolderAsync("Assets\\attributes");
            foreach (var file in from x in await assets.GetFilesAsync() select x)
            {
                try
                {
                    var item = await Model.Attribute.Factory(file);
                    var newName = item.ID + ".attribute";

                    // Kopieren
                    await file.CopyAsync
                    (
                        ApplicationData.Current.LocalFolder,
                        newName,
                        NameCollisionOption.ReplaceExisting
                    );
                }
                catch
                {

                }
            }

            // 6. Vorlagen kopieren
            assets = await appInstalledFolder.GetFolderAsync("Assets\\templates");
            foreach (var file in from x in await assets.GetFilesAsync() select x)
            {
                try
                {
                    var item = await Model.Template.Factory(file);
                    var newName = item.ID + ".template";

                    // Kopieren
                    await file.CopyAsync
                    (
                        ApplicationData.Current.LocalFolder,
                        newName,
                        NameCollisionOption.FailIfExists
                    );
                }
                catch
                {

                }
            }

            // 7. Inventar kopieren
            assets = await appInstalledFolder.GetFolderAsync("Assets\\inventorys");
            foreach (var file in from x in await assets.GetFilesAsync() select x)
            {
                try
                {
                    var item = await Model.Inventory.Factory(file);
                    var newName = item.ID + ".inventory";

                    // Kopieren
                    await file.CopyAsync
                    (
                        ApplicationData.Current.LocalFolder,
                        newName,
                        NameCollisionOption.FailIfExists
                    );
                }
                catch
                {

                }
            }

            // 8. Zustände kopieren
            assets = await appInstalledFolder.GetFolderAsync("Assets\\states");
            foreach (var file in from x in await assets.GetFilesAsync() select x)
            {
                try
                {
                    var item = await Model.State.Factory(file);
                    var newName = item.ID + ".state";

                    // Kopieren
                    await file.CopyAsync
                    (
                        ApplicationData.Current.LocalFolder,
                        newName,
                        NameCollisionOption.FailIfExists
                    );
                }
                catch
                {

                }
            }

            // 9. Sachkonten kopieren
            assets = await appInstalledFolder.GetFolderAsync("Assets\\glaccounts");
            foreach (var file in from x in await assets.GetFilesAsync() select x)
            {
                try
                {
                    var item = await Model.GLAccount.Factory(file);
                    var newName = item.ID + ".glaccount";

                    // Kopieren
                    await file.CopyAsync
                    (
                        ApplicationData.Current.LocalFolder,
                        newName,
                        NameCollisionOption.FailIfExists
                    );
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Lade die Konten
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            VisibilityProgresBar = Visibility.Visible;

            // Leereinträge zum Entfernen
            States.Add(new State());
            GLAccounts.Add(new GLAccount());
            Locations.Add(new Location());
            Suppliers.Add(new Supplier());
            Manufacturers.Add(new Manufacturer());
            CostCenters.Add(new CostCenter());
            Templates.Add(new Template());

            var files = await ApplicationData.Current.LocalFolder.GetFilesAsync();

            // 1. Zustände laden
            foreach (var file in from x in files
                                 where x.FileType.Equals(".state")
                                 select x)
            {
                States.Add(await State.Factory(file));
            }

            States.ForEach(a => a.Commit(false));

            // 2. Sachkonten laden
            foreach (var file in from x in files
                                 where x.FileType.Equals(".glaccount")
                                 select x)
            {
                GLAccounts.Add(await GLAccount.Factory(file));
            }

            GLAccounts.ForEach(a => a.Commit(false));

            // 3. Standorte laden
            foreach (var file in from x in files
                                 where x.FileType.Equals(".location")
                                 select x)
            {
                Locations.Add(await Location.Factory(file));
            }

            Locations.ForEach(a => a.Commit(false));

            // 4. Lieferanten laden
            foreach (var file in from x in files
                                 where x.FileType.Equals(".supplier")
                                 select x)
            {
                Suppliers.Add(await Supplier.Factory(file));
            }

            Suppliers.ForEach(a => a.Commit(false));

            // 5. Hersteller laden
            foreach (var file in from x in files
                                 where x.FileType.Equals(".manufacturer")
                                 select x)
            {
                Manufacturers.Add(await Manufacturer.Factory(file));
            }

            Manufacturers.ForEach(a => a.Commit(false));

            // 6. Kostenstellen laden
            foreach (var file in from x in files
                                 where x.FileType.Equals(".costcenter")
                                 select x)
            {
                CostCenters.Add(await CostCenter.Factory(file));
            }

            CostCenters.ForEach(a => a.Commit(false));

            // 7. Attribute laden
            foreach (var file in from x in files
                                 where x.FileType.Equals(".attribute")
                                 select x)
            {
                Attributes.Add(await Attribute.Factory(file));
            }

            Attributes.ForEach(a => a.Commit(false));

            // 8. Vorlagen laden
            foreach (var file in from x in files
                                 where x.FileType.Equals(".template")
                                 select x)
            {
                Templates.Add(await Template.Factory(file));
            }

            Templates.ForEach(a => a.Commit(false));

            // 9. Inventar laden
            foreach (var file in from x in files
                                 where x.FileType.Equals(".inventory")
                                 select x)
            {
                try
                {
                    if (file != null)
                    {
                        Inventorys.Add(await Inventory.Factory(file));
                    }
                }
                catch
                {

                }
            }

            Inventorys.ForEach(a => 
            {
                a.Commit(false);
            });

            Inventorys = Inventorys.OrderBy(x => x.Name).ToList();
            Templates = Templates.OrderBy(x => x.Name).ToList();
            Attributes = Attributes.OrderBy(x => x.Name).ToList();
            Locations = Locations.OrderBy(x => x.Name).ToList();
            Suppliers = Suppliers.OrderBy(x => x.Name).ToList();
            Manufacturers = Manufacturers.OrderBy(x => x.Name).ToList();
            CostCenters = CostCenters.OrderBy(x => x.Name).ToList();
            States = States.OrderBy(x => x.Name).ToList();
            GLAccounts = GLAccounts.OrderBy(x => x.Name).ToList();

            Inventorys.ForEach(a => { ToImage(a); });
            Templates.ForEach(a => { ToImage(a); });
            Attributes.ForEach(a => { ToImage(a); });
            Locations.ForEach(a => { ToImage(a); });
            Suppliers.ForEach(a => { ToImage(a); });
            Manufacturers.ForEach(a => { ToImage(a); });
            CostCenters.ForEach(a => { ToImage(a); });
            States.ForEach(a => { ToImage(a); });
            GLAccounts.ForEach(a => { ToImage(a); });

            VisibilityProgresBar = Visibility.Collapsed;
        }

        /// <summary>
        /// Konvertiert und speichert das Bild
        /// </summary>
        /// <param name="item">Das Item</param>
        private async void ToImage(Item item)
        {
            if (!string.IsNullOrWhiteSpace(item.ImageBase64) && string.IsNullOrWhiteSpace(item.Image))
            {
                var fileName = Guid.NewGuid() + ".png";
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync
                    (
                        fileName,
                        CreationCollisionOption.ReplaceExisting
                    );

                using (var data = await file.OpenStreamForWriteAsync())
                {
                    var bytes = Convert.FromBase64String(item.ImageBase64);

                    data.Write(bytes, 0, bytes.Count());
                }

                item.Image = fileName;
                item.Commit(true);
            }
        }

        /// <summary>
        /// Lifert die einzige Instanz der Modell-Klasse
        /// </summary>
        public static ViewModel Instance
        {
            get
            {
                if (m_this == null)
                {
                    m_this = new ViewModel();
                }

                return m_this;
            }
        }

        /// <summary>
        /// Liefert oder setzt die Inventarelemente
        /// </summary>
        private List<Inventory> inventorys = new List<Inventory>();
        public List<Inventory> Inventorys
        {
            get
            {
                return inventorys;
            }
            set
            {
                if (inventorys != value)
                {
                    inventorys = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Inventorys"));
                }
            }
        }

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
        /// Liefert oder setzt die gefilterten Inventareinträge
        /// </summary>
        public IEnumerable<Inventory> FilteredInventorys
        {
            get
            {
                return from x in Inventorys
                       where !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(SearchText.ToLower())
                       orderby x.Name
                       select x;
            }
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der gefilterten Inventareinträge
        /// </summary>
        public int FilteredInventorysCount
        {
            get
            {
                return FilteredInventorys.Count();
            }
        }

        /// <summary>
        /// Liefert oder setzt die Anschaffungskosten der gefilterten Inventareinträge
        /// </summary>
        public decimal FilteredInventorysCostValue
        {
            get
            {
                return FilteredInventorys.Sum(x => x.CostValue);
            }
        }

        /// <summary>
        /// Liefert oder setzt die gefilterten Hersteller
        /// </summary>
        public IEnumerable<Manufacturer> FilteredManufacturers
        {
            get
            {
                return from x in Manufacturers
                       where !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(SearchText.ToLower())
                       orderby x.Name
                       select x;
            }
        }

        /// <summary>
        /// Liefert oder setzt die gefilterten Standorte
        /// </summary>
        public IEnumerable<Location> FilteredLocations
        {
            get
            {
                return from x in Locations
                       where !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(SearchText.ToLower())
                       orderby x.Name
                       select x;
            }
        }

        /// <summary>
        /// Liefert oder setzt die gefilterten Lieferanten
        /// </summary>
        public IEnumerable<Supplier> FilteredSuppliers
        {
            get
            {
                return from x in Suppliers
                       where !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(SearchText.ToLower())
                       orderby x.Name
                       select x;
            }
        }

        /// <summary>
        /// Liefert oder setzt die gefilterten Vorlagen
        /// </summary>
        public IEnumerable<Template> FilteredTemplates
        {
            get
            {
                return from x in Templates
                       where !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(SearchText.ToLower())
                       orderby x.Name
                       select x;
            }
        }

        /// <summary>
        /// Liefert oder setzt die gefilterten Kostenstellen
        /// </summary>
        public IEnumerable<CostCenter> FilteredCostCenters
        {
            get
            {
                return from x in CostCenters
                       where !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(SearchText.ToLower())
                       orderby x.Name
                       select x;
            }
        }

        /// <summary>
        /// Liefert oder setzt die gefilterten Attribute
        /// </summary>
        public IEnumerable<Attribute> FilteredAttributes
        {
            get
            {
                return from x in Attributes
                       where !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(SearchText.ToLower())
                       orderby x.Name
                       select x;
            }
        }

        /// <summary>
        /// Liefert oder setzt die gefilterten Sachkonten
        /// </summary>
        public IEnumerable<GLAccount> FilteredGLAccounts
        {
            get
            {
                return from x in GLAccounts
                       where !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(SearchText.ToLower())
                       orderby x.Name
                       select x;
            }
        }

        /// <summary>
        /// Liefert oder setzt die gefilterten Zustände
        /// </summary>
        public IEnumerable<State> FilteredStates
        {
            get
            {
                return from x in States
                       where !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(SearchText.ToLower())
                       orderby x.Name
                       select x;
            }
        }

        /// <summary>
        /// Liefert oder setzt den Suchtext
        /// </summary>
        private string searchText = string.Empty;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SearchText"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredInventorys"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredInventorysCount"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredManufacturers"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredSuppliers"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredLocations"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredCostCenters"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredTemplates"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredAttributes"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredGLAccounts"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredStates"));
                }
            }
        }

        /// <summary>
        /// Export der Daten
        /// </summary>
        /// <param name="fileName">Das Archive</param>
        /// <returns></returns>
        public async Task ExportAsync(StorageFile file)
        {
            // ZipFile.CreateFromDirectory(ApplicationData.Current.LocalFolder.Path, file.Path, CompressionLevel.Optimal, false);

            using (var stream = await file.OpenStreamForWriteAsync())
            { 
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create))
                {
                    foreach (var f in await ApplicationData.Current.LocalFolder.GetFilesAsync())
                    {
                        var buffer = await FileIO.ReadBufferAsync(f);
                        var entry = archive.CreateEntry(f.Name, CompressionLevel.Optimal);
                        using (var entryStream = entry.Open())
                        {
                            using (var outputputStream = entryStream.AsOutputStream())
                            {
                                var b = WindowsRuntimeBufferExtensions.ToArray(buffer);
                                await entryStream.WriteAsync(b, 0, b.Length);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Import der Daten
        /// </summary>
        /// <param name="file">Das Archive</param>
        /// <returns></returns>
        public async Task ImportAsync(StorageFile file)
        {
            //ZipFile.ExtractToDirectory(file.Path, localFolder.Path);

            using (var stream = await file.OpenStreamForReadAsync())
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    foreach (var entry in archive.Entries)
                    {
                        using (Stream entryStream = entry.Open())
                        {
                            byte[] buffer = new byte[entry.Length];
                            entryStream.Read(buffer, 0, buffer.Length);

                            StorageFile uncompressedFile = await ApplicationData.Current.LocalFolder.CreateFileAsync
                            (
                                entry.Name, 
                                CreationCollisionOption.ReplaceExisting
                            );

                            using (IRandomAccessStream uncompressedFileStream = await uncompressedFile.OpenAsync(FileAccessMode.ReadWrite))
                            {
                                using (Stream outstream = uncompressedFileStream.AsStreamForWrite())
                                {
                                    outstream.Write(buffer, 0, buffer.Length);
                                    outstream.Flush();
                                }
                            }
                        }

                    }
                }
            }
        }
    }
}
