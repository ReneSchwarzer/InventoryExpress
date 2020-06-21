using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryExpress.Model
{
    public class AttributeDateTimeValue : Attribute
    {
        /// <summary>
        /// Der Wert
        /// </summary>
        public DateTime Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AttributeDateTimeValue()
        {
        }
    }
}
