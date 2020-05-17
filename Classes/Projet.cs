using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Program
{
    public class Projet
    {
        //Structures
        public struct Date
        {
            public string Jour;
            public string Mois;
            public string Annee;

            public Date(string date)
            {
                string[] splitted = date.Split('/');
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
                string pattern = "^[0-3][0-9]/[0-1][0-9]/2[0-9]{3}$";
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
        }

        //Propriétés
        public int Id { get; set; }
        public string Intitule { get; set; }
        public string Type { get; set; }
        public int NbEleves { get; set; }
        public string[] Promotions { get; set; }
        public string Sujet { get; set; }
        public string[] MotsCles { get; set; }
        public Intervenant Client { get; set; }
        public Livrable[] Livrables { get; set; }
        public Intervenant[] Encadrants { get; set; } //tuteur ou personne qui gère le projet
        public Intervenant[] Reviewers { get; set; } //ceux qui notent
        public Eleve[] Eleves { get; set; }
        public Date DateDebut { get; set; }
        public Date DateFin { get; set; }


        //Constructeur
        public Projet(string intitule, string type, int nbEleves, string[] promos, string sujet, string[] motsCles, Intervenant client, Livrable[] livrables, Intervenant[] encadrants,
            Intervenant[] reviewers, Eleve[] eleves, Date debut, Date fin)
        {
            Id = ++Catalogue.IdMaxProjets;
            Intitule = intitule;
            Type = type;
            NbEleves = nbEleves;
            Promotions = promos;
            Sujet = sujet;
            MotsCles = motsCles;
            Client = client;
            Livrables = livrables;
            Encadrants = encadrants;
            Reviewers = reviewers;
            Eleves = eleves;
            DateDebut = debut;
            DateFin = fin;
        }

        //Constructeur vide utilisé par lors de la désérialisation
        public Projet()
        {

        }

        //Méthodes

        public static Projet CreateProjet()
        {
            //Fonction nécessaires
            string AjoutIntitule()
            {
                Console.WriteLine("\nQuel est l'intitulé de votre projet ?");
                string tmpIntitule = Console.ReadLine();
                return tmpIntitule;
            }

            string AjoutTypeProjet()
            {
                Console.WriteLine("\nQuel est le type de votre projet ?");
                string tmpType = Console.ReadLine();
                return tmpType;
            }

            string[] AjoutPromotion(Eleve[] tabEleves)
            {

                List<string> listePromotions = new List<string>();

                foreach (Eleve e in tabEleves)
                {
                    if (!listePromotions.Contains(e.Promotion))
                    {
                        listePromotions.Add(e.Promotion);
                    }
                }

                return listePromotions.ToArray();
            }

            string AjoutSujet()
            {
                Console.WriteLine("\nQuel est le sujet de votre projet ?");
                string sujet = Console.ReadLine();
                return sujet;
            }

            string[] AjoutMotsCles()
            {
                Console.WriteLine("\nVeuillez choisir au moins un mot clé pour qualifier le projet.");
                List<string> listeMotsCles = new List<string>();
                bool done = false;
                while (!done)
                {
                    Console.Write("Entrez un mot clé ou tapez Y pour quitter : ");
                    string input = Console.ReadLine();

                    if (input.ToUpper() == "Y" && listeMotsCles.Count > 0)
                    {
                        done = true;
                    }
                    else
                    {
                        listeMotsCles.Add(input);
                    }
                }

                return listeMotsCles.ToArray();
            }

            Intervenant AjoutClient()
            {
                Console.WriteLine("\nVous allez maintenant devoir renseigner le client du projet.");
                Intervenant tmpClient = Intervenant.CreateIntervenant(new Role("client"));

                return tmpClient;
            }

            Livrable[] AjoutLivrables()
            {
                bool doneLivrables = false;
                List<Livrable> listeLivrables = new List<Livrable>();

                while (!doneLivrables)
                {
                    listeLivrables.Add(Livrable.CreateLivrable());

                    Console.WriteLine("\nVoulez-vous ajouter un autre livrable ? Y/N");
                    string repLivrable = Console.ReadLine();

                    if (repLivrable.ToUpper() == "N") doneLivrables = true;
                }

                return listeLivrables.ToArray();
            }

            Intervenant[] AjoutEncadrant()
            {
                Console.WriteLine("\nVous allez maintenant devoir renseigner les différents encadrants du projet.");

                List<Intervenant> listeEncadrants = new List<Intervenant>();
                bool doneEncadrants = false;
                do
                {
                    string rep = "";

                    if (listeEncadrants.Count == 0)
                    {
                        Console.WriteLine("Rentrez à présent les informations du premier encadrant");
                        listeEncadrants.Add(Intervenant.CreateIntervenant(new Role("Encadrant")));
                    }
                    else
                    {
                        Console.WriteLine("\nVoulez-vous ajouter un autre encadrant ? Y/N");
                        rep = Console.ReadLine();

                        if (rep.ToUpper() == "Y") listeEncadrants.Add(Intervenant.CreateIntervenant(new Role("Encadrant")));
                        else if (rep.ToUpper() == "N") doneEncadrants = true;

                    }

                } while (!doneEncadrants);

                return listeEncadrants.ToArray();
            }

            Intervenant[] AjoutReviewers()
            {
                Console.WriteLine("\nVous allez maintenant devoir renseigner les différents reviewers du projet.");

                List<Intervenant> listeReviewers = new List<Intervenant>();
                bool doneReviewers = false;
                do
                {
                    string rep = "";

                    if (listeReviewers.Count == 0)
                    {
                        Console.WriteLine("Rentrez à présent les informations du premier reviewer");
                        listeReviewers.Add(Intervenant.CreateIntervenant(new Role("Reviewer")));
                    }
                    else
                    {
                        Console.WriteLine("\nVoulez-vous ajouter un autre reviewer ? Y/N");
                        rep = Console.ReadLine();

                        if (rep.ToUpper() == "Y") listeReviewers.Add(Intervenant.CreateIntervenant(new Role("Reviewer")));
                        else if (rep.ToUpper() == "N") doneReviewers = true;

                    }

                } while (!doneReviewers);

                return listeReviewers.ToArray();
            }

            Eleve[] AjoutEleves()
            {
                Console.WriteLine("\nVous allez maintenant devoir renseigner les différents élèves présents sur le projet.");

                List<Eleve> ListeEleves = new List<Eleve>();
                bool doneEleve = false;
                do
                {
                    string rep = "";

                    if (ListeEleves.Count == 0)
                    {
                        Console.WriteLine("\nRentrez à présent les informations du premier élève");
                        ListeEleves.Add(Intervenant.CreateIntervenant("eleve") as Eleve);
                    }
                    else
                    {
                        Console.WriteLine("\nVoulez-vous ajouter un autre élève ? Y/N");
                        rep = Console.ReadLine();

                        if (rep.ToUpper() == "Y") ListeEleves.Add(Intervenant.CreateIntervenant("eleve") as Eleve); //downcast car l'instance créée est un intervenant de base
                        else if (rep.ToUpper() == "N") doneEleve = true;

                    }

                } while (!doneEleve);


                return ListeEleves.ToArray();
            }

            Date AjoutDate(bool debut, bool error)
            {
                if (error == false)
                {
                    switch (debut)
                    {
                        case true:
                            Console.WriteLine("\nVeuillez rentrer une date de début du projet au format DD/MM/YYYY");
                            break;

                        case false:
                            Console.WriteLine("\nVeuillez rentrer une date de fin du projet au format DD/MM/YYYY");
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("\nCe que vous avez rentré est incorrect. Pour rappel le format de l'année est DD/MM/YYYY");
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

            string intitule = AjoutIntitule();
            string type = AjoutTypeProjet();
            string sujetProjet = AjoutSujet();
            string[] motsCles = AjoutMotsCles();

            Intervenant client = AjoutClient();
            Livrable[] livrables = AjoutLivrables();
            Intervenant[] encadrants = AjoutEncadrant();
            Intervenant[] reviewers = AjoutReviewers();

            Eleve[] eleves = AjoutEleves();
            int nbEleves = eleves.Length;
            string[] promotions = AjoutPromotion(eleves);

            Date dateDebut = AjoutDate(true, false);
            Date dateFin = AjoutDate(false, false);

            return new Projet(intitule, type, nbEleves, promotions, sujetProjet, motsCles, client, livrables, encadrants, reviewers, eleves, dateDebut, dateFin);
        }

        /*
         * Interface de mise à jour d'un projet. Permet à l'utilisateur de modifier les différents éléments
         * d'un projet donné.
         * @arg indicateurChangement, l'id du projet à modifier
         */
        public void MiseAJour(int indicateurChangement)
        {
            Console.Clear();

            // Modification intitulé du projet
            if (indicateurChangement == 1)
            {
                Console.WriteLine($"Vous avez choisi de modifier l'intitulé du projet. \n\nL'intitulé actuel est : {Intitule}");
                Console.WriteLine("\nSaisissez le nouvel intitulé de votre projet : ");
                string intitule = Console.ReadLine();
                Intitule = intitule;
            }

            // Modification type du projet
            else if (indicateurChangement == 2)
            {
                Console.WriteLine("\nSaisissez le nouveau type de votre projet : ");
                string type = Console.ReadLine();
                Type = type;
            }

            // Modification client
            else if (indicateurChangement == 3)
            {
                Console.WriteLine("Vous avez choisi de modifier le client du projet. \nVoici les informations sur le client actuel");
                AfficherIntervenant(Client, "Client");

                Console.WriteLine("");
                Console.WriteLine("Rentrez à présent les informations du nouveau client");
                Client = Intervenant.CreateIntervenant(new Role("Client"));
            }

            // Ajout livrable
            else if (indicateurChangement == 4)
            {
                Console.WriteLine("Vous avez choisi d'ajouter un nouveau livrable à ce projet.\n Voici la liste des livrables du projet.\n");
                AfficherListeLivrables(Livrables);

                Livrable[] tmpLivrables = new Livrable[Livrables.Length + 1];

                // Ajout de tous les livrables déjà existants dans le nouveau tableau de livrables
                for (int i = 0; i < Livrables.Length; i++)
                {
                    tmpLivrables[i] = Livrables[i];
                }
                tmpLivrables[tmpLivrables.Length - 1] = Livrable.CreateLivrable();

                Livrables = tmpLivrables;
            }

            // Modification livrable
            else if (indicateurChangement == 5)
            {
                Console.WriteLine("Vous avez choisi de modifier un livrable. Voici la liste des livrables actuels\n ");
                AfficherListeLivrables(Livrables);

                bool errorLivrable = false;
                bool doneModifLivrable = false;
                while (!doneModifLivrable)
                {
                    if (errorLivrable) Console.WriteLine("Ce que vous avez rentré est incorrect, recommencez.");
                    Console.Write("Indiquez le numéro du livrable à modifier : ");
                    string repLivrable = Console.ReadLine();
                    int indiceLivrable = Program.TryParseReponse(repLivrable);

                    if (indiceLivrable < 0 || indiceLivrable > Livrables.Length)
                    {
                        errorLivrable = true;
                    }
                    else
                    {
                        errorLivrable = false;
                        doneModifLivrable = true;
                        Livrables[indiceLivrable - 1] = Livrable.CreateLivrable();
                    }
                }
            }

            // Ajouter un encadrant
            else if (indicateurChangement == 6)
            {
                Console.WriteLine("Vous avez choisi d'ajouter un nouvel encadrant. \nVoici la liste des encadrants actuels du projet.\n");
                AfficherListeIntervenants(Encadrants, "Encadrants");
                Console.WriteLine("");

                Intervenant[] tmpEncadrants = new Intervenant[Encadrants.Length + 1];

                // Ajout de tous les encadrants déjà existants dans le nouveau tableau d'encadrants
                for (int i = 0; i < Encadrants.Length; i++)
                {
                    tmpEncadrants[i] = Encadrants[i];
                }
                tmpEncadrants[tmpEncadrants.Length - 1] = Intervenant.CreateIntervenant(new Role("Encadrant"));

                Encadrants = tmpEncadrants;
            }

            // Modifier un encadrant
            else if (indicateurChangement == 7)
            {
                Console.WriteLine("Vous avez choisi de modifier un encadrant. \nVoici la liste des encadrants actuels du projet.");
                AfficherListeIntervenants(Encadrants, "Encadrants");
                Console.WriteLine("");

                // Tant que l'utilisateur n'a pas indiqué un numéro d'encadrant valide
                bool errorEncadrant = false;
                bool doneModifEncadrant = false;
                while (!doneModifEncadrant)
                {
                    if (errorEncadrant) Console.WriteLine("Ce que vous avez rentré est incorrect, recommencez.");
                    Console.Write("Indiquez le numéro de l'encadrant à modifier : ");

                    string repEncadrant = Console.ReadLine();
                    int indiceEncadrant = Program.TryParseReponse(repEncadrant);

                    if (indiceEncadrant < 0 || indiceEncadrant > Livrables.Length)
                    {
                        errorEncadrant = true;
                    }
                    else
                    {
                        errorEncadrant = false;
                        doneModifEncadrant = true;
                        Console.WriteLine("\nIndiquez maintenant les informations relatives à l'encadrant.");
                        Encadrants[indiceEncadrant - 1] = Intervenant.CreateIntervenant(new Role("Encadrant"));
                    }
                }
            }

            // Ajouter un reviewer
            else if (indicateurChangement == 8)
            {
                Console.WriteLine("Vous avez choisi d'ajouter un nouveau reviewer. \nVoici la liste des reviewers actuels du projet.\n");
                AfficherListeIntervenants(Reviewers, "Reviewers");
                Console.WriteLine("");

                Intervenant[] tmpReviewers = new Intervenant[Reviewers.Length + 1];

                for (int i = 0; i < Reviewers.Length; i++)
                {
                    tmpReviewers[i] = Reviewers[i];
                }

                tmpReviewers[tmpReviewers.Length - 1] = Intervenant.CreateIntervenant(new Role("Reviewer"));

                Reviewers = tmpReviewers;
            }

            // Modifier un reviewer
            else if (indicateurChangement == 9)
            {
                Console.WriteLine("Vous avez choisi de modifier un reviewer. \nVoici la liste actuelle des reviewer du projet.");
                AfficherListeIntervenants(Reviewers, "Reviewers");
                Console.WriteLine("");

                // Tant que l'utilisateur n'a pas indiqué un numéro de reviewer valide
                bool errorReviewer = false;
                bool doneModifReviewer = false;
                while (!doneModifReviewer)
                {
                    if (errorReviewer) Console.WriteLine("Ce que vous avez rentré est incorrect, recommencez.");
                    Console.Write("Indiquez le numéro du reviewer à modifier : ");

                    string repReviewer = Console.ReadLine();
                    int indiceReviewer = Program.TryParseReponse(repReviewer);

                    if (indiceReviewer < 0 || indiceReviewer > Livrables.Length)
                    {
                        errorReviewer = true;
                    }
                    else
                    {
                        errorReviewer = false;
                        doneModifReviewer = true;
                        Console.WriteLine("\nIndiquez maintenant les informations relatives au reviewer.");
                        Reviewers[indiceReviewer - 1] = Intervenant.CreateIntervenant(new Role("Reviewer"));
                    }
                }
            }

            // Ajouter un élève
            else if (indicateurChangement == 10)
            {
                Console.WriteLine("Vous avez choisi d'ajouter un nouvel élève. \nVoici la liste actuelle des élèves du projet.");
                AfficherListeIntervenants(Eleves, "Eleves");
                Console.WriteLine("");

                Eleve[] tmpEleves = new Eleve[Eleves.Length + 1];

                for (int i = 0; i < Eleves.Length; i++)
                {
                    tmpEleves[i] = Eleves[i];
                }

                Eleve newEleve = Intervenant.CreateIntervenant("eleve") as Eleve;
                tmpEleves[tmpEleves.Length - 1] = newEleve;

                Eleves = tmpEleves as Eleve[];
                NbEleves = Eleves.Length;

                // Ajout de la promotion de l'élève à la liste de spromotions du projet
                List<string> promotions = new List<string>();
                foreach (string promo in Promotions)
                {
                    promotions.Add(promo);
                }

                if (!promotions.Contains(newEleve.Promotion))
                {
                    promotions.Add(newEleve.Promotion);
                }

                Promotions = promotions.ToArray();
            }

            // Modifier un élève
            else if (indicateurChangement == 11)
            {
                Console.WriteLine("Vous avez choisi de modifier un élève. \nVoici la liste actuelle des élèves du projet.");
                AfficherListeIntervenants(Eleves, "Elèves");
                Console.WriteLine("");

                // Tant que l'utilisateur n'a pas indiqué un numéro de reviewer valide
                bool errorEleves = false;
                bool doneModifEleves = false;
                while (!doneModifEleves)
                {
                    if (errorEleves) Console.WriteLine("Ce que vous avez rentré est incorrect, recommencez.");
                    Console.Write("Indiquez le numéro de l'élève à modifier : ");

                    string repEleve = Console.ReadLine();
                    int indiceEleve = Program.TryParseReponse(repEleve);

                    if (indiceEleve < 0 || indiceEleve > Eleves.Length)
                    {
                        errorEleves = true;
                    }
                    else
                    {
                        errorEleves = false;
                        doneModifEleves = true;
                        Console.WriteLine("\nIndiquez maintenant les informations relatives à l'élève.");
                        Eleves[indiceEleve - 1] = Intervenant.CreateIntervenant("eleve") as Eleve;
                    }
                }
            }

            // Modifier la date de début
            else if (indicateurChangement == 12)
            {
                Console.Write("Vous avez choisi de modifier la date de début de projet. \n\nVoici la date de début actuelle :");
                Console.WriteLine(DateDebut.GetDateFormatee());
                Console.WriteLine("");

                bool doneDateDebut = false;
                bool errorDateDebut = false;
                while (!doneDateDebut)
                {
                    if (errorDateDebut) Console.WriteLine("Ce que vous avez rentré est incorrect, recommencez.");
                    Console.Write("Rentrez votre nouvelle date de début au format DD/MM/YYYY : ");

                    string repDate = Console.ReadLine();
                    if (Date.IsDate(repDate))
                    {
                        DateDebut = new Date(repDate);
                        errorDateDebut = false;
                        doneDateDebut = true;
                    }
                    else
                    {
                        errorDateDebut = true;
                    }
                }
            }

            // Modifier la date de fin
            else if (indicateurChangement == 13)
            {
                Console.Write("Vous avez choisi de modifier la date de fin du projet. \n\nVoici la date de fin actuelle :");
                Console.WriteLine(DateFin.GetDateFormatee());
                Console.WriteLine("");

                bool doneDateFin = false;
                bool errorDateFin = false;
                while (!doneDateFin)
                {
                    if (errorDateFin) Console.WriteLine("Ce que vous avez rentré est incorrect, recommencez.");
                    Console.Write("Rentrez votre nouvelle date de fin au format DD/MM/YYYY : ");

                    string repDate = Console.ReadLine();
                    if (Date.IsDate(repDate))
                    {
                        DateFin = new Date(repDate);
                        errorDateFin = false;
                        doneDateFin = true;
                    }
                    else
                    {
                        errorDateFin = true;
                    }
                }
            }
        }

        protected void AfficherListeInfos(string nomInfo, string[] infos)
        {
            if (infos.Length == 1)
            {
                Console.WriteLine($"{nomInfo} : {infos[0]}");
            }
            else if (infos.Length > 1)
            {
                Escapes escapes = new Escapes(new String(' ', nomInfo.Length));

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

        protected void AfficherListeLivrables(Livrable[] livrables)
        {
            Escapes escapes = new Escapes("Livrable 1");
            Console.WriteLine("\n===== Livrables =====");
            for (int i = 0; i < livrables.Length; i++)
            {
                Console.Write($"Livrable {i + 1}");
                livrables[i].PrintInfos(escapes);
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        protected void AfficherListeIntervenants(Intervenant[] encadrants, string typeIntervenant)
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

        protected void AfficherIntervenant(Intervenant intervenant, string typeIntervenant)
        {
            Console.WriteLine($"\n===== {typeIntervenant} =====");

            Escapes escapes = new Escapes();

            string labelEncadrant = $"{typeIntervenant}";
            intervenant.PrintInfosCol(escapes, labelEncadrant);
            Console.WriteLine("");
        }

        public void PrintInfos()
        {
            Console.WriteLine("====== PROJET ======\n");
            Console.WriteLine($"Intitulé : {Intitule}");
            Console.WriteLine($"Type : {Type}");
            Console.WriteLine($"Nombre d'élèves : {NbEleves}");
            AfficherListeInfos("Promotion", Promotions);
            Console.WriteLine($"Sujet : {Sujet}");
            AfficherListeInfos("Mots clés", MotsCles);
            DateDebut.PrintCol("de début");
            DateFin.PrintCol("de fin");

            Console.WriteLine("");
            AfficherIntervenant(Client, "Client");
            AfficherListeLivrables(Livrables);
            AfficherListeIntervenants(Encadrants, "Encadrants");
            AfficherListeIntervenants(Reviewers, "Reviewers");
            AfficherListeIntervenants(Eleves, "Eleves");
        }
    }
}
