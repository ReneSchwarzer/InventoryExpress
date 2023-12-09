﻿using System.Reflection;

namespace InventoryExpress.App
{
    class Program
    {
        private static void Main(string[] args)
        {
            var app = new WebExpress.WebCore.WebEx()
            {
                Name = Assembly.GetExecutingAssembly().GetName().Name
            };

            app.Execution(args);
        }
    }
}
