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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet_BDD
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Admin_Click(object sender, RoutedEventArgs e) //fonction qui permet d'aller vers la page gestionnaire de cooking 
        {
            admin administrateur = new admin();
            administrateur.Show();
            this.Hide();


        }

        private void Client_Click(object sender, RoutedEventArgs e) // fonction qui permet d'aller vers la page de connexion pour un client ou un cdr
        {
            PageConnexion connexion = new PageConnexion();
            connexion.Show();
            this.Hide();

        }

        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            Demo demo = new Demo();
            demo.Show();
            this.Hide();

        }
    }
}
