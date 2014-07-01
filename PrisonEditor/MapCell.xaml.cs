using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using PrisonArchitect.PrisonFile.Blocks;
using Visibility = System.Windows.Visibility;

namespace PrisonEditor
{
    /// <summary>
    ///   Interaction logic for MapCell.xaml
    /// </summary>
    public partial class MapCell : UserControl
    {
        public static List<string> Materials = new List<string>
                                                   {
                                                       "BrickWall",
                                                       "CeramicFloor",
                                                       "ConcreteFloor",
                                                       "ConcreteTiles",
                                                       "ConcreteWall",
                                                       "Dirt",
                                                       "FancyTiles",
                                                       "Fence",
                                                       "Grass",
                                                       "Gravel",
                                                       "LongGrass",
                                                       "MarbleTiles",
                                                       "MetalFloor",
                                                       "MosaicFloor",
                                                       "PavingStone",
                                                       "PerimeterWall",
                                                       "Road",
                                                       "RoadMarkings",
                                                       "RoadMarkingsLeft",
                                                       "RoadMarkingsRight",
                                                       "Sand",
                                                       "Stone",
                                                       "Water",
                                                       "WhiteTiles",
                                                       "WoodenFloor",
                                                   };

        public static List<string> IndoorMaterials = new List<string>
                                                         {
                                                             "CeramicFloor",
                                                             "ConcreteFloor",
                                                             "FancyTiles",
                                                             "MarbleTiles",
                                                             "MetalFloor",
                                                             "MosaicFloor",
                                                             "WhiteTiles",
                                                             "WoodenFloor",
                                                         };

        public static List<string> OutdoorMaterials = new List<string>
                                                          {
                                                              "Dirt",
                                                              "Fence",
                                                              "Grass",
                                                              "Gravel",
                                                              "LongGrass",
                                                              "PerimeterWall",
                                                              "Road",
                                                              "RoadMarkings",
                                                              "RoadMarkingsLeft",
                                                              "RoadMarkingsRight",
                                                              "Sand",
                                                              "Stone",
                                                              "Water",
                                                          };

        public static List<string> UnknownMaterials = new List<string>();

        public static Dictionary<string, BitmapImage> MaterialBitmapImage = new Dictionary<string, BitmapImage>();
// ReSharper disable InconsistentNaming
        private static readonly ContextMenu _contextMenu = new ContextMenu();
// ReSharper restore InconsistentNaming

        private readonly Cells.Cell _cell;
        private string _material;

        public MapCell(Cells.Cell cell)
        {
            _cell = cell;

            InitializeComponent();
        }

        public Cells.Cell Cell
        {
            get { return _cell; }
        }

        public bool Selected
        {
            get { return TileOverlay.Visibility == Visibility.Visible; }
            set { TileOverlay.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        public int X
        {
            get { return _cell.X; }
        }

        public int Y
        {
            get { return _cell.Y; }
        }

        public new double Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                Canvas.SetLeft(this, X*Width);
            }
        }

        public new double Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                Canvas.SetTop(this, Y*Height);
            }
        }

        public string Material
        {
            get { return string.IsNullOrEmpty(_cell.Material) ? "Dirt" : _cell.Material; }
            set
            {
                _cell.Material = value == "Dirt" ? null : value;

                // some materials are only indoors
                if (IndoorMaterials.Contains(Material)) _cell.Indoors = true;
                else if (OutdoorMaterials.Contains(Material)) _cell.Indoors = false;

                TileImage.Source = MaterialBitmapImage.ContainsKey(Material)
                                       ? MaterialBitmapImage[Material]
                                       : MaterialBitmapImage["Unknown"];
            }
        }

        public static void PreloadMaterialBitmaps()
        {
            AddMaterialToDictionary("Unknown");
            foreach (string material in Materials)
                AddMaterialToDictionary(material);
        }

        public static void AddMaterialToDictionary(string material)
        {
            if (MaterialBitmapImage.ContainsKey(material)) return;

            MaterialBitmapImage.Add(material,
                                    ConvertToBitmapImage(
                                        Properties.Resources.ResourceManager.GetObject(material) as Bitmap));
        }

        public static BitmapImage ConvertToBitmapImage(Bitmap bitmap)
        {
            MemoryStream memoryStream = new MemoryStream();
            (bitmap).Save(memoryStream, ImageFormat.Bmp);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            memoryStream.Seek(0, SeekOrigin.Begin);
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.Freeze();

            return bitmapImage;
        }

        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            return;

            #region ContextMenu

            _contextMenu.Items.Clear();

            foreach (string material in Materials)
            {
                MenuItem menuItem = new MenuItem
                                        {
                                            Name = material,
                                            Header = material,
                                            IsCheckable = true,
                                            IsChecked = material == Material
                                        };
                menuItem.Click += (sender1, args) => { Material = ((MenuItem) sender).Header as string; };
                _contextMenu.Items.Add(menuItem);
            }
            _contextMenu.Items.Add(new Separator());
            foreach (string material in UnknownMaterials)
            {
                MenuItem menuItem = new MenuItem
                                        {
                                            Name = material,
                                            Header = material,
                                            IsCheckable = true,
                                            IsChecked = material == Material
                                        };
                menuItem.Click += (sender1, args) => { Material = ((MenuItem) sender).Header as string; };
                _contextMenu.Items.Add(menuItem);
            }

            _contextMenu.PlacementTarget = this;
            _contextMenu.IsOpen = true;

            #endregion ContextMenu
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Selected = true;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Selected = false;
        }
    }
}