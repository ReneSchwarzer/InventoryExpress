using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    public class ItemAaddress : ItemTag
    {
        /// <summary>
        /// Die Aaddresse
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Die Postleitzahl
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Der Ort
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemAaddress()
        {
        }
    }
}
