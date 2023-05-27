using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace ChatGPTPrompt.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class AddToRelevantClassesCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 256;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("f0629abf-98b0-47dc-a28b-4ff0611c2d17");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddToRelevantClassesCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private AddToRelevantClassesCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static AddToRelevantClassesCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in AddToRelevantClassesCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new AddToRelevantClassesCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async void Execute(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            // Get the DTE object, which represents the Visual Studio IDE.
            var dte = await ServiceProvider.GetServiceAsync<DTE, DTE2>();
            Array selectedItems = (Array)dte.ToolWindows.SolutionExplorer.SelectedItems;

            var paths = new List<string>();
            foreach (UIHierarchyItem selectedItem in selectedItems)
            {
                var projectItem = selectedItem.Object as ProjectItem;
                var filePath = projectItem.FileNames[0];
                paths.Add(filePath);
            }

            // Similar code to get your tool window
            var window = this.package.FindToolWindow(typeof(PromptSelectionToolWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            // Here, you would update the ListBox with the paths
            var control = (PromptSelectionToolWindowControl)window.Content;
            foreach (var path in paths)
            {
                control.relevantClassesListBox.Items.Add(path);
            }
        }
    }
}
