using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Program
{
    class Program
    {   /*
        * Permet d'afficher un ensemble de choix associés à un nombre pour que l'utilisateur indique sa réponse
        * @arg enonce, l'énoncé du choix que doit réaliser l'utilisateur 
        * @arg choix, le tableau des différents choix proposés à l'utilisateur
        */
        public static void AfficherGrilleChoix(string enonce, string[] choix)
        {
            Escapes space = new Escapes(enonce);
            Console.Write(enonce);

            foreach(string c in choix)
            {
                Console.Write($" |{c}");

            }
            Console.WriteLine("");

            space.Print(); //on écrit l'espace correspondant à l'énoncé du choix pour décaler ce qu'on va écrire ensuite

            int i = 1;
            foreach (string c in choix)
            {
                Console.Write($"{i} |");
            }
            Console.WriteLine("");
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
            Projet projet = new Projet("projet de gestion de projets", "info", 1, new string[] { "2022", "2020" }, "bimbamboom", new string[] { "projet" },  encadrant,
                 new Livrable[] { livrable, livrable }, plusieursIntervenants, new Intervenant[] { encadrant }, new Eleve[] { eleve }, date, date);

            Intervenant test = new Eleve("Parize", "antoine", new Role[] { role }, "2022", "1A");
            Externe externe = new Externe("Parize", "antoine", new Role[] { role }, "2022");

            //projet.PrintInfos();
            //Catalogue a = new Catalogue();
            /*a.AddProjet(Projet.CreateProjet());
            a.Projets[1].PrintInfos();*/

            //a.PrintCatalogue();


            AfficherGrilleChoix("bonjour", new string[] { "un", "deux", "trois"});

            Console.ReadKey();
        }
    }
}
