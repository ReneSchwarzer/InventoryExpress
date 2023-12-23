using InventoryExpress.Model.Entity;
using System;
using System.IO;
using System.Text.Json.Serialization;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The media.
    /// </summary>
    public class WebItemEntityMedia : WebItem
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
        /// Returns or sets the tags.
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// The file size in bytes.
        /// </summary>
        [JsonIgnore]
        public long Size => File.Exists(Path.Combine(ViewModel.MediaDirectory, Guid)) ? new FileInfo(Path.Combine(ViewModel.MediaDirectory, Guid)).Length : 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityMedia()
        {
        }

        /// <summary>
        /// Copy-Konstruktor
        /// Creates a deep copy.
        /// </summary>
        /// <param name="media">The database object of the media.</param>
        public WebItemEntityMedia(WebItemEntityMedia media)
        {
            Id = media.Id;
            Label = media.Name;
            Name = media.Name;
            Description = media.Description;
            Image = media.Image;
            Tag = media.Tag;
            Created = media.Created;
            Updated = media.Updated;
            Uri = media.Uri;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="media">The database object of the media.</param>
        internal WebItemEntityMedia(Media media)
        {
            Id = media?.Id ?? -1;
            Guid = media?.Guid;
            Label = media?.Name;
            Name = media?.Name;
            Description = media?.Description;
            Tag = media?.Tag;
            Created = media != null ? media.Created : DateTime.Now;
            Updated = media != null ? media.Updated : DateTime.Now;

            Uri = ViewModel.GetUri(this);
        }
    }
}
