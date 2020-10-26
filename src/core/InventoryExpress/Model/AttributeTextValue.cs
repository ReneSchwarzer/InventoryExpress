using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InventoryExpress.Model
{
    [NotMapped]
    public class AttributeTextValue : Attribute
    {
        /// <summary>
        /// Der Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AttributeTextValue()
        {
            Value = string.Empty;
        }
    }
}
