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
using Windows.UI.Xaml;

namespace InventoryExpress.Model
{
    public class Attribute : Item
    {
        private string defaultValue;
        private string _defaultValue;

        /// <summary>
        /// Gibt an oder legt den Standardwert fest
        /// </summary>
        public string DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                if (defaultValue != value)
                {
                    defaultValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Attribute()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">
        ///   Die XML-Struktur, aus dem das 
        ///   Objekt erstellt werden soll
        /// </param>
        protected Attribute(XElement xml)
            : base(xml)
        {
            DefaultValue = (from x in xml.Elements("default")
                            select x.Value.Trim()).FirstOrDefault();
        }

        /// <summary>
        /// Übernimmt die geänderten Daten
        /// </summary>
        /// <param name="durable">true wenn die Daten dauerhaft gespeichert werden sollen</param>
        public override void Commit(bool durable)
        {
            base.Commit(durable);

            _defaultValue = defaultValue;
        }

        /// <summary>
        /// Verwirft die geänderten Daten
        /// </summary>
        public override void Rollback()
        {
            base.Rollback();

            defaultValue = _defaultValue;
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
                var fileName = ID + ".attribute";
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync
                    (
                        fileName,
                        CreationCollisionOption.ReplaceExisting
                    );

                try
                {
                    var root = new XElement("attribute");
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
                               "Beim Speichern ist was schiefgelaufen. Das Attribut wurde nicht gespeichert",
                               "Fehler"
                           );
                    await msg.ShowAsync();
                }
            });
        }

        /// <summary>
        /// Erstellt eine neue Instanz einer Passwortregel 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Die Passwortregel</returns>
        public static async Task<Attribute> Factory(StorageFile file)
        {
            using (var data = await file.OpenStreamForReadAsync())
            {
                XDocument doc = XDocument.Load(data);
                var root = doc.Descendants("attribute");

                return new Attribute(root.FirstOrDefault());
            }
        }
    }
}
