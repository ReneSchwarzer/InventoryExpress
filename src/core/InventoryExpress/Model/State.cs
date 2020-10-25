using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Zustand
    /// </summary>
    [Table("STATE")]
    public class State : Item
    {
        /// <summary>
        /// Zustand als Note
        /// </summary>
        [Column("GRADE")]
        public int Grade { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public State()
            : base()
        {
        }
    }
}
