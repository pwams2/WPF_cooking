using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BDD
{
    class Client
    {
            public string id;
            public string nom_client;
            public string prenom_client;
            public string mdp_client;
            public string adresse_client;
            public string ville_client;
            public int codepostal_client;
            public int telephone_client;
            public string infos_client;
            public int solde_client;
            public bool propose;



            public Client(string id, string nom_client, string prenom_client, string mdp_client, string adresse_client, string ville_client, int codepostal_client, int telephone_client, string infos_client, int solde_client, bool propose)
            {
                this.id = id;
                this.nom_client = nom_client;
                this.prenom_client = prenom_client;
                this.mdp_client = mdp_client;
                this.adresse_client = adresse_client;
                this.ville_client = ville_client;
                this.codepostal_client = codepostal_client;
                this.telephone_client = telephone_client;
                this.infos_client = infos_client;
                this.solde_client = solde_client;
                this.propose = propose;
            }



            public string Id
            {
                get
                {
                    return id;
                }
                set
                {
                    id = value;
                }
            }



            public string Nom_client
            {
                get
                {
                    return nom_client;
                }
                set
                {
                    nom_client = value;
                }
            }


            public string Prenom_client
            {
                get
                {
                    return prenom_client;
                }
                set
                {
                    prenom_client = value;
                }
            }



            public string Mdp_client
            {
                get
                {
                    return mdp_client;
                }
                set
                {
                    mdp_client = value;
                }
            }



            public string Adresse_client
            {
                get
                {
                    return adresse_client;
                }
                set
                {
                    adresse_client = value;
                }
            }



            public string Ville_client
            {
                get
                {
                    return ville_client;
                }
                set
                {
                    ville_client = value;
                }
            }



            public int Codepostal_client
            {
                get
                {
                    return codepostal_client;
                }
                set
                {
                    codepostal_client = value;
                }
            }



            public int Telephone_client
            {
                get
                {
                    return telephone_client;
                }
                set
                {
                    telephone_client = value;
                }
            }



            public string Infos_client
            {
                get
                {
                    return infos_client;
                }
                set
                {
                    infos_client = value;
                }
            }



            public int Solde_client
            {
                get
                {
                    return solde_client;
                }
                set
                {
                    solde_client = value;
                }
            }



            public bool Propose
            {
                get
                {
                    return propose;
                }
                set
                {
                    propose = value;
                }
            }
        }



        class Cdr1 : Client
        {
            public int nbVente_Cdr;
            //public bool propose;
            public Cdr1(string id, string nom_client, string prenom_client, string mdp_client, string adresse_client, string ville_client, int codepostal_client, int telephone_client, string infos_client, int solde_client, bool propose, int NbVente_Cdr)
                : base(id, nom_client, prenom_client, mdp_client, adresse_client, ville_client, codepostal_client, telephone_client, infos_client, solde_client, propose)
            {
                // this.propose = propose;
                this.NbVente_Cdr = NbVente_Cdr;
            }


            public int NbVente_Cdr
            {
                get
                {
                    return nbVente_Cdr;
                }
                set
                {
                    nbVente_Cdr = value;
                }
            }
        }
}

