// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 2/15/2013 3:00:10 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using Telerik.Windows.Data;

    /// <summary>
    /// Binding source of telerik:RadDataPager
    /// </summary>
    public class PagedCollectionView<T> : ViewModelBase, IPagedCollectionView, INotifyCollectionChanged, IEnumerable<T>
    {
        private IList<T> _items = new List<T>();
        private int _pageSize;
        private int _pageIndex;
        private int _itemCount;
        private int _totalCount;
        private bool _isPageChanging = false;
        private bool _isCountRefreshed = false;
        private Func<int, int, IList<T>> _getPage;
        private Func<int> _getCount;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="getPage">A delegate function takes pageIndex & pageSize as args, and return IList</param>
        /// <param name="getCount">A delegate function takes no arg, and return count</param>
        /// <param name="pageSize">Page size</param>
        public PagedCollectionView(Func<int, int, IList<T>> getPage, Func<int> getCount, int pageSize = 2) // update pageSize
        {
            if (getPage == null)
            {
                throw new ArgumentNullException("getPage");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("PageSize should greater than 0!");
            }

            this._pageSize = pageSize;
            this._getPage = getPage;
            this._getCount = getCount;
        }

        public event EventHandler<PageChangingEventArgs> PageChanging;

        public event EventHandler<EventArgs> PageChanged;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public bool CanChangePage
        {
            get { return true; }
        }

        public bool IsPageChanging
        {
            get
            {
                return this._isPageChanging;
            }
            private set
            {
                if (this._isPageChanging != value)
                {
                    this._isPageChanging = value;
                    this.OnPropertyChanged(() => this.IsPageChanging);
                }
            }
        }

        public int ItemCount
        {
            get
            {
                return _itemCount;
            }
            private set
            {
                if (this._itemCount != value)
                {
                    this._itemCount = value;
                    this.OnPropertyChanged(() => this.ItemCount);
                }
            }
        }

        public int TotalItemCount
        {
            get
            {
                return _totalCount;
            }
            private set
            {
                if (this._totalCount != value)
                {
                    this._totalCount = value;
                    this.OnPropertyChanged(() => this.TotalItemCount);
                }
            }
        }

        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            private set
            {
                this.IsPageChanging = true;
                this._pageIndex = value;

                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += this.OnBackgroundWorkerDoWork;
                backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
                backgroundWorker.RunWorkerAsync();
            }
        }

        public int PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("value", "The PageSize should be positive.");
                }

                if (this._pageSize != value)
                {
                    this._pageSize = value;
                    this.OnPropertyChanged(() => this.PageSize);
                    this.OnPropertyChanged(() => this.ItemCount);
                    this.MoveToFirstPage();
                }
            }
        }

        public bool MoveToFirstPage()
        {
            return this.MoveToPage(0);
        }

        public bool MoveToLastPage()
        {
            return this.MoveToPage(this.TotalItemCount / this.PageSize);
        }

        public bool MoveToNextPage()
        {
            return this.MoveToPage(this.PageIndex + 1);
        }

        public bool MoveToPreviousPage()
        {
            return this.MoveToPage(this.PageIndex - 1);
        }

        public bool MoveToPage(int pageIndex)
        {
            if (this.OnPageChanging(pageIndex) && pageIndex != -1)
            {
                return false;
            }

            this.PageIndex = pageIndex;
            this.OnPageChanged();
            return true;
        }

        public IList<T> GetPage(int pageIndex)
        {
            return this._getPage == null ? new List<T>() : this._getPage(pageIndex, PageSize);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Refresh()
        {
            this._isCountRefreshed = false;
            this.MoveToPage(this._pageIndex);
        }

        private bool OnPageChanging(int newPageIndex)
        {
            PageChangingEventArgs e = new PageChangingEventArgs(newPageIndex);
            if (this.PageChanging != null)
            {
                this.PageChanging(this, e);
            }

            return e.Cancel;
        }

        private void OnPageChanged()
        {
            EventArgs e = EventArgs.Empty;
            if (this.PageChanged != null)
            {
                this.PageChanged(this, e);
            }
        }

        private void OnCollectionChanged()
        {
            NotifyCollectionChangedEventArgs e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, e);
            }
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.DoWork -= this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= this.OnBackgroundWorkerRunWorkerCompleted;

            if (e.Result != null)
            {
                this.ItemCount = (int)e.Result;
                this.TotalItemCount = this.ItemCount;
            }

            this.OnCollectionChanged();
            this.IsPageChanging = false;
            this.OnPropertyChanged(() => this.PageIndex);
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            do
            {
                this._items = this._getPage(_pageIndex, PageSize);
            }
            while (this._items.Count == 0 && --this._pageIndex >= 0);

            if (this._pageIndex < 0)
            {
                this._pageIndex = 0;
            }

            if (!this._isCountRefreshed)
            {
                if (this._getCount == null)
                {
                    e.Result = null;
                }
                else
                {
                    e.Result = this._getCount();
                }
                this._isCountRefreshed = true;
            }
        }
    }
}