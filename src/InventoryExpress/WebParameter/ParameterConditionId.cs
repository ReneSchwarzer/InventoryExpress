using WebExpress.WebMessage;

namespace InventoryExpress.Parameter
{
    public class ParameterConditionId : WebExpress.WebMessage.Parameter
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
