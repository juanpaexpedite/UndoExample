using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UndoExample.Managers
{
    public static class CloneManager
    {
        public static T Clone<T>(this T original) where T : class
        {
            var serialized = JsonSerializer.Serialize<T>(original);

            var clone = JsonSerializer.Deserialize<T>(serialized);

            return clone;
        }

        public static object Clone(this object original, Type type)
        {
            var serialized = JsonSerializer.Serialize(original);

            return JsonSerializer.Deserialize(serialized, type);
        }
    }
}
