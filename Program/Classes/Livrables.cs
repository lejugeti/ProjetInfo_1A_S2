using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class Livrable
    {
        protected string _type;
        protected string _deadline;
        protected string _description;
        protected Intervenant _client;
        protected string _lienExterne;

        //Propriétés
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public string Deadline
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
        public Livrable(string type, string deadline, string description, Intervenant client, string lien)
        {
            _type = type; 
            _deadline = deadline;
            _description = description;
            _client = client;
            _lienExterne = lien;
        }
        public Livrable(string type, string deadline, string description, Intervenant client)
        {
            _type = type;
            _deadline = deadline;
            _description = description;
            _client = client;
        }
        public Livrable(string type, string deadline, string description)
        {
            _type = type;
            _deadline = deadline;
            _description = description;
        }
    }
}
