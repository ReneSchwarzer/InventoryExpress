using WebExpress.WebMessage;

namespace InventoryExpress.Parameters
{
    public class ParameterCostCenterId : Parameter
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
