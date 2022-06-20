using System.Windows;
using System.Windows.Input;

namespace GitMarket.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для CommentDialogWindow.xaml
    /// </summary>
    public partial class CommentDialogWindow : Window
    {
        public delegate void GetCommentDel(string comment);//комментарий возвращает только при принятии и нажатии кнопки ОК
        public event GetCommentDel? GetCommentEvent;
        public CommentDialogWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            else if (e.Key == Key.Return)
            {
                GetCommentEvent(CommentLabel.Text.ToString());
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetCommentEvent(CommentLabel.Text.ToString());
            Close();
        }
    }
}
