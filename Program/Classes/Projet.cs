using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    class Projet
    {
        public string Type { get; set; }
        public int NbEleves { get; set; }
        public string AnneRealisation { get; set; }
        public string[] Promotions { get; set; }
        public string Sujet { get; set; }
        public string[] MotsCles { get; set; }
        public Livrable[] Livrables { get; set; }
        public Intervenant[] Encadrant { get; set; } //tuteur ou personne qui gère le projet
        public Intervenant[] Reviewers { get; set; } //ceux qui notent
        public Eleve[] Eleves { get; set; }
        public string DateDebut { get; set; }
        public string DateFin { get; set; }
        public int Duree { get; set; } //durée en jours

        //Constructeur
        public Projet(string type, int nbEleves, string anneeRealisation, string[] promos, string sujet, string[] motsCles, Livrable[] livrables, Intervenant[] encadrant,
            Intervenant[] reviewers, Eleve[] eleves, string debut, string fin, int duree)
        {
            Type = type;
            NbEleves = nbEleves;
            AnneRealisation = anneeRealisation;
            Promotions = promos;
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
        public static Projet CreerProjet()
        {
            //insérer ici tous les trucs pour demander les trucs nécessaires à la création du projet

            Console.WriteLine("Vous avez choisi de créer un projet. Veuillez rentrer l'ensemble des informations demandées.");
            
            string AjoutTypeProjet()
            {
                Console.WriteLine("Quel est le type de votre projet ?");
                string type = Console.ReadLine();
                return type; 
            }

            int AjoutNbEleves(bool error)
            {
                if (error == false)
                {
                    Console.WriteLine("\n Combien d'élèves faisaient partie de ce projet ?");
                }
                else
                {
                    Console.WriteLine("Ce que vous avez rentré est incorrect. Veuillez rentrer un nombre d'élèves correct");
                }
                
                string inputNbEleves = Console.ReadLine();

                try
                {
                    int nbEleves = Int32.Parse(inputNbEleves);
                    return nbEleves;
                }
                catch (FormatException)
                {
                    return AjoutNbEleves(true); //récursivité pour demander en boucle un input correct
                }
            }
            
            string AjoutAnneeRealisation()
            {
                string pattern = "^20[0-9]{2}-20[0-9]{2}$";
                Regex regAnneeReal = new Regex(pattern);

                bool done = false;
                string input="";
                while (!done)
                {
                    Console.WriteLine("En quelle année a été réalisée ce projet ? (ex : 2019-2020)");
                    input = Console.ReadLine();
                    bool success = regAnneeReal.Match(input).Success;
                    if (success)
                    {
                        done = true;
                    }
                    else
                    {
                        Console.WriteLine("Ce que vous avez rentré n'est pas valide, recommencez.");
                    }
                }

                return input;
            }
            
            string[] AjoutPromotion()
            {
                Console.WriteLine("Quelles promotions étaient impliquées dans ce Projet ? (rentrez les années unes à unes)");
                List<string> promotions = new List<string>();
                bool done = false;
                while (!done)
                {
                    Console.Write("Entrez une année ou tapez Y pour quitter : ");
                    string input = Console.ReadLine();

                    string pattern = "^20[0-9]{2}$";
                    Regex regAnnee = new Regex(pattern);
                    bool success = regAnnee.Match(input).Success;

                    if (success)
                    {
                        promotions.Add(input);
                    }
                    else if (input.ToUpper() == "Y" && promotions.Count > 0)
                    {
                        done = true;
                    }
                    else
                    {
                        Console.WriteLine("Ce que vous avez rentré n'est pas une année, recommencez.");
                    }
                }

                string[] tabPromotions = new string[promotions.Count];
                for(int i = 0; i < promotions.Count; i++)
                {
                    tabPromotions[i] = promotions[i];
                }

                return tabPromotions;
            }

            string AjoutSujet()
            {
                Console.WriteLine("Quel est le type de votre projet ?");
                string sujet = Console.ReadLine();
                return sujet;
            }

            string[] AjoutMotsCles()
            {
                Console.WriteLine("Veuillez choisir au moins un mot clé pour qualifier le projet.");
                List<string> motsCles = new List<string>();
                bool done = false;
                while (!done)
                {
                    Console.Write("Entrez un mot clé ou tapez Y pour quitter : ");
                    string input = Console.ReadLine();

                    if (input.ToUpper() == "Y" && motsCles.Count > 0)
                    {
                        done = true;
                    }
                    else
                    {
                        motsCles.Add(input);
                    }
                }

                return motsCles.ToArray();
            }

            /*Livrable[] AjoutLivrables()
            {

            }*/

            /*Intervenant[] AjoutEncadrant()
            {

            }*/
        }
    }
}
