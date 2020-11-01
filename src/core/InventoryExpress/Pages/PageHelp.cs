using InventoryExpress.Controls;
using InventoryExpress.Model;
using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageHelp : PageBase
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageHelp()
            : base("Hilfe")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Main.Content.Add(new ControlText()
            {
                Text = "Die Inventardatenbank für private Zwecke.",
                Format = TypeFormatText.Paragraph
            });

            Main.Content.Add(new ControlText()
            {
                Text = "Mit InventoryExpress verwalten Sie ihren persönlichen Besitz.InventoryExpress ist für den privaten Bereich ausgelegt. Verwalten Sie zum Beispiel Ihre Sammlerobjekte oder Haushaltsgegenstände und behalten Sie somit einen Überblick über Ihre Anschaffungen.",
                Format = TypeFormatText.Paragraph
            });

            Main.Content.Add(new ControlText()
            {
                Text = "Datenschutzrichtlinie: Die während der Nutzung eingegebenen Daten werden lokal auf Ihrem Gerät als Dateien gespeichert und über die Cloud gesichert.Sie behalten jederzeit die Datenhoheit.Die Daten werden zu keiner Zeit an Dritte übermittelt.Persönliche Informationen und Standortinformationen werden nicht erhoben.",
                Format = TypeFormatText.Paragraph
            });

            Main.Content.Add(new ControlText()
            {
                Text = "Haftungsausschluss: Die Haftung für Schäden durch Sachmängel wird ausgeschlossen.Die Haftung auf Schadensersatz wegen Körperverletzung sowie bei grober Fahrlässigkeit oder Vorsatz bleibt unberührt.",
                Format = TypeFormatText.Paragraph
            });
        }
    }
}
