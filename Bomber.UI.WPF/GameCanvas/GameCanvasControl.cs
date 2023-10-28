using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Bomber.BL;
using Bomber.UI.Shared.Entities;
using Bomber.UI.Shared.Views;
using Bomber.UI.WPF.Entities;
using Bomber.UI.WPF.ViewModels;
using GameFramework.Map.MapObject;

namespace Bomber.UI.WPF.GameCanvas
{
    public class GameCanvasControl : Canvas, IEntityViewDisposedSubscriber
    {
        public void OnViewDisposed(IBomberMapEntityView view)
        {
            if (view is Shape shape)
            {
                Children.Remove(shape);
            }
        }
        
        private void UpdateEntities()
        {
            foreach (var entityView in EntityViews)
            {
                if (entityView is Shape shape)
                {
                    Children.Add(shape);
                }
                entityView.Attach(this);
            }
        }
        
        private void UpdateMapObjects()
        {
            foreach (var mapObject in MapObjects)
            {
                if (mapObject is Shape shape)
                {
                    Children.Add(shape);
                } 
            }
        }
        
        public IBombWatcher BombWatcher
        {
            get => (IBombWatcher)GetValue(BombWatcherProperty);
            set => SetValue(BombWatcherProperty, value);
        }

        public static readonly DependencyProperty BombWatcherProperty = DependencyProperty.Register(
            nameof(BombWatcher), 
            typeof(IBombWatcher), 
            typeof(GameCanvasControl),
            new PropertyMetadata(default(IBomberMapEntityView), OnBombWatcherChanged));
        
        private static void OnBombWatcherChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((GameCanvasControl)d).OnBombWatcherChanged((IBombWatcher)e.NewValue);
        }
        
        private void OnBombWatcherChanged(IBombWatcher eNewValue)
        {
            BombWatcher = eNewValue;
        }
        
        public ObservableCollection<IBomberMapEntityView> EntityViews
        {
            get => (ObservableCollection<IBomberMapEntityView>)GetValue(EntityViewsProperty);
            set => SetValue(EntityViewsProperty, value);
        }

        public static readonly DependencyProperty EntityViewsProperty = DependencyProperty.Register(
            nameof(EntityViews), 
            typeof(ObservableCollection<IBomberMapEntityView>), 
            typeof(GameCanvasControl),
            new PropertyMetadata(default(IBomberMapEntityView), OnEntityViewsChanged));
        
        private static void OnEntityViewsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((GameCanvasControl)d).OnEntityViewsChanged((ObservableCollection<IBomberMapEntityView>)e.NewValue);
        }
        
        private void OnEntityViewsChanged(ObservableCollection<IBomberMapEntityView> eNewValue)
        {
            EntityViews = eNewValue;
            UpdateEntities();
        }

        public ObservableCollection<IMapObject2D> MapObjects
        {
            get => (ObservableCollection<IMapObject2D>)GetValue(MapObjectsProperty);
            set => SetValue(MapObjectsProperty, value);
        }

        public static readonly DependencyProperty MapObjectsProperty = DependencyProperty.Register(
            nameof(MapObjects), 
            typeof(ObservableCollection<IMapObject2D>), 
            typeof(GameCanvasControl),
            new PropertyMetadata(default(IBomberMapEntityView), OnMapObjectsChanged));
        
        private static void OnMapObjectsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((GameCanvasControl)d).OnMapObjectsChanged((ObservableCollection<IMapObject2D>)e.NewValue);
        }
        
        private void OnMapObjectsChanged(ObservableCollection<IMapObject2D> eNewValue)
        {
            MapObjects = eNewValue;
            UpdateMapObjects();
        }
    }
}
