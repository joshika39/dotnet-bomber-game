﻿using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.Forms.Views.Entities
{
    public sealed partial class BombView : UserControl, IBombView
    {
        private readonly IConfigurationService2D _configurationService;
        private readonly ICollection<IEntityViewSubscriber> _subscribers = new List<IEntityViewSubscriber>();
        private readonly ICollection<IEntityViewDisposedSubscriber> _disposedSubscribers = new List<IEntityViewDisposedSubscriber>();
        public BombView(IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            InitializeComponent();
            Width = _configurationService.Dimension - 2;
            Height = _configurationService.Dimension - 2;
            BackColor = Color.DarkGreen;
            var padding = _configurationService.Dimension - 2;

            var mainPanel = new Panel
            {
                BackColor = Color.Black,
                Width = padding / 2,
                Height = padding / 2,
                Top = padding / 4,
                Left = padding / 4
            };
            
            var line1 = new Panel
            {
                BackColor = Color.Black,
                ForeColor = Color.Black,
                Width = padding - 4,
                Height = 4,
                Left = 2
            };
            line1.Top = padding / 2 - 2;
            
            var line2 = new Panel
            {
                BackColor = Color.Black,
                ForeColor = Color.Black,
                Width = 4,
                Height = padding - 4,
                Top = 2,
            };
            line2.Left = padding / 2 - 2;
            
            Controls.Add(mainPanel);
            Controls.Add(line1);
            Controls.Add(line2);
            line1.BringToFront();
            line2.BringToFront();
            Disposed += OnDisposed;
        }
        
        private void OnDisposed(object? sender, EventArgs e)
        {
            foreach (var subscriber in _disposedSubscribers)
            {
                subscriber.OnViewDisposed(this);
            }
        }

        public void UpdatePosition(IPosition2D position)
        {
            BringToFront();
            Top = position.Y * _configurationService.Dimension + 1;
            Left = position.X * _configurationService.Dimension + 1;
        }
        public void EntityViewLoaded()
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.OnViewLoaded();
            }
        }
        
        public void Attach(IEntityViewSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }
        
        public void Attach(IEntityViewDisposedSubscriber subscriber)
        {
            _disposedSubscribers.Add(subscriber);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EntityViewLoaded();
        }
    }
}
