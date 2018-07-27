using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using HTTPRequest;

namespace TestRequest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RequestSync request;
        public static string baseURL = "http://stor.skytest.purity.no/api/";
        private static string token;
        public MainWindow()
        {
            InitializeComponent();
            getToken();
        }
        private void getToken()
        {
            request = new RequestSync(HttpMethod.Post, baseURL, "v1/access/connect");
            Dictionary<string, string> body = new Dictionary<string, string>
                {
                    { "phrase", "phrase1" }
                };
            request.setBody(body);
            request.send();
            token = (string)(request.responseJsonBody())["access-token"];
            textBoxResponse.Text = request.infoAboutCurrentRequest();
        }
        private void buttonPOST_Click(object sender, RoutedEventArgs e)
        {
            request.setUri("v1/storage/system");
            request.setHTTPMethod(HttpMethod.Post);
            Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "access-token", token },
                    { "auth_user", "1" }
                };
            Dictionary<string, string> body = new Dictionary<string, string>
                {
                    { "name", textBoxtSS.Text },
                    { "type_id", "1" },
                    { "class_id", "1" }
                };
            request.setData(headers, null, body);
            request.send();
            textBoxResponse.Text = request.infoAboutCurrentRequest();
        }        
        private void buttonGET_Click(object sender, RoutedEventArgs e)
        {
            request.setUri("v1/storage/system/index");
            request.setHTTPMethod(HttpMethod.Get);
            Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "access-token", token },
                    { "auth_user", "1" }
                };
            request.setHeaders(headers);
            request.send();
            textBoxResponse.Text = request.infoAboutCurrentRequest();
        }

        private void buttonDEL_Click(object sender, RoutedEventArgs e)
        {
            request.setUri("/v1/storage/system/"+textBoxtSS.Text);
            request.setHTTPMethod(HttpMethod.Delete);
            Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "access-token", token },
                    { "auth_user", "1" }
                };
            request.setHeaders(headers);
            request.send();
            textBoxResponse.Text = request.infoAboutCurrentRequest();
        }
    }
}
