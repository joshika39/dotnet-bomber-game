using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using GameFramework.UI.Forms.Map;

namespace Bomber.UI.Forms.Views.Main
{
    internal class BomberMapView : FormsMapControl, IBomberMapView
    {
        public void PlantBomb(IBombView bombView)
        {
            if (bombView is Control control)
            {
                Controls.Add(control);
            }
        }
        
        public void DeleteBomb(IBombView bombView)
        {
            if (bombView is Control control)
            {
                Controls.Remove(control);
            }
        }
    }
}
