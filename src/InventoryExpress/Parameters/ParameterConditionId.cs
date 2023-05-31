using WebExpress.WebMessage;

namespace InventoryExpress.Parameters
{
    public class ParameterConditionId : Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterConditionId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterConditionId(string value)
            : base("ConditionId", value, ParameterScope.Url)
        {

        }
    }
}
