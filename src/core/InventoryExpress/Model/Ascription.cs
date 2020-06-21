using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Zuschreibung
    /// 
    /// Eine Zuschreibung kann auf ein Inventar erfolgen, um diesen zu Erweitern (z.B. Speichererweiterung)
    /// ohne das die Zuschreibung selbst ím Inventar eigeständig geführt wird
    /// </summary>
    public class Ascription : Item
    {
        /// <summary>
        /// Liefert oder setzt das übergeordnete Inventar
        /// </summary>
        public Inventory Parent { get; set; }

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
        /// Das Anschaffungsdatum
        /// </summary>
        public DateTimeOffset? PurchaseDate { get; set; }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public DateTimeOffset? DerecognitionDate { get; set; }

        /// <summary>
        /// Der Hersteller
        /// </summary>
        public Manufacturer Manufacturer { get; set; }

        /// <summary>
        /// Der Zustand
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// Der Lieferant
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Ascription()
            : base()
        {
            Attributes = new List<AttributeTextValue>();
            PurchaseDate = DateTime.Today;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">
        ///   Die XML-Struktur, aus dem das 
        ///   Objekt erstellt werden soll
        /// </param>
        public Ascription(XElement xml)
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

            var costvalue = (from x in xml.Elements("costvalue")
                             select x.Value.Trim()).FirstOrDefault();

            CostValue = !string.IsNullOrWhiteSpace(costvalue) ? Convert.ToDecimal(costvalue, CultureInfo.InvariantCulture) : 0;

            var receiptDate = (from x in xml.Elements("purchasedate")
                               select x.Value.Trim()).FirstOrDefault();

            PurchaseDate = !string.IsNullOrWhiteSpace(receiptDate) ? Convert.ToDateTime(receiptDate) : (DateTime?)null;

            var derecognitionDate = (from x in xml.Elements("derecognitiondate")
                                     select x.Value.Trim()).FirstOrDefault();

            DerecognitionDate = !string.IsNullOrWhiteSpace(derecognitionDate) ? Convert.ToDateTime(derecognitionDate) : (DateTime?)null;

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
        }

        /// <summary>
        /// Wandelt das Objekt in XML um
        /// </summary>
        /// <param name="xml"></param>
        public new void ToXML(XElement xml)
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

            xml.Add(new XElement("costvalue", CostValue.ToString(CultureInfo.InvariantCulture)));
            xml.Add(new XElement("purchasedate", PurchaseDate));
            xml.Add(new XElement("derecognitiondate", DerecognitionDate));
            xml.Add(new XElement("manufacturer", Manufacturer != null && !string.IsNullOrWhiteSpace(Manufacturer.Name) ? Manufacturer.Name : null));
            xml.Add(new XElement("state", State != null && !string.IsNullOrWhiteSpace(State.Name) ? State.Name : null));
            xml.Add(new XElement("supplier", Supplier != null && !string.IsNullOrWhiteSpace(Supplier.Name) ? Supplier.Name : null));
        }
    }
}
