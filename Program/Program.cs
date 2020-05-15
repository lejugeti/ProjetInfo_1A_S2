using System;
using System.Collections.Generic;

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
        * Permet d'afficher l'ensemble des projets d'un tableau de projet. Les projets sont numérotés.
        * @arg projets, le tableau des projets à imprimer dans la console.
        */
        public static void AfficherListeProjets(Projet[] projets)
        {
            for (int i = 0; i < projets.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {projets[i].Intitule}");
            }
            Console.WriteLine("");
        }

        /*
        * Permet d'afficher l'ensemble des projets d'une liste de projet. Les projets sont numérotés.
        * @arg projets, le tableau des projets à imprimer dans la console.
        */
        public static void AfficherListeProjets(List<Projet> projets)
        {
            for (int i = 0; i < projets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {projets[i].Intitule}");
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
            AfficherGrilleChoix("Que voulez-vous faire ?", new string[] { "Consulter l'ensemble des projets", "ajouter un projet", "Quitter" });

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
                    AjouterProjet(new Catalogue());
                    doneRep = true;
                }
                else if (rep == "3")
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

        /*
         * Permet à l'utilisateur de consulter un projet précis en lui demandant lequel il veut voir.
         * Si la réponse n'est pas valide, la fonction est utilisée à nouveau.
         * @arg catalogue, le Catalogue contenant tous les projets
         * @arg error, indique si le numéro de projet saisi est valide ou pas. 
         */
        protected static void ConsulterProjetParticulier(List<Projet> projets, bool error)
        {

            if (error) Console.WriteLine("Ce que vous avez saisi est incorrect. Recommencez.\n");
            Console.Write("Saisissez le numéro du projet à consulter : ");

            string rep = Console.ReadLine();
            int repInt = Int32.Parse(rep);
            
            if (repInt > projets.Count || repInt <= 0)
            {
                ConsulterProjetParticulier(projets, true);
            }

            else
            {
                projets[repInt - 1].PrintInfos();

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
                        /*catalogue.RemoveProjet(repInt - 1);*/
                        doneRep = true;
                    }
                    else
                    {
                        errorChoix = true;
                    }
                } while (!doneRep);
            }
        }
        
        /*
         * Interface de recherche de projet selon des méthodes de recherche différentes.
         * Si le choix de recherche n'est pas valide, la fonction est utilisée à nouveau.
         * @arg catalogue, le Catalogue contenant tous les projets
         * @arg error, indique si le numéro de projet saisi est valide ou pas. 
         */
        protected static void ChercherProjet(Catalogue catalogue)
        {
            bool error = false;
            bool doneRecherches = false;

            //Tant que l'utilisateur veut faire des recherches
            while(!doneRecherches)
            {
                Console.Clear();
                Console.WriteLine("======= Recherche de Projet =======");
                
                if (error) Console.WriteLine("\n Ce que vous avez rentré est incorrect, recommencez.");
                AfficherGrilleChoix("Quel type de recherche souhaitez-vous faire ?", new string[] { "Recherche générale", "Par nom d'élève", "Par année de réalisation", "Par promotion", "Par mots clés", "Revenir au menu précédent" });

                Console.Write("Votre réponse : ");
                string rep = Console.ReadLine();

                if (rep == "1")
                {
                    EffectuerRecherchePrecise(catalogue, "Générale");
                }
                else if (rep == "2")
                {
                    EffectuerRecherchePrecise(catalogue, "Par Elève");
                }
                else if (rep == "3")
                {
                    EffectuerRecherchePrecise(catalogue, "Par année");
                }
                else if (rep == "4")
                {
                    EffectuerRecherchePrecise(catalogue, "Par Promotion");
                }
                else if (rep == "5")
                {
                    EffectuerRecherchePrecise(catalogue, "Par mots clés");
                }
                else if (rep == "6")
                {   
                    //Permet de sortir du module de recherche
                    doneRecherches = true;
                }
                else
                {
                    error = true;
                }
            }
            
        }

        /*
         * Effectue la recherche choisie et permet à l'utilisateur de choisir s'il veut consulter des projets
         * ou revenir en arrière.
         * @arg catalogue, le Catalogue contenant tous les projets
         * @arg type, le type de recherche effectuée 
         */
        protected static void EffectuerRecherchePrecise(Catalogue catalogue, string type)
        {
            Console.Clear();
            Console.WriteLine($"======= Recherche {type} ======");
            if(type == "Générale") Console.Write("\nCe type de recherche est général, il renverra tous les projets matchant votre requête.");
            Console.Write("\n\nSaisissez votre recherche : ");

            string rep = Console.ReadLine();

            List<Projet> resultats = new List<Projet>();
            if (type == "Générale")
            {
                resultats = Recherche.RechercheGenerale(catalogue, rep);
            }
            else if (type == "Par Elève")
            {
                resultats = Recherche.RechercheParEleve(catalogue, rep);
            }
            else if (type == "Par année")
            {
                resultats = Recherche.RechercheParAnnee(catalogue, rep);
            }
            else if (type == "Par Promotion")
            {
                resultats = Recherche.RechercheParPromotion(catalogue, rep);
            }
            else if (type == "Par mots clés")
            {
                resultats = Recherche.RechercheParMotsClefs(catalogue, rep);
            }

            //Affichage des projets trouvés et consultation des projets
            bool doneRep;
            
            if (resultats.Count > 0)  //Si la recherche trouve des résultats elle est effectuée
            {
                doneRep = false;
            }
            else
            {
                Console.WriteLine("Nous n'avons pas trouvé de projets correspondant à votre recherche. Appuyez sur une touche pour continuer.");
                Console.ReadKey();
                doneRep = true;
            }

            bool errorChoix = false;
            string repChoix;
            while (!doneRep)
            {
                Console.Clear();
                Console.WriteLine("======= Résultats de votre recherche =======\n");
                AfficherListeProjets(resultats);

                AfficherGrilleChoix("Que voulez-vous faire ?", new string[] { "Consulter un projet particulier", "Effectuer une autre recherche" });
                
                if (errorChoix)
                {
                    Console.Write("Ce que vous avez entré est incorrect \nRentrez à nouveau votre réponse : ");
                }
                Console.Write("Votre réponse : ");
                repChoix = Console.ReadLine();

                if (repChoix == "1")
                {
                    ConsulterProjetParticulier(resultats, false);                 
                }
                else if (repChoix == "2")
                {
                    doneRep = true;
                }
                else
                {
                    errorChoix = true;
                }
            }
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
         * Le projet est supprimé de l'arborescence XML supportant le programme.
         * @arg catalogue, le Catalogue contenant tous les projets
         * @arg error, indique si le numéro de projet saisi est valide ou pas. On part du principe qu'il n'y a pas d'erreur pour le premier appel.
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

            Catalogue a = new Catalogue();


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
