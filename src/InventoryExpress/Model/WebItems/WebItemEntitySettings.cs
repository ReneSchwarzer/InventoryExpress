﻿using InventoryExpress.Model.Entity;
using System.Text.Json.Serialization;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Attribut
    /// </summary>
    public class WebItemEntitySettings : WebItem
    {
        /// <summary>
        /// Die Währung
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntitySettings()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="setting">Das Datenbankobjektes der Einstellungen</param>
        public WebItemEntitySettings(Setting setting)
        {
            Currency = setting.Currency;
        }
    }
}