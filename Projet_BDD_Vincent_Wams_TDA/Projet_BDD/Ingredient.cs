using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BDD
{
    class Ingredient
    {
        string nom_produit;
        string categorie_produit;
        int stockactuel_produit;
        int stockmax_produit;
        int stockmin_produit;
        int reference_fournisseur;
        string unite_produit;



        public Ingredient(string nom_produit, string categorie_produit, int stockactuel_produit, int stockmax_produit, int stockmin_produit, int reference_fournisseur, string unite_produit)
        {
            this.nom_produit = nom_produit;
            this.categorie_produit = categorie_produit;
            this.stockactuel_produit = stockactuel_produit;
            this.stockmax_produit = stockmax_produit;
            this.stockmin_produit = stockmin_produit;
            this.reference_fournisseur = reference_fournisseur;
            this.unite_produit = unite_produit;
        }



        public string Nom_produit
        {
            get
            {
                return nom_produit;
            }
            set
            {
                nom_produit = value;
            }
        }



        public string Categorie_produit
        {
            get
            {
                return categorie_produit;
            }
            set
            {
                categorie_produit = value;
            }
        }



        public int Stockactuel_produit
        {
            get
            {
                return stockactuel_produit;
            }
            set
            {
                stockactuel_produit = value;
            }
        }



        public int Stockmax_produit
        {
            get
            {
                return stockmax_produit;
            }
            set
            {
                stockmax_produit = value;
            }
        }



        public int Stockmin_produit
        {
            get
            {
                return stockmin_produit;
            }
            set
            {
                stockmin_produit = value;
            }
        }



        public int Reference_fournisseur
        {
            get
            {
                return reference_fournisseur;
            }
            set
            {
                reference_fournisseur = value;
            }
        }

        public string Unite_produit
        {
            get
            {
                return unite_produit;
            }
            set
            {
                unite_produit = value;
            }

        }
    }
}
