using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Program
{
    public class Catalogue
    {
        public Projet[] Projets { get; set; }
        
        //Constructeurs

        /*
         * Construit l'instance de Catalogue en s'appuyant sur le fichier XML contenant l'ensemble des projets.
         */
        public Catalogue()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Projet[]));
                using (StreamReader sr = new StreamReader("../../../test.xml"))
                {
                    Projets = (Projet[])serializer.Deserialize(sr);
                }
            }
            catch (FileNotFoundException)
            {
                Projets = new Projet[0];

                XmlSerializer serializer = new XmlSerializer(typeof(Projet[]));
                using (StreamWriter sw = new StreamWriter("../../../test.xml"))
                {
                    serializer.Serialize(sw, Projets);
                }
            }
        }

        public Catalogue(List<Projet> projets)
        {
            Projets = projets.ToArray();
        }

        public Catalogue(Projet[] projets)
        {
            Projets = projets;
        }

        //Méthodes
        public void AddProjet(Projet projet)
        {
            List<Projet> tmpProjet = this.ToList();
            tmpProjet.Add(projet);
            Projets = tmpProjet.ToArray(); // on met à jour en mémoire la liste de projets de Catalogue

            XmlSerializer serializer = new XmlSerializer(typeof(Projet[]));
            using(StreamWriter sw = new StreamWriter("../../../test.xml"))
            {
                serializer.Serialize(sw, Projets);
            }
        }

        public void RemoveProjet(int idProjet)
        {
            List<Projet> tmpProjet = this.ToList();
            tmpProjet.RemoveAt(idProjet);
            Projets = tmpProjet.ToArray(); // on met à jour en mémoire la liste de projets de Catalogue

            XmlSerializer serializer = new XmlSerializer(typeof(Projet[]));
            using (StreamWriter sw = new StreamWriter("../../../../test.xml"))
            {
                serializer.Serialize(sw, Projets);
            }
        }

        public List<Projet> ToList()
        {
            List<Projet> projets = new List<Projet>();

            foreach(Projet p in Projets)
            {
                projets.Add(p);
            }

            return projets;
        }
        public void PrintCatalogue()
        {
            if(Projets.Length == 0)
            {
                Console.WriteLine("Il n'y a pas encore de projets enregistrés");
            }
            Console.WriteLine("===== Liste de tous les projets =====");
            /*for(int i = 0; i < Projets.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Projets[i].Intitule}");
            }
            Console.WriteLine("");*/

            Program.AfficherListeProjets(Projets);
        }

        
    }
}
