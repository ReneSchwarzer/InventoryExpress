﻿using InventoryExpress.Model;
using InventoryExpress.Parameters;
using System.Collections.Generic;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExTitle("inventoryexpress:inventoryexpress.details.label")]
    [WebExSegmentGuid(typeof(ParameterInventoryId), "inventoryexpress:inventoryexpress.details.label")]
    [WebExContextPath("/")]
    [WebExModule(typeof(Module))]
    [WebExContext("general")]
    [WebExContext("inventorydetails")]
    public sealed class PageInventoryDetails : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Die Beschreibung The inventory item.es
        /// </summary>
        private ControlText Description { get; } = new ControlText()
        {
            Format = TypeFormatText.Paragraph,
            TextColor = new PropertyColorText(TypeColorText.Dark)
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageInventoryDetails()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("InventoryId")?.Value;
            var classes = new List<string>() { "w-100" };
            var inventory = ViewModel.GetInventory(guid);

            var mediaItems = ViewModel.GetInventoryAttachments(inventory)
                                      .Where(x => x.Name.ToLower().EndsWith(".jpg") || x.Name.ToLower().EndsWith(".png") || x.Name.ToLower().EndsWith(".svg"));

            Uri.Display = inventory?.Name;
            Title = inventory?.Name;
            Description.Text = inventory?.Description;

            context.VisualTree.Content.Primary.Add(new ControlPanelFlexbox(new ControlCarousel
            (
                mediaItems.Select(x => new ControlCarouselItem()
                {
                    Control = new ControlImage("id_" + x.Id) { Uri = context.ContextPath.Append("media").Append(x.Id), Classes = classes }
                }).ToArray()
            )
            {
                Width = TypeWidth.SeventyFive
            })
            {
                Layout = TypeLayoutFlexbox.Default,
                Justify = TypeJustifiedFlexbox.Center
            });

            context.VisualTree.Content.Primary.Add(Description);
        }
    }
}
