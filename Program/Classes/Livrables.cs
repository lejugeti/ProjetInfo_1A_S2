using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Program
{
    public class Livrable
    {
        protected string _type;
        protected Projet.Date _deadline;
        protected string _description;
        protected string _lienExterne;

        //Propriétés
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public Projet.Date Deadline
        {
            get { return _deadline; }
            set { _deadline = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string LienExterne
        {
            get { return _lienExterne; }
            set { _lienExterne = value; }
        }

        //Constructeurs
        public Livrable(string type, Projet.Date deadline, string description, string lien)
        {
            _type = type; 
            _deadline = deadline;
            _description = description;
            _lienExterne = lien;
        }
        public Livrable(string type, Projet.Date deadline, string description)
        {
            _type = type;
            _deadline = deadline;
            _description = description;
        }
        public Livrable()
        {

        }
        //Méthodes
        public static Livrable CreateLivrable()
        {
            
            Console.WriteLine("\nVeuillez rentrer les informations relatives au nouveau livrable");
            Console.Write("Le type de votre livrable : ");
            string type = Console.ReadLine();

            string deadlineInput = "";
            bool errorDeadline = false;
            do
            {
                if(errorDeadline) Console.Write("\nLa date est incorrecte, utilisez le format dd/mm/yyyy : ");
                else Console.Write("Sa deadline (dd/mm/yyyy): ");

                deadlineInput = Console.ReadLine();
                bool isDeadline = Projet.Date.IsDate(deadlineInput);

                if (isDeadline) errorDeadline = true;
            }
            while (!errorDeadline);
            Projet.Date deadline = new Projet.Date(deadlineInput);

            Console.Write("Une description de votre livrable : ");
            string description = Console.ReadLine();

            Console.WriteLine("\nVoulez-vous ajouter un lien externe pour ce livrable ? Y/N");
            string repLien = Console.ReadLine();
            string lien = "";
            if (repLien.ToUpper() == "Y")
            {
                Console.Write("Rentrez le lien voulu : ");
                lien = Console.ReadLine();
            }
            else
            {
                lien = "Null";
            }

            return new Livrable(type, deadline, description, lien);
        }

        public void PrintInfos()
        {
            Console.WriteLine("==== Livrable ====");

            Console.WriteLine($"Type : {Type}");
            Console.WriteLine($"Deadline : ");
            Deadline.Print();
            Console.WriteLine($"Description : {Description}");
            
            if(LienExterne!=null) Console.WriteLine(LienExterne);
        }

        public void PrintInfos(Escapes escapes)
        { 
            Console.WriteLine($" |Type : {Type}");
            escapes.Print();
            Console.WriteLine($"Deadline : {Deadline.GetDateFormatee()}");
            escapes.Print();
            Console.WriteLine($"Description : {Description}");
            
            if (LienExterne != null)
            {
                escapes.Print();
                Console.WriteLine($"Lien externe : {LienExterne}");
            }
        }
    }
}

