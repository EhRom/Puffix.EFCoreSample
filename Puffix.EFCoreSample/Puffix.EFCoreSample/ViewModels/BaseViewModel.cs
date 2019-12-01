using Puffix.EFCoreSample.Models;
using Puffix.EFCoreSample.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Puffix.EFCoreSample.ViewModels
{
    /// <summary>
    /// Base view model.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Indicates whether the page is "busy" or not.
        /// </summary>
        private bool isBusy = false;

        /// <summary>
        /// Title of the page.
        /// </summary>
        private string title = string.Empty;

        /// <summary>
        /// Indicates whether the page is "busy" or not.
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        /// <summary>
        /// Title of the page.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        //public IDataStore<Cat, int> DataStore => DependencyService.Get<IDataStore<Cat, int>>();

        /// <summary>
        /// Data store.
        /// </summary>
        // TODO vNext => use Autofac
        public IDataStore<Cat, int> DataStore => App.DataStore;

        #region INotifyPropertyChanged

        /// <summary>
        /// Event to notify when a property value changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Set the property value.
        /// </summary>
        /// <typeparam name="T">Type of the property value.</typeparam>
        /// <param name="backingStore">Reference to the background field.</param>
        /// <param name="value">New value.</param>
        /// <param name="propertyName">Name of the property (name of the memeber which called the method).</param>
        /// <param name="onChanged">Action on change.</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Fire the OnPropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
