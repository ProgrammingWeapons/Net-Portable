using System.ComponentModel;

namespace ProgrammingWeapons
{
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// This can be used with KingOfMagic nuget package. See it's documentation for details.
        /// </summary>
        protected static void Raise() {}


        /// <summary>
        /// Raises PropertyChanged event. 
        /// This method can be auto raised, if used KindOfMagic nuget package and setted [Magic] attribute. 
        /// See KingOfMagic documentation for details.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected virtual void RaisePropertyChanged(string propName) {
            if (propName.IsNullEmptyOrWhitespace())
                return;

            var evnt = PropertyChanged;
            if (evnt != null)
                evnt(this, new PropertyChangedEventArgs(propName));
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}