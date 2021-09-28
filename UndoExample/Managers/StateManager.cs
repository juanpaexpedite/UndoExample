using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace UndoExample.Managers
{
    public class StateManager
    {
        public static void AddTracking(ref Action action)
        {
            action = SaveState;
        }

        public static void Start()
        {
            InitializeWatch();
        }

        internal static void RestoreState()
        {
            busy = true;

            var instance = ViewModelLocator.Instance;

            foreach (var info in instance.ViewModels)
            {
                var stack = ViewModelLocator.States[info.Name];
                if (stack.Count > 1)
                {
                    var current = stack.Pop();
                    var previous = stack.Pop();
                    info.SetValue(instance, previous);
                }

            }

            StableSaveState();

            busy = false;
        }

        private static bool busy = false;
        public static void SaveState()
        {
            if (busy)
            {
                return;
            }
            watch.Stop();
            watch.Start();
        }

        private static Timer watch = null;

        private static void InitializeWatch()
        {
            watch = new Timer();
            watch.Interval = 1000;
            watch.Elapsed += (s, e) => StableSaveState();
        }

        private static void StableSaveState()
        {
            watch.Stop();

            busy = true;

            var instance = ViewModelLocator.Instance;

            foreach (var info in instance.ViewModels)
            {
                var viewmodel = info.GetValue(instance);
                var stack = ViewModelLocator.States[info.Name];
                var clone = viewmodel.Clone(viewmodel.GetType());
                stack.Push(clone);
            }

            busy = false;
        }
    }
}
