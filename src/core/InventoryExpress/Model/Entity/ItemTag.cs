﻿namespace InventoryExpress.Model.Entity
{
    public class ItemTag : Item
    {
        /// <summary>
        /// Die Schlagwörter
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemTag()
        {
        }
    }
}