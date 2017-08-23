using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace InventoryExpress.Model
{
    public class Item : INotifyPropertyChanged
    {
        private string name;
        private string _name;
        private string memo;
        private string _memo;
        private string tag;
        private string _tag;
        private DateTime timestamp;
        private DateTime _timestamp;

        private BitmapImage image;

        private string imageBase64;
        private string _imageBase64;

        /// <summary>
        /// Die Versionsnummer
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Die ID (Die ID dient auch als Initialisierungsvektor für die Verschlüsselung)
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Der Name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Die Beschreibung
        /// </summary>
        public string Memo
        {
            get
            {
                return memo;
            }
            set
            {
                if (memo != value)
                {
                    memo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Tag
        /// </summary>
        public string Tag
        {
            get
            {
                return tag;
            }
            set
            {
                if (tag != value)
                {
                    tag = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Zeitstempel
        /// </summary>
        public DateTime Timestamp
        {
            get
            {
                return timestamp;
            }
            set
            {
                if (timestamp != value)
                {
                    timestamp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Bild
        /// </summary>
        public BitmapImage Image
        {
            get
            {
                return image;
            }
        }

        /// <summary>
        /// Bild
        /// </summary>
        public string ImageBase64
        {
            get
            {
                return imageBase64;
            }
            set
            {
                if (imageBase64 != value)
                {
                    imageBase64 = value;
                    NotifyPropertyChanged();

                    image = !string.IsNullOrWhiteSpace(value) ? ConvertToImage(value) : null;
                    NotifyPropertyChanged("Image");
                }
            }
        }

        /// <summary>
        /// Übernimmt die geänderten Daten
        /// </summary>
        public virtual void Commit()
        {
            Commit(false);
        }

        /// <summary>
        /// Übernimmt die geänderten Daten
        /// </summary>
        /// <param name="durable">true wenn die Daten dauerhaft gespeichert werden sollen</param>
        public virtual void Commit(bool durable)
        {
            _name = Name;
            _memo = Memo;
            _tag = Tag;
            _timestamp = Timestamp;
            _imageBase64 = ImageBase64;
            
            if (durable)
            {
                Save();
            }
        }

        /// <summary>
        /// Verwirft die geänderten Daten
        /// </summary>
        public virtual void Rollback()
        {
            Name = _name;
            Memo = _memo;
            Tag = _tag;
            Timestamp = _timestamp;
            ImageBase64 = _imageBase64;
        }

        /// <summary>
        /// Das PropertyChanged-Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Item()
        {
            Version = "1.0";
            ID = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">
        ///   Die XML-Struktur, aus dem das 
        ///   Objekt erstellt werden soll
        /// </param>
        protected Item(XElement xml)
            : this()
        {
            Version = (from x in xml.Attributes("version")
                       select x.Value.Trim()).FirstOrDefault();

            ID = (from x in xml.Elements("id")
                  select x.Value.Trim()).FirstOrDefault();

            Name = (from x in xml.Elements("name")
                    select x.Value.Trim()).FirstOrDefault();

            Memo = (from x in xml.Elements("memo")
                    select x.Value.Trim()).FirstOrDefault();

            var datetime = (from x in xml.Elements("timestamp")
                            select x.Value.Trim()).FirstOrDefault();

            Timestamp = !string.IsNullOrWhiteSpace(datetime) ?
                            Convert.ToDateTime(datetime) :
                            DateTime.Now;

            ImageBase64 = (from x in xml.Elements("image")
                           select x.Value).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(ID))
            {
                ID = Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// Konvertierung von Bas64 zu Image 
        /// </summary>
        /// <param name="base64">Der Base64-String</param>
        /// <returns>Das konvertierende Bild<returns>
        public static BitmapImage ConvertToImage(string base64)
        {
            var bytes = Convert.FromBase64String(base64);

            try
            {
                using (var stream = new MemoryStream(bytes))
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    var bitmap = new BitmapImage();
                    var res = bitmap.SetSourceAsync(stream.AsRandomAccessStream());

                    return bitmap;
                }
            }
            catch
            {

            }

            return null;
        }

        /// <summary>
        /// Konvertierung von Image zu Bas64 
        /// </summary>
        /// <param name="file">Das zu konvertierende Bild, welches als Datei gespeichert ist</param>
        /// <param name="imageSize">Die Größe des Bildes</param>
        /// <returns>Der Base64-String</returns>
        public static async Task<string> ConvertToIBase64Async(StorageFile file, uint imageSize)
        {
            using (var thumbnail = await file.GetScaledImageAsThumbnailAsync(ThumbnailMode.PicturesView, imageSize, ThumbnailOptions.ResizeThumbnail))
            {
                if (thumbnail != null)
                {
                    byte[] fileBytes = null;

                    fileBytes = new byte[thumbnail.Size];
                    using (DataReader reader = new DataReader(thumbnail))
                    {
                        await reader.LoadAsync((uint)thumbnail.Size);
                        reader.ReadBytes(fileBytes);
                    }

                    return Convert.ToBase64String(fileBytes);
                }
            }

            return null;
        }

        /// <summary>
        /// Wandelt das Objekt in XML um
        /// </summary>
        /// <param name="xml"></param>
        protected virtual void ToXML(XElement xml)
        {
            if (!string.IsNullOrWhiteSpace(Version))
            {
                xml.Add(new XAttribute("version", Version));
            }

            xml.Add(new XElement("id", !string.IsNullOrWhiteSpace(ID) ? ID : Guid.NewGuid().ToString()));

            if (!string.IsNullOrWhiteSpace(Name))
            {
                xml.Add(new XElement("name", Name));
            }

            if (!string.IsNullOrWhiteSpace(Memo))
            {
                xml.Add(new XElement("memo", Memo));
            }

            if (!string.IsNullOrWhiteSpace(Tag))
            {
                xml.Add(new XElement("tag", Tag));
            }

            if (!string.IsNullOrWhiteSpace(ImageBase64))
            {
                xml.Add(new XElement("image", new XCData(ImageBase64)));
            }

            xml.Add(new XElement("timestamp", Timestamp));
        }

        /// <summary>
        /// Lößt das PropertyChanged-Event aus
        /// </summary>
        /// <param name="propertyName">Der Name der Eigenschaft</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            // Da dieser Aufruf nicht abgewartet wird, wird die Ausführung der aktuellen Methode fortgesetzt, bevor der Aufruf abgeschlossen ist
#pragma warning disable CS4014
            // IM UI-Thread ausführen
            var res = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });

#pragma warning restore CS4014
        }

        /// <summary>
        /// Als Datei Speichern
        /// </summary>
        protected virtual void Save()
        {

        }
    }
    }
