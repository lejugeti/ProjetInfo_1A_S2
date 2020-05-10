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

        //Méthodes
        public void AddProjet(Projet projet)
        {
            List<Projet> tmpProjet = new List<Projet>();

            foreach(Projet p in Projets)
            {
                tmpProjet.Add(p);
            }

            tmpProjet.Add(projet);
            Projets = tmpProjet.ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(Projet[]));
            using(StreamWriter sw = new StreamWriter("../../../test.xml"))
            {
                serializer.Serialize(sw, Projets);
            }
        }

        public void RemoveProjet(int idProjet)
        {
            List<Projet> tmpProjet = new List<Projet>();

            foreach (Projet p in Projets)
            {
                tmpProjet.Add(p);
            }

            tmpProjet.RemoveAt(idProjet);
            Projets = tmpProjet.ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(Projet[]));
            using (StreamWriter sw = new StreamWriter("../../../../test.xml"))
            {
                serializer.Serialize(sw, Projets);
            }
        }

        public void PrintCatalogue()
        {
            Console.WriteLine("===== Liste de tous les projets =====");
            for(int i = 0; i < Projets.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Projets[i].Intitule}");
            }
        }

        
    }
}
