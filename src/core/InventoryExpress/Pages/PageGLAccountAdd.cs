using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Linq;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageGLAccountAdd : PageBase, IGLAccount
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularGLAccount form;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageGLAccountAdd()
            : base("Sachkonto hinzufügen")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            form = new ControlFormularGLAccount(this)
            {
                RedirectUrl = Uri.Take(-1)
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Main.Content.Add(form);

            form.GLAccountName.Validation += (s, e) =>
            {
                if (e.Value.Count() < 1)
                {
                    e.Results.Add(new ValidationResult() { Text = "Geben Sie einen gültigen Namen ein!", Type = TypesInputValidity.Error });
                }
                else if (ViewModel.Instance.GLAccounts.Where(x => x.Name.Equals(e.Value, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                {
                    e.Results.Add(new ValidationResult() { Text = "Das Sachkonto wird bereits verwendet. Geben Sie einen anderen Namen an!", Type = TypesInputValidity.Error });
                }
            };

            form.ProcessFormular += (s, e) =>
            {
                // Neues Herstellerobjekt erstellen und speichern
                var gLAccount = new GLAccount()
                {
                    Name = form.GLAccountName.Value,
                    //Tag = form.Tag.Value,
                    Discription = form.Discription.Value
                };

                DB.Instance.GLAccounts.Add(gLAccount);
                DB.Instance.SaveChanges();
            };
        }

        /// <summary>
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
