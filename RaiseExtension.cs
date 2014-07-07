using System;

namespace ProgrammingWeapons
{
    public static class RaiseExtension
    {
        public static void Raise(this Action action) {
            var act = action;
            if (act != null)
                act();
        }
        public static void Raise<T>(this Action<T> action, T value) {
            var act = action;
            if (act != null)
                act(value);
        }
    }
}