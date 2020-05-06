using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class Role
    {
        protected string _nom;

        //Propriétés
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        //Constructeurs
        public Role(string nom)
        {
            _nom = nom;
        }

        //Méthodes
        public static Role CreateRole()
        {
            Console.Write("Indiquez le rôle de cette personne : ");
            string role = Console.ReadLine();

            return new Role(role);
        }

        public void PrintInfos()
        {
            Console.Write($"{Nom} ");
        }
    }
}
