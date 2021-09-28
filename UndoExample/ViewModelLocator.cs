using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UndoExample.ViewModels;

namespace UndoExample
{
    public class ViewModelLocator : ObservableObject
    {
        public static Dictionary<string, Stack<object>> States = new Dictionary<string, Stack<object>>();
        public static ViewModelLocator Instance { get; set; }

        public PropertyInfo[] ViewModels;


        private MainWindowViewModel mainviewmodel = null;
        public MainWindowViewModel MainWindowViewModel
        {
            get { return mainviewmodel; }
            set
            {
                SetProperty(ref mainviewmodel, value);
            }
        }

        public ViewModelLocator()
        {
            Instance = this;
            ViewModels = Instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        public void InstanceTrackViewModel<T>(T viewmodel, string viewmodelname) where T : class
        {
            var property = Instance.GetType().GetProperty(viewmodelname);
            property.SetValue(Instance, viewmodel);
            States.Add(viewmodelname, new Stack<object>());
        }
    }
}
