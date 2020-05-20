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
            Catalogue catalogue = new Catalogue();

            bool doneRep = false;
            bool error = false;
            string rep;
            do
            {
                Console.Clear();
                Console.WriteLine("========= Logiciel de consultation des projets de l'ENSC =========\n");
                AfficherGrilleChoix("Que voulez-vous faire ?", new string[] { "Consulter l'ensemble des projets", "Ajouter un projet", "Quitter" });

                if (error)
                {
                    Console.Write("Ce que vous avez entré est incorrect \nRentrez à nouveau votre réponse : ");
                }
                rep = Console.ReadLine();

                if (rep == "1")
                {
                    ConsulterProjets();
                    doneRep = false;
                }
                else if (rep == "2")
                {
                    AjouterProjet(false);
                    doneRep = false;
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
            // Boucle ne se terminant que si l'utilisateur décide de quitter le programme
            bool doneConsulter = false;
            while (!doneConsulter)
            {
                Console.Clear();
                Catalogue.PrintCatalogue();

                // Si le catalogue est vide on restreint les options
                if (Catalogue.Projets.Length == 0)
                {
                    AfficherGrilleChoix("Que voulez-vous faire ?", new string[] { "Ajouter un projet", "Quitter" });

                    // boucle while pour demander une réponse valide à l'utilisateur
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
                            AjouterProjet(false);
                            doneRep = true;
                        }
                        else if (rep == "2")
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

                // Si le Catalogue n'est pas vide
                else
                {
                    AfficherGrilleChoix("Que voulez-vous faire ?", new string[] { "Consulter un projet particulier", "Chercher un projet", "Ajouter un projet", "Supprimer un projet", "Quitter" });

                    // boucle while pour demander une réponse valide à l'utilisateur
                    bool doneRep = false;
                    bool error = false;
                    do
                    {
                        if (error)
                        {
                            Console.Write("Ce que vous avez entré est incorrect \nRentrez à nouveau votre réponse : ");
                        }
                        int rep = TryParseReponse(Console.ReadLine());

                        if (rep == 1)
                        {
                            ConsulterProjetParticulier(false);
                            doneRep = true;
                        }
                        else if (rep == 2)
                        {
                            ChercherProjet();
                            doneRep = true;
                        }
                        else if (rep == 3)
                        {
                            AjouterProjet(false);
                            doneRep = true;
                        }
                        else if (rep == 4)
                        {
                            SupprimerProjet(false);
                            doneRep = true;
                        }
                        else if (rep == 5)
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
        }

        /*
         * Permet à l'utilisateur de consulter un projet précis en lui demandant lequel il veut voir.
         * Si la réponse n'est pas valide, la fonction est utilisée à nouveau.
         * Cette fonction n'est utilisée que s'il y a une affichage global du catalogue, pas lorsqu'il y a une recherche.
         * @arg catalogue, le Catalogue contenant tous les projets
         * @arg error, indique si le numéro de projet saisi est valide ou pas. 
         */
        protected static void ConsulterProjetParticulier(bool error)
        {

            if (error) Console.WriteLine("Ce que vous avez saisi est incorrect. Recommencez.\n");
            Console.Write("Saisissez le numéro du projet à consulter : ");


            string rep = Console.ReadLine();
            int repAffichage = TryParseReponse(rep); // le numéro du projet que l'utilisateur veut consulter

            if (repAffichage > Catalogue.Projets.Length || repAffichage <= 0)
            {
                ConsulterProjetParticulier(true);
            }

            else
            {
                bool doneProjetParticulier = false;
                bool errorProjetParticulier = false;
                string repProjetParticulier;
                do
                {
                    Console.Clear();

                    Catalogue.Projets[repAffichage - 1].PrintInfos();
                    AfficherGrilleChoix("Que voulez-vous faire ?", new string[] { "Revenir au menu précédent", "Modifier ce projet", "Supprimer ce projet" });

                    if (errorProjetParticulier)
                    {
                        Console.Write("Ce que vous avez entré est incorrect \nRentrez à nouveau votre réponse : ");
                    }

                    Console.Write("Votre réponse : ");
                    repProjetParticulier = Console.ReadLine();

                    //Revenir au menu précédent
                    if (repProjetParticulier == "1")
                    {
                        doneProjetParticulier = true;
                    }

                    // Modifier le projet
                    else if (repProjetParticulier == "2")
                    {
                        ChangerProjet(Catalogue.Projets[repAffichage - 1]);
                    }

                    // Supprimer le projet
                    else if (repProjetParticulier == "3")
                    {
                        int removeId = Catalogue.Projets[repAffichage - 1].Id;
                        Catalogue.RemoveProjet(removeId);
                        doneProjetParticulier = true;
                    }
                    else
                    {
                        errorProjetParticulier = true;
                    }
                } while (!doneProjetParticulier);
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

            if (error) Console.WriteLine("\nCe que vous avez saisi est incorrect. Recommencez.\n");
            Console.Write("Saisissez le numéro du projet à consulter : ");

            string rep = Console.ReadLine();
            int repAffichage = TryParseReponse(rep);

            if (repAffichage > projets.Count || repAffichage <= 0)
            {
                ConsulterProjetParticulier(projets, true);
            }

            else
            {
                bool doneProjetParticulier = false;
                bool errorProjetParticulier = false;
                string repProjetParticulier;
                do
                {
                    projets[repAffichage - 1].PrintInfos();
                    AfficherGrilleChoix("Que voulez-vous faire ?", new string[] { "Revenir au menu précédent", "Modifier ce projet", "Supprimer ce projet" });

                    if (errorProjetParticulier)
                    {
                        Console.Write("Ce que vous avez entré est incorrect \nRentrez à nouveau votre réponse : ");
                    }

                    Console.Write("Votre réponse : ");
                    repProjetParticulier = Console.ReadLine();

                    //Revenir au menu précédent
                    if (repProjetParticulier == "1")
                    {
                        doneProjetParticulier = true;
                    }

                    // Modifier le projet
                    else if (repProjetParticulier == "2")
                    {
                        ChangerProjet(projets[repAffichage - 1]);
                    }

                    // Supprimer le projet
                    else if (repProjetParticulier == "3")
                    {
                        int removeId = projets[repAffichage - 1].Id;
                        Catalogue.RemoveProjet(removeId);
                        projets.RemoveAt(repAffichage - 1);
                        doneProjetParticulier = true;
                    }

                    else
                    {
                        errorProjetParticulier = true;
                    }
                } while (!doneProjetParticulier);
            }
        }

        /*
         * Interface de recherche de projet selon des méthodes de recherche différentes.
         * Si le choix de recherche n'est pas valide, la fonction est utilisée à nouveau.
         * Cette interface englobe toutes les possibilités de recherche.
         * @arg catalogue, le Catalogue contenant tous les projets
         * @arg error, indique si le numéro de projet saisi est valide ou pas. 
         */
        protected static void ChercherProjet()
        {
            bool error = false;
            bool doneRecherches = false;

            //Tant que l'utilisateur veut faire des recherches
            while (!doneRecherches)
            {
                Console.Clear();
                Console.WriteLine("======= Recherche de Projet =======");

                if (error) Console.WriteLine("\n Ce que vous avez rentré est incorrect, recommencez.");
                AfficherGrilleChoix("Quel type de recherche souhaitez-vous faire ?", new string[] { "Recherche générale", "Par intitulé", "Par nom d'élève", "Par année de réalisation", "Par promotion", "Par mots clés", "Revenir au menu précédent" });

                Console.Write("Votre réponse : ");
                string rep = Console.ReadLine();

                if (rep == "1")
                {
                    EffectuerRecherchePrecise("Générale");
                    error = false;
                }
                else if (rep == "2")
                {
                    EffectuerRecherchePrecise("Par intitulé");
                    error = false;
                }
                else if (rep == "3")
                {
                    EffectuerRecherchePrecise("Par Elève");
                    error = false;
                }
                else if (rep == "4")
                {
                    EffectuerRecherchePrecise("Par année");
                    error = false;
                }
                else if (rep == "5")
                {
                    EffectuerRecherchePrecise("Par Promotion");
                    error = false;
                }
                else if (rep == "6")
                {
                    EffectuerRecherchePrecise("Par mots clés");
                    error = false;
                }
                else if (rep == "7")
                {
                    //Permet de sortir du module de recherche
                    doneRecherches = true;
                }
                else
                {
                    // Si la réponse est invalide
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
        protected static void EffectuerRecherchePrecise(string type)
        {
            Console.Clear();
            Console.WriteLine($"======= Recherche {type} ======");
            if (type == "Générale") Console.Write("\nCe type de recherche est général, il renverra tous les projets matchant votre requête.");
            Console.Write("\n\nSaisissez votre recherche : ");

            string recherche = Console.ReadLine(); // on demande à l'utilisateur sa recherche

            List<Projet> resultats = new List<Projet>();
            if (type == "Générale")
            {
                resultats = Recherche.RechercheGenerale(recherche);
            }
            else if (type == "Par intitulé")
            {
                resultats = Recherche.RechercheParIntitule(recherche);
            }
            else if (type == "Par Elève")
            {
                resultats = Recherche.RechercheParEleve(recherche);
            }
            else if (type == "Par année")
            {
                resultats = Recherche.RechercheParAnnee(recherche);
            }
            else if (type == "Par Promotion")
            {
                resultats = Recherche.RechercheParPromotion(recherche);
            }
            else if (type == "Par mots clés")
            {
                resultats = Recherche.RechercheParMotsClefs(recherche);
            }

            //Affichage des projets trouvés et consultation des projets
            bool doneRep;
            if (resultats.Count > 0)  //Si la recherche trouve des résultats ils sont alors affichés
            {
                doneRep = false;
            }
            else
            {
                Console.WriteLine("Nous n'avons pas trouvé de projets correspondant à votre recherche. Appuyez sur une touche pour continuer.");
                Console.ReadKey();
                doneRep = true;
            }

            // Tant que l'utilisateur n'a pas fini de consulter les projets obtenus via la recherche
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
                    errorChoix = false;
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
        protected static void AjouterProjet(bool error)
        {
            Console.Clear();
            Console.Write("Vous avez décidé d'ajouter un projet. Voulez-vous continuer ? \nVotre réponse (O/N) : ");

            if (error) Console.WriteLine("Ce que vous avez saisi est incorrect, recommencez");
            string confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "O")
            {
                Catalogue.AddProjet(Projet.CreateProjet());
                Console.WriteLine("\nL'ajout du projet est réussie. Appuyez sur une touche pour continuer.");
                Console.ReadKey();
            }
            else if (confirmation.ToUpper() == "N")
            {
                Console.WriteLine("Ajout de projet annulée. Appuyez sur n'importe quelle touche pour continuer.");
                Console.ReadKey();
            }
            else AjouterProjet(true);
        }

        /*
         * Instance de suppression de projet. Permet à l'utilisateur de choisir quel projet il veut supprimer.
         * Cette fonction est appelée lorsque l'utilisateur consulter l'ensemble des projets et pas un projet
         * particulier.
         * Le projet est supprimé de l'arborescence XML supportant le programme.
         * @arg catalogue, le Catalogue contenant tous les projets
         * @arg error, indique si le numéro de projet saisi est valide ou pas. On part du principe qu'il n'y a pas d'erreur pour le premier appel.
         */
        protected static void SupprimerProjet(bool error)
        {
            if (error) Console.WriteLine("\nCe que vous avez saisi est incorrect. Recommencez.\n");
            Console.Write("Saisissez le numéro du projet à supprimer (Y pour quitter) : ");

            string rep = Console.ReadLine();
            int repInt = TryParseReponse(rep);

            if (rep.ToUpper() == "Y")
            {
                // Permet de sortir de la fonction si l'utilisateur ne veut pas supprimer de projet en fait
            }
            else if (repInt > Catalogue.Projets.Length || repInt <= 0)
            {
                SupprimerProjet(true);
            }
            else
            {
                int removeId = Catalogue.Projets[repInt - 1].Id;
                Catalogue.RemoveProjet(removeId);
            }
        }

        /*
         * Interface de changement de projet. Permet à l'utilisateur d'effectuer des modifications
         * sur un projet déjà existant.
         * @arg idProjet, l'Id du projet à mettre à jour
         */
        protected static void ChangerProjet(Projet projetToChange)
        {
            int indexProjet = Catalogue.FindProjet(projetToChange.Id);

            bool doneMaj = false;
            bool errorMaj = false;
            while (!doneMaj)
            {
                Console.Clear();

                projetToChange.PrintInfos();

                Console.WriteLine("\n====== Mise à jour d'un projet ======\n");
                AfficherGrilleChoix("Que voulez-vous faire ?", new string[] { "Changer l'intitulé", "Changer le type", "Modifier le Client", "Ajouter un livrable", "Modifier un livrable", "Ajouter un encadrant", "Modifier un encadrant", "Ajouter un reviewer", "Modifier un reviewer", "Ajouter un élève", "Modifier un élève", "Modifier la date de début", "Modifier la date de fin", "Revenir au menu précédent" });

                if (errorMaj) Console.WriteLine("Ce que vous avez saisi est incorrect, recommencez.");
                Console.Write("Votre réponse : ");
                int reponse = TryParseReponse(Console.ReadLine());

                if (reponse <= 0 || reponse > 14)
                {
                    errorMaj = true;
                }
                else if (reponse == 14)
                {
                    doneMaj = true;
                }
                else
                {
                    errorMaj = false;
                    projetToChange.MiseAJour(reponse);
                    Catalogue.Projets[indexProjet] = projetToChange;
                    Catalogue.Save();
                }
            }
        }

        /*
         * Tente de parse la réponse de l'utilisateur. Si le Parse réussit la fonction renvoie la réponse
         * sous forme de int, -1 si le parse échoue.
         * @arg rep, la réponse de l'utilisateur obtenue après un Console.ReadLine();
         */
        public static int TryParseReponse(string rep)
        {
            try
            {
                int parsed = Int32.Parse(rep);
                return parsed;
            }
            catch (FormatException)
            {
                return -1;
            }
        }

        static void Main(string[] args)
        {
            RunMenu();
        }
    }
}
