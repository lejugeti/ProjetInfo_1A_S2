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

    }
}
