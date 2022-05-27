using InventoryExpress.Model.Entity;
using System.Linq;
using System.Text.Json.Serialization;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Zustand
    /// </summary>
    public class WebItemEntityCondition : WebItemEntity
    {
        /// <summary>
        /// Zustand als Note
        /// </summary>
        [JsonPropertyName("grade")]
        public int Grade { get; set; }

        /// <summary>
        /// Ermittelt, ob der Zustand in Verwendung ist oder nicht
        /// </summary>
        [JsonPropertyName("isinuse")]
        public bool IsInUse => ViewModel.GetConditionInUse(this);

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityCondition()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="condition">Das Datenbankobjektes des Zustandes</param>
        public WebItemEntityCondition(Condition condition)
            : base(condition)
        {
            Grade = condition.Grade;
            Uri = ViewModel.GetConditionUri(condition.Guid);
            Image = ViewModel.GetConditionIamgeUri(condition.Grade);
        }
    }
}
