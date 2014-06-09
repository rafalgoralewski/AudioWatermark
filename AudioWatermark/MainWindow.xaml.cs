using AudioWatermarkCore;
using AudioWatermarkCore.Domain;
using AudioWatermarkEcho;
using AudioWatermarkLSB;
using Microsoft.Win32;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AudioWatermark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WAVFile originalFile;
        private WAVFile encodedFile;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void mniReadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            
            WAVReader reader = new WAVReader();
            originalFile = reader.ReadFile(ofd.FileName);

            MessageBox.Show("Plik wczytany");
        }

        private void BtnLSB_Click(object sender, RoutedEventArgs e)
        {
            if (originalFile == null)
            {
                MessageBox.Show("Wczytaj plik");
                return;
            }

            if (tbxMessage.Text.Count() < 3)
            {
                MessageBox.Show("Wpisz wiadomość");
                return;
            }

            encodedFile = new LSBCoder().WriteMessageToFile(originalFile, tbxMessage.Text);

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.ShowDialog();

            WAVReader reader = new WAVReader();
            string filename = sfd.SafeFileName;
            reader.SaveFile(encodedFile, filename);
        }

        private void BtnEcho_Click(object sender, RoutedEventArgs e)
        {
            if (originalFile == null)
            {
                MessageBox.Show("Wczytaj plik");
                return;
            }

            if (tbxMessage.Text.Count() < 3)
            {
                MessageBox.Show("Wpisz wiadomość");
                return;
            }

            EchoCoder coder = new EchoCoder();

            encodedFile = coder.WriteMessageToFile(originalFile, tbxMessage.Text);



            /////////////
            string decodedMessage = coder.Decode(encodedFile, originalFile);
        }
    }
}
