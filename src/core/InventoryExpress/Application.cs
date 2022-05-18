using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress
{
    [ID("InventoryExpress")]
    [Name("app.name")]
    [Description("app.description")]
    [Icon("/assets/img/inventoryexpress.svg")]
    [AssetPath("ix")]
    [DataPath("ix")]
    [ContextPath("/ix")]
    [Option("webexpress.webapp.*")]
    public sealed class Application : IApplication
    {
        /// <summary>
        /// Der Anwendungskontext
        /// </summary>
        public IApplicationContext Context { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Application()
        {
        }

        /// <summary>
        /// Initialisierung der Anwendung. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Initialization(IApplicationContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Anwendung mit der Arbeit beginnt. Der Aufruf erfolgt nebenläufig.
        /// </summary>
        public void Run()
        {
            // Willkommensnachricht
            NotificationManager.CreateNotification
            (
                heading: I18N("inventoryexpress:app.notification.welcome.label"),
                message: I18N("inventoryexpress:app.notification.welcome.description"),
                icon: Context.Icon,
                durability: 30000
            );
        }

        /// <summary>
        /// Freigeben von nicht verwalteten Ressourcen, welche wärend der Verwendung reserviert wurden.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
