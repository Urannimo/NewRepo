using System;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Input;

namespace FastTranslateApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ICommand Translate { get; set; }

        private LangDict languages;

        private HotKey _hotkey;

        public MainWindow()
        {
            InitializeComponent();

            languages = new LangDict();

            Loaded += (s, e) =>
            {
                _hotkey = new HotKey(ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift, Keys.Left, this);
                _hotkey.HotKeyPressed += (k) => this.Show();
            };
        }

        private void mainMenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void translateButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(resultTextBox.Document.ContentStart, resultTextBox.Document.ContentEnd);

            HttpWebRequest newReq = WebRequest.CreateHttp(appSettings.apiBaseTranslate + appSettings.getApikey() + "&text=" + sourceTextBox.Text + "&lang=" + ((KeyValuePair<string, string>)langListFrom.SelectedItem).Key + "-" + ((KeyValuePair<string, string>)langListTo.SelectedItem).Key);

            dynamic buf = JsonConvert.DeserializeObject<dynamic>(Sender.sendRequest(newReq));

            textRange.Text = (string)buf.text[0];
        }

        private void getLangButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(resultTextBox.Document.ContentStart, resultTextBox.Document.ContentEnd);

            var response = getLanguage(sourceTextBox.Text);
            dynamic buf = JsonConvert.DeserializeObject<dynamic>(response);

            langListFrom.SelectedValue = (string)buf.lang;
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

            var bufText = textRange.Text;
            textRange.Text = sourceTextBox.Text;
            sourceTextBox.Text = bufText;

            var bufLang = ((KeyValuePair<string, string>)langListTo.SelectedItem).Key;
            langListTo.SelectedValue = ((KeyValuePair<string, string>)langListFrom.SelectedItem).Key;
            langListFrom.SelectedValue = bufLang;
        }

        private void FastCommandTranslate(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            translateButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
        }
    }
}
