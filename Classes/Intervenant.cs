using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Program
{
    // on indique au sérialiseur quels types peut prendre un objet Intervenant
    [XmlInclude(typeof(Enseignant))]
    [XmlInclude(typeof(Externe))]       
    [XmlInclude(typeof(Eleve))]
    public class Intervenant
    {
        public string _nom;
        public string _prenom;
        public Role[] _roles;

        //Propriétés
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }
        public Role[] Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        public Role this[int indexRole]
        {
            get { return _roles[indexRole]; }
            set { _roles[indexRole] = value; }
        }

        //Constructeur
        public Intervenant(string nom, string prenom, Role[] roles)
        {
            _nom = nom;
            _prenom = prenom;
            _roles = roles;
        }

        //Constructeur vide utilisé par lors de la désérialisation
        public Intervenant()
        {

        }
        //Méthodes

        /*
         * Interface de création d'un intervenant. Cette fonction permet de créer tous les types 
         * d'intervenants possibles en demandant à l'utilisateur quel type d'intervenant il veut créer.
         */
        public static Intervenant CreateIntervenant()
        {
            Console.Write("\nLe nom de la personne : ");
            string nom = Console.ReadLine();
            Console.Write("Son Prénom : ");
            string prenom = Console.ReadLine();

            //Sélection spécification de la classe intervenant
            string inputSpec;
            bool doneSpec = false;
            do
            {
                Console.WriteLine("Cette personne est un");
                Console.WriteLine("Eleve : entrez 1,   Enseignant : entrez 2,  Externe : entrez 3,  Autre : entrez 4");

                Regex regSpec = new Regex("^[1-4]$");
                inputSpec = Console.ReadLine();

                if (regSpec.Match(inputSpec).Success)
                {
                    doneSpec = true;
                }
                else
                {
                    Console.WriteLine("Votre entrée est incorrecte");
                }
            }
            while (!doneSpec);

            //Création des rôles de l'intervenant
            Console.WriteLine("\nVous allez maintenant rentrer le ou les rôles de cette personne.");
            List<Role> roles = new List<Role>();

            bool doneRole = false;
            do
            {
                if (roles.Count == 0) Console.Write("Indiquez le rôle de cette personne dans ce projet : ");
                else Console.Write("Indiquez un autre rôle ou entrez Y pour quitter : ");

                string roleInput = Console.ReadLine();
                if (roleInput.ToUpper() == "Y") doneRole = true;
                else roles.Add(new Role(roleInput));
            }
            while (!doneRole);

            //Création de l'intervenant
            if (inputSpec == "1") //élève
            {
                return Eleve.CreateEleve(nom, prenom, roles.ToArray());
            }
            else if (inputSpec == "2") //enseignant
            {
                return Enseignant.CreateEnseignant(nom, prenom, roles.ToArray());
            }
            else if (inputSpec == "3")//externe
            {
                return Externe.CreateExterne(nom, prenom, roles.ToArray());
            }
            else
            {
                return new Intervenant(nom, prenom, roles.ToArray());
            }
        }

        /*
         * Permet de créer un intervenant en indiquant directement sa spécification (eleve etc)
         */
        public static Intervenant CreateIntervenant(string type)
        {
            Console.Write("\nLe nom de la personne : ");
            string nom = Console.ReadLine();
            Console.Write("Son Prénom : ");
            string prenom = Console.ReadLine();

            //Création des rôles de l'intervenant
            Console.WriteLine("\nVous allez maintenant rentrer le ou les rôles de cette personne.");
            List<Role> roles = new List<Role>();

            bool doneRole = false;
            do
            {
                if (roles.Count == 0) Console.Write("Indiquez le rôle de cette personne dans ce projet : ");
                else Console.Write("Indiquez un autre rôle ou entrez Y pour quitter : ");

                string roleInput = Console.ReadLine();
                if (roleInput.ToUpper() == "Y") doneRole = true;
                else roles.Add(new Role(roleInput));
            }
            while (!doneRole);

            //Création de l'intervenant
            if (type == "eleve") //élève
            {
                return Eleve.CreateEleve(nom, prenom, roles.ToArray());
            }
            else if (type == "enseignant") //enseignant
            {
                return Enseignant.CreateEnseignant(nom, prenom, roles.ToArray());
            }
            else if (type == "externe")//externe
            {
                return Externe.CreateExterne(nom, prenom, roles.ToArray());
            }
            else
            {
                return new Intervenant(nom, prenom, roles.ToArray());
            }
        }

        /*
         * Permet de créer un intervenant en indiquant directement son rôle
         */
        public static Intervenant CreateIntervenant(Role role)
        {
            Console.Write("\nLe nom de la personne : ");
            string nom = Console.ReadLine();
            Console.Write("Son Prénom : ");
            string prenom = Console.ReadLine();

            //Sélection spécification de la classe intervenant
            string inputSpec;
            bool doneSpec = false;
            do
            {
                Console.WriteLine("Cette personne est un");
                Console.WriteLine("Eleve : entrez 1,   Enseignant : entrez 2,  Externe : entrez 3,  Autre : entrez 4");

                Regex regSpec = new Regex("^[1-4]$");
                inputSpec = Console.ReadLine();

                if (regSpec.Match(inputSpec).Success)
                {
                    doneSpec = true;
                }
                else
                {
                    Console.WriteLine("Votre entrée est incorrecte");
                }
            }
            while (!doneSpec);

            //Création des rôles de l'intervenant
            Role[] roles = new Role[] { role };

            //Création de l'intervenant
            if (inputSpec == "1") //élève
            {
                return Eleve.CreateEleve(nom, prenom, roles);
            }
            else if (inputSpec == "2") //enseignant
            {
                return Enseignant.CreateEnseignant(nom, prenom, roles);
            }
            else if (inputSpec == "3")//externe
            {
                return Externe.CreateExterne(nom, prenom, roles);
            }
            else
            {
                return new Intervenant(nom, prenom, roles);
            }
        }

        /*
         * Affiche l'ensemble des informations d'un intervenant en spécifiant son statut à afficher.
         * @arg escapes, un objet Escapes permettant de gérer les espaces dans la console pour aligner les éléments à afficher.
         * @arg nomInfo, le statut de l'intervenant à afficher.
         */
        public virtual void PrintInfosCol(Escapes escapes, string nomInfo)
        {
            
            escapes.Add(nomInfo); //On ajoute l'ensemble des espaces pour aligner les infos
            Console.Write(nomInfo);

            //Permet d'afficher les infos de l'individu 
            Console.WriteLine($" |Nom : {Nom}");
            escapes.Print();
            Console.WriteLine($"Prénom : {Prenom}");

            //Rôles
            escapes.Print();
            Console.Write($"Roles : ");
            for (int i = 0; i < Roles.Length; i++)
            {
                Console.Write($"{i + 1}.");
                this[i].PrintInfos();
            }
            Console.WriteLine("");

            //Suppression des espaces car on va passer à un autre intervenant
            escapes.Spaces.RemoveAt(escapes.Spaces.Count - 1);
        }
    }

    public class Enseignant : Intervenant
    {
        public string _laboratoire;
        public Matiere _matiere;

        //Propriétés
        public string Laboratoire
        {
            get { return _laboratoire; }
            set { _laboratoire = value; }
        }

        public Matiere Matiere
        {
            get { return _matiere; }
            set { _matiere = value; }
        }

        //Constructeur
        public Enseignant(string nom, string prenom, Role[] roles, string laboratoire, Matiere matiere) : base(nom, prenom, roles)
        {
            _laboratoire = laboratoire;
            _matiere = matiere;
        }

        //Constructeur vide utilisé par lors de la désérialisation
        public Enseignant()
        {

        }

        //Méthodes

        /*
         * Interface de création d'un Enseignant. L'utilisateur doit rentrer l'ensemble des informations nécessaire à la création
         * @arg nom, le nom de l'Enseignant
         * @arg prénom, le prénnom de l'Enseignant
         * @arg roles, l'ensemble des roles de l'Enseignant
         */
        public static Enseignant CreateEnseignant(string nom, string prenom, Role[] roles)
        {
            //Laboratoire
            Console.Write("Rentrez le nom du laboratoire ou Y si il n'en possède pas : ");
            string labInput = Console.ReadLine();

            if (labInput.ToUpper() == "Y") labInput = "Null";

            //Matière
            Matiere matiere = Matiere.CreateMatiere();

            return new Enseignant(nom, prenom, roles, labInput, matiere);
        }

        
        /*
         * Affiche l'ensemble des informations de l'enseignant en spécifiant son statut à afficher.
         * @arg escapes, un objet Escapes permettant de gérer les espaces dans la console pour aligner les éléments à afficher.
         * @arg nomInfo, le statut de l'enseignant à afficher.
         */
        public override void PrintInfosCol(Escapes escapes, string nomInfo)
        {
            escapes.Add(nomInfo);

            Console.Write(nomInfo);
            //Permet d'afficher les infos de l'individu 
            Console.WriteLine($" |Nom : {Nom}");
            escapes.Print();
            Console.WriteLine($"Prénom : {Prenom}");

            //Rôles
            escapes.Print();
            Console.Write($"Roles : ");
            for (int i = 0; i < Roles.Length; i++)
            {
                Console.Write($"{i + 1}.");
                this[i].PrintInfos();
            }
            
            Console.WriteLine("");
            escapes.Print();
            Console.WriteLine($"Laboratoire : {Laboratoire}");
            escapes.Print();
            Matiere.PrintInfosCol(escapes, "Matière");

            escapes.Spaces.RemoveAt(escapes.Spaces.Count - 1);
        }
    }

    public class Externe : Intervenant
    {
        public string _organisme;

        //Propriétés
        public string Organisme
        {
            get { return _organisme; }
            set { _organisme = value; }
        }

        //Constructeur
        public Externe(string nom, string prenom, Role[] roles, string organisme) : base(nom, prenom, roles)
        {
            _organisme = organisme;
        }

        //Constructeur vide utilisé par lors de la désérialisation
        public Externe()
        {

        }

        //Méthodes 

        /*
         * Interface de création d'un Externe. L'utilisateur doit rentrer l'ensemble des informations nécessaire à la création
         * @arg nom, le nom de l'Externe
         * @arg prénom, le prénnom de l'Externe
         * @arg roles, l'ensemble des roles de l'Externe
         */
        public static Externe CreateExterne(string nom, string prenom, Role[] roles)
        {
            Console.Write("Entrez l'oganisme de l'intervenant : ");
            string organisme = Console.ReadLine();

            return new Externe(nom, prenom, roles, organisme);
        }


        /*
         * Affiche l'ensemble des informations de l'Externe en spécifiant son statut à afficher.
         * @arg escapes, un objet Escapes permettant de gérer les espaces dans la console pour aligner les éléments à afficher.
         * @arg nomInfo, le statut de l'Externe à afficher.
         */
        public override void PrintInfosCol(Escapes escapes, string nomInfo)
        {
            escapes.Add(nomInfo);

            Console.Write(nomInfo);
            //Permet d'afficher les infos de l'individu 
            Console.WriteLine($" |Nom : {Nom}");
            escapes.Print();
            Console.WriteLine($"Prénom : {Prenom}");

            //Rôles
            escapes.Print();
            Console.Write($"Roles : ");
            for (int i = 0; i < Roles.Length; i++)
            {
                Console.Write($"{i + 1}.");
                this[i].PrintInfos();
            }

            Console.WriteLine("");
            escapes.Print();
            Console.WriteLine($"Organisme : {Organisme}");

            escapes.Spaces.RemoveAt(escapes.Spaces.Count - 1);
        }
    }

    public class Eleve : Intervenant
    {
        public string _promotion;
        public string _annee;

        //Propriétés
        public string Promotion
        {
            get { return _promotion; }
            set { _promotion = value; }
        }
        public string Annee
        {
            get { return _annee; }
            set { _annee = value; }
        }



        //Constructeur
        public Eleve(string nom, string prenom, Role[] roles, string promotion, string annee) : base(nom, prenom, roles)
        {
            _promotion = promotion;
            _annee = annee;
        }

        //Constructeur vide utilisé par lors de la désérialisation
        public Eleve()
        {

        }

        //Méthodes

        /*
         * Interface de création d'un Eleve. L'utilisateur doit rentrer l'ensemble des informations nécessaire à la création
         * @arg nom, le nom de l'Eleve
         * @arg prénom, le prénnom de l'Eleve
         * @arg roles, l'ensemble des roles de l'Eleve
         */
        public static Eleve CreateEleve(string nom, string prenom, Role[] roles)
        {
            // promotion de l'élève
            bool donePromo = false;
            string promoInput;
            do
            {
                Console.Write("\nRentrez la promotion de l'élève (ex : 2020) : ");
                promoInput = Console.ReadLine();
                if (Projet.Date.IsPromotion(promoInput))
                {
                    donePromo = true;
                }
                else
                {
                    Console.WriteLine("La promotion rentrée est incorrecte");
                }
            } while (!donePromo);

            // année de l'élève (1A, 2A ou 3A)
            bool doneAnnee = false;
            string anneeInput;
            do
            {
                Console.WriteLine("\nEn quelle année était l'élève ?");
                Console.WriteLine("1A : 1,   2A : 2,   3A : 3");
                Regex regAnnee = new Regex("^[1-3]$");
                anneeInput = Console.ReadLine();

                if (regAnnee.Match(anneeInput).Success)
                {
                    doneAnnee = true;
                    switch (anneeInput)
                    {
                        case "1":
                            anneeInput = "1A";
                            break;
                        case "2":
                            anneeInput = "2A";
                            break;
                        case "3":
                            anneeInput = "3A";
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("L'année rentrée est incorrecte");
                }
            } while (!doneAnnee);


            return new Eleve(nom, prenom, roles, promoInput, anneeInput);
        }

        /*
         * Affiche l'ensemble des informations de l'Eleve en spécifiant son statut à afficher.
         * @arg escapes, un objet Escapes permettant de gérer les espaces dans la console pour aligner les éléments à afficher.
         * @arg nomInfo, le statut de l'Eleve à afficher.
         */
        public override void PrintInfosCol(Escapes escapes, string nomInfo)
        {
            escapes.Add(nomInfo);

            Console.Write(nomInfo);
            //Permet d'afficher les infos de l'individu 
            Console.WriteLine($" |Nom : {Nom}");
            escapes.Print();
            Console.WriteLine($"Prénom : {Prenom}");

            //Rôles
            escapes.Print();
            Console.Write($"Roles : ");
            for (int i = 0; i < Roles.Length; i++)
            {
                Console.Write($"{i + 1}.");
                this[i].PrintInfos();
            }

            Console.WriteLine("");
            escapes.Print();
            Console.WriteLine($"Promotion : {Promotion}");
            escapes.Print();
            Console.WriteLine($"Année : {Annee}");

            escapes.Spaces.RemoveAt(escapes.Spaces.Count - 1);
        }
    }

}
