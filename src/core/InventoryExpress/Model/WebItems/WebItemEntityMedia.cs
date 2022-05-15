using InventoryExpress.Model.Entity;
using System;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Medien
    /// </summary>
    public class WebItemEntityMedia : WebItem
    {
        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Der Zeitstempel der letzten Änderung
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Die Schlagwörter
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityMedia()
        {
            ID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="media">Das Datenbankobjekt der Medien</param>
        public WebItemEntityMedia(Media media)
        {
            ID = media.Guid;
            Label = media.Name;
            Name = media.Name;
            Description = media.Description;
            Image = ViewModel.GetMediaUri(media.Guid);
            Tag = media.Tag;
            Created = media.Created;
            Updated = media.Updated;
            Uri = ViewModel.GetMediaUri(media.Guid);
        }
    }
}
