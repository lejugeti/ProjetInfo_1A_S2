using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Program
{
    public class Escapes
    {
        public List<string> Spaces { get; set; }

        public string this[int i]
        {
            get { return Spaces[i]; }
            set { Spaces[i] = value; }
        }

        //Constructeur
        public Escapes()
        {
            Spaces = new List<string>();
        }

        public Escapes(string newSpaces)
        {
            Spaces = new List<string>();
            Spaces.Add(newSpaces);
        }
        
        //Méthodes
        public void Print()
        {
            for (int i = 0; i < Spaces.Count; i++)
            {
                Console.Write($"{Spaces[i]}|");
            }
        }

        public void Add(string spaces)
        {
            Spaces.Add(new String(' ', spaces.Length + 1));
        }
    }
}
