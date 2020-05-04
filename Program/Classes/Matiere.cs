using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class Matiere
    {
        protected string _nom;
        protected string _code;
        protected string[] _modeEvaluation;

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
        public string[] ModeEvaluation
        {
            get { return _modeEvaluation; }
            set { _modeEvaluation = value; }
        }

        //Constructeur
        public Matiere(string nom, string code, string[] modeEval)
        {
            Code = code;
            ModeEvaluation = modeEval;
        }
    }
}
