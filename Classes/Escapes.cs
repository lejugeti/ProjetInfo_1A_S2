using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Program
{
    /*
     * Classe permettant de gérer l'alignement de l'affichage dans la console en stockant
     * des espaces et en permettant de les afficher de manière "propre".
     */
    public class Escapes
    {
        public List<string> Spaces { get; set; }

        //Constructeur
        public Escapes()
        {
            Spaces = new List<string>();
        }

        //Crée un objet Escapes en ajoutant directement 
        public Escapes(string newSpaces)
        {
            Spaces = new List<string>();
            Spaces.Add(new String(' ', newSpaces.Length + 1));
        }
        
        //Méthodes

        /*
         * Affiche les espaces contenus dans l'instance d'objet Escapes pour décaler l'affichage
         */
        public void Print()
        {
            for (int i = 0; i < Spaces.Count; i++)
            {
                Console.Write($"{Spaces[i]}|");
            }
        }

        /*
         * Ajoute un ensemble d'espaces à l'instance de l'objet.
         */
        public void Add(string spaces)
        {
            Spaces.Add(new String(' ', spaces.Length + 1));
        }
    }
}
