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
using MySql.Data.MySqlClient;

namespace Projet_BDD
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Window
    {
        public Accueil()
        {
            InitializeComponent();
        }
        string connexionString = "SERVER=localhost;PORT=3306;" +
                                       "DATABASE=projet1;" +
                                       "UID=root;PASSWORD=Paris123.";  // Adresse de ma base de données

        private void VoirPlat_Click(object sender, RoutedEventArgs e) // fonction qui permet de voir les plats disponible 
        {
            liste.Items.Clear();


            MySqlConnection connection = new MySqlConnection(connexionString); // Création de la connexion avec ma base de données 
            connection.Open();

            MySqlCommand commande = new MySqlCommand("select Nom_Recette from recette", connection); // Requete SQL 
            MySqlDataReader lecture;
            lecture = commande.ExecuteReader();
            while (lecture.Read())  // lecture de toutes les recettes puis on les ajoute dans une liste 
            {
                string currentRowAsString = "";

                string valueAsString = lecture.GetValue(0).ToString();
                currentRowAsString += valueAsString;

                liste.Items.Add(currentRowAsString);

            }
        }

        private void Valider_Click(object sender, RoutedEventArgs e) // bouton de validation,lors de la validation plusieurs fonctions se passent :  
        {
            int prixtotal = 0;
            int sous_total1 = 0;
            int sous_total2 = 0;
            int sous_total3 = 0;

            Bdd bdd = new Bdd();


            if (liste.SelectedItems.Count == 1)
            {
                int prix1 = bdd.PrixPlat(Choix1.Text);
                int q1 = Convert.ToInt32(Quantite1.Text);

                sous_total1 = bdd.OperationFinale(Choix1.Text, prix1, q1) * q1;

                bdd.MiseAjourStock(Choix1.Text, q1);

                bdd.MiseAJourSolde(Choix1.Text, q1);

                bdd.MiseAjourNbVenteCdr(Choix1.Text, q1);
            }
            else if (liste.SelectedItems.Count == 2)
            {

                int prix1 = bdd.PrixPlat(Choix1.Text);
                int prix2 = bdd.PrixPlat(Choix2.Text);
                int q1 = Convert.ToInt32(Quantite1.Text);
                int q2 = Convert.ToInt32(Quantite2.Text);

                sous_total1 = bdd.OperationFinale(Choix1.Text, prix1, q1) * q1;
                sous_total2 = bdd.OperationFinale(Choix2.Text, prix2, q2) * q2;

                bdd.MiseAjourStock(Choix1.Text, q1);
                bdd.MiseAjourStock(Choix2.Text, q2);

                bdd.MiseAJourSolde(Choix1.Text, q1);
                bdd.MiseAJourSolde(Choix2.Text, q2);

                bdd.MiseAjourNbVenteCdr(Choix1.Text, q1);
                bdd.MiseAjourNbVenteCdr(Choix2.Text, q2);

            }
            else if (liste.SelectedItems.Count == 3)
            {
                int prix1 = bdd.PrixPlat(Choix1.Text);
                int prix2 = bdd.PrixPlat(Choix2.Text);
                int prix3 = bdd.PrixPlat(Choix3.Text);
                int q1 = Convert.ToInt32(Quantite1.Text);
                int q2 = Convert.ToInt32(Quantite2.Text);
                int q3 = Convert.ToInt32(Quantite3.Text);

                sous_total1 = bdd.OperationFinale(Choix1.Text, prix1, q1) * q1;
                sous_total2 = bdd.OperationFinale(Choix2.Text, prix2, q2) * q2;
                sous_total3 = bdd.OperationFinale(Choix3.Text, prix3, q3) * q3;


                bdd.MiseAjourStock(Choix1.Text, q1);
                bdd.MiseAjourStock(Choix2.Text, q2);
                bdd.MiseAjourStock(Choix3.Text, q3);

                bdd.MiseAJourSolde(Choix1.Text, q1);
                bdd.MiseAJourSolde(Choix2.Text, q2);
                bdd.MiseAJourSolde(Choix3.Text, q3);

                bdd.MiseAjourNbVenteCdr(Choix1.Text, q1);
                bdd.MiseAjourNbVenteCdr(Choix2.Text, q2);
                bdd.MiseAjourNbVenteCdr(Choix3.Text, q3);
            }

            prixtotal = sous_total1 + sous_total2 + sous_total3;
            PrixTotal.Text = prixtotal.ToString();
            bdd.Xml();
            bdd.XML_Fournisseur();

            if (Choix1.Text == String.Empty && Choix2.Text == String.Empty && Choix2.Text == String.Empty) // Si aucun choix n est fait alors aucune commande
            {
                MessageBox.Show("Aucune commande effectuée");
            }
            else
            {

                MessageBox.Show("Vos commandes ont bien été prise en compte : " + Choix1.Text + " " + Choix2.Text + " " + Choix3.Text + " et vous avez payé :" + prixtotal + " cook");
            }
        }

        private void ChoixPlat_Click(object sender, RoutedEventArgs e)
        {

            if (liste.SelectedItems.Count == 0)
            {
                Choix1.Text = null;
                Choix2.Text = null;
                Choix3.Text = null;
            }
            else if (liste.SelectedItems.Count == 1)
            {
                Choix1.Text = liste.SelectedItems[0].ToString();
                Choix2.Text = null;
                Choix3.Text = null;
            }
            else if (liste.SelectedItems.Count == 2)
            {
                Choix1.Text = liste.SelectedItems[0].ToString();
                Choix2.Text = liste.SelectedItems[1].ToString();
                Choix3.Text = null;

            }
            else if (liste.SelectedItems.Count == 3)
            {
                Choix1.Text = liste.SelectedItems[0].ToString();
                Choix2.Text = liste.SelectedItems[1].ToString();
                Choix3.Text = liste.SelectedItems[2].ToString();
            }

        }

        


        private void RetourAccueil_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
