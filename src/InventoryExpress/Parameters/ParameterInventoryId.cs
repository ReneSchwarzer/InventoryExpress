using WebExpress.WebMessage;

namespace InventoryExpress.Parameters
{
    public class ParameterInventoryId : Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterInventoryId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterInventoryId(string value)
            : base("InventoryId", value, ParameterScope.Url)
        {

        }
    }
}
