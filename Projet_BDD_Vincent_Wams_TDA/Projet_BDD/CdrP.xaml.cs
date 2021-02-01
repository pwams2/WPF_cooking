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
    /// Logique d'interaction pour CdrP.xaml
    /// </summary>
    public partial class CdrP : Window
    {
        public string[] ingredients { get; set; }
        public CdrP()
        {
            Bdd bdd = new Bdd();
            InitializeComponent();
            ingredients = bdd.LireNomProduit().ToArray(); // ces 2 lignes de codes permettent d'avoir la liste des ingrédients qui sont enregistrés dans la base de données lors de l'ajout d'une recette 
            DataContext = this;
        }
        string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=projet1;" +
                                         "UID=root;PASSWORD=Paris123.";


        private void Affichage_Click(object sender, RoutedEventArgs e)
        {

            MySqlConnection connection = new MySqlConnection(connexionString);
            connection.Open();

            MySqlCommand commande = new MySqlCommand("select Nom_recette,NbVente from recette where Id='" + id.Text + "'", connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(commande);
            DataTable dt = new DataTable("Nom_recette", "NbVente");
            dataAdapter.Fill(dt);
            Data.ItemsSource = dt.DefaultView;
            dataAdapter.Update(dt);

            connection.Close();

        }  // 2eme onglet, on affiche les recettes crées par le cdr connecte 

        private void Valider_Click(object sender, RoutedEventArgs e)
        {

            string[] Ing = new string[8];
            int[] Quantite = new int[8];

            Ing[0] = Ing1.Text;
            Ing[1] = Ing2.Text;
            Ing[2] = Ing3.Text;
            Ing[3] = Ing4.Text;
            Ing[4] = Ing5.Text;
            Ing[5] = Ing6.Text;
            Ing[6] = Ing7.Text;
            Ing[7] = Ing8.Text;

            Quantite[0] = int.Parse(Q1.Text);
            Quantite[1] = int.Parse(Q2.Text);
            Quantite[2] = int.Parse(Q3.Text);
            Quantite[3] = int.Parse(Q4.Text);
            Quantite[4] = int.Parse(Q5.Text);
            Quantite[5] = int.Parse(Q6.Text);
            Quantite[6] = int.Parse(Q7.Text);
            Quantite[7] = int.Parse(Q8.Text);

            string nomrecette = nom.Text;
            string typerecette = type.Text;
            string descriptifrecette = descriptif.Text;
            int prixrecette = int.Parse(prix.Text);

            Recette recette = new Recette(nomrecette, typerecette, descriptifrecette, prixrecette, 0, id.Text, 2, 2);
            Bdd bdd = new Bdd();
            bdd.AddRecette(recette);

            for (int i = 0; i < Ing.Length; i++)
            {
                Constitue constitue = new Constitue(nomrecette, Ing[i], Quantite[i]);
                Bdd bdd1 = new Bdd();
                bdd1.AddConstitue(constitue);
            }
            bdd.Supprimer();
            MessageBox.Show("Bien enregistré");


        } // Valider la recette ( enregistrement dans la base de donnée ) 

        private void CommandePlat_Click(object sender, RoutedEventArgs e)
        {
            Accueil accueil = new Accueil();
            accueil.Show();
            this.Hide();
        } // fonction qui permet d'aller vers la page de commande 

        private void RetourAccueil_Click(object sender, RoutedEventArgs e) // fonction pour retourner vers la page d'accueil 
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
