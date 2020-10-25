using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    public class Attribute : Item
    {
        /// <summary>
        /// Gibt an oder legt den Standardwert fest
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Attribute()
        {

        }
    }
}
