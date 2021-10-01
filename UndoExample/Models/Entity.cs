using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UndoExample.Models
{
    public class Entity : ObservableObject
    {
        public static Action SaveState = null;
        
        public static Action<object, string, object> StackUndoable = null;

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                SetProperty(ref id, value);
            }
        }

        public bool SetUndoableProperty<T>(ref T field, T newvalue, [CallerMemberName] string? propertyName = null)
        {
            var lastvalue = field;
            if (SetProperty(ref field, newvalue, propertyName))
            {
                StackUndoable?.Invoke(this, propertyName, lastvalue);
                return true;
            }
            return false;
        }

        public bool SetTrackProperty<T>(ref T field, T newvalue, [CallerMemberName] string? propertyName = null)
        {
            var lastvalue = field;
            if (SetProperty(ref field, newvalue, propertyName))
            {
                SaveState?.Invoke();
                return true;
            }
            return false;
        }
    }
}
