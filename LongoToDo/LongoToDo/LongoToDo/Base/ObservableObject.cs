using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LongoToDo.Base
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null,
            Func<T, T, bool> validateValue = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);

            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void Invalidate() => OnPropertyChanged(null);
    }
}

