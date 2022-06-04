using InventoryExpress.Model.Entity;
using System;
using System.Text.Json.Serialization;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemEntity : WebItem
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
        /// Das Bild
        /// </summary>
        [JsonPropertyName("media")]
        public WebItemEntityMedia Media { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntity()
        {
        }

        /// <summary>
        /// Copy-Konstruktor
        /// Erstellt eine Tiefenkopie.
        /// </summary>
        /// <param name="item">Das zu kopierende Objekt</param>
        public WebItemEntity(WebItemEntity item)
            : base(item)
        {
            Description = item.Description;
            Created = item.Created;
            Updated = item.Updated;
            Media = item.Media;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das Datenbankobjektes des Herstellers</param>
        public WebItemEntity(Item item)
        {
            Id = item.Guid;
            Label = item.Name;
            Name = item.Name;
            Description = item.Description;
            Image = ViewModel.GetMediaUri(item.MediaId);
            Created = item.Created;
            Updated = item.Updated;

            Media = ViewModel.GetMedia(item.MediaId);
        }
    }
}
