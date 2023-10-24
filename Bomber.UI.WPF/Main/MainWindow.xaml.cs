using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;
using Bomber.UI.WPF.ViewModels;
using UiFramework.Shared;
using IMainWindow = Bomber.UI.WPF.Views.IMainWindow;

namespace Bomber.UI.WPF.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : IMainWindow
    {
        private readonly IMainWindowViewModel _viewModel;
        public MainWindow(IMainWindowViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            InitializeComponent();
            DataContext = viewModel.DataContext;
        }

        public DialogResult ShowOnTop()
        {
            return ShowDialog() ?? false ? UiFramework.Shared.DialogResult.Resolved : UiFramework.Shared.DialogResult.Cancelled;
        }
        
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            
            // _viewModel.HandleKeyPress(GetCharFromKey(e.Key), _player);

            if (e.Key == Key.B)
            {
                _viewModel.PutBomb();
            }
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
