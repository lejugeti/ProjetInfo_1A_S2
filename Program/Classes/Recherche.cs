using System;
using System.Collections.Generic;
using System.Text;

namespace Program.Classes
{
    class Recherche
    {   
        //Propriétés
        public Projet[] Resultat { get; set; }

        //Constructeur
        protected Recherche(Projet[] resultat)
        {
            Resultat = resultat;
        }

        //Méthodes

    }
}
