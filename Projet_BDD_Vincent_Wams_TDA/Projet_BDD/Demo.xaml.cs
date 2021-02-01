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
    /// Logique d'interaction pour Demo.xaml
    /// </summary>
    public partial class Demo : Window
    {
        public Demo()
        {
            InitializeComponent();
        }
        string connexionString = "SERVER=localhost;PORT=3306;" +
                                       "DATABASE=projet1;" +
                                       "UID=root;PASSWORD=Paris123.";
        

        private void Resultat_Click(object sender, RoutedEventArgs e)
        {
            Bdd bdd = new Bdd();
            int nbrclient = bdd.NbrClient() ;
            int nbrcdr = bdd.NbrCdr();
            int nbrrecette = bdd.NbrRecette();
            nbrClient.Text = nbrclient.ToString();
            nbrCdr.Text = nbrcdr.ToString();
            nbrRecette.Text = nbrrecette.ToString();

            listeproduits.Items.Clear();
            int[] stock_actuel_demo = bdd.LireStockActuel();
            int[] stock_min_demo = bdd.LireStockMin();
            string[] produits_demo;
            MySqlConnection connection = new MySqlConnection(connexionString);
            List<string> result = new List<string>();

            try
            {

                connection.Open();
                MySqlCommand verif = new MySqlCommand("select Nom_Produit from Produit ;", connection);
                MySqlDataReader myReader;
                myReader = verif.ExecuteReader();
                
                using (var reader = myReader)
                {
                    while (reader.Read())
                        result.Add(reader.GetString(0));
                }
            }
            catch
            {
                //
            }
            connection.Close();

            produits_demo = result.ToArray();
            for (int i = 0; i < produits_demo.Length; i++)
            {
                if (stock_actuel_demo[i] <= 2 * stock_min_demo[i])
                {
                    listeproduits.Items.Add(produits_demo[i]);
                }
            }
            connection.Open();

            MySqlCommand commande = new MySqlCommand("select Id,NbVente_Cdr from cdr order by NbVente_Cdr desc;", connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(commande);
            DataTable dt = new DataTable("Id", "NbVente_Cdr");
            dataAdapter.Fill(dt);
            dataCdr.ItemsSource = dt.DefaultView;
            dataAdapter.Update(dt);

            connection.Close();

        } // lors de l'appuie sur le bouton résultat : nous voyons les résultats du nombre de clients, de cdr, de recettes, ainsi qu'une liste de tous les ingrédients 

        private void Produitselectionne_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connexionString);
            if (listeproduits.SelectedItem == null) // si la liste est nulle 
            {
                MessageBox.Show("Vous devez cliquer sur Résultats");
            }
            else
            {
                string choix = listeproduits.SelectedItem.ToString();
                connection.Open();

                MySqlCommand commande = new MySqlCommand("select Nom_recette,quantite from constitue where Nom_Produit='" + choix + "'", connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(commande); // cela permet de récupérer ce qui y a dans la base de données et ensuite ajouter dans le datatable 
                DataTable dt = new DataTable("Nom_recette", "quantite");
                dataAdapter.Fill(dt); // on ajoute dans le tableau 
                data.ItemsSource = dt.DefaultView; //affichage du tableau 
                dataAdapter.Update(dt);

                connection.Close();
            }
        } // Lorsqu'on séléctionne un ingrédient, on peut voir dans quelle recette il apparait avec les quantités nécessaire à la réalisation. 

        private void RetourMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        } // Bouton permettant de retourner vers la page d'accueil
    }
}
