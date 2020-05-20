using System;

namespace Program
{
    public class Matiere
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

        //Constructeur vide utilisé par lors de la désérialisation
        public Matiere()
        {

        }

        //Méthode

        /*
         * Interface de création de la matière. L'utilisateur doit rentrer toutes les informations nécessaires à la création
         */
        public static Matiere CreateMatiere()
        {
            Console.Write("Rentrez la matière de l'enseignant : ");
            string nom = Console.ReadLine();
            Console.Write("Rentrez le Code de la matière : ");
            string code = Console.ReadLine();

            return new Matiere(nom, code);
        }

        /*
         * Affiche l'ensemble des informations de la matière en respectant l'alignement
         * @arg escapes, un objet Escapes capable de gérer les espaces pour aligner l'affichage
         * @arg nomInfo, le nom de l'information à afficher (ici "Matière")
         */
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
