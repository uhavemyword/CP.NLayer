using CP.NLayer.Client.WpfClient.Common;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls.Map;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.UserMap
{
    public partial class LayoutView : UserControl
    {
        /// <summary>
        /// The threshold distance the mouse-cursor must move before drag-selection begins.
        /// </summary>
        private static readonly double _dragThreshold = 3;

        private static readonly double _sizeThreshold = 5;
        private Color _ownedColor = (Color)Application.Current.FindResource("OwnedShapeColor");
        private Color _defaultColor = (Color)Application.Current.FindResource("DefaultShapeColor");

        private bool _isLeftMouseDownOnShape;
        private bool _isLeftMouseAndControlDownOnShape;
        private bool _isDraggingShape;
        private bool _isResizingShape;

        private Point _originalMousePoint;
        private ObservableCollection<MapShape> _selectedShapes = new ObservableCollection<MapShape>();

        public LayoutView(List<ShapeData> shapeDataList)
        {
            InitializeComponent();

            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }

            this.ShapeDataList = shapeDataList;
            this.AlignCommand = new DelegateCommand<string>(ExecuteAlignCommand, CanExecuteAlignCommand);
            LoadMap();
            _selectedShapes.CollectionChanged += SelectedShapes_CollectionChanged;

            this.DataContext = this;
        }

        #region AlignCommand

        public DelegateCommand<string> AlignCommand { get; set; }

        public bool CanExecuteAlignCommand(string operation)
        {
            return this._selectedShapes.Count > 1;
        }

        public void ExecuteAlignCommand(string operation)
        {
            List<MapRectangle> selectedShapes = this._selectedShapes.OfType<MapRectangle>().ToList();
            var baseShape = selectedShapes[0];
            Location newLocation = new Location();

            switch (operation)
            {
                case "left":
                    {
                        newLocation.Longitude = baseShape.Location.Longitude;
                        foreach (var shape in selectedShapes)
                        {
                            newLocation.Latitude = shape.Location.Latitude;
                            shape.Location = newLocation;
                        }
                    }
                    break;

                case "up":
                    {
                        newLocation.Latitude = baseShape.Location.Latitude;
                        foreach (var shape in selectedShapes)
                        {
                            newLocation.Longitude = shape.Location.Longitude;
                            shape.Location = newLocation;
                        }
                    }
                    break;

                case "right":
                    {
                        foreach (var shape in selectedShapes)
                        {
                            newLocation.Longitude = baseShape.Location.Longitude + baseShape.GeoSize.Width - shape.GeoSize.Width;
                            newLocation.Latitude = shape.Location.Latitude;
                            shape.Location = newLocation;
                        }
                    }
                    break;

                case "down":
                    {
                        foreach (var shape in selectedShapes)
                        {
                            newLocation.Latitude = baseShape.Location.Latitude - baseShape.GeoSize.Height + shape.GeoSize.Height;
                            newLocation.Longitude = shape.Location.Longitude;
                            shape.Location = newLocation;
                        }
                    }
                    break;

                case "size":
                    {
                        foreach (var shape in selectedShapes)
                        {
                            shape.Width = baseShape.Width;
                            shape.Height = baseShape.Height;
                        }
                    }
                    break;
            }
            this.IsLayoutChanged = true;
        }

        #endregion

        public List<ShapeData> ShapeDataList { get; set; }

        public bool IsLayoutChanged { get; private set; }

        public void LoadMap()
        {
            this.IsLayoutChanged = false;
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
            this.AlignCommand.RaiseCanExecuteChanged();
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
                    MapUtilities.ColorShape(shape, _defaultColor);
                }

                if (this._selectedShapes.Count > 0)
                {
                    MapUtilities.ColorShape(this._selectedShapes.First(), _ownedColor);
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
                    shape.MouseLeftButtonUp += Shape_MouseLeftButtonUp;
                    shape.MouseMove += Shape_MouseMove;
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

        private void Shape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var shape = sender as MapShape;

            _isLeftMouseDownOnShape = true;

            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                _isLeftMouseAndControlDownOnShape = true;
            }
            else
            {
                _isLeftMouseAndControlDownOnShape = false;
                if (this._selectedShapes.Count == 0)
                {
                    this._selectedShapes.Add(shape);
                }
                else if (this._selectedShapes.Contains(shape))
                {
                    //do nothing
                }
                else
                {
                    this._selectedShapes.Clear();
                    this._selectedShapes.Add(shape);
                }
            }

            shape.CaptureMouse();
            _originalMousePoint = e.GetPosition(this._radMap);

            e.Handled = true;
        }

        private void Shape_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isLeftMouseDownOnShape)
            {
                var shape = sender as MapShape;

                if (!_isDraggingShape && !_isResizingShape)
                {
                    if (_isLeftMouseAndControlDownOnShape)
                    {
                        if (this._selectedShapes.Contains(shape))
                        {
                            this._selectedShapes.Remove(shape);
                        }
                        else
                        {
                            this._selectedShapes.Add(shape);
                        }
                    }
                    else
                    {
                        if (this._selectedShapes.Count == 1 && this._selectedShapes.Contains(shape))
                        {
                            //do nothing
                        }
                        else
                        {
                            this._selectedShapes.Clear();
                            this._selectedShapes.Add(shape);
                        }
                    }
                }

                shape.ReleaseMouseCapture();
                _isLeftMouseDownOnShape = false;
                _isLeftMouseAndControlDownOnShape = false;

                e.Handled = true;
            }

            _isDraggingShape = false;
            _isResizingShape = false;
        }

        private void Shape_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isResizingShape)
            {
                int minLength = 20;
                Point currentMousePoint = e.GetPosition(this._radMap);
                var rectangle = sender as MapRectangle;
                if (rectangle != null)
                {
                    if (this._radMap.Cursor == Cursors.SizeNWSE)
                    {
                        var newWidth = rectangle.Width + currentMousePoint.X - _originalMousePoint.X;
                        rectangle.Width = newWidth > minLength ? newWidth : rectangle.Width;
                        var newHeight = rectangle.Height + currentMousePoint.Y - _originalMousePoint.Y;
                        rectangle.Height = newHeight > minLength ? newHeight : rectangle.Height;
                    }
                    else if (this._radMap.Cursor == Cursors.SizeNS)
                    {
                        var newHeight = rectangle.Height + currentMousePoint.Y - _originalMousePoint.Y;
                        rectangle.Height = newHeight > minLength ? newHeight : rectangle.Height;
                    }
                    else if (this._radMap.Cursor == Cursors.SizeWE)
                    {
                        var newWidth = rectangle.Width + currentMousePoint.X - _originalMousePoint.X;
                        rectangle.Width = newWidth > minLength ? newWidth : rectangle.Width;
                    }
                    this.IsLayoutChanged = true;
                }
                _originalMousePoint = currentMousePoint;
            }
            else if (_isDraggingShape)
            {
                Point currentMousePoint = e.GetPosition(this._radMap);
                Location originalMouseLocation = Location.GetCoordinates(this._radMap, _originalMousePoint);
                Location currentMouseLocation = Location.GetCoordinates(this._radMap, currentMousePoint);

                _originalMousePoint = currentMousePoint;

                foreach (MapShape shape in this._selectedShapes)
                {
                    // following code will fail when the shape is MapPolygon
                    //Location newLocation = new Location();
                    //newLocation.Latitude = shape.GeoBounds.Center.Latitude + currentMouseLocation.Latitude - originalMouseLocation.Latitude;
                    //newLocation.Longitude = shape.GeoBounds.Center.Longitude + currentMouseLocation.Longitude - originalMouseLocation.Longitude;
                    //shape.MoveTo(newLocation);

                    var rectangle = shape as MapRectangle;
                    if (rectangle != null)
                    {
                        Location newLocation = new Location();
                        newLocation.Latitude = rectangle.Location.Latitude + currentMouseLocation.Latitude - originalMouseLocation.Latitude;
                        newLocation.Longitude = rectangle.Location.Longitude + currentMouseLocation.Longitude - originalMouseLocation.Longitude;
                        rectangle.Location = newLocation;
                        this.IsLayoutChanged = true;
                    }
                }
            }
            else
            {
                var rect = sender as MapRectangle;
                if (rect != null)
                {
                    var screenBounds = rect.ScreenBounds;
                    var cursorPoint = e.GetPosition(this._radMap);
                    if (IsNearRightLowerCorner(screenBounds, cursorPoint))
                    {
                        this._radMap.Cursor = Cursors.SizeNWSE;
                    }
                    else if (IsNearRightBound(screenBounds, cursorPoint))
                    {
                        this._radMap.Cursor = Cursors.SizeWE;
                    }
                    else if (IsNearLowerBound(screenBounds, cursorPoint))
                    {
                        this._radMap.Cursor = Cursors.SizeNS;
                    }
                    else
                    {
                        this._radMap.Cursor = Cursors.Hand;
                    }
                }

                if (_isLeftMouseDownOnShape)
                {
                    if (this._radMap.Cursor == Cursors.Hand)
                    {
                        //
                        // The user is left-dragging the rectangle,
                        // but don't initiate the drag operation until
                        // the mouse cursor has moved more than the threshold value.
                        //
                        Point currentMousePoint = e.GetPosition(this);
                        var dragDelta = currentMousePoint - _originalMousePoint;
                        double dragDistance = Math.Abs(dragDelta.Length);
                        if (dragDistance > _dragThreshold)
                        {
                            //
                            // When the mouse has been dragged more than the threshold value commence dragging the rectangle.
                            //
                            _isDraggingShape = true;
                        }
                    }
                    else
                    {
                        _isResizingShape = true;
                    }

                    e.Handled = true;
                }
            }
        }

        private bool IsNearLowerBound(Rect screenBounds, Point cursorPoint)
        {
            if (Math.Abs(cursorPoint.Y - screenBounds.Bottom) < _sizeThreshold && cursorPoint.X <= screenBounds.Right && cursorPoint.X >= screenBounds.Left)
            {
                return true;
            }
            return false;
        }

        private bool IsNearRightBound(Rect screenBounds, Point cursorPoint)
        {
            if (Math.Abs(cursorPoint.X - screenBounds.Right) < _sizeThreshold && cursorPoint.Y <= screenBounds.Bottom && cursorPoint.Y >= screenBounds.Top)
            {
                return true;
            }
            return false;
        }

        private bool IsNearRightLowerCorner(Rect screenBounds, Point cursorPoint)
        {
            return IsNearRightBound(screenBounds, cursorPoint) && IsNearLowerBound(screenBounds, cursorPoint);
        }

        public void SaveShapeDataList()
        {
            var result = new List<ShapeData>();

            foreach (var item in this._layer.Items)
            {
                var rectangle = item as MapRectangle;
                if (rectangle != null)
                {
                    var user = rectangle.Tag as CP.NLayer.Models.Entities.User;
                    if (user != null)
                    {
                        var data = new ShapeData()
                        {
                            Latitude = rectangle.Location.Latitude,
                            Longitude = rectangle.Location.Longitude,
                            Width = rectangle.Width,
                            Height = rectangle.Height,
                            Tag = user
                        };
                        result.Add(data);
                    }
                }
            }

            this.ShapeDataList = result;
            this.IsLayoutChanged = false;
        }
    }
}