using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public MainWindow()
        {
            InitializeComponent();

            Configs newConfig = new Configs()
            {
                Color = "RED",
                AppName = "MyApp",
                Port = 255,
                Logins = new[]
                {
                    "Logg1",
                    "Logg2",
                    "Admin",
                    "NoAdmin"
                }
            };

            FileConfig config = new FileConfig("Конфига", newConfig);
            config.Save();

            FileConfig configOpen = new FileConfig("C:\\test\\Конфига.xml");
            configOpen.Open();
            Configs КОНФИГ = configOpen.Configs;
        }
    }
}
