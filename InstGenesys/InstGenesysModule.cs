using Genesyslab.Desktop.Infrastructure;
using Genesyslab.Desktop.Infrastructure.Commands;
using Genesyslab.Desktop.Infrastructure.DependencyInjection;
using Genesyslab.Desktop.Infrastructure.ViewManager;
using Genesyslab.Desktop.Modules.Windows.Views.Interactions.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXConnect.Desktop.Modules.InstGenesys
{
    class InstGenesysModule : IModule
    {
        readonly IObjectContainer container;
        readonly IViewManager viewManager;
        readonly ICommandManager commandManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionExtensionSampleModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="viewManager">The view manager.</param>
        /// <param name="commandManager">The command manager.</param>
        public InstGenesysModule(IObjectContainer container, IViewManager viewManager, ICommandManager commandManager)
        {
            this.container = container;
            this.viewManager = viewManager;
            this.commandManager = commandManager;
        }
        public void Initialize()
        {
            // Add a view in the right panel in the interaction window

            // Here we register the view (GUI) "IMySampleView" and its behavior counterpart "IMySampleViewModel"
            container.RegisterType<IInstGenesysView, InstGenesysView>();
            container.RegisterType<IInstGenesysModel, InstInteractionModel>();

            // Put the MySample view in the region "InteractionWorksheetRegion"
            viewManager.ViewsByRegionName["InteractionDetailsRegion"].Add(
                new ViewActivator() { ViewType = typeof(IMainToolbarInteractionContainerView), ViewName = "InstGenesys", ActivateView = true }
            );
        }
    }
}
