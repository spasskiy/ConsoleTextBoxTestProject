using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;



namespace ConsoleTextBoxTestProject
{
    public partial class ConsoleTextBox : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Скроллбар текстового поля
        /// </summary>
        private ScrollBar? _scrollBar;

        /// <summary>
        /// DependencyProperty для Text
        /// </summary>
        public static readonly DependencyProperty TextProperty;
        
        static ConsoleTextBox()
        {
            TextProperty = DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(ConsoleTextBox));
        }
           

        //private string? _text;
        /// <summary>
        /// Текст в текстовом поле
        /// </summary>
        public string? Text
        {
            get { return (string)GetValue(TextProperty); }
            set {
                SetValue(TextProperty, value);
                //SetOptions(nameof(Text), ref _text, value);
            } 
        }

        /// <summary>
        /// Конструктор класса ConsoleTextBox
        /// </summary>
        public ConsoleTextBox()
        {
            DataContext = this;
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        /// <summary>
        /// Обработчик события загрузки окна
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _scrollBar = VisualTreeFinder.FindChild<ScrollBar>(textBox);
            if (_scrollBar != null)
            {
                _scrollBar.IsVisibleChanged += ScrollBar_IsVisibleChanged;
            }
        }

        /// <summary>
        /// Обработчик события изменения видимости скроллбара
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void ScrollBar_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_scrollBar is not null)
            {
                if (_scrollBar.IsVisible)
                {
                    clearButton.Margin = new Thickness(0, 4, 22, 0);
                }
                else
                {
                    clearButton.Margin = new Thickness(0, 4, 4, 0);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки очистки текстового поля
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Text);
            Text = string.Empty;            
        }

        #region INotifyPropertyChanged
        /// <summary>
        /// Событие, возникающее при изменении свойства
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызывает событие PropertyChanged
        /// </summary>
        /// <param name="e">Аргументы события</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Устанавливает значение свойства и вызывает событие PropertyChanged
        /// </summary>
        /// <typeparam name="T">Тип свойства</typeparam>
        /// <param name="property">Имя свойства</param>
        /// <param name="variable">Ссылка на переменную свойства</param>
        /// <param name="value">Новое значение свойства</param>
        protected void SetOptions<T>(string property, ref T variable, T value)
        {
            variable = value;
            OnPropertyChanged(new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
