using WebExpress.WebMessage;

namespace InventoryExpress.Parameter
{
    public class ParameterCostCenterId : WebExpress.WebMessage.Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterCostCenterId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterCostCenterId(string value)
            : base("CostCenterId", value, ParameterScope.Url)
        {

        }
    }
}
