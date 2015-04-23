// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 2013/2/7 12:36:41
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;

    /// <summary>
    /// ViewModel for telerik:RadBusyIndicator Control
    /// </summary>
    public class RadBusyModel : INotifyPropertyChanged
    {
        private bool _isBusy = false;
        private bool _isIndeterminate = true;
        private string _busyContent = "In Process...";
        private TimeSpan _displayAfter = new TimeSpan(0); //new TimeSpan(5000000); //half second
        private HashSet<string> _executingTasks = new HashSet<string>();
        private object _syncObj = new object();

        public TimeSpan DisplayAfter
        {
            get { return this._displayAfter; }
            set
            {
                if (this._displayAfter != value)
                {
                    this._displayAfter = value;
                    this.OnPropertyChanged(() => this.DisplayAfter);
                }
            }
        }

        public bool IsIndeterminate
        {
            get { return this._isIndeterminate; }
            set
            {
                if (this._isIndeterminate != value)
                {
                    this._isIndeterminate = value;
                    this.OnPropertyChanged(() => this.IsIndeterminate);
                }
            }
        }

        public bool IsBusy
        {
            get { return this._isBusy; }
            set
            {
                if (this._isBusy != value)
                {
                    this._isBusy = value;
                    this.OnPropertyChanged(() => this.IsBusy);
                }
            }
        }

        public string BusyContent
        {
            get { return this._busyContent; }
            set
            {
                if (this._busyContent != value)
                {
                    this._busyContent = value;
                    this.OnPropertyChanged(() => this.BusyContent);
                }
            }
        }

        public void Await()
        {
            //Debug.Print("Await thread: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            while (this.IsBusy)
            {
                System.Threading.Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Do work async.
        /// </summary>
        /// <param name="action">Action which need to be ran in new thread.</param>
        /// <param name="onCompleted">Action which will be called after async action completed.</param>
        /// <param name="await">if true, only raise onCompleted action until no task is running</param>
        public void DoWorkAsync(Action action, Action onCompleted = null, bool await = false)
        {
            //Debug.Print("DoWorkAsync thread: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            string taskId = Guid.NewGuid().ToString();
            var args = new object[] { taskId, action, onCompleted, await };
            lock (this._syncObj)
            {
                this._executingTasks.Add(taskId);
                this.IsBusy = true;
            }
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
            backgroundWorker.RunWorkerAsync(args);
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Debug.Print("OnBackgroundWorkerRunWorkerCompleted thread: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            var backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.DoWork -= this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= this.OnBackgroundWorkerRunWorkerCompleted;
            if (e.Error == null)
            {
                var onCompleted = e.Result as Action;
                if (onCompleted != null)
                {
                    onCompleted();
                }
            }
            else
            {
                throw e.Error;
            }
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            //Debug.Print("OnBackgroundWorkerDoWork thread: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            var args = e.Argument as object[];
            Action action = args[1] as Action;
            try
            {
                if (action != null)
                {
                    action();
                }
            }
            catch
            {
                e.Cancel = true;
                throw;
            }
            finally
            {
                lock (this._syncObj)
                {
                    this._executingTasks.Remove(args[0] as string);
                    if (this._executingTasks.Count == 0)
                    {
                        this.IsBusy = false;
                    }
                }
            }

            bool await = (bool)args[3];
            if (await)
            {
                Await();
            }

            e.Result = args[2] as Action;
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