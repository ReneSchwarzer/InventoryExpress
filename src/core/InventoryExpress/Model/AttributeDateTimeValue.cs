using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    [NotMapped]
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
