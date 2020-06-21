using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryExpress.Model
{
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
