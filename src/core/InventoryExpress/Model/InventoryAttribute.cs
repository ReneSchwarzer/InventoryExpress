﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Attribute eines Inventars
    /// </summary>
    public partial class InventoryAttribute
    {
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }

        /// <summary>
        /// Der Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public InventoryAttribute()
            : base()
        {
        }
    }
}
