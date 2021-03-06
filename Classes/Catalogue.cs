﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Program
{
    public class Catalogue
    {
        public static Projet[] Projets { get; set; }
        public static int IdMaxProjets { get; set; } // L'ID maximum des projets présents dans le catalogue


        //Constructeurs

        /*
         * Construit l'instance de Catalogue en s'appuyant sur le fichier XML contenant l'ensemble des projets.
         */
        public Catalogue()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Projet[]));
                using (StreamReader sr = new StreamReader("../../catalogue.xml"))
                {
                    Projets = (Projet[])serializer.Deserialize(sr);
                }
            }
            catch (FileNotFoundException) // Si le fichier n'existe pas on le crée
            {
                Projets = new Projet[0];

                XmlSerializer serializer = new XmlSerializer(typeof(Projet[]));
                using (StreamWriter sw = new StreamWriter("../../catalogue.xml"))
                {
                    serializer.Serialize(sw, Projets);
                }
            }

            // on détermine l'id max des projets contenus dans le fichier XML
            IdMaxProjets = FindIdGlobal();
        }


        //Méthodes

        /*
         * Permet d'obtenir l'id global des projets au lancement du programme.
         * @return idMax, l'ID le plus haut trouvé dans le catalogue de projet
         */
        public static int FindIdGlobal()
        {
            int idMax = 0;
            foreach (Projet projet in Catalogue.Projets)
            {
                if (projet.Id > idMax) idMax = projet.Id;
            }

            return idMax;
        }

        /*
         * Cherche dans le catalogue de projet le projet possèdant l'Id passé en argument.
         * Renvoie l'indice du projet si la recherche réussie, renvoie -1 sinon.
         * @arg idProjet, l'Id du projet à trouver dans le catalogue
         * @return l'indice du projet matchant l'Id passé en argument, -1 si la recherche échoue.
         */
        public static int FindProjet(int idProjet)
        {
            for (int i = 0; i < Projets.Length; i++)
            {
                if (Projets[i].Id == idProjet) return i;
            }

            return -1;
        }

        /*
         * Sauvegarde le catalogue dans un fichier XML par sérialisation
         */
        public static void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Projet[]));
            using (StreamWriter sw = new StreamWriter("../../catalogue.xml"))
            {
                serializer.Serialize(sw, Projets);
            }
        }

        /*
         * Permet d'ajouter un projet au catalogue de projet déjà existant. Le catalogue est ensuite 
         * écrit dans un fichier XML.
         * @arg projet, le projet à ajouter au catalogue
         */
        public static void AddProjet(Projet projet)
        {
            List<Projet> tmpProjet = Catalogue.ToList();
            tmpProjet.Add(projet);
            Projets = tmpProjet.ToArray(); // on met à jour en mémoire la liste de projets de Catalogue

            Save();
        }

        /*
         * Permet de supprimer un projet du catalogue en indiquant en argument l'id du projet à supprimer.
         * Le catalogue mis à jour est ensuite écrit dans un fichier XML.
         * @arg, l'id du projet à supprimer
         */
        public static void RemoveProjet(int idProjet)
        {
            List<Projet> tmpProjet = Catalogue.ToList();

            // On essaye de trouver le projet en fonction de son Id pour le supprimer
            for (int i = 0; i < tmpProjet.Count; i++)
            {
                if (Catalogue.Projets[i].Id == idProjet) tmpProjet.RemoveAt(i);
            }

            Projets = tmpProjet.ToArray(); // on met à jour en mémoire la liste de projets de Catalogue

            Save(); // le catalogue est sauvegardé dans le fichier XML
        }

        /*
         * Renvoie le catalogue de projet sous forme de liste de Projet
         * @return projets, une list de Projet
         */
        public static List<Projet> ToList()
        {
            List<Projet> projets = new List<Projet>();

            foreach (Projet p in Projets)
            {
                projets.Add(p);
            }

            return projets;
        }

        /*
         * Affiche dans la console l'ensemble des projets du catalogues en les numérotant de 1 à n.
         */
        public static void PrintCatalogue()
        {
            if (Projets.Length == 0)
            {
                Console.WriteLine("Il n'y a pas encore de projets enregistrés");
            }
            else
            {
                Console.WriteLine("===== Liste de tous les projets =====");
            }

            Program.AfficherListeProjets(Projets);
        }
    }
}
