using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace WpfApp2.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // The method that is used to raise the PropertyChanged event.
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // The SetProperty method to simplify property change notification.
        protected bool SetProperty<T>(ref T field, T value, string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName ?? nameof(field));
            return true;
        }
    }
}
