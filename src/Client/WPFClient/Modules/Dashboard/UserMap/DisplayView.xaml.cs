using CP.NLayer.Client.WpfClient.Common;
using CP.NLayer.Resources.UI;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls.Map;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.UserMap
{
    public partial class DisplayView : UserControl
    {
        private ObservableCollection<MapShape> _selectedShapes = new ObservableCollection<MapShape>();

        public DisplayView(List<ShapeData> shapeDataList)
        {
            InitializeComponent();

            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }

            this.ShapeDataList = shapeDataList;
            SubscribeUsersColorChangedEvent();
            LoadMap();
            _selectedShapes.CollectionChanged += SelectedShapes_CollectionChanged;
        }

        public List<ShapeData> ShapeDataList { get; set; }

        public void LoadMap()
        {
            this._selectedShapes.Clear();
            this._layer.Items.Clear();

            var mapPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "UserMap", "map.kml");
            if (!File.Exists(mapPath))
            {
                throw new FileNotFoundException("map.kml not found!");
            }

            var reader = new MapShapeReader();
            reader.Source = new Uri(mapPath, UriKind.Absolute);
            reader.ReadCompleted -= Reader_ReadCompleted;
            reader.ReadCompleted += Reader_ReadCompleted;
            this._layer.Reader = reader;
        }

        private void SelectedShapes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ColorShapes();
            PublishSelectedUsersChangedEvent();
        }

        private void ColorShapes()
        {
            foreach (var item in this._layer.Items)
            {
                var shape = item as MapShape;
                if (shape != null)
                {
                    if (this._selectedShapes.Contains(shape))
                    {
                        MapUtilities.SelectShape(shape);
                    }
                    else
                    {
                        MapUtilities.UnSelectShape(shape);
                    }
                }
            }
        }

        private void Reader_ReadCompleted(object sender, ReadShapesCompletedEventArgs e)
        {
            AddShapes();
            HandleShapes();
        }

        private void AddShapes()
        {
            if (this.ShapeDataList == null || this.ShapeDataList.Count == 0)
            {
                return;
            }

            foreach (var item in this.ShapeDataList)
            {
                var shape = MapUtilities.CreateUserShape(item);
                this._layer.Items.Add(shape);
            }
        }

        private void HandleShapes()
        {
            foreach (var item in this._layer.Items)
            {
                var shape = item as MapShape;
                if (shape != null)
                {
                    shape.MouseLeftButtonDown += Shape_MouseLeftButtonDown;
                    shape.MouseEnter += Shape_MouseEnter;
                    shape.MouseLeave += Shape_MouseLeave;
                }
            }
        }

        private void Shape_MouseLeave(object sender, MouseEventArgs e)
        {
            this._radMap.Cursor = null;
        }

        private void Shape_MouseEnter(object sender, MouseEventArgs e)
        {
            this._radMap.Cursor = Cursors.Hand;
        }

        private void PublishSelectedUsersChangedEvent()
        {
            var users = new List<CP.NLayer.Models.Entities.User>();
            foreach (var shape in this._selectedShapes)
            {
                var user = shape.Tag as CP.NLayer.Models.Entities.User;
                if (user != null)
                {
                    users.Add(user);
                }
            }

            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAggregator.GetEvent<SelectedUsersChangedEvent>().Publish(users);
        }

        #region SubscribeEvent

        private SubscriptionToken _subscriptionToken;

        private void SubscribeUsersColorChangedEvent()
        {
            var usersColorChangedEvent = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<UsersColorChangedEvent>();
            if (_subscriptionToken != null)
            {
                usersColorChangedEvent.Unsubscribe(_subscriptionToken);
            }
            _subscriptionToken = usersColorChangedEvent.Subscribe(UsersColorChangedEventHandler);
        }

        private void UsersColorChangedEventHandler(UsersColorModel payload)
        {
            if (!string.IsNullOrEmpty(payload.Subject))
            {
                this._captionTextBox.Text = string.Format(UiResources.ColoredBy_1, payload.Subject);
            }
            else
            {
                this._captionTextBox.Text = string.Empty;
            }

            var userDic = payload.UsersColorDic;
            foreach (var item in this._layer.Items)
            {
                var shape = item as MapShape;
                if (shape != null)
                {
                    var user = shape.Tag as CP.NLayer.Models.Entities.User;
                    if (user != null)
                    {
                        if (userDic.Any(x => x.Key.Id == user.Id))
                        {
                            var color = userDic.First(x => x.Key.Id == user.Id).Value;
                            MapUtilities.ColorShape(shape, color);
                        }
                    }
                }
            }
        }

        #endregion

        private void Shape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var shape = sender as MapShape;

            //if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            //{
            //    if (this._selectedShapes.Contains(shape))
            //    {
            //        this._selectedShapes.Remove(shape);
            //    }
            //    else
            //    {
            //        this._selectedShapes.Add(shape);
            //    }
            //}
            //else
            //{
            this._selectedShapes.Clear();
            this._selectedShapes.Add(shape);
            //}
        }
    }
}