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
    }
}