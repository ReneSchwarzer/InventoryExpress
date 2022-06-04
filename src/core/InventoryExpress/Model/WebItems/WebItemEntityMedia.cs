using InventoryExpress.Model.Entity;
using System;
using System.IO;
using System.Text.Json.Serialization;
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
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Der Zeitstempel der letzten Änderung
        /// </summary>
        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Die Schlagwörter
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// Die Dateigröße im Bytes
        /// </summary>
        [JsonIgnore]
        public long Size => File.Exists(Path.Combine(ViewModel.MediaDirectory, Id)) ? new FileInfo(Path.Combine(ViewModel.MediaDirectory, Id)).Length : 0;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityMedia()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="media">Das Datenbankobjekt der Medien</param>
        public WebItemEntityMedia(Media media)
        {
            Id = media.Guid;
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
