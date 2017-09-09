using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace InventoryExpress.Model
{
    public class Inventory : Item
    {
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        private Template template;
        private Template _template;
        private List<AttributeTextValue> attributes;
        private List<AttributeTextValue> _attributes;
        private List<Ascription> ascriptions;
        private List<Ascription> _ascriptions;

        private bool like;
        private bool _like;

        /// <summary>
        /// Anschaffungssdatum
        /// </summary>
        private DateTimeOffset? purchaseDate;
        private DateTimeOffset? _purchaseDate;

        /// <summary>
        /// Abgangsdatum
        /// </summary>
        private DateTimeOffset? derecognitionDate;
        private DateTimeOffset? _derecognitionDate;

        /// <summary>
        /// Der Standort
        /// </summary>
        private Location location;
        private Location _location;

        /// <summary>
        /// Die Kostenstelle
        /// </summary>
        private CostCenter costCenter;
        private CostCenter _costCenter;

        /// <summary>
        /// Der Hersteller
        /// </summary>
        private Manufacturer manufacturer;
        private Manufacturer _manufacturer;

        /// <summary>
        /// Der Zustand
        /// </summary>
        private State state;
        private State _state;

        /// <summary>
        /// Der Lieferant
        /// </summary>
        private Supplier supplier;
        private Supplier _supplier;

        /// <summary>
        /// Das Sachkonto
        /// </summary>
        private GLAccount glaccount;
        private GLAccount _glaccount;

        /// <summary>
        /// Der Zugehörig zu ID
        /// </summary>
        private string parent;
        private string _parent;

        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        private decimal costvalue;
        private decimal _costvalue;

        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        public decimal CostValue
        {
            get
            {
                return costvalue;
            }
            set
            {
                if (costvalue != value)
                {
                    costvalue = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Das Template
        /// </summary>
        public Template Template
        {
            get
            {
                return template;
            }
            set
            {
                if (template != value)
                {
                    template = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Die Attribute des Kontos
        /// </summary>
        public List<AttributeTextValue> Attributes
        {
            get
            {
                return attributes;
            }
            set
            {
                if (attributes != value)
                {
                    attributes = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Die Zuschreibungen
        /// </summary>
        public List<Ascription> Ascriptions
        {
            get
            {
                return ascriptions;
            }
            set
            {
                if (ascriptions != value)
                {
                    ascriptions = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Das Konto ist ein Favorit
        /// </summary>
        public bool Like
        {
            get
            {
                return like;
            }
            set
            {
                if (like != value)
                {
                    like = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Das Anschaffungsdatum
        /// </summary>
        public DateTimeOffset? PurchaseDate
        {
            get
            {
                return purchaseDate;
            }
            set
            {
                if (purchaseDate != value)
                {
                    purchaseDate = value.Value.Date;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("PurchaseDateString");
                }
            }
        }

        /// <summary>
        /// Das Anschaffungsdatum
        /// </summary>
        public string PurchaseDateString
        {
            get
            {
                return purchaseDate.HasValue ? purchaseDate.Value.ToString("dd.MM.yyyy") : "";
            }
        }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public DateTimeOffset? DerecognitionDate
        {
            get
            {
                return derecognitionDate;
            }
            set
            {
                if (derecognitionDate != value)
                {
                    derecognitionDate = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("DerecognitionDateString");
                    NotifyPropertyChanged("DerecognitionDateVisible");
                    NotifyPropertyChanged("DerecognitionDateEnable");
                }
            }
        }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public string DerecognitionDateString
        {
            get
            {
                return derecognitionDate.HasValue ? derecognitionDate.Value.ToString("dd.MM.yyyy") : "";
            }
        }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public Visibility DerecognitionDateVisible
        {
            get
            {
                return derecognitionDate.HasValue ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public bool? DerecognitionDateEnable
        {
            get
            {
                return derecognitionDate.HasValue ? true : false;
            }
            set
            {
                if (DerecognitionDateEnable != value)
                {
                    if (value.HasValue && value.Value)
                    {
                        DerecognitionDate = DateTime.Today;
                    }
                    else
                    {
                        DerecognitionDate = null;
                    }
                }
            }
        }

        /// <summary>
        /// Der Standort
        /// </summary>
        public Location Location
        {
            get
            {
                return location;
            }
            set
            {
                if (location != value)
                {
                    location = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Sichtbarkei des Stadortes
        /// Ist kein Standort festgelegt, dann können die entsprechenden Steuerelemente ausgeblendet werden
        /// </summary>
        public Visibility LocationVisibility
        {
            get { return Location != null && !string.IsNullOrEmpty(Location.Name) ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// Die Kostenstelle
        /// </summary>
        public CostCenter CostCenter
        {
            get
            {
                return costCenter;
            }
            set
            {
                if (costCenter != value)
                {
                    costCenter = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Sichtbarkei des Kostenstelle
        /// Ist keine Kostenstelle festgelegt, dann können die entsprechenden Steuerelemente ausgeblendet werden
        /// </summary>
        public Visibility CostCenterVisibility
        {
            get { return CostCenter != null && !string.IsNullOrEmpty(CostCenter.Name) ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// Der Hersteller
        /// </summary>
        public  Manufacturer Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                if (manufacturer != value)
                {
                    manufacturer = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Sichtbarkei des Herstellers
        /// Ist kein Hersteller festgelegt, dann können die entsprechenden Steuerelemente ausgeblendet werden
        /// </summary>
        public Visibility ManufacturerVisibility
        {
            get { return Manufacturer != null && !string.IsNullOrEmpty(Manufacturer.Name) ? Visibility.Visible : Visibility.Collapsed; }
        }

        // <summary>
        /// Der Zustand
        /// </summary>
        public State State
        {
            get
            {
                return state;
            }
            set
            {
                if (state != value)
                {
                    state = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Sichtbarkei des Zustandes
        /// Ist kein Zustand festgelegt, dann können die entsprechenden Steuerelemente ausgeblendet werden
        /// </summary>
        public Visibility StateVisibility
        {
            get { return State != null && !string.IsNullOrEmpty(State.Name) ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// Der Lieferant
        /// </summary>
        public Supplier Supplier
        {
            get
            {
                return supplier;
            }
            set
            {
                if (supplier != value)
                {
                    supplier = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Sichtbarkei des Lieferanten
        /// Ist kein Lieferant festgelegt, dann können die entsprechenden Steuerelemente ausgeblendet werden
        /// </summary>
        public Visibility SupplierVisibility
        {
            get { return Supplier != null && !string.IsNullOrEmpty(Supplier.Name) ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// Das Sachkonto
        /// </summary>
        public GLAccount GLAccount
        {
            get
            {
                return glaccount;
            }
            set
            {
                if (glaccount != value)
                {
                    glaccount = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Sichtbarkei des Sachkontos
        /// Ist kein Sachkonto festgelegt, dann können die entsprechenden Steuerelemente ausgeblendet werden
        /// </summary>
        public Visibility GLAccountVisibility
        {
            get { return GLAccount != null && !string.IsNullOrEmpty(GLAccount.Name) ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// Teil eines Ganzen
        /// </summary>
        public string Parent
        {
            get
            {
                return parent;
            }
            set
            {
                if (parent != value)
                {
                    parent = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Teil eines Ganzen
        /// </summary>
        public Inventory ParentItem
        {
            get
            {
                return (from x in Model.ViewModel.Instance.Inventorys
                        where x.ID.Equals(Parent, StringComparison.OrdinalIgnoreCase)
                        select x).FirstOrDefault();
            }
            set
            {
                if (Parent != value.ID)
                {
                    Parent = value.ID;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("Parent");
                }
            }
        }

        /// <summary>
        /// Liefert die Sichtbarkei der Zugehörigkeit
        /// Ist keine Zuständigkeit festgelegt, dann können die entsprechenden Steuerelemente ausgeblendet werden
        /// </summary>
        public Visibility ParentVisibility
        {
            get { return !string.IsNullOrEmpty(Parent) ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Inventory()
            :base()
        {
            _attributes = new List<AttributeTextValue>();
            attributes = new List<AttributeTextValue>();

            _ascriptions = new List<Ascription>();
            ascriptions = new List<Ascription>();

            _purchaseDate = DateTime.Today;
            purchaseDate = DateTime.Today;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">
        ///   Die XML-Struktur, aus dem das 
        ///   Objekt erstellt werden soll
        /// </param>
        protected Inventory(XElement xml)
            : base(xml)
        {
            _attributes = new List<AttributeTextValue>();

            var template = (from x in xml.Elements("template")
                            select x.Value.Trim()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(template))
            {
                Template = (from x in Model.ViewModel.Instance.Templates
                            where !string.IsNullOrWhiteSpace(x.Name) && x.Name.Equals(template, StringComparison.OrdinalIgnoreCase)
                            select x).FirstOrDefault();
            }

            var attributes = new List<AttributeTextValue>();

            foreach (var v in xml.Elements("attribute"))
            {
                var id = v.Attribute("id") != null ? v.Attribute("id").Value : null;
                var name = v.Attribute("name") != null ? v.Attribute("name").Value : null;
                var value = v.Value;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    attributes.AddRange(from x in Model.ViewModel.Instance.Attributes
                                        where x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                                        select new AttributeTextValue()
                                        {
                                            ID = !string.IsNullOrWhiteSpace(id) ? id : Guid.NewGuid().ToString(),
                                            Name = name,
                                            Memo = x.Memo,
                                            Tag = x.Tag,
                                            Value = value
                                        });
                }
            }

            Attributes = attributes.Distinct().ToList();

            var ascriptions = new List<Ascription>();

            foreach (var v in xml.Elements("ascription"))
            {
                ascriptions.Add(new Ascription(v) { Parent = this });
            }

            Ascriptions = ascriptions.Distinct().ToList();
            Ascriptions.ForEach(a => a.Commit(false));

            var like = (from x in xml.Elements("like")
                        select x.Value.Trim()).FirstOrDefault();
            
            Like = !string.IsNullOrWhiteSpace(like) ? Convert.ToBoolean(like) : false;

            var costvalue = (from x in xml.Elements("costvalue")
                        select x.Value.Trim()).FirstOrDefault();

            CostValue = !string.IsNullOrWhiteSpace(costvalue) ? Convert.ToDecimal(costvalue, CultureInfo.InvariantCulture) : 0;

            var purchaseDate = (from x in xml.Elements("purchasedate")
                           select x.Value.Trim()).FirstOrDefault();

            PurchaseDate = !string.IsNullOrWhiteSpace(purchaseDate) ? Convert.ToDateTime(purchaseDate) : (DateTime?) null;

            var derecognitionDate = (from x in xml.Elements("derecognitiondate")
                               select x.Value.Trim()).FirstOrDefault();

            DerecognitionDate = !string.IsNullOrWhiteSpace(derecognitionDate) ? Convert.ToDateTime(derecognitionDate) : (DateTime?) null;

            var location = (from x in xml.Elements("location")
                            select x.Value.Trim()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(location))
            {
                Location = (from x in Model.ViewModel.Instance.Locations
                            where !string.IsNullOrWhiteSpace(x.Name) && x.Name.Equals(location, StringComparison.OrdinalIgnoreCase)
                            select x).FirstOrDefault();
            }

            var costCenter = (from x in xml.Elements("costcenter")
                            select x.Value.Trim()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(costCenter))
            {
                CostCenter = (from x in Model.ViewModel.Instance.CostCenters
                              where !string.IsNullOrWhiteSpace(x.Name) && x.Name.Equals(costCenter, StringComparison.OrdinalIgnoreCase)
                              select x).FirstOrDefault();
            }

            var manufacturer = (from x in xml.Elements("manufacturer")
                              select x.Value.Trim()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(manufacturer))
            {
                Manufacturer = (from x in Model.ViewModel.Instance.Manufacturers
                                where !string.IsNullOrWhiteSpace(x.Name) && x.Name.Equals(manufacturer, StringComparison.OrdinalIgnoreCase)
                                select x).FirstOrDefault();
            }

            var state = (from x in xml.Elements("state")
                                select x.Value.Trim()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(state))
            {
                State = (from x in Model.ViewModel.Instance.States
                         where !string.IsNullOrWhiteSpace(x.Name) && x.Name.Equals(state, StringComparison.OrdinalIgnoreCase)
                         select x).FirstOrDefault();
            }

            var supplier = (from x in xml.Elements("supplier")
                         select x.Value.Trim()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(supplier))
            {
                Supplier = (from x in Model.ViewModel.Instance.Suppliers
                         where !string.IsNullOrWhiteSpace(x.Name) && x.Name.Equals(supplier, StringComparison.OrdinalIgnoreCase)
                         select x).FirstOrDefault();
            }

            var glaccount = (from x in xml.Elements("glaccount")
                            select x.Value.Trim()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(glaccount))
            {
                GLAccount = (from x in Model.ViewModel.Instance.GLAccounts
                            where !string.IsNullOrWhiteSpace(x.Name) && x.Name.Equals(glaccount, StringComparison.OrdinalIgnoreCase)
                            select x).FirstOrDefault();
            }

            Parent = (from x in xml.Elements("parent")
                      select x.Value.Trim()).FirstOrDefault();
        }

        /// <summary>
        /// Übernimmt die geänderten Daten
        /// </summary>
        /// <param name="durable">true wenn die Daten dauerhaft gespeichert werden sollen</param>
        public override void Commit(bool durable)
        {
            if (template != null)
            {
                // Leere Attribute löschen
                var attributes = (from x in Attributes
                                  where !string.IsNullOrWhiteSpace(x.Value)
                                  select x).ToList();

                Attributes = attributes;
            }

            _template = Template;
            _attributes = Attributes;
            _ascriptions = Ascriptions;
            _like = Like;
            _costvalue = CostValue;
            _purchaseDate = PurchaseDate;
            _derecognitionDate = DerecognitionDate;
            _location = Location;
            _costCenter = CostCenter;
            _manufacturer = Manufacturer;
            _state = State;
            _supplier = Supplier;
            _glaccount = GLAccount;
            _parent = Parent;

            base.Commit(durable);
        }

        /// <summary>
        /// Verwirft die geänderten Daten
        /// </summary>
        public override void Rollback()
        {
            base.Rollback();

            Template = _template;
            Attributes = _attributes;
            Ascriptions = _ascriptions;
            Like = _like;
            CostValue = _costvalue;
            PurchaseDate = _purchaseDate;
            DerecognitionDate = _derecognitionDate;
            Location = _location;
            CostCenter = _costCenter;
            Manufacturer = _manufacturer;
            State = _state;
            Supplier = _supplier;
            GLAccount = _glaccount;
            Parent = _parent;
        }

        /// <summary>
        /// Als Datei Speichern
        /// </summary>
        protected override async void Save()
        {
            // IM UI-Thread ausführen
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            async () =>
            {
                await semaphore.WaitAsync();

                var fileName = ID + ".inventory";
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync
                    (
                        fileName,
                        CreationCollisionOption.ReplaceExisting
                    );

                try
                {
                    var root = new XElement("inventory");
                    ToXML(root);

                    using (var data = await file.OpenStreamForWriteAsync())
                    {
                        var doc = new XDocument(root);
                        doc.Save(data);
                    }
                }
                catch
                {
                    MessageDialog msg = new MessageDialog
                           (
                               "Beim Speichern ist was schiefgelaufen. Das Inventar wurde nicht gespeichert",
                               "Fehler"
                           );

                    await msg.ShowAsync();
                }

                semaphore.Release();
            });
        }

        /// <summary>
        /// Wandelt das Objekt in XML um
        /// </summary>
        /// <param name="xml"></param>
        protected override void ToXML(XElement xml)
        {
            base.ToXML(xml);

            xml.Add(new XElement("template", Template != null && !string.IsNullOrWhiteSpace(Template.Name) ? Template.Name : null));

            foreach (var v in Attributes)
            {
                xml.Add
                (
                    new XElement
                    (
                        "attribute",
                        new XAttribute("id", v.ID),
                        new XAttribute("name", v.Name),
                        v.Value
                    )
                );
            }

            foreach (var v in Ascriptions)
            {
                var root = new XElement("ascription");
                v.ToXML(root);

                xml.Add(root);
            }

            xml.Add(new XElement("like", Like));
            xml.Add(new XElement("costvalue", CostValue.ToString(CultureInfo.InvariantCulture)));
            xml.Add(new XElement("purchasedate", PurchaseDate));
            xml.Add(new XElement("derecognitiondate", DerecognitionDate));
            xml.Add(new XElement("location", Location != null && !string.IsNullOrWhiteSpace(Location.Name) ? Location.Name : null));
            xml.Add(new XElement("costcenter", CostCenter != null && !string.IsNullOrWhiteSpace(CostCenter.Name) ? CostCenter.Name : null));
            xml.Add(new XElement("manufacturer", Manufacturer != null && !string.IsNullOrWhiteSpace(Manufacturer.Name) ? Manufacturer.Name : null));
            xml.Add(new XElement("state", State != null && !string.IsNullOrWhiteSpace(State.Name) ? State.Name : null));
            xml.Add(new XElement("supplier", Supplier != null && !string.IsNullOrWhiteSpace(Supplier.Name) ? Supplier.Name : null));
            xml.Add(new XElement("glaccount", GLAccount != null && !string.IsNullOrWhiteSpace(GLAccount.Name) ? GLAccount.Name : null));
            xml.Add(new XElement("parent", Parent));
        }
        
        /// <summary>
        /// Erstellt eine neue Instanz eines Kontos 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Das Konto</returns>
        public static async Task<Inventory> Factory(StorageFile file)
        {
            using (var data = await file.OpenStreamForReadAsync())
            {
                XDocument doc = XDocument.Load(data);
                var root = doc.Descendants("inventory");

                return new Inventory(root.FirstOrDefault());
            }
        }
    }
}
