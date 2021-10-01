using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using UndoExample.Managers;
using UndoExample.Models;

namespace UndoExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Random generator = new Random();

        public string Greeting => "Welcome to Avalonia!";

        public ColorDescriptor MainColor { get; set; }

        public ICommand RandomColorCommand { get; }

        private void OnRandomColor()
        {
            MainColor.Color = $"#{generator.Next(0, 255):X2}00{generator.Next(0, 255):X2}";
        }

        public ICommand UndoCommand { get; }

        private void OnUndo()
        {
            //StateManager.RestoreState();
            EntityStateManager.Undo();
        }

        public MainWindowViewModel()
        {
            MainColor = new ColorDescriptor()
            {
                Color = "#FF00FF"
            };
            RandomColorCommand = new RelayCommand(OnRandomColor);
            UndoCommand = new RelayCommand(OnUndo);
        }

        public ObservableCollection<string> History { get; } = new ObservableCollection<string>();
    }
}
