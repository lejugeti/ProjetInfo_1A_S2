using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Role role = new Role("tuteur");
            Matiere matiere = new Matiere("cognition", "666");
            Projet.Date date = new Projet.Date("20/10/2020");
            Intervenant encadrant = new Enseignant("lespinet", "véro", new Role[] { role, role }, "ISM", matiere);
            Livrable livrable = new Livrable("vidéo", date, "ceci est un livrable", encadrant);
            Enseignant enseignant = new Enseignant("lespinet", "véro", new Role[] { role, role }, "ISM", matiere);
            Intervenant[] plusieursIntervenants = new Intervenant[] { encadrant, encadrant };
            Eleve eleve = new Eleve("Parize", "antoine", new Role[] { role }, "2022", "1A");
            Projet projet = new Projet("projet de gestion de projets", "info", 1, new string[] { "2022", "2020" }, "bimbamboom", new string[] { "projet" },
                new Livrable[] { livrable , livrable}, plusieursIntervenants, new Intervenant[] { encadrant }, new Eleve[] { eleve }, date, date);

            Intervenant test = new Eleve("Parize", "antoine", new Role[] { role }, "2022", "1A");
            Externe externe = new Externe("Parize", "antoine", new Role[] { role }, "2022");
           
            //projet.PrintInfos();
            
            

            Console.ReadKey();
        }
    }
}
