using System;

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
            Console.ReadKey();
        }
    }
}
