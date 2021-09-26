using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Logiciel_Nettoyage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public DirectoryInfo winTemp;
        public DirectoryInfo appTemp;
        public DirectoryInfo winTest;
        int totalRemovedFiles;
        int totalRemovedDir;

        public MainWindow()
        {
            InitializeComponent();
            winTemp = new DirectoryInfo(@"C:\Windows\Temp");
            winTest = new DirectoryInfo(@"C:\Program Files (x86)");
            appTemp = new DirectoryInfo(System.IO.Path.GetTempPath());
        }

        // Calcul de la taille d'un dossier
        public long DirSize(DirectoryInfo dir)
        {

            foreach (FileInfo file in dir.GetFiles())
            {
                try
                {                    
                    totalRemovedFiles++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                    continue;
                }
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                try
                {
                    totalRemovedDir++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                    continue;
                }
            }
            return dir.GetFiles().Sum(fi => fi.Length) + dir.GetDirectories().Sum(di => DirSize(di));
        }

        // Vider un dossier

        public void ClearTempData(DirectoryInfo di)
        {
            foreach(FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                    Console.WriteLine(file.FullName);
                    totalRemovedFiles++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                    continue;
                }
            }

            foreach(DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                    Console.WriteLine(dir.FullName);
                    totalRemovedDir++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                    continue;
                }
            }
        }

        private void Button_update_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Votre logiciel est à jour", "Mise à jour",MessageBoxButton.OK, MessageBoxImage.Information);
            lastupdate.Content = DateTime.Now;
        }

        private void Button_history_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TO DO: Créer une page historique", "Mise à jour", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_historyleft_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TO DO: Créer une page historique", "Mise à jour", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_website_click(object sender, RoutedEventArgs e)
        {
            try 
            {
                Process.Start(new ProcessStartInfo("http://www.basketusa.com")
                {
                    UseShellExecute = true
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        private void Button_analyse_Click(object sender, RoutedEventArgs e)
        {
            AnalyseFolders();
        }

        public void AnalyseFolders()
        {
            cleanspace.Content = "Début de l'analyse...";
            long totalSize = 0;

            //totalSize += DirSize(winTemp) / 1048576;
            totalSize += DirSize(winTest) / 1048576;
            //totalSize += DirSize(appTemp) / 1048576;

            cleanspace.Content = totalSize + " Mb (" + totalRemovedDir + " Dossiers, " + totalRemovedFiles + " fichiers)";
            lastanalyse.Content = DateTime.Now;
        }
    }
}
