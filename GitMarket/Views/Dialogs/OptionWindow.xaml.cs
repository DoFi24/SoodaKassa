using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для OptionWindow.xaml
    /// </summary>
    public partial class OptionWindow : Window
    {
        public Action InBlurMain;
        public OptionWindow()
        {
            InitializeComponent();
            GetPrinters();
        }

        private void GetPrinters()
        {
            LocalPrintServer printServer = new LocalPrintServer();
            PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
            PrintDialog p = new PrintDialog();

            int o = 0;
            foreach (PrintQueue printer in printQueuesOnLocalServer)
            {
                TextBlock b = new TextBlock();
                b.Text = printer.Name;
                printers.Items.Add(b);
                if (p.PrintQueue.Name.ToString() == printer.Name.ToString())
                    printers.SelectedIndex = o;
                o++;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (printers.Text == string.Empty)
                return;

            myPrinters.SetDefaultPrinter(printers.Text.ToString());
            MessageBox.Show("Успешно сохранено!");
            InBlurMain();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InBlurMain();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
    public static class myPrinters
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);

    }
}
