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
using UDPService;

namespace WPF_UDPServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _counter = 0;

        private UDPServer _server = new UDPServer();

        public MainWindow()
        {
            InitializeComponent();

            RunServer();
        }
        
        private void RunServer()
        {
            _server.Initialize();
            _server.OnMassegeRecieved += OnMessageRecieved;
            _server.StartMessageLoop();
        }

        private Task OnMessageRecieved(string message)
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    TextBlock tb = new TextBlock();
                    tb.FontSize = 10;
                    //tb.FontWeight = FontWeights.Bold;
                    tb.Text = message;
                    
                    Border border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1);
                    border.Margin = new Thickness(0, 5, 5, 0);

                    border.Child = tb;
                    
                    sp_messagesShower.Children.Add(border);
                });     
            });
        }

        private void Btn_addMsg_Click(object sender, RoutedEventArgs e)
        {
            _counter++;

            TextBlock tb = new TextBlock();
            tb.FontSize = 20;
            //tb.FontWeight = FontWeights.Bold;
            tb.Text = _counter.ToString();
            sp_messagesShower.Children.Add(tb);
        }
    }
}
