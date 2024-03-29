﻿using InventoryExpress.Model;
using InventoryExpress.Parameter;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using WebExpress.WebScope;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.details.label")]
    [SegmentGuid<ParameterInventoryId>("inventoryexpress:inventoryexpress.details.label")]
    [ContextPath("/")]
    [Module<Module>]
    public sealed class PageInventoryDetails : PageWebApp, IPageInventory, IScope
    {
        /// <summary>
        /// Returns the description.
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

            var guid = context.Request.GetParameter<ParameterInventoryId>();
            var classes = new List<string>() { "w-100" };
            var inventory = ViewModel.GetInventory(guid?.Value);

            var mediaItems = ViewModel.GetInventoryAttachments(inventory)
                                      .Where(x => x.Name.ToLower().EndsWith(".jpg") || x.Name.ToLower().EndsWith(".png") || x.Name.ToLower().EndsWith(".svg"));

            context.Uri.Display = inventory?.Name;
            Title = inventory?.Name;
            Description.Text = inventory?.Description;

            context.VisualTree.Content.Primary.Add(new ControlPanelFlexbox(new ControlCarousel
            (
                mediaItems.Select(x => new ControlCarouselItem()
                {
                    Control = new ControlImage("id_" + x.Id) { Uri = context.ContextPath.Append("media").Append(x.Guid), Classes = classes }
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
