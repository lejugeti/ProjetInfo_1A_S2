using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class Projet
    {
        public string Type { get; set; }
        public int NbEleves { get; set; }
        public string AnneRealisation { get; set; }
        public string[] Promotion { get; set; }
        public string Sujet { get; set; }
        public string[] MotsCles { get; set; }
        public Livrable[] Livrables { get; set; }
        public Intervenant Encadrant { get; set; } //tuteur ou personne qui gère le projet
        public Intervenant[] Reviewers { get; set; } //ceux qui notent
        public Eleve[] Eleves { get; set; }
        public string DateDebut { get; set; }
        public string DateFin { get; set; }
        public int Duree { get; set; } //durée en jours

        //Constructeur
        public Projet(string type, int nbEleves, string anneeRealisation, string[] promo, string sujet, string[] motsCles, Livrable[] livrables, Intervenant encadrant,
            Intervenant[] reviewers, Eleve[] eleves, string debut, string fin, int duree)
        {
            Type = type;
            NbEleves = nbEleves;
            AnneRealisation = anneeRealisation;
            Promotion = promo;
            Sujet = sujet;
            MotsCles = motsCles;
            Livrables = livrables;
            Encadrant = encadrant;
            Reviewers = reviewers;
            Eleves = eleves;
            DateDebut = debut;
            DateFin = fin;
            Duree = duree;
        }

        //Méthodes
        /*public static Projet CreerProjet()
        {
            //insérer ici tous les trucs pour demander les trucs nécessaires à la création du projet
        }*/
    }
}
