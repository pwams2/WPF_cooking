using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Xml;
using System.IO;

namespace Projet_BDD
{
    class Bdd
    {
        private MySqlConnection connection;

        public Bdd()
        {
            this.InitConnexion();
        }

        public void InitConnexion()
        {
            string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=projet1;" +
                                         "UID=root;PASSWORD=Paris123.";
            connection = new MySqlConnection(connexionString);
            //connection.Open();
        }

        
        public void AddContact(Client contact)
        {
            try
            {
                connection.Open();
                // Création d'une commande SQL en fonction de l'objet connection
                MySqlCommand cmd = this.connection.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO Client (Id, nom_client, prenom_client,mdp_client,adresse_client,ville_client,codepostal_client,telephone_client,infos_client,solde_client,propose) VALUES (@id, @nom_client, @prenom_client ,@mdp_client,@adresse_client,@ville_client,@codepostal_client,@telephone_client,@infos_client,@solde_client,@propose)";

                // utilisation de l'objet contact passé en paramètre
                cmd.Parameters.AddWithValue("@id", contact.Id);
                cmd.Parameters.AddWithValue("@nom_client", contact.Nom_client);
                cmd.Parameters.AddWithValue("@prenom_client", contact.Prenom_client);

                cmd.Parameters.AddWithValue("@mdp_client", contact.Mdp_client);
                cmd.Parameters.AddWithValue("@adresse_client", contact.Adresse_client);
                cmd.Parameters.AddWithValue("@ville_client", contact.Ville_client);
                cmd.Parameters.AddWithValue("@codepostal_client", contact.Codepostal_client);
                cmd.Parameters.AddWithValue("@telephone_client", contact.Telephone_client);
                cmd.Parameters.AddWithValue("@infos_client", contact.Infos_client);
                cmd.Parameters.AddWithValue("@solde_client", contact.Solde_client);
                cmd.Parameters.AddWithValue("@propose", contact.Propose);


                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                // Fermeture de la connexion
                //this.connection.Close();
            }
            catch
            {
                // Gestion des erreurs :
                // Possibilité de créer un Logger pour les exceptions SQL reçus
                // Possibilité de créer une méthode avec un booléan en retour pour savoir si le contact à été ajouté correctement.
            }
            connection.Close();
        } // Ajout d'un nouveau client dans la base de données

        public void AddCdr(bool propose, int nbvente, string id)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Cdr (propose_Cdr, NbVente_Cdr, Id) VALUES (@propose, @nbvente, @id)";

                cmd.Parameters.AddWithValue("@propose", propose);
                cmd.Parameters.AddWithValue("@nbvente", nbvente);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" ErreurConnexion : " + ex.ToString());
                return;
            }
            connection.Close();
        }// Ajout d'un nouveau cdr dans la base de données

        public void AddRecette(Recette recette)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();

                cmd.CommandText = "INSERT INTO Recette (nom_recette,type_recette,descriptif_recette,prix_recette,NbVente,remuneration_cui,id,remuneration_cdr) VALUES (@nom_recette,@type_recette,@descriptif_recette,@prix_recette,@NbVente,@remuneration_cui,@id,@remuneration_cdr)";

                // utilisation de l'objet contact passé en paramètre
                cmd.Parameters.AddWithValue("@nom_recette", recette.Nom_recette);
                cmd.Parameters.AddWithValue("@type_recette", recette.Type_recette);
                cmd.Parameters.AddWithValue("@descriptif_recette", recette.Descriptif_recette);
                cmd.Parameters.AddWithValue("@prix_recette", recette.Prix_recette);
                cmd.Parameters.AddWithValue("@nbvente", recette.NbVente_Cdr);
                cmd.Parameters.AddWithValue("@remuneration_cui", recette.Remuneration_Cuis);
                cmd.Parameters.AddWithValue("@id", recette.Id);
                cmd.Parameters.AddWithValue("@remuneration_cdr", recette.Remuneration_Cdr);


                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
        }// Ajout d'une nouvelle recette dans la base de données

        public void AddConstitue(Constitue constitue)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();

                cmd.CommandText = "INSERT INTO constitue (nom_recette,nom_produit,quantite) VALUES (@nom_recette,@nom_produit,@quantite)";

                // utilisation de l'objet contact passé en paramètre
                cmd.Parameters.AddWithValue("@nom_recette", constitue.Nom_recette);
                cmd.Parameters.AddWithValue("@nom_produit", constitue.Nom_produit);
                cmd.Parameters.AddWithValue("@quantite", constitue.Quantite);

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch
            {
                //
            }
        }// Lors de la création d'une nouvelle recette , on ajoute dans la table constitue : le nom de la recette, les produits nécessaire et les quantitées nécessaire



        public int Verification(string id, string mdp)
        {
            int count = 0;
            connection.Open();
            MySqlCommand verif = new MySqlCommand("select * from Client where id='" + id + "'and Mdp_Client='" + mdp + "';", connection);
            MySqlDataReader myReader;

            myReader = verif.ExecuteReader();

            while (myReader.Read())
            {
                count = count + 1;
            }
            connection.Close();

            return count;
        } // Fonction pour vérifier si l'id et le mdp correspond bien ensemble 

        public string Lire(string mdp)
        {
            connection.Open();
            MySqlCommand verif = new MySqlCommand("select Prenom_Client from Client where Mdp_Client='" + mdp + "';", connection);
            MySqlDataReader myReader;
            string prenom = "";
            myReader = verif.ExecuteReader();
            while (myReader.Read())
            {
                for (int i = 0; i < myReader.FieldCount; i++)
                {
                    prenom = myReader.GetString(i);
                }

            }
            connection.Close();
            return prenom;

        } // lecture du prénom du client , pour afficher bonjour "prenom du client" sur la page de commande 

        public bool VerificationCdr(string id)
        {
            bool verif = false;
            connection.Open();
            MySqlCommand commande = new MySqlCommand("select propose from Client where id='" + id + "';", connection);
            MySqlDataReader myReader;
            myReader = commande.ExecuteReader();
            while (myReader.Read())
            {
                for (int i = 0; i < myReader.FieldCount; i++)
                {
                    verif = myReader.GetBoolean(i);
                }
            }
            connection.Close();
            return verif;
        } // fonction pour vérifier si la personne qui se connecte est un client ou un cdr, pour ensuite l'envoyer sur la bonne page ( soit page cdr soit page client )

        public string Solde(string mdp)
        {
            connection.Open();
            MySqlCommand verif = new MySqlCommand("select Solde_Client from Client where Mdp_Client='" + mdp + "';", connection);
            MySqlDataReader myReader;
            string solde = "";
            myReader = verif.ExecuteReader();
            while (myReader.Read())
            {
                for (int i = 0; i < myReader.FieldCount; i++)
                {
                    solde = myReader.GetString(i);
                }

            }
            connection.Close();
            return solde;

        } // Affichage du solde sur la page de commande 

        public int PrixPlat(string nomrecette)
        {
            int prix = 0;
            try
            {
                connection.Open();
                MySqlCommand verif = new MySqlCommand("select Prix_Recette from recette where Nom_recette='" + nomrecette + "';", connection);
                MySqlDataReader myReader;
                myReader = verif.ExecuteReader();
                while (myReader.Read())
                {
                    for (int i = 0; i < myReader.FieldCount; i++)
                    {
                        prix = Convert.ToInt32(myReader.GetString(i));
                    }

                }
            }
            catch
            {
                //
            }
            connection.Close();
            return prix;
        }  // Récupération du prix du plat 

        public int NbVente(string nomrecette)
        {
            int nbvente = 0;
            try
            {
                connection.Open();
                MySqlCommand verif = new MySqlCommand("select NbVente from recette where Nom_recette='" + nomrecette + "';", connection);
                MySqlDataReader myReader;
                myReader = verif.ExecuteReader();
                while (myReader.Read())
                {
                    for (int i = 0; i < myReader.FieldCount; i++)
                    {
                        nbvente = Convert.ToInt32(myReader.GetString(i));
                    }

                }
            }
            catch
            {
                //
            }
            connection.Close();
            return nbvente;
        } // Nombre de ventes selon le plat 


        public int OperationFinale(string nomrecette, int prix, int quantite)
        {
            int prixrecette = 0;
            if (quantite > 10)
            {
                prixrecette = (2 + prix);
            }
            else if (quantite > 50) 
            {
                prixrecette = (5 + prix);

            }
            else if (quantite <= 10)
            {
                prixrecette = prix;
            }
            int nbvente_nouveau = NbVente(nomrecette) + quantite;
            try
            {
                connection.Open();
                MySqlTransaction tr = connection.BeginTransaction();
                MySqlCommand update = new MySqlCommand("Update recette set Nbvente='" + nbvente_nouveau + "' where Nom_Recette='" + nomrecette + "';", connection);
                update.ExecuteNonQuery();
                tr.Commit();
            }
            catch
            {
                //
            }
            connection.Close();

            return prixrecette;
        } // selon le cahier des charges le prix augmentent selon le nombres de commandes , puis on incrémente le nombres de ventes de cette recette

        public int NbVenteCdr(string choix)
        {
            int nbvente = 0;
            string id = LectureId(choix);
            try
            {
                connection.Open();
                MySqlCommand verif = new MySqlCommand("select NbVente_Cdr from cdr where Id ='" + id + "';", connection);
                MySqlDataReader myReader;
                myReader = verif.ExecuteReader();
                while (myReader.Read())
                {
                        nbvente = Convert.ToInt32(myReader.GetString(0));

                }
            }
            catch
            {
                //
            }
            connection.Close();
            return nbvente;
        }

        public void MiseAjourNbVenteCdr(string choix, int quantite)
        {
            string id = LectureId(choix);
            int nbvente = NbVenteCdr(choix);
            nbvente = nbvente + quantite;
            try
            {
                connection.Open();
                MySqlTransaction tr = connection.BeginTransaction();
                MySqlCommand update = new MySqlCommand("Update cdr set Nbvente_Cdr='" + nbvente + "' where Id ='" + id+ "';", connection);
                update.ExecuteNonQuery();
                tr.Commit();
            }
            catch
            {
                //
            }
            connection.Close();
        }

        public string MeilleurCdr()
        {
            string Cdr = "";
            try
            {
                connection.Open();
                MySqlCommand verif = new MySqlCommand("select Id,max(NbVente_Cdr) from cdr;", connection);
                MySqlDataReader myReader;
                myReader = verif.ExecuteReader();
                while (myReader.Read())
                {
                    Cdr = myReader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            connection.Close();
            return Cdr;
        } //  Pour le gestionnaire, récupérer le meilleur cdr de la semaine ( celui qui a le plus de ventes ) 

        public List<string> LireNomProduit()
        {

            connection.Open();
            MySqlCommand verif = new MySqlCommand("select Nom_Produit from produit;", connection);
            MySqlDataReader myReader;

            myReader = verif.ExecuteReader();
            List<string> result = new List<string>();
            using (var reader = myReader)
            {
                while (reader.Read())
                    result.Add(reader.GetString(0));
            }
            connection.Close();
            return result;
        }// liste de tous produits disponible dans la base de données 

        public void AddProduit(Ingredient produit)
        {
            try
            {
                // Ouverture de la connexion SQL
                this.connection.Open();



                // Création d'une commande SQL en fonction de l'objet connection
                MySqlCommand cmd = this.connection.CreateCommand();



                // Requête SQL
                cmd.CommandText = "INSERT INTO Produit (nom_produit, categorie_produit, stockactuel_produit, stockmax_produit, stockmin_produit, reference_fournisseur, unite_produit) VALUES (@nom_produit, @categorie_produit, @stockactuel_produit, @stockmax_produit, @stockmin_produit, @reference_fournisseur, @unite_produit);";



                // utilisation de l'objet contact passé en paramètre
                cmd.Parameters.AddWithValue("@nom_produit", produit.Nom_produit);
                cmd.Parameters.AddWithValue("@categorie_produit", produit.Categorie_produit);
                cmd.Parameters.AddWithValue("@stockactuel_produit", produit.Stockactuel_produit);
                cmd.Parameters.AddWithValue("@stockmax_produit", produit.Stockmax_produit);
                cmd.Parameters.AddWithValue("@stockmin_produit", produit.Stockmin_produit);
                cmd.Parameters.AddWithValue("@reference_fournisseur", produit.Reference_fournisseur);
                cmd.Parameters.AddWithValue("@unite_produit", produit.Unite_produit);



                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();



                // Fermeture de la connexion
                this.connection.Close();
            }
            catch
            {
            }

        } // Ajout d'un produit dans la base de données 


        public void Supprimer()
        {
            try
            {
                string sup = "";
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("delete from constitue where Nom_Produit='" + sup + "';");
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //
            }
            connection.Close();
        } // On supprime les cases non remplies dans la table constitue 



        public List<string> LireProduit(string choix)
        {

            connection.Open();
            MySqlCommand verif = new MySqlCommand("select Nom_Produit from constitue where Nom_Recette='" + choix + "';", connection);
            MySqlDataReader myReader;

            myReader = verif.ExecuteReader();
            List<string> result = new List<string>();
            using (var reader = myReader)
            {
                while (reader.Read())
                    result.Add(reader.GetString(0));
            }
            connection.Close();
            return result;
        } // On récupère dans une liste de string tous les produits nécessaires pour une recette 

        public List<int> QuantiteRecette(string nomrecette, List<string> tab1)
        {
            connection.Open();
            List<int> result = new List<int>();

            for (int i = 0; i < tab1.Count; i++)
            {

                MySqlCommand verif = new MySqlCommand("select Quantite from constitue where Nom_Recette='" + nomrecette + "' and Nom_produit='" + tab1[i] + "';", connection);
                MySqlDataReader myReader;

                myReader = verif.ExecuteReader();

                using (var reader = myReader)
                {
                    while (reader.Read())
                        result.Add(Convert.ToInt32(reader.GetString(0)));

                }
            }
            connection.Close();
            return result;
        }   // On récupère dans une liste de int tous les quantités nécessaires pour une recette choisi


        public List<int> LireQuantiteProduit(List<string> tab1)
        {
            connection.Open();
            List<int> result = new List<int>();

            for (int i = 0; i < tab1.Count; i++)
            {


                MySqlCommand verif = new MySqlCommand("select StockActuel_Produit from produit where Nom_produit='" + tab1[i] + "';", connection);
                MySqlDataReader myReader;

                myReader = verif.ExecuteReader();

                using (var reader = myReader)
                {
                    while (reader.Read())
                        result.Add(Convert.ToInt32(reader.GetString(0)));


                }
            }
            connection.Close();
            return result;
        }  // Récupération des stocks actuel pour la liste de produits 

        public void MiseAjourStock(string choix, int nbcommande) // Mise à jour des stocks lorsqu'une recette est commandée 
        {
            List<string> NomProduit = LireProduit(choix);
            List<int> QuantiteUtilise = QuantiteRecette(choix, NomProduit);
            List<int> StockActuel = LireQuantiteProduit(NomProduit);

            List<int> MiseAjourStock = new List<int>();
            for (int i = 0; i < StockActuel.Count; i++)
            {
                StockActuel[i] = StockActuel[i] - (nbcommande * QuantiteUtilise[i]);
            }
            MiseAjourStock = StockActuel;

            try
            {
                connection.Open();

                for (int i = 0; i < MiseAjourStock.Count; i++)
                {

                    MySqlTransaction tr = connection.BeginTransaction();
                    MySqlCommand update = new MySqlCommand("Update produit set StockActuel_Produit='" + MiseAjourStock[i] + "' where Nom_Produit='" + NomProduit[i] + "';", connection);
                    update.ExecuteNonQuery();
                    tr.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            connection.Close();




        }


        // ADMIN SUPPRESSION D'UN CDR DE LA BASE DE DONNEES

        public void SupprimerCdr(string choix)
        {
            try
            {
                
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("delete from cdr where Id='" + choix + "';");
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //
            }
            connection.Close();
        } //  fonction pour supprimer un cdr de la base de données 

        public void MiseAjourClient(string choix)
        {
            try
            {
                int variable = 0;
                connection.Open();
                MySqlTransaction tr = connection.BeginTransaction();
                MySqlCommand update = new MySqlCommand("Update client set propose='" + variable + "' where Id='" + choix + "';", connection);
                update.ExecuteNonQuery();
                tr.Commit();

            }
            catch 
            {

               //
            }
            connection.Close();
        } // Lors de la suppression d'un cdr, celui ci devient un client lambda

        public string NomRecette(string choix)
        {
            string nomrecette = " ";
            try
            {
                connection.Open();
                MySqlCommand verif = new MySqlCommand("select Nom_Recette from recette where Id='" + choix + "';", connection);
                MySqlDataReader myReader;

                myReader = verif.ExecuteReader();

                while (myReader.Read())
                {
                    nomrecette = myReader.GetString(0);
                }

            }
            catch
            {
                //
            }
            connection.Close();
            return nomrecette;
        } //Recuperation du nom de la recette, pour ensuite supprimer la recette dans la table constitue

        public void SupprimerConstitue(string choix)
        {
            string nomrecette = NomRecette(choix);
            try
            {

                connection.Open();
                MySqlCommand cmd = new MySqlCommand("delete from constitue where Nom_Recette" +
                    "='" + nomrecette + "';");
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //
            }
            connection.Close();
        } // Suppression de la recette dans constitue

        public void SupprimerRecette(string choix)
        {
            try
            {

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "delete from recette where Id='" + choix + "';";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                //
            }
        } // suppression de la recette dans la table recette 


        // GESTION DES STOCKS : 

        public int[] LireStockMin()
        {
            int[] tabstockmin;
            connection.Open();
            MySqlCommand verif = new MySqlCommand("select StockMin_Produit from Produit ;", connection);
            MySqlDataReader myReader;
            myReader = verif.ExecuteReader();
            List<int> result2 = new List<int>();
            using (var reader = myReader)
            {
                while (reader.Read())
                    result2.Add(Convert.ToInt32(reader.GetString(0)));
            }
            connection.Close();
            tabstockmin = result2.ToArray();
            return tabstockmin;
        } // lecture du stock minimum   


        public int[] LireStockActuel()
        {
            int[] tabstockact;
            connection.Open();
            MySqlCommand verif = new MySqlCommand("select StockActuel_Produit from Produit ;", connection);
            MySqlDataReader myReader;
            myReader = verif.ExecuteReader();
            List<int> result = new List<int>();
            using (var reader = myReader)
            {
                while (reader.Read())
                    result.Add(Convert.ToInt32(reader.GetString(0)));
            }
            connection.Close();
            tabstockact = result.ToArray();
            return tabstockact;
        } // lecture du stock actuel

        public string[] Classement_Produit()
        {

            string[] noms_produits_classe;
            List<string> nom_produit = new List<string>();
            connection.Open();
            MySqlCommand references = new MySqlCommand("select Nom_Produit from Produit order by Reference_Fournisseur asc ;", connection);
            MySqlDataReader myReader;
            myReader = references.ExecuteReader();
            using (var reader = myReader)
            {
                while (reader.Read())
                {
                    nom_produit.Add(reader.GetString(0));
                }
            }
            connection.Close();
            noms_produits_classe = nom_produit.ToArray();
            return noms_produits_classe;
        } // On classe les produits de facon ascendant 

        public int[] LireReferencesFournisseur_Acommander(string[] produit)
        {
            produit = ProdACommander();
            int[] ref_fournisseurs;
            List<int> ref_fournisseur = new List<int>();
            connection.Open();
            for (int i = 0; i < produit.Length; i++)
            {
                MySqlCommand references = new MySqlCommand("select Reference_Fournisseur from Produit where Nom_Produit='" + produit[i] + "'order by Reference_Fournisseur asc ;", connection);
                MySqlDataReader myReader;
                myReader = references.ExecuteReader();
                using (var reader = myReader)
                {
                    while (reader.Read())
                    {
                        ref_fournisseur.Add(Convert.ToInt32(reader.GetString(0)));
                    }
                }
            }
            connection.Close();
            ref_fournisseurs = ref_fournisseur.ToArray();
            return ref_fournisseurs;
        } // Lecture des ref fournisseur des produits a commander 


        public int[] CompMinAct() // fonction qui permet de savoir si un produit est a commander ou pas 
        {
            int[] tabact = LireStockActuel();
            int[] tabmin = LireStockMin();



            int[] acommander = new int[tabact.GetLength(0)];


            for (int i = 0; i < tabact.GetLength(0); i++)
            {
                if (tabact[i] <= tabmin[i])
                {
                    acommander[i] = 1; // 1 il faut commander 
                }
                else
                {
                    acommander[i] = 0; // a ne pas commander 
                }
            }
            return acommander;
        } // 

        public string[] ProdACommander()
        {
            string[] produitclasse = Classement_Produit();
            List<string> prodcommande = new List<string>();


            int[] valuecommande = CompMinAct();
            for (int i = 0; i < produitclasse.GetLength(0); i++)
            {
                if (valuecommande[i] == 1)
                {
                    prodcommande.Add(produitclasse[i]);
                }
            }

            string[] prodcommandefinal = prodcommande.ToArray();
            return prodcommandefinal;
        } // fonction avec les noms des produits a commander

        public int[] LectureStockMax(string[] ProdAcommander)
        {
            int[] tab = new int[] { };
            connection.Open();
            List<int> result = new List<int>();


            for (int i = 0; i < ProdAcommander.Length; i++)
            {


                MySqlCommand verif = new MySqlCommand("select StockMax_Produit, from produit where Nom_produit='" + ProdAcommander[i] + "';", connection);
                MySqlDataReader myReader;

                myReader = verif.ExecuteReader();



                using (var reader = myReader)
                {
                    while (reader.Read())
                        result.Add(Convert.ToInt32(reader.GetString(0)));


                }

                tab = result.ToArray();

            }
            connection.Close();
            return tab;
        } // Lecture du stock max


        public int[] LireStockActuelRéduit()
        {
            string[] prodACommander = ProdACommander();
            int[] prodQAct;
            List<int> result = new List<int>();
            //int[] tabstockact;
            connection.Open();
            for (int i = 0; i < prodACommander.GetLength(0); i++)
            {
                MySqlCommand verif = new MySqlCommand("select StockActuel_Produit from Produit where Nom_Produit = '" + prodACommander[i] + "';", connection);
                MySqlDataReader myReader;
                myReader = verif.ExecuteReader();
                using (var reader = myReader)
                {
                    while (reader.Read())
                        result.Add(Convert.ToInt32(reader.GetString(0)));
                }
            }

            connection.Close();
            prodQAct = result.ToArray();
            return prodQAct;
        } // on verifie le stock actuel des produits a commander 


        public int[] LireStockMaxRéduit()
        {
            string[] prodACommander = ProdACommander();
            int[] prodQMax;
            List<int> result = new List<int>();
            //int[] tabstockact;
            connection.Open();
            for (int i = 0; i < prodACommander.GetLength(0); i++)
            {
                MySqlCommand verif = new MySqlCommand("select StockMax_Produit from Produit where Nom_Produit = '" + prodACommander[i] + "';", connection);
                MySqlDataReader myReader;
                myReader = verif.ExecuteReader();
                using (var reader = myReader)
                {
                    while (reader.Read())
                        result.Add(Convert.ToInt32(reader.GetString(0)));
                }
            }


            connection.Close();
            prodQMax = result.ToArray();
            return prodQMax;
        }// on verifie le stock max des produits a commander 


        public int[] QuantitesACommander() // Quantité a commander 
        {
            int[] quantitesMaxACommander = LireStockMaxRéduit();
            int[] quantitesActACommander = LireStockActuelRéduit();
            int[] quantitesACommander = new int[quantitesMaxACommander.GetLength(0)];

            for (int i = 0; i < quantitesMaxACommander.GetLength(0); i++)
            {
                quantitesACommander[i] = quantitesMaxACommander[i] - quantitesActACommander[i];
            }
            return quantitesACommander;
        }



        // GESTION DU SOLDE DU CDR LORS DE LA COMMANDE D'UNE RECETTE 

        public string LectureId(string nomrecette)
        {
            string idCdr = "";
            try
            {
                connection.Open();
                MySqlCommand verif = new MySqlCommand("select Id from recette where Nom_Recette='" + nomrecette + "';", connection);
                MySqlDataReader myReader;

                myReader = verif.ExecuteReader();

                while (myReader.Read())
                {
                    idCdr = myReader.GetString(0);
                }

            }
            catch
            {
                //
            }
            connection.Close();
            return idCdr;
        } // Lecture du cdr de la recette commandée

        public int LectureSolde(string idCdr)
        {
            int solde = 0;
            try
            {
                connection.Open();
                MySqlCommand verif = new MySqlCommand("select Solde_Client from client where Id='" + idCdr + "';", connection);
                MySqlDataReader myReader;

                myReader = verif.ExecuteReader();

                while (myReader.Read())
                {
                    solde = Convert.ToInt32(myReader.GetString(0));
                }

            }
            catch
            {
                //
            }
            connection.Close();
            return solde;
        } // Lecture du solde du cdr en question 


        public void MiseAJourSolde(string choix, int quantite)
        {
            string idcdr = LectureId(choix);
            int solde = LectureSolde(idcdr);

            if (quantite > 50)
            {
                solde = solde + 4 * quantite;
            }
            else
            {
                solde = solde + 2 * quantite;
            }

            try
            {
                connection.Open();



                MySqlTransaction tr = connection.BeginTransaction();
                MySqlCommand update = new MySqlCommand("Update client set Solde_Client='" + solde + "' where Id='" + idcdr + "';", connection);
                update.ExecuteNonQuery();
                tr.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            connection.Close();

        } // puis on met a jour son solde 




        // Derniere partie, la partie XML 

        public void Xml()
        {
            
           int[] quantite = QuantitesACommander();
           string[] produit = ProdACommander();

            XmlDocument docXml = new XmlDocument();
            XmlElement racine = docXml.CreateElement("Réapprovisionnement");
            docXml.AppendChild(racine);

            XmlDeclaration xmldec1 = docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");
            docXml.InsertBefore(xmldec1, racine);
            for (int i = 0; i < produit.Length; i++)
            {

                XmlElement Produit = docXml.CreateElement("produit");
                Produit.InnerText = produit[i];
                racine.AppendChild(Produit);

                XmlElement autreBalise1 = docXml.CreateElement("Quantité");
                autreBalise1.InnerText = quantite[i].ToString();
                Produit.AppendChild(autreBalise1);

            }

            docXml.Save("commande.xml");
           


        } // fonction qui crée le fichier XML par produit 

        public void XML_Fournisseur()
        {
            int[] quantite = QuantitesACommander();
            string[] nom_produits = ProdACommander();
            int[] ref_fournisseurs = LireReferencesFournisseur_Acommander(nom_produits);

            XmlDocument docXml = new XmlDocument();
            XmlElement racine = docXml.CreateElement("Réapprovisionnement");
            docXml.AppendChild(racine);

            XmlDeclaration xmldec1 = docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");
            docXml.InsertBefore(xmldec1, racine);
            for (int i = 0; i < nom_produits.Length - 1; i++)
            {
                XmlElement Fournisseur = docXml.CreateElement("Ref-Fournisseur");
                Fournisseur.InnerText = ref_fournisseurs[i].ToString();
                racine.AppendChild(Fournisseur);


                XmlElement Produit = docXml.CreateElement("produit");
                Produit.InnerText = nom_produits[i];
                Fournisseur.AppendChild(Produit);

                XmlElement autreBalise1 = docXml.CreateElement("Quantité");
                autreBalise1.InnerText = quantite[i].ToString();
                Produit.AppendChild(autreBalise1);


            }
            docXml.Save("commande_fournisseur.xml");
            Console.WriteLine("fichier commande.xml créé");
        }  // fonction qui crée le fichier XML par fournisseur 


        // Mode Demo : 

        public int NbrClient()
        {
            int nombre = 0;
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("select count(*) from client;", connection);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nombre = Convert.ToInt32(reader.GetString(0));
                }

            }
            catch
            {
                //
            }
            connection.Close();
            return nombre;
        } // fonction qui retourne le nombre de client qui est inscrit dans la base de données

        public int NbrCdr()
        {
            int nombre = 0;
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("select count(*) from cdr;", connection);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nombre = Convert.ToInt32(reader.GetString(0));
                }

            }
            catch
            {
                //
            }
            connection.Close();
            return nombre;
        } //fonction qui retourne le nombre de cdr qui est inscrit dans la base de données

        public int NbrRecette()
        {
            int nombre = 0;
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("select count(*) from recette;", connection);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nombre = Convert.ToInt32(reader.GetString(0));
                }

            }
            catch
            {
                //
            }
            connection.Close();
            return nombre;
        } //fonction qui retourne le nombre de recette qui est inscrit dans la base de données







    }
}
