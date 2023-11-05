using Bomber.BL.Impl;
using Bomber.BL.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Map.MapObject;

namespace Bomber.UI.Forms.Tiles
{
    public partial class PlaceHolderTile : UserControl, IPlaceHolder
    {
        public IPosition2D Position { get; }
        public TileType Type { get; private set; }

        public PlaceHolderTile(IPosition2D position, IConfigurationService2D configurationService, TileType tileType)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
            InitializeComponent();
            Type = tileType;
            Top = position.Y * configurationService.Dimension;
            Left = position.X * configurationService.Dimension;
            Width = configurationService.Dimension - 2;
            Height = configurationService.Dimension - 2;
            ChangeColor();
            SendToBack();
        }


        private void OnTileClicked(object sender, EventArgs e)
        {
            Type = Constants.GetNextTileType(Type);
            ChangeColor();
        }

        private void ChangeColor()
        {
            BackColor = Type switch
            {
                TileType.Ground => Color.Green,
                TileType.Wall => Color.Gray,
                TileType.Hole => Color.Black,
                _ => BackColor
            };
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
