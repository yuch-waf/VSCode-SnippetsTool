using Prism.Ioc;
using SnippetsInstaller.Interface;
using SnippetsInstaller.Models;
using SnippetsInstaller.Views;
using System.Windows;
using System.Windows.Forms;

namespace SnippetsInstaller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IService, Service>();
            containerRegistry.RegisterForNavigation<FileTool>();
            containerRegistry.RegisterForNavigation<CustomSnippetsCs>();
            containerRegistry.RegisterForNavigation<CustomSnippetsJava>();
        }
    }
}
