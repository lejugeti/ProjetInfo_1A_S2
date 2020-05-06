using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class Matiere
    {
        protected string _nom;
        protected string _code;

        //Propriétés
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }


        //Constructeur
        public Matiere(string nom, string code)
        {
            Nom = nom;
            Code = code;
        }

        //Méthode
        public static Matiere CreateMatiere()
        {
            Console.Write("Rentrez la matière de l'enseignant : ");
            string nom = Console.ReadLine();
            Console.Write("Rentrez le Code de la matière : ");
            string code = Console.ReadLine();

            return new Matiere(nom, code);
        }

        public void PrintInfos()
        {
            Console.Write("Matière");
            Console.WriteLine($" | Nom : {Nom}");
            Console.WriteLine($"        | Code : {Code}");
            Console.WriteLine("");
        }

        public virtual void PrintInfosCol(Escapes escapes, string nomInfo)
        {
            escapes.Add(nomInfo);

            Console.Write(nomInfo);
            //Permet d'afficher les infos de l'individu 
            Console.WriteLine($" |Nom : {Nom}");
            escapes.Print();
            Console.WriteLine($"Code : {Code}");
            
            escapes.Spaces.RemoveAt(1);
        }
    }
}
