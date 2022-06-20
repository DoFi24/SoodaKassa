using System.Windows;
using System.Windows.Input;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для SearchDialogWindow.xaml
    /// </summary>
    public partial class SearchDialogWindow : Window
    {
        public SearchDialogWindow()
        {
            InitializeComponent();
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                CheckProduct();
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }
        private void CheckProduct()
        {
           /* if (StatisticalResources.productListForSearch == null)
                return;

            DataRow[] res = StatisticalResources.productListForSearch.Select("barcode = '" + BarcodeLabel.Text.ToString().Trim() + "'");// '"+ BarcodeLabel + "'
            //MessageBox.Show(res.Count()+" "+ StatisticalResources.productListForSearch.Rows.Count+"\n"+ StatisticalResources.productListForSearch.Rows[4][2].ToString());
            if (res.Count() == 0)
            {
                return;
            }
            BarcodeLabel.Text = "";
            BarcodeLabel.IsReadOnly = true;
            ProductName.Opacity = 1;
            ProductName.FontSize = 14;
            ProductName.Text = res[0][1].ToString();
            ProductPrice.Visibility = Visibility.Visible;
            ProductName.Visibility = Visibility.Visible;
            ProductPrice.Opacity = 1;
            BarcodeImage.Visibility = Visibility.Collapsed;

            DateBaseOnly db = new DateBaseOnly();
            db.del += (x) =>
            {
                if (x.Rows.Count > 0)
                {
                    ProductPrice.Text = x.Rows[0][0].ToString() + " сом";

                    if (x.Rows[0][1].ToString() != "")
                    {
                        using (MemoryStream memstr = new MemoryStream(Convert.FromBase64String(x.Rows[0][1].ToString())))
                        {
                            BitmapImage b = new BitmapImage();
                            b.BeginInit();
                            b.CacheOption = BitmapCacheOption.OnLoad;
                            b.StreamSource = memstr;
                            b.EndInit();
                            ProductImage.Source = b;
                        }
                    }
                }
            };
            db.SoursData("SELECT t.sale_price, t.image FROM `products` AS t WHERE id=" + res[0][0].ToString());
*/
        }

        //bool flag = false;
        private void BarcodeLabel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           /* if (flag)
            {
                return;
            }
            BarcodeImage.Visibility = Visibility.Collapsed;
            ProductName.Visibility = Visibility.Collapsed;
            flag = true;*/
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
