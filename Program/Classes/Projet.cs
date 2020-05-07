using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    class Projet
    {
        //Structures
        public struct Date
        {
            string Jour;
            string Mois;
            string Annee;

            public Date(string date)
            {
                string[] splitted = date.Split("/");
                Jour = splitted[0];
                Mois = splitted[1];
                Annee = splitted[2];
            }

            public String GetAnnee()
            {
                return Annee;
            }
            
            //Méthodes
            public static bool IsDate(string date)
            {
                string pattern = "^[0-3]{2}/[0-1][0-9]/2[0-9]{3}$";
                Regex dateRegex = new Regex(pattern);
                bool regSuccess = dateRegex.Match(date).Success;

                return regSuccess;
            }

            public static bool IsPromotion(string promo)
            {
                string pattern = "^2[0-9]{3}$";
                Regex promoRegex = new Regex(pattern);
                bool regSuccess = promoRegex.Match(promo).Success;

                return regSuccess;
            }

            public void Print()
            {
                Console.WriteLine($"{Jour}/{Mois}/{Annee}");
            }
            public void PrintCol(string suffixeDate)
            {
                //argument suffixeDate vient compléter le print de la date pour ajouter des précisions 
                Console.Write($"Date {suffixeDate} : ");
                Console.WriteLine($"{this.GetDateFormatee()}");
            }

            public string GetDateFormatee()
            {
                return $"{Jour}/{Mois}/{Annee}";
            }
            /* INSERER FONCTION IsChronologic qui vérifie si la date de début est bien antérieure à celle de fin*/
        }

        private static List<Projet> _allProjects;

        //Propriétés
        public string Intitule { get; set; }
        public string Type { get; set; }
        public int NbEleves { get; set; }
        public string[] Promotions { get; set; }
        public string Sujet { get; set; }
        public string[] MotsCles { get; set; }
        public Livrable[] Livrables { get; set; }
        public Intervenant[] Encadrants { get; set; } //tuteur ou personne qui gère le projet
        public Intervenant[] Reviewers { get; set; } //ceux qui notent
        public Eleve[] Eleves { get; set; }
        public Date DateDebut { get; set; }
        public Date DateFin { get; set; }

        public static List<Projet> AllProjects
        {
            get { return _allProjects; }
        }

        //Constructeur
        public Projet(string intitule, string type, int nbEleves, string[] promos, string sujet, string[] motsCles, Livrable[] livrables, Intervenant[] encadrants,
            Intervenant[] reviewers, Eleve[] eleves, Date debut, Date fin)
        {
            Intitule = intitule;
            Type = type;
            NbEleves = nbEleves;
            Promotions = promos;
            Sujet = sujet;
            MotsCles = motsCles;
            Livrables = livrables;
            Encadrants = encadrants;
            Reviewers = reviewers;
            Eleves = eleves;
            DateDebut = debut;
            DateFin = fin;
            _allProjects.Add(this);

        }

        //Méthodes
        public static Projet CreateProjet()
        {
            //Fonction nécessaires
            string AjoutIntitule()
            {
                Console.WriteLine("Quel est l'intitulé de votre projet ?");
                string intitule = Console.ReadLine();
                return intitule;
            }
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
                    Console.WriteLine("\nCombien d'élèves faisaient partie de ce projet ?");
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
                        
            string[] AjoutPromotion()
            {
                Console.WriteLine("\nQuelles promotions étaient impliquées dans ce Projet ? (rentrez les années unes à unes)");
                List<string> promotions = new List<string>();
                bool done = false;
                while (!done)
                {
                    if(promotions.Count==0) Console.Write("Entrez une année : ");
                    else Console.Write("Entrez une année ou tapez Y pour quitter : ");

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
                Console.WriteLine("Quel est le sujet de votre projet ?");
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

            Livrable[] AjoutLivrables()
            {
                bool doneLivrables = false;
                List<Livrable> livrables = new List<Livrable>();

                while (!doneLivrables)
                {
                    livrables.Add(Livrable.CreateLivrable());

                    Console.WriteLine("Voulez-vous ajouter un autre livrable ? Y/N");
                    string repLivrable = Console.ReadLine();

                    if (repLivrable.ToUpper() == "N") doneLivrables = true;
                }

                return livrables.ToArray();
            }

            Intervenant[] AjoutEncadrant()
            {
                Console.WriteLine("Vous allez maintenant devoir renseigner les différents encadrants du projet.");

                List<Intervenant> encadrants = new List<Intervenant>();
                bool doneEncadrants = false;
                do
                {
                    string rep = "";

                    if (encadrants.Count == 0)
                    {
                        Console.WriteLine("Rentrez à présent les informations du premier encadrant");
                        encadrants.Add(Intervenant.CreateIntervenant());
                    }
                    else 
                    {
                        Console.WriteLine("Voulez-vous ajouter un autre encadrant ? Y/N");
                        rep = Console.ReadLine();

                        if (rep.ToUpper() == "Y") encadrants.Add(Intervenant.CreateIntervenant());
                        else if (rep.ToUpper() == "N") doneEncadrants = true; 
                        
                    }
                    
                } while (!doneEncadrants);

                return encadrants.ToArray();
            }

            Intervenant[] AjoutReviewers()
            {
                Console.WriteLine("Vous allez maintenant devoir renseigner les différents reviewers du projet.");

                List<Intervenant> reviewers = new List<Intervenant>();
                bool doneReviewers = false;
                do
                {
                    string rep = "";

                    if (reviewers.Count == 0)
                    {
                        Console.WriteLine("Rentrez à présent les informations du premier reviewer");
                        reviewers.Add(Intervenant.CreateIntervenant());
                    }
                    else
                    {
                        Console.WriteLine("Voulez-vous ajouter un autre reviewer ? Y/N");
                        rep = Console.ReadLine();

                        if (rep.ToUpper() == "Y") reviewers.Add(Intervenant.CreateIntervenant());
                        else if (rep.ToUpper() == "N") doneReviewers = true;

                    }

                } while (!doneReviewers);

                return reviewers.ToArray();
            }

            Eleve[] AjoutEleves()
            {
                Console.WriteLine("Vous allez maintenant devoir renseigner les différents élèves présents sur le projet.");

                List<Intervenant> reviewers = new List<Intervenant>();
                bool doneReviewers = false;
                do
                {
                    string rep = "";

                    if (reviewers.Count == 0)
                    {
                        Console.WriteLine("Rentrez à présent les informations du premier élève");
                        reviewers.Add(Intervenant.CreateIntervenant("eleve"));
                    }
                    else
                    {
                        Console.WriteLine("Voulez-vous ajouter un autre reviewer ? Y/N");
                        rep = Console.ReadLine();

                        if (rep.ToUpper() == "Y") reviewers.Add(Intervenant.CreateIntervenant("eleve"));
                        else if (rep.ToUpper() == "N") doneReviewers = true;

                    }

                } while (!doneReviewers);

                return (Eleve[]) reviewers.ToArray();
            }

            Date AjoutDate(bool debut, bool error)
            {
                if (error == false)
                {
                    switch (debut)
                    {
                        case true:
                            Console.WriteLine("Veuillez rentrer une date de début du projet au format DD/MM/YYYY");
                            break;

                        case false:
                            Console.WriteLine("Veuillez rentrer une date de fin du projet au format DD/MM/YYYY");
                            break;
                    }
                    
                }
                else
                {
                    Console.WriteLine("Ce que vous avez rentré est incorrect. Pour rappel le format de l'année est DD/MM/YYYY");
                }

                string input = Console.ReadLine(); 

                if (Date.IsDate(input))
                {
                    return new Date(input);
                }
                else
                {
                    return AjoutDate(debut, true);
                }
            }


            //Exécution
            Console.WriteLine("Vous avez choisi de créer un projet. Veuillez rentrer l'ensemble des informations demandées.");

            string intitule = AjoutIntitule();
            string type = AjoutTypeProjet();
            int nbEleves = AjoutNbEleves(false);
            string[] promotions = AjoutPromotion();
            string sujetProjet = AjoutSujet();
            string[] motsCles = AjoutMotsCles();
            Livrable[] livrables = AjoutLivrables();
            Intervenant[] encadrants = AjoutEncadrant();
            Intervenant[] reviewers = AjoutReviewers();
            Eleve[] eleves = AjoutEleves();
            Date dateDebut = AjoutDate(true, false);
            Date dateFin = AjoutDate(false, false);

            return new Projet(intitule, type, nbEleves, promotions, sujetProjet, motsCles, livrables, encadrants, reviewers, eleves, dateDebut, dateFin);
        }

        public void PrintInfos()
        {
            void AfficherListeInfos(string nomInfo, string[] infos)
            {
                if (infos.Length == 1)
                {
                    Console.WriteLine($"{nomInfo} : {infos[0]}");
                }
                else if (infos.Length > 1)
                {
                    Escapes escapes = new Escapes(new String(' ', nomInfo.Length + 1));

                    Console.WriteLine($"{nomInfo} |{infos[0]}");
                    for (int i = 1; i < infos.Length; i++)
                    {
                        escapes.Print();
                        Console.WriteLine($"{infos[i]}");
                    }
                }
                else
                {
                    Console.WriteLine($"{nomInfo} : Aucune donnée rentrée");
                }
            }
            void AfficherListeLivrables(Livrable[] livrables)
            {
                Escapes escapes = new Escapes(new String(' ', 11));
                Console.WriteLine("\n===== Livrables =====");
                for(int i = 0; i<livrables.Length; i++)
                {
                    Console.Write($"Livrable {i + 1}");
                    livrables[i].PrintInfos(escapes);
                    Console.WriteLine("");
                }
                Console.WriteLine("");
            }

            void AfficherListeIntervenants(Intervenant[] encadrants, string typeIntervenant)
            {
                Console.WriteLine($"===== {typeIntervenant} =====");

                Escapes escapes = new Escapes();
                for (int i = 0; i < encadrants.Length; i++)
                {
                    string labelEncadrant = $"{typeIntervenant} {i + 1}";                   
                    encadrants[i].PrintInfosCol(escapes, labelEncadrant);
                    Console.WriteLine("");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("====== PROJET ======");
            Console.WriteLine($"Intitulé : {Intitule}");
            Console.WriteLine($"Type : {Type}");
            Console.WriteLine($"Nombre d'élèves : {NbEleves}");
            AfficherListeInfos("Promotion", Promotions);
            Console.WriteLine($"Sujet : {Sujet}");
            AfficherListeInfos("Mots clés", MotsCles);
            DateDebut.PrintCol("de début");
            DateFin.PrintCol("de fin");
            AfficherListeLivrables(Livrables);
            AfficherListeIntervenants(Encadrants, "Encadrants");
            AfficherListeIntervenants(Reviewers, "Reviewers");
            AfficherListeIntervenants(Eleves, "Eleves");
        }
    }
}
