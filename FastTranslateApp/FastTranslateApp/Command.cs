using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastTranslateApp
{
    public static class Command
    {
        public static readonly RoutedUICommand Translate = new RoutedUICommand("Translate text", "Translate", typeof(MainWindow));
    }
}
