using System;
using System.Collections.Generic;
using System.Linq;
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

namespace audio_recorder
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private MainWindow m_mainWindown;

        public SettingsWindow( MainWindow _main )
        {
            InitializeComponent();
            m_mainWindown = _main;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var colorDialog = new System.Windows.Forms.ColorDialog();

            if( colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                var color = colorDialog.Color;
                colorRect.Fill = new SolidColorBrush( color.ToMediaColor() );

                m_mainWindown.MainCurve = color;
            }
        }
    }

    public static class ColorConvertor
    {
        public static System.Windows.Media.Color ToMediaColor(
            this System.Drawing.Color _baseColor
        )
        {
            var newColor = new System.Windows.Media.Color();

            newColor.R = _baseColor.R;
            newColor.G = _baseColor.G;
            newColor.B = _baseColor.B;
            newColor.A = _baseColor.A;

            return newColor;
        }
    }
}
