using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBlazor
{
    public class SharedService
    {
        private string _data;

        public string Data
        {
            get => _data;
            set
            {
                _data = value;
                OnDataChanged?.Invoke();
            }
        }

        public event Action OnDataChanged;
    }

    public partial class MainWindow : Window
    {
        private SharedService _sharedService;

        public MainWindow()
        {
            InitializeComponent();

            ServiceCollection serviceCollection = new();
            serviceCollection.AddWpfBlazorWebView();

            SharedService sharedService = new();
            sharedService.Data = "James";

            serviceCollection.AddSingleton(sharedService);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _sharedService = serviceProvider.GetRequiredService<SharedService>();
            _sharedService.OnDataChanged += _sharedService_OnDataChanged;
            Resources.Add("services", serviceProvider);
        }

        private void _sharedService_OnDataChanged()
        {
            test.Text = _sharedService.Data;
        }
    }
}