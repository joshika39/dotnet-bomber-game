using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;
using Bomber.BL.Feedback;
using Bomber.BL.Impl.Entities;
using Bomber.BL.Impl.Models;
using Bomber.BL.Map;
using Bomber.UI.WPF.Entities;
using Bomber.UI.WPF.GameCanvas;
using Bomber.UI.WPF.ViewModels;
using CommunityToolkit.Mvvm.Input;
using GameFramework.GameFeedback;
using Infrastructure.Time.Listeners;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace Bomber.UI.WPF.Main
{
    internal partial class MainWindowViewModel : AMainWindowModel, IMainWindowViewModel, ITickListener
    {
        private string _currentTime;
        public GameCanvasControl MapCanvas { get; set; }

        public object DataContext => this;
        public double CanvasWidth => ConfigurationService.Dimension * BoardService.GetActiveMap<IBomberMap, IBomberMapSource, IBomberMapView>()?.SizeX ?? 0d;
        public double CanvasHeight => ConfigurationService.Dimension * BoardService.GetActiveMap<IBomberMap, IBomberMapSource, IBomberMapView>()?.SizeY ?? 0d;
        
        public TimeSpan ElapsedTime { get; set; }
        public string CurrentTime
        {
            get => _currentTime;
            private set => SetProperty(ref _currentTime, value);
        }

        public MainWindowViewModel(IServiceProvider provider) : base(provider)
        {
            GameManager.Timer.PeriodicOperation(1000, this, LifeCycleManager.Token);
            _currentTime = "";
            CurrentTime = ElapsedTime.ToString("g");
            MapCanvas = new GameCanvasControl();
        }

        public void RaiseTick(int round)
        {
            CurrentTime = GameManager.Timer.Elapsed.ToString("g");
        }

        public override void OnGameReset()
        {
            base.OnGameReset();
            CurrentTime = TimeSpan.Zero.ToString("g");
        }

        [RelayCommand]
        private void OnOpen()
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "BoB files (*.bob)|*.bob",
                InitialDirectory = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "joshik39", "Bomber", "maps")
            };
            if (!openDialog.ShowDialog() ?? false)
            {
                return;
            }

            MapCanvas.Clear();
            var map = OpenMap(openDialog.FileName, MapCanvas);
            
            GameManager.StartGame(new GameplayFeedback(FeedbackLevel.Info, "Game started!"));
            BoardService.SetActiveMap(map);
        }

        [RelayCommand]
        private void OnSave()
        {
            var map = BoardService.GetActiveMap<IBomberMap>();
            map?.SaveProgress();
        }

        [RelayCommand]
        private void OnKeyPress(KeyEventArgs e)
        {
            HandleKeyPress(GetCharFromKey(e.Key));
        }
        
        private enum MapType : uint
        {
            MapvkVkToVsc = 0x0,
        }

        [DllImport("user32.dll")]
        private static extern int ToUnicode(
            uint wVirtKey,
            uint wScanCode,
            byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)] 
            StringBuilder pwszBuff,
            int cchBuff,
            uint wFlags);

        [DllImport("user32.dll")]
        private static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, MapType uMapType);

        private static char GetCharFromKey(Key key)
        {
            var ch = ' ';

            var virtualKey = KeyInterop.VirtualKeyFromKey(key);
            var keyboardState = new byte[256];
            GetKeyboardState(keyboardState);

            var scanCode = MapVirtualKey((uint)virtualKey, MapType.MapvkVkToVsc);
            var stringBuilder = new StringBuilder(2);

            var result = ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
            switch (result)
            {
                case -1: 
                    break;
                case 0: 
                    break;
                case 1:
                {
                    ch = stringBuilder[0];
                    break;
                }
                default:
                {
                    ch = stringBuilder[0];
                    break;
                }
            }
            return ch;
        }
    }
}
