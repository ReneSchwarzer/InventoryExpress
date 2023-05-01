using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [WebExID("SettingConditionEdit")]
    [WebExTitle("inventoryexpress:inventoryexpress.condition.edit.label")]
    [WebExSegmentGuid("ConditionID", "inventoryexpress:inventoryexpress.condition.edit.display")]
    [WebExContextPath("/Setting/SettingCondition/edit")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    [WebExContext("conditionedit")]
    public sealed class PageSettingConditionEdit : PageWebAppSetting, IPageCondition
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularCondition Form { get; } = new ControlFormularCondition("condition")
        {
            Edit = true
        };

        /// <summary>
        /// Liefert oder setzt den zu ändernden Zustand
        /// </summary>
        private WebItemEntityCondition Condition { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingConditionEdit()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            Form.ConditionName.Value = Condition?.Name;
            Form.Description.Value = Condition?.Description;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Zustand ändern und speichern
            Condition.Name = Form.ConditionName.Value;
            Condition.Description = Form.Description.Value;
            Condition.Updated = DateTime.Now;

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateCondition(Condition);

                transaction.Commit();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.condition.notification.edit"),
                    new ControlLink()
                    {
                        Text = Condition.Name,
                        Uri = ViewModel.GetConditionUri(Condition.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: Condition.Image,
                durability: 10000
            );
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("ConditionID")?.Value;
            Condition = ViewModel.GetCondition(guid);

            Uri.Display = Condition.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
