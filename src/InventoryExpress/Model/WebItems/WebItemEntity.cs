using InventoryExpress.Model.Entity;
using System;
using System.Text.Json.Serialization;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemEntity : WebItem
    {
        /// <summary>
        /// Returns or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// The timestamp of the last change.
        /// </summary>
        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Returns or sets the media.
        /// </summary>
        [JsonPropertyName("media")]
        public WebItemEntityMedia Media { get; set; } = new WebItemEntityMedia();

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntity()
        {
        }

        /// <summary>
        /// Copy-Konstruktor
        /// Creates a deep copy.
        /// </summary>
        /// <param name="item">The object to be copied.</param>
        public WebItemEntity(WebItemEntity item)
            : base(item)
        {
            Description = item.Description;
            Created = item.Created;
            Updated = item.Updated;
            Media = item.Media;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">The database object.</param>
        public WebItemEntity(Item item)
        {
            Id = item.Id;
            Guid = item.Guid;
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
