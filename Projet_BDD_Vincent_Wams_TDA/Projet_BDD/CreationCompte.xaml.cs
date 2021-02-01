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

namespace Projet_BDD
{
    /// <summary>
    /// Logique d'interaction pour CreationCompte.xaml
    /// </summary>
    public partial class CreationCompte : Window
    {
        public CreationCompte()
        {
            InitializeComponent();
        }
        private void AnnulerButton_Click(object sender, RoutedEventArgs e) // fonction qui permet de retourner en arrière vers la page de connexion
        {
            MainWindow Demarrage = new MainWindow();
            Demarrage.Show();
            this.Hide();
        }

        private void Valider_Click(object sender, RoutedEventArgs e) // fonction pour création d'un nouveau client ou cdr
        {
            if (Id.Text == String.Empty || mdp1.Password == String.Empty || codepostal1.Text == String.Empty || telephone1.Text == String.Empty) // vérifier si les données obligatoires sont remplies 
            {
                MessageBox.Show("Veuillez remplir toutes les données obligatoires");
            }
            else
            {
                string id = Id.Text;
                int codepostal_client = int.Parse(codepostal1.Text);
                int telephone_client = int.Parse(telephone1.Text);
                string nom_client = nom1.Text;
                string prenom_client = prenom1.Text;
                string mdp_client = mdp1.Password.ToString();
                string mdp_2 = mdp2.Password.ToString();
                string adresse_client = adresse1.Text;
                string ville_client = ville1.Text;
                string infos_client = info1.Text;
                int solde_client = int.Parse(solde.Text);
                bool cdr = Convert.ToBoolean(Check.IsChecked); // récupération de toutes les variables entrées par l'utilisateur 

                if (mdp_client != mdp_2)
                {
                    MessageBox.Show("Vous n'avez pas entré le meme mot de passe");
                }


                else if (cdr == true) //si c est un cdr, on le rajoute dans la liste client ( addcontact ) et dans la liste cdr
                {

                    Bdd bdd1 = new Bdd();
                    Cdr1 cdr1 = new Cdr1(id, nom_client, prenom_client, mdp_client, adresse_client, ville_client, codepostal_client, telephone_client, infos_client, solde_client, true, 0);
                    bdd1.AddContact(cdr1);
                    Bdd bdd2 = new Bdd();
                    bdd2.AddCdr(cdr, 0, id);
                    MessageBox.Show("Bien enregistré");

                }
                else if (cdr == false) // si c'est seulement un client on ajoute dans la liste client 
                {
                    Bdd bdd = new Bdd();
                    Client client = new Client(id, nom_client, prenom_client, mdp_client, adresse_client, ville_client, codepostal_client, telephone_client, infos_client, solde_client, false);
                    bdd.AddContact(client);
                    MessageBox.Show("Bien enregistré");
                }

                MainWindow Demarrage = new MainWindow();
                Demarrage.Show();
                this.Hide();
            }

        }

    }
}
