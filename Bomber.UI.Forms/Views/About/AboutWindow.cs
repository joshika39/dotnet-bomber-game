using Bomber.UI.Forms._Interface;
using DialogResult = UiFramework.Shared.DialogResult;

namespace Bomber.UI.Forms
{
    public partial class AboutWindow : Form, IAboutWindow
    {
        public AboutWindow()
        {
            InitializeComponent();
        }
        
        public DialogResult ShowOnTop()
        {
            throw new NotImplementedException();
        }
    }
}

