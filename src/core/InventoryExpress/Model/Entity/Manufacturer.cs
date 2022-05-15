﻿using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Hersteller
    /// </summary>
    public class Manufacturer : ItemAddress
    {
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Manufacturer()
            : base()
        {
        }
    }
}
