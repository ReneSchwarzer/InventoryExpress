using InventoryExpress.Model.Entity;

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
        public int Grade { get; set; }

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
