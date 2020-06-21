using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Inventar
    /// </summary>
    public class Inventory : Item
    {
        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        public decimal CostValue { get; set; }

        /// <summary>
        /// Das Template
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// Die Attribute des Kontos
        /// </summary>
        public List<AttributeTextValue> Attributes { get; set; }

        /// <summary>
        /// Die Zuschreibungen
        /// </summary>
        public List<Ascription> Ascriptions { get; set; }

        /// <summary>
        /// Das Konto ist ein Favorit
        /// </summary>
        public bool Like { get; set; }

        /// <summary>
        /// Das Anschaffungsdatum
        /// </summary>
        public DateTimeOffset? PurchaseDate { get; set; }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public DateTimeOffset? DerecognitionDate { get; set; }

        /// <summary>
        /// Der Standort
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Die Kostenstelle
        /// </summary>
        public CostCenter CostCenter { get; set; }

        /// <summary>
        /// Der Hersteller
        /// </summary>
        public Manufacturer Manufacturer { get; set; }

        // <summary>
        /// Der Zustand
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// Der Lieferant
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Das Sachkonto
        /// </summary>
        public GLAccount GLAccount { get; set; }

        /// <summary>
        /// Teil eines Ganzen
        /// </summary>
        public string Parent { get; set; }

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
                }
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Inventory()
            : base()
        {
            Attributes = new List<AttributeTextValue>();
            Ascriptions = new List<Ascription>();

            PurchaseDate = DateTime.Today;
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
            Attributes = new List<AttributeTextValue>();

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

            var like = (from x in xml.Elements("like")
                        select x.Value.Trim()).FirstOrDefault();

            Like = !string.IsNullOrWhiteSpace(like) ? Convert.ToBoolean(like) : false;

            var costvalue = (from x in xml.Elements("costvalue")
                             select x.Value.Trim()).FirstOrDefault();

            CostValue = !string.IsNullOrWhiteSpace(costvalue) ? Convert.ToDecimal(costvalue, CultureInfo.InvariantCulture) : 0;

            var purchaseDate = (from x in xml.Elements("purchasedate")
                                select x.Value.Trim()).FirstOrDefault();

            PurchaseDate = !string.IsNullOrWhiteSpace(purchaseDate) ? Convert.ToDateTime(purchaseDate) : (DateTime?)null;

            var derecognitionDate = (from x in xml.Elements("derecognitiondate")
                                     select x.Value.Trim()).FirstOrDefault();

            DerecognitionDate = !string.IsNullOrWhiteSpace(derecognitionDate) ? Convert.ToDateTime(derecognitionDate) : (DateTime?)null;

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
        /// Erstellt eine neue Instanz eines Inventars 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Das Inventar</returns>
        public static Inventory Factory(string file)
        {
            using (var content = File.OpenRead(file))
            {
                var doc = XDocument.Load(content);
                var root = doc.Descendants("inventory");

                return new Inventory(root.FirstOrDefault());
            }
        }
    }
}
