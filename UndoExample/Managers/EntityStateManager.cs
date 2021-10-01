using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndoExample.Managers
{
    public class EntityStateManager
    {
        private static bool undoing = false;
        public static void StackUndoable(object entity, string property, object value)
        {
            if (!undoing)
            {
                ViewModelLocator.UndoableStack.Push((entity, property, value));
                //In case you want to show in a list
                ViewModelLocator.Instance.MainWindowViewModel.History.Add($"{entity.ToString().Split('.').Last()}.{property}:{value}");
            }
        }

        public static void Undo()
        {
            try
            {
                if (ViewModelLocator.UndoableStack.Count > 0)
                {
                    undoing = true;
                    (object entity, string property, object value) = ViewModelLocator.UndoableStack.Pop();
                    entity.GetType().GetProperty(property).SetValue(entity, value);
                    undoing = false;

                    //In case you want to show in a list
                    ViewModelLocator.Instance.MainWindowViewModel.History.RemoveAt(ViewModelLocator.UndoableStack.Count);
                }
            }
            catch
            {

            }
        }
    }
}
