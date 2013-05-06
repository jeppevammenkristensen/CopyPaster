using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;

namespace CopyPaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Clicked(object sender, RoutedEventArgs e)
        {
            var result = Clipboard.GetImage();
            if (result == null)
            {
                MessageBox.Show("No image");
                return;
            }

            var directorypath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Screendumps");
            var filePath = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";

            var directory = new DirectoryInfo(directorypath);
            if (!directory.Exists)
            {
                directory.Create();
            }
            
            using (var stream = new FileStream(Path.Combine(directorypath,filePath), FileMode.Create))
            {
                var encode = new JpegBitmapEncoder();
                encode.QualityLevel = 100;
                encode.Frames.Add(BitmapFrame.Create(result));
                encode.Save(stream);
            }

            Clipboard.SetText(Path.Combine(directorypath,filePath));
            
        }
    }
}
