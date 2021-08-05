﻿using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("Settings")]
    [Title("inventoryexpress.settings.label")]
    [Segment("settings", "inventoryexpress.settings.label")]
    [Path("/")]
    [Module("InventoryExpress")]
    [Context("admin")]
    public sealed class PageSettings : PageTemplateWebApp, IPageInventory
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularSettings Form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettings()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            var setting = null as Setting;

            lock (ViewModel.Instance.Database)
            {
                setting = ViewModel.Instance.Settings.FirstOrDefault();
            }

            Form = new ControlFormularSettings("settings")
            {
                RedirectUri = Uri,
                EnableCancelButton = false,
                BackUri = Uri.Take(-1)
            };

            Form.InitializeFormular += (s, e) =>
            {
                Form.Currency.Validation += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(e.Value))
                    {
                        e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.settings.validation.currency.null"), Type = TypesInputValidity.Error });
                    }
                    else if (e.Value.Length > 10)
                    {
                        e.Results.Add(new ValidationResult() { Text = this.I18N("inventoryexpress.settings.validation.currency.tolong"), Type = TypesInputValidity.Error });
                    }
                };
            };

            Form.FillFormular += (s, e) =>
            {
                //lock (ViewModel.Instance.Database)
                //{
                //    setting = ViewModel.Instance.Settings.FirstOrDefault();
                //}
                Form.Currency.Value = setting?.Currency;
            };

            Form.ProcessFormular += (s, e) =>
            {
                lock (ViewModel.Instance.Database)
                {
                    if (setting == null)
                    {
                        ViewModel.Instance.Settings.Add(new Setting()
                        {
                            Currency = Form.Currency.Value
                        });
                    }
                    else
                    {
                        setting.Currency = Form.Currency.Value;
                    }

                    ViewModel.Instance.SaveChanges();
                }
            };

            Content.Primary.Add(Form);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();
        }
    }
}