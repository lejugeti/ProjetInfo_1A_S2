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
            Matiere matiere = new Matiere("cognition", "666", new string[] { "projets relous" });
            Livrable livrable = new Livrable("vidéo", "aujourd'hui", "ceci est un livrable");
            Intervenant encadrant = new Enseignant("lespinet", "véro", new Role[] { role }, new string[] { "ISM" }, new Matiere[] { matiere });
            Eleve eleve = new Eleve("Parize", "antoine", new Role[] { role }, "2022", "2019-2020");

            Projet projet = new Projet("info", 1, "2020", new string[] { "2022" }, "bimbamboom", new string[] { "projet" },
                new Livrable[] { livrable }, encadrant, new Intervenant[] { encadrant }, new Eleve[] { eleve }, "mtn", "là", 1);

            Console.WriteLine(projet.Sujet);

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

            

            Console.ReadKey();
        }
    }
}
