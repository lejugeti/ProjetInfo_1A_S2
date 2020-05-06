using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class Livrable
    {
        protected string _type;
        protected Projet.Date _deadline;
        protected string _description;
        protected Intervenant _client;
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
        public Intervenant Client
        {
            get { return _client; }
            set { _client = value; }
        }
        public string LienExterne
        {
            get { return _lienExterne; }
            set { _lienExterne = value; }
        }

        //Constructeurs
        public Livrable(string type, Projet.Date deadline, string description, Intervenant client, string lien)
        {
            _type = type; 
            _deadline = deadline;
            _description = description;
            _client = client;
            _lienExterne = lien;
        }
        public Livrable(string type, Projet.Date deadline, string description, Intervenant client)
        {
            _type = type;
            _deadline = deadline;
            _description = description;
            _client = client;
        }
        public Livrable(string type, Projet.Date deadline, string description)
        {
            _type = type;
            _deadline = deadline;
            _description = description;
        }

        //Méthodes
        public static Livrable CreateLivrable()
        {
            
            Console.WriteLine("Veuillez rentrer les informations relatives aux livrables");
            Console.Write("Le type de votre livrable : ");
            string type = Console.ReadLine();

            string deadlineInput = "";
            bool errorDeadline = false;
            do
            {
                if(errorDeadline) Console.Write("La date est incorrecte, utilisez le format dd/mm/yyyy : ");
                else Console.Write("Sa deadline (dd/mm/yyyy): ");

                deadlineInput = Console.ReadLine();
                bool isDeadline = Projet.Date.IsDate(deadlineInput);

                if (!isDeadline) errorDeadline = true;
            }
            while (!errorDeadline);
            Projet.Date deadline = new Projet.Date(deadlineInput);

            Console.Write("Une description de votre livrable : ");
            string description = Console.ReadLine();

            Console.WriteLine("\n Vous allez maintenant devoir rentrer les informations relatives au client du projet.");
            Intervenant client = Intervenant.CreateIntervenant();

            Console.WriteLine("Voulez-vous ajouter un lien externe pour ce livrable ? Y/N");
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

            return new Livrable(type, deadline, description, client, lien);
        }

        public void PrintInfos()
        {
            Console.WriteLine("==== Livrable ====");

            Console.WriteLine($"Type : {Type}");
            Console.WriteLine($"Deadline : ");
            Deadline.Print();
            Console.WriteLine($"Description : {Description}");

            if (Client != null) 
            { 
                if(Client is Enseignant)
                {
                    Enseignant tmpClient = Client as Enseignant;
                    tmpClient.PrintInfos();
                }
                else if(Client is Externe)
                {
                    Externe tmpClient = Client as Externe;
                    tmpClient.PrintInfos();
                }
                else if(Client is Eleve)
                {
                    Eleve tmpClient = Client as Eleve;
                    tmpClient.PrintInfos();
                }
                else
                {
                    Client.PrintInfos("Client");
                }
            }
            
            if(LienExterne!=null) Console.WriteLine(LienExterne);
        }

        public void PrintInfos(Projet.Escapes escapes)
        { 
            Console.WriteLine($" |Type : {Type}");
            escapes.Print();
            Console.WriteLine($"Deadline : {Deadline.GetDateFormatee()}");
            escapes.Print();
            Console.WriteLine($"Description : {Description}");
            escapes.Print();

            if (Client != null)
            {
                //escapes.Add(new String(' ', 7));
                if (Client is Enseignant)
                {
                    Enseignant tmpClient = Client as Enseignant;
                    tmpClient.PrintInfosCol(escapes, "Client");
                    
                }
                else if (Client is Externe)
                {
                    Externe tmpClient = Client as Externe;
                    tmpClient.PrintInfosCol(escapes, "Client");
                }
                else if (Client is Eleve)
                {
                    Eleve tmpClient = Client as Eleve;
                    tmpClient.PrintInfosCol(escapes, "Client");
                }
                else
                {
                    Client.PrintInfos("Client");
                }
            }


            if (LienExterne != null)
            {
                escapes.Print();
                Console.WriteLine($"|Lien externe : {LienExterne}");
            }
            
        }
    }
}

