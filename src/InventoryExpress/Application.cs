﻿using WebExpress.Internationalization;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;

namespace InventoryExpress
{
    [Name("app.name")]
    [Description("app.description")]
    [Icon("/assets/img/inventoryexpress.svg")]
    [AssetPath("ix")]
    [DataPath("ix")]
    [ContextPath("/ix")]
    [WebExOption<WebExpress.WebApp.Module>]
    public sealed class Application : IApplication
    {
        /// <summary>
        /// Der Anwendungskontext
        /// </summary>
        public IApplicationContext Context { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Application()
        {
        }

        /// <summary>
        /// Initialization der Anwendung. Hier können z.B. verwaltete Ressourcen geladen werden. 
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
