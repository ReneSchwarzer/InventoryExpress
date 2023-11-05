using WebExpress.WebMessage;

namespace InventoryExpress.Parameter
{
    public class ParameterLedgerAccountId : WebExpress.WebMessage.Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterLedgerAccountId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterLedgerAccountId(string value)
            : base("LedgerAccountId", value, ParameterScope.Url)
        {

        }
    }
}
