using System;

namespace Program
{
    class Program
    {   /*
        * Permet d'afficher un ensemble de choix associés à un nombre pour que l'utilisateur indique sa réponse
        * @arg enonce, l'énoncé du choix que doit réaliser l'utilisateur 
        * @arg choix, le tableau des différents choix proposés à l'utilisateur
        */
        protected static void AfficherGrilleChoix(string enonce, string[] choix)
        {
            Escapes space = new Escapes(enonce);

            Console.Write($"{enonce} |");

            int i = 0;
            foreach (string c in choix)
            {
                Console.WriteLine($"{c} -> {++i}");

                if (i < choix.Length) space.Print();
            }
            Console.WriteLine("");
        }

        /*
         * Affiche le menu de départ de l'application.
         */
        protected static void RunMenu()
        {
            Console.Clear();
            Console.WriteLine("========= Logiciel de consultation des projets de l'ENSC =========\n");
            AfficherGrilleChoix("Que voulez-vous faire ?", new string[] { "Consulter l'ensemble des projets", "Chercher un projet", "ajouter un projet", "Quitter" });

            bool doneRep = false;
            bool error = false;
            string rep;
            do
            {
                if (error)
                {
                    Console.Write("Ce que vous avez entré est incorrect \nRentrez à nouveau votre réponse : ");
                }
                rep = Console.ReadLine();

                if (rep == "1")
                {
                    ConsulterProjets();
                    doneRep = true;
                }
                else if (rep == "2")
                {
                    ChercherProjet(new Catalogue());
                    doneRep = true;
                }
                else if (rep == "3")
                {
                    AjouterProjet(new Catalogue());
                    doneRep = true;
                }
                else if (rep == "4")
                {
                    doneRep = true;
                    Environment.Exit(0);
                }
                else
                {
                    error = true;
                }
            } while (!doneRep);

        }

        /*
         * Affiche l'ensemble des projets ainsi qu'un menu de fonctionnalités
         */
        protected static void ConsulterProjets()
        {
            Catalogue catalogue = new Catalogue();

            bool doneConsulter = false;
            while (!doneConsulter)
            {
                Console.Clear();
                catalogue.PrintCatalogue();
                AfficherGrilleChoix("Que voulez-vous faire ?", new string[] { "Consulter un projet particulier", "Chercher un projet", "Ajouter un projet", "Supprimer un projet", "Quitter" });

                bool doneRep = false;
                bool error = false;
                string rep;
                do
                {
                    if (error)
                    {
                        Console.Write("Ce que vous avez entré est incorrect \nRentrez à nouveau votre réponse : ");
                    }
                    rep = Console.ReadLine();

                    if (rep == "1")
                    {
                        ConsulterProjetParticulier(catalogue, false);
                        doneRep = true;
                    }
                    else if (rep == "2")
                    {
                        ChercherProjet(catalogue);
                        doneRep = true;
                    }
                    else if (rep == "3")
                    {
                        AjouterProjet(catalogue);
                        doneRep = true;
                    }
                    else if (rep == "4")
                    {
                        SupprimerProjet(catalogue, false);
                        doneRep = true;
                    }
                    else if (rep == "5")
                    {
                        doneRep = true;
                        Environment.Exit(0);
                    }
                    else
                    {
                        error = true;
                    }
                } while (!doneRep);
            }

        }

        protected static void ChercherProjet(Catalogue catalogue)
        {

        }

        /*
        * Instance de création de projet. Permet à l'utilisateur de créer un projet.
        */
        protected static void AjouterProjet(Catalogue catalogue)
        {
            Console.Clear();
            Console.WriteLine("Vous avez décidé d'ajouter un projet. Rentrez à présent l'ensemble des informations nécessaires.\n");

            catalogue.AddProjet(Projet.CreateProjet());
            Console.WriteLine("\nL'ajout du projet est réussie. Appuyez sur une touche pour continuer.");
            Console.ReadKey();
        }

        /*
         * Instance de suppression de projet. Permet à l'utilisateur de choisir quel projet il veut supprimer.
         */
        protected static void SupprimerProjet(Catalogue catalogue, bool error)
        {
            if (error) Console.WriteLine("Ce que vous avez saisi est incorrect. Recommencez.\n");
            Console.Write("Saisissez le numéro du projet à supprimer : ");

            string rep = Console.ReadLine();
            int repInt = Int32.Parse(rep);
            if (repInt > catalogue.Projets.Length || repInt <= 0)
            {
                ConsulterProjetParticulier(catalogue, true);
            }

            else
            {
                catalogue.RemoveProjet(repInt - 1);
            }
        }

        /*
         * Permet à l'utilisateur de consulter un projet précis en lui demandant lequel il veut voir.
         * Si la réponse n'est pas valide, la fonction est utilisée à nouveau.
         * @arg catalogue, le Catalogue contenant tous les projets
         * @arg error, indique si le numéro de projet saisi est valide ou pas. 
         */
        protected static void ConsulterProjetParticulier(Catalogue catalogue, bool error)
        {

            if (error) Console.WriteLine("Ce que vous avez saisi est incorrect. Recommencez.\n");
            Console.Write("Saisissez le numéro du projet à consulter : ");

            string rep = Console.ReadLine();
            int repInt = Int32.Parse(rep);
            if (repInt > catalogue.Projets.Length || repInt <= 0)
            {
                ConsulterProjetParticulier(catalogue, true);
            }

            else
            {
                catalogue.Projets[repInt - 1].PrintInfos();

                AfficherGrilleChoix("Que voulez-vous faire ?", new string[] { "Revenir au menu précédent", "Supprimer ce projet" });
                bool doneRep = false;
                bool errorChoix = false;
                string repChoix;
                do
                {
                    if (errorChoix)
                    {
                        Console.Write("Ce que vous avez entré est incorrect \nRentrez à nouveau votre réponse : ");
                    }
                    repChoix = Console.ReadLine();

                    if (repChoix == "1")
                    {
                        doneRep = true;
                    }
                    else if (repChoix == "2")
                    {
                        catalogue.RemoveProjet(repInt - 1);
                        doneRep = true;
                    }
                    else
                    {
                        errorChoix = true;
                    }
                } while (!doneRep);
            }
        }

        static void Main(string[] args)
        {
            Role role = new Role("tuteur");
            Matiere matiere = new Matiere("cognition", "666");
            Projet.Date date = new Projet.Date("20/10/2020");
            Intervenant encadrant = new Enseignant("lespinet", "véro", new Role[] { role, role }, "ISM", matiere);
            Livrable livrable = new Livrable("vidéo", date, "ceci est un livrable");
            Enseignant enseignant = new Enseignant("lespinet", "véro", new Role[] { role, role }, "ISM", matiere);
            Intervenant[] plusieursIntervenants = new Intervenant[] { encadrant, encadrant };
            Eleve eleve = new Eleve("Parize", "antoine", new Role[] { role }, "2022", "1A");
            Projet projet = new Projet("projet de gestion de projets", "info", 1, new string[] { "2022", "2020" }, "bimbamboom", new string[] { "projet" }, encadrant,
                 new Livrable[] { livrable, livrable }, plusieursIntervenants, new Intervenant[] { encadrant }, new Eleve[] { eleve }, date, date);

            Intervenant test = new Eleve("Parize", "antoine", new Role[] { role }, "2022", "1A");
            Externe externe = new Externe("Parize", "antoine", new Role[] { role }, "2022");

            //projet.PrintInfos();
            //Catalogue a = new Catalogue();
            /*a.AddProjet(Projet.CreateProjet());
            a.Projets[1].PrintInfos();*/

            //a.PrintCatalogue();



            bool done = false;
            while (!done)
            {
                RunMenu();
            }



            Console.ReadKey();
        }
    }
}
