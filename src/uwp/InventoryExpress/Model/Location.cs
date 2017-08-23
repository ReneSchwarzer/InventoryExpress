using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Standort
    /// </summary>
    public class Location : Item
    {
        private string address;
        private string _address;
        private string zip;
        private string _zip;
        private string place;
        private string _place;
        private string building;
        private string _building;
        private string room;
        private string _room;

        /// <summary>
        /// Die Adresse
        /// </summary>
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if (address != value)
                {
                    address = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Die Postleitzahl
        /// </summary>
        public string Zip
        {
            get
            {
                return zip;
            }
            set
            {
                if (zip != value)
                {
                    zip = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Der Ort
        /// </summary>
        public string Place
        {
            get
            {
                return place;
            }
            set
            {
                if (place != value)
                {
                    place = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Das Gebäude
        /// </summary>
        public string Building
        {
            get
            {
                return building;
            }
            set
            {
                if (building != value)
                {
                    building = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Der Raum innerhalb des Gebäudes
        /// </summary>
        public string Room
        {
            get
            {
                return room;
            }
            set
            {
                if (room != value)
                {
                    room = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Location()
            :base()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">
        ///   Die XML-Struktur, aus dem das 
        ///   Objekt erstellt werden soll
        /// </param>
        protected Location(XElement xml)
            : base(xml)
        {
            _address = (from x in xml.Elements("address")
                    select x.Value.Trim()).FirstOrDefault();

            _zip = (from x in xml.Elements("zip")
                    select x.Value.Trim()).FirstOrDefault();

            _place = (from x in xml.Elements("place")
                    select x.Value.Trim()).FirstOrDefault();

            _building = (from x in xml.Elements("building")
                    select x.Value.Trim()).FirstOrDefault();

            _room = (from x in xml.Elements("room")
                    select x.Value.Trim()).FirstOrDefault();
        }

        /// <summary>
        /// Übernimmt die geänderten Daten
        /// </summary>
        /// <param name="durable">true wenn die Daten dauerhaft gespeichert werden sollen</param>
        public override void Commit(bool durable)
        {
            _address = address;
            _zip = zip;
            _place = place;
            _building = building;
            _room = room;

            base.Commit(durable);
        }

        /// <summary>
        /// Verwirft die geänderten Daten
        /// </summary>
        public override void Rollback()
        {
            base.Rollback();

            Address = _address;
            Zip = _zip;
            Place = _place;
            Building = _building;
            Room = _room;
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
                var fileName = ID + ".location";
                var file = await ApplicationData.Current.RoamingFolder.CreateFileAsync
                    (
                        fileName,
                        CreationCollisionOption.ReplaceExisting
                    );

                try
                {
                    var root = new XElement("location");
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
                               "Beim Speichern ist was schiefgelaufen. Der Standort wurde nicht gespeichert",
                               "Fehler"
                           );
                    await msg.ShowAsync();
                }
            });
        }

        /// <summary>
        /// Wandelt das Objekt in XML um
        /// </summary>
        /// <param name="xml"></param>
        protected override void ToXML(XElement xml)
        {
            base.ToXML(xml);

            xml.Add(new XElement("address", Address));
            xml.Add(new XElement("zip", Zip));
            xml.Add(new XElement("place", Place));
            xml.Add(new XElement("building", Building));
            xml.Add(new XElement("room", Room));
        }

        /// <summary>
        /// Erstellt eine neue Instanz eines Kontos 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Das Konto</returns>
        public static async Task<Location> Factory(StorageFile file)
        {
            using (var data = await file.OpenStreamForReadAsync())
            {
                XDocument doc = XDocument.Load(data);
                var root = doc.Descendants("location");

                return new Location(root.FirstOrDefault());
            }
        }
    }
}
