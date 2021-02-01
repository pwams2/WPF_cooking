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
using System.Data;


namespace Projet_BDD
{
    /// <summary>
    /// Logique d'interaction pour admin.xaml
    /// </summary>
    public partial class admin : Window
    {
        public admin()
        {
            InitializeComponent();
        }

        string connexionString = "SERVER=localhost;PORT=3306;" +
                                        "DATABASE=projet1;" +
                                        "UID=root;PASSWORD=Paris123.";


        private void Resultat_Click(object sender, RoutedEventArgs e) 
        {
            Bdd bdd = new Bdd();
            MeilleurCdr.Text = bdd.MeilleurCdr();

            MySqlConnection connection = new MySqlConnection(connexionString);
            connection.Open();

            MySqlCommand commande = new MySqlCommand("select Nom_Recette,Type_Recette,Id from recette order by NbVente desc limit 5;", connection); // requete SQL 
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(commande);
            DataTable dt = new DataTable(""); // on initialise un tableau 
            dataAdapter.Fill(dt); // on ajoute dans le tableau 
            TopRecette.ItemsSource = dt.DefaultView;
            dataAdapter.Update(dt);


        }  // Fonction avec les résultats du meilleur cdr et le top 5 des recettes 

        private void AffichageRecette_Click(object sender, RoutedEventArgs e) // fonction qui permet d'afficher les noms des recettes dans la liste 
        {
            liste.Items.Clear();

            MySqlConnection connection = new MySqlConnection(connexionString);
            connection.Open();

            MySqlCommand commande = new MySqlCommand("select Nom_Recette from recette", connection); // requete SQL 
            MySqlDataReader lecture;
            lecture = commande.ExecuteReader();
            while (lecture.Read()) // lecture 
            {
                string currentRowAsString = "";

                string valueAsString = lecture.GetValue(0).ToString();
                currentRowAsString += valueAsString;

                liste.Items.Add(currentRowAsString); // puis on ajoute dans la liste 


            }
        }

        private void SupprimerRecette_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connexionString);
            
            try
            {
               
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "delete from recette where Nom_recette='" + liste.SelectedItem.ToString() + "';";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                //
            }
            

            try
            {

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "delete from constitue where Nom_recette='" + liste.SelectedItem.ToString() + "';";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                //
            }
            

        } // Lors de la suppression d'une recette il faut supprimer à la fois dans la table Recette et dans la table constitue

        private void RetourAccueil_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        } // fonction pour retourner vers la page d'accueil 

        private void SupprimerCdr_Click(object sender, RoutedEventArgs e) // Lors de la suppression d'un cdr il faut a la fois le mettre en client, supprimer ces recettes et ainsi supprimer la recette dans la table constitue
        {
            
            string choix = listeCdr.SelectedItem.ToString();
            Bdd bdd = new Bdd();
            bdd.SupprimerConstitue(choix);
            bdd.SupprimerRecette(choix);
            bdd.SupprimerCdr(choix);
            bdd.MiseAjourClient(choix);

        } 

        private void AffichageCdr_Click(object sender, RoutedEventArgs e)
        {
            listeCdr.Items.Clear();

            MySqlConnection connection = new MySqlConnection(connexionString);
            connection.Open();

            MySqlCommand commande = new MySqlCommand("select Id from cdr", connection); // requete SQL 
            MySqlDataReader lecture;
            lecture = commande.ExecuteReader();
            while (lecture.Read()) // lecture 
            {
                string currentRowAsString = "";

                string valueAsString = lecture.GetValue(0).ToString();
                currentRowAsString += valueAsString;

                listeCdr.Items.Add(currentRowAsString); // puis on ajoute dans la liste 


            }
        } // fonction qui permet d'afficher la liste des cdr dans une liste 

        private void AjourProduit_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(refe.Text) > 6 || nomproduit.Text == string.Empty || categorieproduit.Text == string.Empty || stockmin.Text == string.Empty || stockmax.Text == string.Empty || refe.Text == string.Empty)
            {
                MessageBox.Show("Les références produits sont comprises entre 1 et 6");
            }
            else
            if( nomproduit.Text == string.Empty || categorieproduit.Text == string.Empty || stockmin.Text == string.Empty || stockmax.Text == string.Empty || refe.Text == string.Empty || unitepro.Text == string.Empty)
            {
                MessageBox.Show("Veuillez remplir toutes les données obligatoires");
            }
            else
            {
                string nom = nomproduit.Text;
                string categorie = categorieproduit.Text;
                int stockActuel = Convert.ToInt32(stockactuel.Text);
                int stockMin = Convert.ToInt32(stockmin.Text);
                int stockMax = Convert.ToInt32(stockmax.Text);
                int reference = Convert.ToInt32(refe.Text);
                string unite = unitepro.Text;
                Ingredient ingredient = new Ingredient(nom, categorie, stockActuel, stockMax, stockMin, reference, unite);
                Bdd bdd = new Bdd();
                bdd.AddProduit(ingredient);
                MessageBox.Show("Bien enregistré");
            }
        }
    }
}
