using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using LostMinions.Functions;
using Microsoft.Win32;
using PrisonArchitect.PrisonFile;
using PrisonArchitect.PrisonFile.BlockWrappers;
using PrisonArchitect.PrisonFile.BlockWrappers.Helper;

namespace PrisonEditor
{
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AssemblyInformation _assemblyInformation = new AssemblyInformation();
        private PrisonFile _prisonFile;
        private int _tileSize = 32;

        public MainWindow()
        {
            InitializeComponent();

            Title = null;

            DispatcherTimer dispatcherTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 1)};
            dispatcherTimer.Tick += (sender, args) => { MemoryUsage.Text = "Memory: " + string.Format("{0:0.00} MB", GC.GetTotalMemory(true)/1024.0/1024.0); };
            dispatcherTimer.Start();

            MapCell.PreloadMaterialBitmaps();
            MaterialImage.Source = MapCell.MaterialBitmapImage["Dirt"];
            UpdateMaterialsListView();
        }

        public new string Title
        {
            get { return base.Title; }
            set
            {
                base.Title = _assemblyInformation.AssemblyTitle() + " " + _assemblyInformation.AssemblyFileVersion();
                if (!string.IsNullOrEmpty(value)) base.Title += " - " + value;
            }
        }

        #region Events

        #region Canvas

        private void MapCanvas_LeftButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            HitTestResult result = VisualTreeHelper.HitTest((UIElement) sender, e.GetPosition((UIElement) sender));
            HandleHitTestResult(result);
        }

        private void MapCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestResult result = VisualTreeHelper.HitTest((UIElement) sender, e.GetPosition((UIElement) sender));
            if (e.LeftButton == MouseButtonState.Pressed) HandleHitTestResult(result);
        }

        #endregion Canvas

        #region Menu

        private void MenuItemFileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
                                            {
                                                FileName = "default",
                                                DefaultExt = ".prison",
                                                Filter = "Prison documents (.prison)|*.prison",
                                                InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                                                @"Introversion\Prison Architect\saves\")
                                            };

            // Show open file dialog box 
            bool? result = openFileDialog.ShowDialog();

            // Process open file dialog box results 
            if (result == true) LoadFile(openFileDialog.FileName);
        }

        private void MenuItemFileSave_Click(object sender, RoutedEventArgs e) { SaveFile(_prisonFile.FileName); }

        private void MenuItemFileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
                                            {
                                                FileName = Path.GetFileNameWithoutExtension(_prisonFile.FileName),
                                                DefaultExt = ".prison",
                                                Filter = "Prison documents (.prison)|*.prison",
                                                InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                                                @"Introversion\Prison Architect\saves\")
                                            };

            // Show open file dialog box 
            bool? result = saveFileDialog.ShowDialog();

            // Process open file dialog box results 
            if (result == true) SaveFile(saveFileDialog.FileName);
        }

        private void MenuItemFileExit_Click(object sender, RoutedEventArgs e) { Close(); }

        private void MenuItemTileSize_Checked(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem == null) return;

            int tileSize = _tileSize;
            switch (menuItem.Header.ToString())
            {
                case "16x16":
                    _tileSize = 16;
                    break;
                case "32x32":
                    _tileSize = 32;
                    break;
                case "64x64":
                    _tileSize = 64;
                    break;
            }

            if (tileSize == _tileSize) return;

            IsEnabled = false;
            OverlayGrid.Visibility = Visibility.Visible;
            OverlayTextBlock.Text = "Changing tile size to " + menuItem.Header;

            UpdateMapCanvas();

            OverlayGrid.Visibility = Visibility.Collapsed;
            IsEnabled = true;
        }

        #region FillMode

        public enum EFIllMode
        {
            SINGLE,
            BUCKET,
        }

        private EFIllMode _fillMode = EFIllMode.SINGLE;

        private void MenuItemFillMode_Checked(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem == null) return;

            switch (menuItem.Header.ToString())
            {
                case "Single":
                    _fillMode = EFIllMode.SINGLE;
                    break;
                case "Bucket":
                    _fillMode = EFIllMode.BUCKET;
                    break;
            }
        }

        #endregion FillMode

        #endregion Menu

        private EInOut _inOut = EInOut.Unchanged;

        private void MaterialListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView) sender;

            string material = null;
            if (listView.SelectedItem == null)
            {
                MaterialTextBlock.Text = null;
                MaterialImage.Source = null;
            }
            else
            {
                material = (string) ((ListViewItem) listView.SelectedItem).Content;
                MaterialTextBlock.Text = material;
                MaterialImage.Source = MapCell.MaterialBitmapImage.ContainsKey(material) ? MapCell.MaterialBitmapImage[material] : MapCell.MaterialBitmapImage["Unknown"];
            }

            if (Cell.IsAlwaysIndoor(material))
            {
                InOutStackPanel.IsEnabled = false;
                InOutIndoor.IsChecked = true;
            }
            else if (Cell.IsAlwaysOutdoor(material))
            {
                InOutStackPanel.IsEnabled = false;
                InOutOutdoor.IsChecked = true;
            }
            else
            {
                InOutStackPanel.IsEnabled = true;
                InOutUnchanged.IsChecked = true;
            }
        }

        private void RadioButtonIndoor_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton == null) return;

            _inOut = (EInOut) Enum.Parse(typeof (EInOut), (string) radioButton.Content);
        }

        private enum EInOut
        {
            // ReSharper disable InconsistentNaming
            Unchanged,
            Indoor,
            Outdoor,
            // ReSharper restore InconsistentNaming
        }

        #endregion Events

        private void UpdateMaterialsListView()
        {
            MaterialListView.Items.Clear();
            List<string> allMaterials = new List<string>(Cell.Materials).Concat(MapCell.UnknownMaterials).OrderBy(material => material).Distinct().ToList();
            foreach (string material in allMaterials)
            {
                ListViewItem listViewItem = new ListViewItem {Name = material, Content = material};
                MaterialListView.Items.Add(listViewItem);
                if (material == "Dirt") MaterialListView.SelectedItem = listViewItem;
            }
        }

        private void UpdateMapCanvas()
        {
            MapCanvas.Width = _prisonFile.NumCellsX*_tileSize;
            MapCanvas.Height = _prisonFile.NumCellsY*_tileSize;

            MapCanvas.Children.Clear();
            foreach (Cell cell in _prisonFile.Cells)
                MapCanvas.Children.Add(new MapCell(cell) {Width = _tileSize, Height = _tileSize, Material = cell.Material});
        }

        private void HandleHitTestResult(HitTestResult hitTestResult)
        {
            if (hitTestResult == null) return;

            DependencyObject element = FindVisualParent<MapCell>(hitTestResult.VisualHit as UIElement);
            if (element == null) return;

            if (_fillMode == EFIllMode.BUCKET)
            {
                IsEnabled = false;
                OverlayGrid.Visibility = Visibility.Visible;
                OverlayTextBlock.Text = "Working";
            }

            #region BackgroundWorker

            BackgroundWorker backgroundWorker = new BackgroundWorker
                                                {
                                                    WorkerReportsProgress = false,
                                                    WorkerSupportsCancellation = false
                                                };
            backgroundWorker.DoWork += (o, args) =>
                                       {
                                           Cell cell = (Cell) ((object[]) args.Argument)[0];
                                           string material = (string) ((object[]) args.Argument)[1];

                                           List<Point> points = new List<Point>();
                                           GetNearByMatchingCells(cell, material, points);

                                           args.Result = new object[] {points, material};
                                       };
            backgroundWorker.RunWorkerCompleted += (o, args) =>
                                                   {
                                                       List<Point> points = (List<Point>) ((object[]) args.Result)[0];
                                                       string material = (string) ((object[]) args.Result)[1];

                                                       foreach (Point point in points)
                                                       {
                                                           MapCell mapCell = MapCanvas.Children.OfType<MapCell>().FirstOrDefault(m => m.X == (int) point.X && m.Y == (int) point.Y);
                                                           if (mapCell == null) continue;

                                                           switch (_inOut)
                                                           {
                                                               case EInOut.Unchanged:
                                                                   break;
                                                               case EInOut.Indoor:
                                                                   mapCell.Indoors = true;
                                                                   break;
                                                               case EInOut.Outdoor:
                                                                   mapCell.Indoors = false;
                                                                   break;
                                                           }
                                                           mapCell.Material = material;
                                                       }

                                                       OverlayGrid.Visibility = Visibility.Collapsed;
                                                       IsEnabled = true;
                                                   };
            backgroundWorker.RunWorkerAsync(new[] {((MapCell) element).Cell, ((ListViewItem) MaterialListView.SelectedItem).Content});

            #endregion BackgroundWorker
        }

        private void GetNearByMatchingCells(Cell cell, string material, List<Point> points, string oldMaterial = "")
        {
            // make sure we only visit each cell once
            Point point = points.FirstOrDefault(p => (int) p.X == cell.X && (int) p.Y == cell.Y);
            if (point != default(Point)) return;

            bool indoors = cell.Indoors;
            switch (_inOut)
            {
                case EInOut.Unchanged:
                    break;
                case EInOut.Indoor:
                    indoors = true;
                    break;
                case EInOut.Outdoor:
                    indoors = false;
                    break;
            }

            // don't bother trying to replace stuff that doesn't change
            if (cell.Material == material && indoors == cell.Indoors) return;

            points.Add(new Point(cell.X, cell.Y)); // add current cell

            // anything past this is for bucket fill
            if (_fillMode != EFIllMode.BUCKET) return;

            // if we're the first cell let's mark the old material as ours
            if (string.IsNullOrEmpty(oldMaterial)) oldMaterial = cell.Material;

            int x = cell.X;
            int y = cell.Y;

            Cell tempCell;

            tempCell = _prisonFile.Cells.FirstOrDefault(c => c.Material == oldMaterial && c.X == (x - 1) && c.Y == y);
            if (tempCell != null)
            { // left
                GetNearByMatchingCells(tempCell, material, points, oldMaterial);
            }

            tempCell = _prisonFile.Cells.FirstOrDefault(c => c.Material == oldMaterial && c.X == (x + 1) && c.Y == y);
            if (tempCell != null)
            { // right
                GetNearByMatchingCells(tempCell, material, points, oldMaterial);
            }

            tempCell = _prisonFile.Cells.FirstOrDefault(c => c.Material == oldMaterial && c.X == x && c.Y == (y - 1));
            if (tempCell != null)
            { // top
                GetNearByMatchingCells(tempCell, material, points, oldMaterial);
            }

            tempCell = _prisonFile.Cells.FirstOrDefault(c => c.Material == oldMaterial && c.X == x && c.Y == (y + 1));
            if (tempCell != null)
            { // bottom
                GetNearByMatchingCells(tempCell, material, points, oldMaterial);
            }
        }

        private void LoadFile(string filename)
        {
            Title = Path.GetFileName(filename);

            IsEnabled = false;
            OverlayGrid.Visibility = Visibility.Visible;
            OverlayTextBlock.Text = "Loading" + Environment.NewLine + Path.GetFileName(filename);

            #region BackgroundWorker

            BackgroundWorker backgroundWorker = new BackgroundWorker
                                                {
                                                    WorkerReportsProgress = false,
                                                    WorkerSupportsCancellation = false
                                                };
            backgroundWorker.DoWork += (o, args) =>
                                       {
                                           PrisonFile prisonFile = new PrisonFile(args.Argument as string);
                                           args.Result = prisonFile;

                                           GC.Collect();
                                       };
            backgroundWorker.RunWorkerCompleted += (o, args) =>
                                                   {
                                                       _prisonFile = (PrisonFile) args.Result;

                                                       MapCell.UnknownMaterials =
                                                           _prisonFile.Cells.Select(cell => cell.Material).Distinct()
                                                               .Where(material => !string.IsNullOrEmpty(material) && !Cell.Materials.Contains(material)).ToList();

                                                       MapCell.PreloadMaterialBitmaps();
                                                       UpdateMaterialsListView();
                                                       UpdateMapCanvas();

                                                       DataContext = _prisonFile;

                                                       OverlayGrid.Visibility = Visibility.Collapsed;
                                                       SaveMenu.IsEnabled = SaveAsMenu.IsEnabled = MainTabControl.IsEnabled = true;
                                                       IsEnabled = true;
                                                   };
            backgroundWorker.RunWorkerAsync(filename);

            #endregion BackgroundWorker
        }

        private void SaveFile(string filename)
        {
            Title = Path.GetFileName(filename);

            IsEnabled = false;
            OverlayGrid.Visibility = Visibility.Visible;
            OverlayTextBlock.Text = "Saving" + Environment.NewLine + Path.GetFileName(filename);

            #region BackgroundWorker

            BackgroundWorker backgroundWorker = new BackgroundWorker
                                                {
                                                    WorkerReportsProgress = false,
                                                    WorkerSupportsCancellation = false
                                                };
            backgroundWorker.DoWork += (o, args) =>
                                       {
                                           File.WriteAllText((string) args.Argument, _prisonFile.Output);
                                           _prisonFile.FileName = (string) args.Argument;

                                           GC.Collect();
                                       };
            backgroundWorker.RunWorkerCompleted += (o, args) =>
                                                   {
                                                       OverlayGrid.Visibility = Visibility.Collapsed;
                                                       IsEnabled = true;
                                                   };
            backgroundWorker.RunWorkerAsync(filename);

            #endregion BackgroundWorker
        }

        public static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                    return correctlyTyped;

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
    }
}
