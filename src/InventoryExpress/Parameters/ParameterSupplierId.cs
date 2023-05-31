using WebExpress.WebMessage;

namespace InventoryExpress.Parameters
{
    public class ParameterSupplierId : Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterSupplierId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterSupplierId(string value)
            : base("SupplierId", value, ParameterScope.Url)
        {

        }
    }
}
