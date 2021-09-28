using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using UndoExample.Managers;
using UndoExample.Models;
using UndoExample.ViewModels;
using UndoExample.Views;

namespace UndoExample
{
    public class App : Application
    {
        public void InitializeUndoTracking()
        {
            StateManager.AddTracking(ref Entity.SaveState);
            StateManager.Start();

            ViewModelLocator.Instance = new ViewModelLocator();

            ViewModelLocator.Instance.InstanceTrackViewModel(new MainWindowViewModel(), nameof(MainWindowViewModel));

            
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            InitializeUndoTracking();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    //Not required now
                    //DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
