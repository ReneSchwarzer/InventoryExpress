using WebExpress.WebMessage;

namespace InventoryExpress.Parameter
{
    public class ParameterExportId : WebExpress.WebMessage.Parameter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterExportId()
         : this(null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public ParameterExportId(string value)
            : base("ExportId", value, ParameterScope.Url)
        {

        }
    }
}
