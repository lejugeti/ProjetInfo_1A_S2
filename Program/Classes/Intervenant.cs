using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class Intervenant
    {
        protected string _nom;
        protected string _prenom;
        protected Role[] _roles;

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

        //Constructeur
        public Intervenant(string nom, string prenom, Role[] roles)
        {
            _nom = nom;
            _prenom = prenom;
            _roles = roles;
        }
    }

    class Enseignant : Intervenant
    {
        protected string[] _laboratoires;
        protected Matiere[] _matieres;

        //Propriétés
        public string[] Laboratoires
        {
            get { return _laboratoires; }
            set { _laboratoires = value; }
        }

        public Matiere[] Matieres
        {
            get { return _matieres; }
            set { _matieres = value; }
        }

        //Constructeur
        public Enseignant(string nom, string prenom, Role[] roles, string[] laboratoires, Matiere[] matieres) : base(nom, prenom, roles)
        {
            _laboratoires = laboratoires;
            _matieres = matieres;
        }
    }

    class Externe : Intervenant
    {
        protected string[] _organismes;

        //Propriétés
        public string[] Organismes
        {
            get { return _organismes; }
            set { _organismes = value; }
        }

        //Constructeur
        public Externe(string nom, string prenom, Role[] roles, string[] organismes) : base(nom,  prenom, roles)
        {
            _organismes = organismes;
        }
    }

    class Eleve : Intervenant
    {
        protected string _promotion;
        protected string _annee;

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
    }
}
