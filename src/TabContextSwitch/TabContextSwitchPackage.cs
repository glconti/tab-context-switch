#region using

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.Shell;
using TabContextSwitch.Core;
using TabContextSwitch.Core.Impl;

#endregion

namespace TabContextSwitch
{
    /// <summary>
    ///   This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     The minimum requirement for a class to be considered a valid package for Visual Studio
    ///     is to implement the IVsPackage interface and register itself with the shell.
    ///     This package uses the helper classes defined inside the Managed Package Framework (MPF)
    ///     to do it: it derives from the Package class that provides the implementation of the
    ///     IVsPackage interface and uses the registration attributes defined in the framework to
    ///     register itself and its components with the shell. These attributes tell the pkgdef creation
    ///     utility what data to put into .pkgdef file.
    ///   </para>
    ///   <para>
    ///     To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in
    ///     .vsixmanifest file.
    ///   </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class TabContextSwitchPackage : Package
    {
        /// <summary>
        ///   TabContextSwitchPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "06464a9e-6d5f-4136-bc1f-ca7fa3f39e67";
        private ITabManager _tabManager;

        /// <summary>
        ///   Initializes a new instance of the <see cref="TabContextSwitchPackage" /> class.
        /// </summary>
        public TabContextSwitchPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        ///   Initialization of the package; this method is called right after the package is sited, so this is the place
        ///   where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            _tabManager = InitializeTabContextSwitch();
        }

        private ITabManager InitializeTabContextSwitch()
        {
            //var vsEnvironment = (DTE2)GetService(typeof(DTE));

            ////var vsEnvironment = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));

            //if (vsEnvironment == null) return null;

            //var x = vsEnvironment.DTE.ActiveDocument;

            //var y = vsEnvironment.ActiveSolutionProjects;

            //x.Activate();

            //y.GetType();

            IUnityContainer container = new UnityContainer();

            container
                .RegisterType<ITabManager, TabManager>()
                .RegisterType<IEventProvider, VsEventProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IStorageService, FileStorageService>(new ContainerControlledLifetimeManager())
                .RegisterType<ISerializationService, JsonSerializationService>(new ContainerControlledLifetimeManager())
                ; //.RegisterInstance(vsEnvironment);

            container.RegisterType<ISourceControlService, LibGit2SharpSourceControlService>(new ContainerControlledLifetimeManager());

            //var teamExplorer = GetService(typeof(ITeamExplorer)) as ITeamExplorer;

            //if (teamExplorer == null)
            //{
            //    container.RegisterType<ISourceControlService, LibGit2SharpSourceControlService>(new ContainerControlledLifetimeManager());
            //}
            //else
            //{
            //    container.RegisterInstance(teamExplorer);

            //    container.RegisterType<ISourceControlService, VsGitSourceControlService>(new ContainerControlledLifetimeManager());
            //}

            return container.Resolve<ITabManager>();
        }


        #endregion
    }
}