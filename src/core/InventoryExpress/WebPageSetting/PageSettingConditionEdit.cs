﻿using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using System.IO;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingConditionEdit")]
    [Title("inventoryexpress:inventoryexpress.condition.edit.label")]
    [SegmentGuid("ConditionID", "inventoryexpress:inventoryexpress.condition.edit.display")]
    [Path("/Setting/SettingCondition/edit")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("conditionedit")]
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
        /// <param name="context">Der Kontext</param>
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
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
            Form.BackUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            Form.ConditionName.Value = Condition?.Name;
            Form.Description.Value = Condition?.Description;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.Image.Name) as ParameterFile;

            // Zustand ändern und speichern
            Condition.Name = Form.ConditionName.Value;
            Condition.Description = Form.Description.Value;
            Condition.Updated = DateTime.Now;
            Condition.Media.Name = file?.Value;

            ViewModel.AddOrUpdateCondition(Condition);
            ViewModel.Instance.SaveChanges();

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(Condition.Media, file?.Data);
                ViewModel.Instance.SaveChanges();
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
                        Uri = new UriRelative(ViewModel.GetConditionUri(Condition.ID))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(Condition.Image),
                durability: 10000
            );
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("ConditionID")?.Value;
            Condition = ViewModel.GetCondition(guid);

            context.Request.Uri.Display = Condition.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
