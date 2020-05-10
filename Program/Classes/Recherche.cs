using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Program.Classes
{
    public class Recherche
    {   
        //Propriétés
        public Projet[] Resultat { get; set; }

        //Constructeur
        public Recherche(Projet[] resultat)
        {
            Resultat = resultat;
        }

        public Recherche()
        {

        }
        //Méthodes

    }
}
