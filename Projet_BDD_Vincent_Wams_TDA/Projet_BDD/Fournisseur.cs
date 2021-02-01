using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BDD
{
    class Fournisseur
    {
        int reference_fournisseur;
        string nom_fournisseur;
        int telephone_fournisseur;




        public Fournisseur(int reference_fournisseur, string nom_fournisseur, int telephone_fournisseur)
        {
            this.reference_fournisseur = reference_fournisseur;
            this.nom_fournisseur = nom_fournisseur;
            this.telephone_fournisseur = telephone_fournisseur;
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



        public string Nom_fournisseur
        {
            get
            {
                return nom_fournisseur;
            }
            set
            {
                nom_fournisseur = value;
            }
        }



        public int Telephone_fournisseur
        {
            get
            {
                return telephone_fournisseur;
            }
            set
            {
                telephone_fournisseur = value;
            }
        }
    }
}
