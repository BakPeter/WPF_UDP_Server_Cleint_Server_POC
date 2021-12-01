using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace WPF_UDPClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UDPClient _client = new UDPClient();

        public MainWindow()
        {
            InitializeComponent();

            RunClient();
        }

        private void RunClient()
        {
            _client.Initialize(IPAddress.Loopback, UDPServer.PORT);
        }

        private async Task SendMessageAsync()
        {
            var str = tbox_msgInput.Text;
            var clientName = tbox_clientName.Text;
            var time = DateTime.Now;
            var msg = $"{clientName}\n{time}\n{str}";

            await _client.Send(Encoding.UTF8.GetBytes(msg));
            //MessageBox.Show(msg);
        }

        private void Btn_sendMsg_Click(object sender, RoutedEventArgs e)
        {
            _ = SendMessageAsync();
        }
    }
}
