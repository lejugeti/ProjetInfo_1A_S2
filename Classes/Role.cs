using System;

namespace Program
{
    public class Role
    {
        protected string _nom;

        //Propriétés
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        //Constructeurs

        /*
         * Crée une instance de Role en spécifiant directement le nom du rôle
         */
        public Role(string nom)
        {
            _nom = nom;
        }

        //Constructeur vide utilisé par lors de la désérialisation
        public Role()
        {

        }


        //Méthodes

        /*
         * Affiche dans la console le nom du rôle
         */
        public void PrintInfos()
        {
            Console.Write($"{Nom} ");
        }
    }
}
