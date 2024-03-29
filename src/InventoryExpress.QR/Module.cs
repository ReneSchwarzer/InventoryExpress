﻿using WebExpress.WebAttribute;
using WebExpress.WebModule;

namespace InventoryExpress.QR
{
    [Name("module.name")]
    [Description("module.description")]
    [Icon("/assets/img/Logo.png")]
    [AssetPath("/")]
    [ContextPath("/qr")]
    [Application<Application>]
    public sealed class Module : IModule
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Module()
        {
        }

        /// <summary>
        /// Initialization of the module. Here, for example, managed resources can be loaded.
        /// </summary>
        /// <param name="context">The context that applies to the execution of the plugin.</param>
        public void Initialization(IModuleContext context)
        {
        }

        /// <summary>
        /// Invoked when the module starts working. The call is concurrent.
        /// </summary>
        public void Run()
        {

        }

        /// <summary>
        /// Release unmanaged resources that have been reserved during use.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
