﻿using InventoryExpress.Model.Entity;
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
        /// Returns or sets the description.
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
        /// Constructor
        /// </summary>
        public WebItemEntityMedia()
        {
        }

        /// <summary>
        /// Copy-Konstruktor
        /// Erstellt eine Tiefenkopie.
        /// </summary>
        /// <param name="media">Das Datenbankobjekt der Medien</param>
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
        /// <param name="media">Das Datenbankobjekt der Medien</param>
        public WebItemEntityMedia(Media media)
        {
            Id = media?.Guid;
            Label = media?.Name;
            Name = media?.Name;
            Description = media?.Description;
            Image = ViewModel.GetMediaUri(media?.Guid);
            Tag = media?.Tag;
            Created = media != null ? media.Created : DateTime.Now;
            Updated = media != null ? media.Updated : DateTime.Now;
            Uri = ViewModel.GetMediaUri(media?.Guid);
        }
    }
}