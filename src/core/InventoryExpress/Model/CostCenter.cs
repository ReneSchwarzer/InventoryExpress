﻿using System.Collections.Generic;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Kostenstelle
    /// </summary>
    public class CostCenter : ItemTag
    {
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public CostCenter()
            : base()
        {
        }
    }
}
