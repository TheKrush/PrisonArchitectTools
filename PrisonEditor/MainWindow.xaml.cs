using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using LostMinions.Functions;
using Microsoft.Win32;
using PrisonArchitect.PrisonFile;
using PrisonArchitect.PrisonFile.Blocks;
using Debug = System.Diagnostics.Debug;
using Visibility = System.Windows.Visibility;

namespace PrisonEditor
{
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int TileSize = 16;
        private readonly AssemblyInformation _assemblyInformation = new AssemblyInformation();
        private readonly BackgroundWorker _loadPrisonFile;
        private PrisonFile _prisonFile;

        public MainWindow()
        {
            InitializeComponent();

            Title = null;

            DispatcherTimer dispatcherTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 1)};
            dispatcherTimer.Tick += MemoryUsageTimerTick;
            dispatcherTimer.Start();
            MemoryUsageTimerTick(null, null);

            MapCell.PreloadMaterialBitmaps();
            MaterialImage.Source = MapCell.MaterialBitmapImage["Dirt"];
            UpdateMaterialsListView();

            _loadPrisonFile = new BackgroundWorker {WorkerReportsProgress = false, WorkerSupportsCancellation = false};
            _loadPrisonFile.DoWork += (sender, args) =>
                                          {
                                              PrisonFile prisonFile = new PrisonFile(args.Argument as string);
                                              args.Result = prisonFile;

                                              GC.Collect();
                                          };
            _loadPrisonFile.RunWorkerCompleted += LoadPrisonFileOnRunWorkerCompleted;
        }

        private void MemoryUsageTimerTick(object sender, EventArgs eventArgs)
        {
            MemoryUsage.Text = "Memory: " + string.Format("{0:0.00} MB", GC.GetTotalMemory(true)/1024.0/1024.0);
        }

        private void LoadPrisonFileOnRunWorkerCompleted(object sender,
                                                        RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            _prisonFile = runWorkerCompletedEventArgs.Result as PrisonFile;

            Debug.Assert(_prisonFile != null, "_prisonFile != null");

            MapCell.UnknownMaterials =
                _prisonFile.Cells.Blocks.Cast<Cells.Cell>().Select(cell => cell.Material).Distinct().Where(
                    material => !string.IsNullOrEmpty(material) && !MapCell.Materials.Contains(material)).ToList();

            MapCell.PreloadMaterialBitmaps();
            UpdateMaterialsListView();
            UpdateMapCanvas();

            MapOverlayGrid.Visibility = Visibility.Collapsed;
            MapGrid.IsEnabled = IsEnabled = true;
        }

        private void UpdateMaterialsListView()
        {
            MaterialListView.Items.Clear();
            List<string> allMaterials =
                new List<string>(MapCell.Materials).Concat(MapCell.UnknownMaterials).OrderBy(material => material).
                    Distinct().ToList();
            foreach (string material in allMaterials)
            {
                ListViewItem listViewItem = new ListViewItem { Name = material, Content = material };
                MaterialListView.Items.Add(listViewItem);
                if (material == "Dirt") MaterialListView.SelectedItem = listViewItem;
            }
        }

        private void UpdateMapCanvas()
        {
            MapCanvas.Width = _prisonFile.NumCellsX*TileSize;
            MapCanvas.Height = _prisonFile.NumCellsY*TileSize;

            MapCanvas.Children.Clear();
            foreach (Cells.Cell cell in _prisonFile.Cells.Blocks.Cast<Cells.Cell>())
                MapCanvas.Children.Add(new MapCell(cell) {Width = TileSize, Height = TileSize, Material = cell.Material});
        }

        private void MapCanvas_LeftButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Retrieve the coordinate of the mouse position.
            Point pt = e.GetPosition((UIElement) sender);

            // Perform the hit test against a given portion of the visual object tree.
            HitTestResult result = VisualTreeHelper.HitTest((UIElement) sender, pt);
            HandleHitTestResult(result);
        }

        private void MaterialListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView) sender;

            if (listView.SelectedItem == null)
                MaterialImage.Source = null;
            else
            {
                string material = (string) ((ListViewItem) listView.SelectedItem).Content;
                MaterialImage.Source = MapCell.MaterialBitmapImage.ContainsKey(material)
                                           ? MapCell.MaterialBitmapImage[material]
                                           : MapCell.MaterialBitmapImage["Unknown"];
            }
        }

        private void MapCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Retrieve the coordinate of the mouse position.
            Point pt = e.GetPosition((UIElement) sender);

            // Perform the hit test against a given portion of the visual object tree.
            HitTestResult result = VisualTreeHelper.HitTest((UIElement) sender, pt);

            if (e.LeftButton == MouseButtonState.Pressed)
                HandleHitTestResult(result);
        }

        private void HandleHitTestResult(HitTestResult hitTestResult)
        {
            if (hitTestResult == null) return;

            DependencyObject element = hitTestResult.VisualHit;
            while (element != null && !(element is MapCell))
                element = VisualTreeHelper.GetParent(element);

            if (element != null)
                ((MapCell) element).Material = (string) ((ListViewItem) MaterialListView.SelectedItem).Content;
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

        private void MenuItemFileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
                                                {
                                                    FileName = "default",
                                                    DefaultExt = ".prison",
                                                    Filter = "Prison documents (.prison)|*.prison",
                                                    InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Introversion\Prison Architect\saves\")
                                                };

            // Show open file dialog box 
            bool? result = openFileDialog.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string filename = openFileDialog.FileName;

                Title = Path.GetFileName(filename);

                IsEnabled = false;
                MapOverlayGrid.Visibility = Visibility.Visible;
                MapOverlayTextBlock.Text = "Loading" + Environment.NewLine + Path.GetFileName(filename);
                _loadPrisonFile.RunWorkerAsync(filename);
            }
        }

        private void MenuItemFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItemFileSave_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(_prisonFile.FileName, _prisonFile.Output);
        }

        private void MenuItemFileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = Path.GetFileNameWithoutExtension(_prisonFile.FileName),
                DefaultExt = ".prison",
                Filter = "Prison documents (.prison)|*.prison",
                InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Introversion\Prison Architect\saves\")
            };

            // Show open file dialog box 
            bool? result = saveFileDialog.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string filename = saveFileDialog.FileName;

                File.WriteAllText(filename, _prisonFile.Output);
            }
        }
    }
}