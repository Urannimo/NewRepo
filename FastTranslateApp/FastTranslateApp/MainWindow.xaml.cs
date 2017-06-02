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
            KeyValuePair gg = langListFrom.SelectedItem;
            //textRange.Text = langListFrom.SelectedItem;
        }

        private void getLangButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(resultTextBox.Document.ContentStart, resultTextBox.Document.ContentEnd);

            var response = getLanguage(sourceTextBox.Text);
            dynamic buf = JsonConvert.DeserializeObject<dynamic>(response);

            textRange.Text = languages.getLanguageByCode((string)buf.lang);
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
    }
}
