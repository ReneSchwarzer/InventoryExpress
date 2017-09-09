using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using Windows.UI.Popups;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Vorlage
    /// </summary>
    public class Template : Item
    {
        private string url;
        private string _url;
        private List<Attribute> attributes;
        private List<Attribute> _attributes;

        /// <summary>
        /// Die URL des Providers
        /// </summary>
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                if (url != value)
                {
                    url = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Die Attribute der Vorlage
        /// </summary>
        public List<Attribute> Attributes
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
                    NotifyPropertyChanged("UnusedAttributes");
                }
            }
        }

        /// <summary>
        /// Die ungenutzten Attribute der Vorlage
        /// </summary>
        public List<Attribute> UnusedAttributes
        {
            get
            {
                // ToDo Menge a \ b
                var list = (from x in Model.ViewModel.Instance.Attributes
                            where Attributes.Find(a => a.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase)) == null
                            select x).ToList();

                return list;
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Template()
            : base()
        {
            _attributes = new List<Attribute>();
            attributes = new List<Attribute>();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">
        ///   Die XML-Struktur, aus dem das 
        ///   Objekt erstellt werden soll
        /// </param>
        protected Template(XElement xml)
            : base(xml)
        {
            _attributes = new List<Attribute>();

            Url = (from x in xml.Elements("url")
                   select x.Value.Trim()).FirstOrDefault();

            var attributes = new List<Attribute>();

            foreach (var v in xml.Elements("attribute"))
            {
                var name = v.Attribute("name") != null ? v.Attribute("name").Value : null;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    attributes.AddRange(from x in Model.ViewModel.Instance.Attributes
                                        where x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                                        select x);
                }
            }

            Attributes = attributes.Distinct().ToList();
        }

        /// <summary>
        /// Übernimmt die geänderten Daten
        /// </summary>
        /// <param name="durable">true wenn die Daten dauerhaft gespeichert werden sollen</param>
        public override void Commit(bool durable)
        {
            _url = url;
            _attributes = attributes;

            base.Commit(durable);
        }

        /// <summary>
        /// Verwirft die geänderten Daten
        /// </summary>
        public override void Rollback()
        {
            base.Rollback();

            url = _url;
            attributes = _attributes;
        }

        /// <summary>
        /// Als Datei Speichern
        /// </summary>
        protected override async void Save()
        {
            var fileName = ID + ".template";
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync
                (
                    fileName,
                    CreationCollisionOption.ReplaceExisting
                );

            try
            {
                var root = new XElement("template");
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
                           "Beim Speichern ist was schiefgelaufen. Die Vorlage wurde nicht gespeichert",
                           "Fehler"
                       );
                await msg.ShowAsync();
            }
        }

        /// <summary>
        /// Wandelt das Objekt in XML um
        /// </summary>
        /// <param name="xml"></param>
        protected override void ToXML(XElement xml)
        {
            base.ToXML(xml);

            xml.Add(new XElement("url", Url));
            
            foreach (var v in Attributes)
            {
                xml.Add(new XElement("attribute", new XAttribute("name", v.Name)));
            }
        }

        /// <summary>
        /// Erstellt eine neue Instanz einer Vorlage 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Die Vorlage</returns>
        public static async Task<Template> Factory(StorageFile file)
        {
            using (var data = await file.OpenStreamForReadAsync())
            {
                XDocument doc = XDocument.Load(data);
                var root = doc.Descendants("template");

                return new Template(root.FirstOrDefault());
            }
        }
    }
}
