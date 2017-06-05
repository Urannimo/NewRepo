using System;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FastTranslateApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LangDict languages;

        public MainWindow()
        {
            InitializeComponent();

            languages = new LangDict();
        }

        private void mainMenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void translateButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(resultTextBox.Document.ContentStart, resultTextBox.Document.ContentEnd);

            HttpWebRequest newReq = WebRequest.CreateHttp(appSettings.apiBaseTranslate + appSettings.getApikey() + "&text=" + sourceTextBox.Text + "&lang=ru-" + ((KeyValuePair<string, string>)langListTo.SelectedItem).Key);

            dynamic buf = JsonConvert.DeserializeObject<dynamic>(Sender.sendRequest(newReq));

            textRange.Text = (string)buf.text[0];
        }

        private void getLangButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(resultTextBox.Document.ContentStart, resultTextBox.Document.ContentEnd);

            var response = getLanguage(sourceTextBox.Text);
            dynamic buf = JsonConvert.DeserializeObject<dynamic>(response);

            langListTo.SelectedValue = (string)buf.lang;
        }

        private string getLanguage(string baseText)
        {
            try
            {
                HttpWebRequest newReq = WebRequest.CreateHttp(appSettings.apiBaseDetect + appSettings.getApikey() + "&text=" + baseText);
                return Sender.sendRequest(newReq);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private void swapButton_Click(object sender, RoutedEventArgs e)
        {
            var textRange = new TextRange(resultTextBox.Document.ContentStart, resultTextBox.Document.ContentEnd);

            var buf = textRange.Text;
            textRange.Text = sourceTextBox.Text;
            sourceTextBox.Text = buf;
        }
    }
}
