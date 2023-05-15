using WebExpress.Internationalization;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;

namespace InventoryExpress
{
    [WebExName("app.name")]
    [WebExDescription("app.description")]
    [WebExIcon("/assets/img/inventoryexpress.svg")]
    [WebExAssetPath("ix")]
    [WebExDataPath("ix")]
    [WebExContextPath("/ix")]
    [WebExOption(typeof(WebExpress.WebApp.Module))]
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
            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                heading: InternationalizationManager.I18N("inventoryexpress:app.notification.welcome.label"),
                message: InternationalizationManager.I18N("inventoryexpress:app.notification.welcome.description"),
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
