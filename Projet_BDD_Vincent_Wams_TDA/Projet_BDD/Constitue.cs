using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BDD
{
    class Constitue
    {
        string nom_recette;
        string nom_produit;
        int quantite;



        public Constitue(string nom_recette, string nom_produit, int quantite)
        {
            this.nom_recette = nom_recette;
            this.nom_produit = nom_produit;
            this.quantite = quantite;

        }


        public string Nom_recette
        {
            get
            {
                return nom_recette;
            }
            set
            {
                nom_recette = value;
            }
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


        public int Quantite
        {
            get
            {
                return quantite;
            }
            set
            {
                quantite = value;
            }
        }
    }
}
