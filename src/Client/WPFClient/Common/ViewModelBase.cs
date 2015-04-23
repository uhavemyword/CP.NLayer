// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 1/3/2013 10:48:28 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using CP.NLayer.Models;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Windows.Threading;

    public abstract class ViewModelBase : DataErrorInfo, INotifyPropertyChanged
    {
        private ObservableCollection<ValidationResult> _validationResults = new ObservableCollection<ValidationResult>();

        public ViewModelBase()
        {
            ValidationResults.CollectionChanged += ValidationResults_CollectionChanged;
        }

        private void ValidationResults_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged(() => this.ValidationResults);
        }

        public ObservableCollection<ValidationResult> ValidationResults
        {
            get { return _validationResults; }
            set
            {
                if (!object.Equals(_validationResults, value))
                {
                    _validationResults = value;
                    this.OnPropertyChanged(() => this.ValidationResults);
                }
            }
        }

        public RadBusyModel BusyModel { get; set; }

        public IRegionManager RegionManager
        {
            get { return ServiceLocator.Current.GetInstance<IRegionManager>(); }
        }

        public Microsoft.Practices.Unity.IUnityContainer Container
        {
            get { return ServiceLocator.Current.GetInstance<IUnityContainer>(); }
        }

        public static void InvokeOnUIThread(Action action)
        {
            Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
            if (!currentDispatcher.CheckAccess())
            {
                currentDispatcher.BeginInvoke(action);
                return;
            }
            else
            {
                action();
                return;
            }
        }

        #region INotifyPropertyChanged

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises this object's PropertyChanged event for each of the properties.
        /// </summary>
        /// <param name="propertyNames">The properties that have a new value.</param>
        protected void OnPropertyChanged(params string[] propertyNames)
        {
            if (propertyNames != null)
            {
                string[] strArrays = propertyNames;
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string str = strArrays[i];
                    this.OnPropertyChanged(str);
                }
                return;
            }
            else
            {
                throw new ArgumentNullException("propertyNames");
            }
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">The type of the property that has a new value</typeparam>
        /// <param name="propertyExpression">A Lambda expression representing the property that has a new value.</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression != null)
            {
                string propertyName = memberExpression.Member.Name;
                this.OnPropertyChanged(propertyName);
            }
        }

        #endregion INotifyPropertyChanged
    }
}