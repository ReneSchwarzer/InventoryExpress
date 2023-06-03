using WebExpress.WebMessage;

namespace InventoryExpress.Parameter
{
    public class ParameterManufacturerId : WebExpress.WebMessage.Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterManufacturerId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterManufacturerId(string value)
            : base("ManufacturerId", value, ParameterScope.Url)
        {

        }
    }
}
