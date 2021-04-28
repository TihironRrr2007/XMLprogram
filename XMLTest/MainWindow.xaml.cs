using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XMLTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly OpenFileDialog _ofd;
        public readonly SaveFileDialog _sfd;
        private bool _Save = true;

        private Color _dataColor;
        private BindingList<Line> _DataLines;

        public MainWindow()
        {
            InitializeComponent();

            _ofd = new OpenFileDialog { Filter = "xml (*.xml)|*.xml|All files (*.*)|*.*" };
            _sfd = new SaveFileDialog { Filter = "xml (*.xml)|*.xml|All files (*.*)|*.*" };

            //var lines = new List<Line>
            //{
            //    new Line("Note-1", false),
            //    new Line("Note-2", true),
            //    new Line("Note-3", false)
            //};

            //Configs newConfig = new Configs(new Color(255, 255, 255, 255), lines);

            //FileConfig config = new FileConfig("Test", newConfig);
            //config.Save();


        }

        private void OpenMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (_ofd.ShowDialog() == true)
            {
                _Save = false;
                PathLabel.Content = _ofd.FileName;
                FileConfig configOpen = new FileConfig(_ofd.FileName);
                configOpen.Open();

                _DataLines = new BindingList<Line>();
                foreach (Line line in configOpen.Configs.lines)
                {
                    _DataLines.Add(line);
                }
                NoteDataGrid.ItemsSource = _DataLines;

                _dataColor = configOpen.Configs.backGroundColor;
            }
        }

        private void SaveMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (_sfd.ShowDialog() == true)
            {
                _Save = true;
                try
                {
                    Configs newConfig = new Configs(_dataColor, _DataLines);
                    FileConfig config = new FileConfig(_sfd.FileName, newConfig);
                    config.Save();
                    _Save = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_Save)
            {
                MessageBoxResult result = MessageBox.Show("Вы точно хотите закрыть документ? Он не сохранен",
                    "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes)
                {
                    return;
                }
            }
            NoteDataGrid.ItemsSource = _DataLines = null;
            _Save = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!_Save)
            {
                MessageBoxResult result = MessageBox.Show("Вы точно хотите выйти? Данные не сохранены",
                    "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void NewMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _DataLines = new BindingList<Line>();
            NoteDataGrid.ItemsSource = _DataLines;
            _Save = false;
            
        }
    }
}