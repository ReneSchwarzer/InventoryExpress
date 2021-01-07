﻿using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("Template")]
    [Title("inventoryexpress.templates.label")]
    [Segment("templates", "inventoryexpress.templates.label")]
    [Path("/")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("template")]
    public sealed class PageTemplates : PageTemplateWebApp, IPageTemplate
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplates()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };
            var list = null as ICollection<Template>;

            lock (ViewModel.Instance.Database)
            {
                list = ViewModel.Instance.Templates.OrderBy(x => x.Name).ToList();
            }

            foreach (var template in list)
            {
                var card = new ControlCardTemplate()
                {
                    Template = template
                };

                grid.Content.Add(card);
            }

            Content.Primary.Add(grid);
        }
    }
}
