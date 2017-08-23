﻿using System;
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
    /// Sachkonto
    /// </summary>
    public class GLAccount : Item
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public GLAccount()
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
        protected GLAccount(XElement xml)
            : base(xml)
        {
        }

        /// <summary>
        /// Übernimmt die geänderten Daten
        /// </summary>
        /// <param name="durable">true wenn die Daten dauerhaft gespeichert werden sollen</param>
        public override void Commit(bool durable)
        {
            base.Commit(durable);
        }

        /// <summary>
        /// Verwirft die geänderten Daten
        /// </summary>
        public override void Rollback()
        {
            base.Rollback();
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
                var fileName = ID + ".account";
                var file = await ApplicationData.Current.RoamingFolder.CreateFileAsync
                    (
                        fileName,
                        CreationCollisionOption.ReplaceExisting
                    );

                try
                {
                    var root = new XElement("account");
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
                               "Beim Speichern ist was schiefgelaufen. Das Sachkonto wurde nicht gespeichert",
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
        }

        /// <summary>
        /// Erstellt eine neue Instanz eines Sachkontos 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Das Konto</returns>
        public static async Task<GLAccount> Factory(StorageFile file)
        {
            using (var data = await file.OpenStreamForReadAsync())
            {
                XDocument doc = XDocument.Load(data);
                var root = doc.Descendants("glaccount");

                return new GLAccount(root.FirstOrDefault());
            }
        }
    }
}
