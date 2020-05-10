using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Program.Classes
{

    static class Recherche
    {
        //Méthodes
        
        /**
         * Recherche par élève
         * @param nomEleve Le nom de famille de l'élève concerné par la recherche
         * @return La liste des projets associés à cet élève
         */
        public static List<Projet> RechercheParEleve(String nomEleve)
        {
            List<Eleve> eleves = Eleve.ListeEleves;
            List<Projet> projetsEleve = new List<Projet>();
            
            // Pour chaque élève
            foreach (var eleve in eleves)
            {
                // Si le nom de l'élève correspond à nomEleve
                if (eleve.Nom.ToLower().Equals(nomEleve.ToLower()))
                {
                    // Pour chaque projet de cet élève
                    foreach (var projet in eleve.ListeProjets)
                    {
                        projetsEleve.Add(projet);
                    }      
                }
            }

            return projetsEleve;
        }

        /**
         * Recherche par année
         * @param L'année concernée par la recherche
         * @return La liste des projets de cette année
         */
        public static List<Projet> RechercheParAnnee(String annee)
        {
            List<Projet> projets = Projet.AllProjects;
            List<Projet> projetsAnnee = new List<Projet>();

            // Pour chaque projet
            foreach (var projet in projets)
            {
                // Si la date de fin du projet correspond à l'année entrée
                if (projet.DateFin.GetAnnee() == annee)
                {
                    projetsAnnee.Add(projet);
                }
            }

            return projetsAnnee;
        }


        /**
         * Recherche par promotion
         * @param promotion La promotion concernée par la recherche
         * @return La liste des projets pour cette promotion
         */
        public static List<Projet> RechercheParPromotion(String promotion)
        {
            List<Eleve> eleves = Eleve.ListeEleves;
            List<Projet> projetsPromotion = new List<Projet>();

            // Pour chaque élève
            foreach (var eleve in eleves) 
            {
                // Si la promotion de l'élève correspond à la promotion entrée
                if (eleve.Promotion.Equals(promotion))
                {
                    // Pour chaque projet de cet élève
                    foreach (var projet in eleve.ListeProjets)
                    {
                        projetsPromotion.Add(projet);
                    }
                }
            }

            return projetsPromotion;
        }

        /**
         * Recherche par mot clé
         * @param Le mot clé concerné par la recherche
         * @return La liste des projets correspondant à ce mot clé
         */
        public static List<Projet> RechercheParMotClefs(String motClef)
        {
            List<Projet> projets = Projet.AllProjects;
            List<Projet> projetsMotClefs = new List<Projet>();

            // Pour chaque projet
            foreach (var projet in projets)
            {
                // Pour chaque mot clé du projet en cours
                foreach (var motcle in projet.MotsCles)
                {
                    // Si le mot clé en cours correspond au mot clé entré
                    if (motcle.ToLower().Equals(motClef.ToLower()))
                    {
                        projetsMotClefs.Add(projet);
                    }
                }
            }

            return projetsMotClefs;
        }

        /**
         * Recherche par intitulé
         * @param intitule L'intitulé cncerné par la recherche
         * @return La liste des projets ayant cet intitulé
         */
        public static List<Projet> RechercheParIntitule(String intitule)
        {
            List<Projet> projets = Projet.AllProjects;
            List<Projet> projetsIntitule = new List<Projet>();

            // Pour tous les projets
            foreach (var projet in projets)
            {
                // Si l'intitulé du projet en cours correspond à l'intitulé entré
                if (projet.Intitule.ToLower().Equals(intitule.ToLower()))
                {
                    projetsIntitule.Add(projet);
                }
            }

            return projetsIntitule;
        }

        /**
         * Recherche générale, la fonction cherche le String entré parmi :
         *     - les noms des élèves
         *     - les années
         *     - les promotions
         *     - les mots clé des projets
         *     - les intitulés des projets
         * @param recherche La chaîne de caractère à rechercher
         * @return La liste de tous les projets qui correspondent à cette chaîne
         */
        static List<Projet> RechercheGenerale(String recherche)
        {
            List<Projet> projetsEleve = new List<Projet>();
            List<Projet> projetsAnnee = new List<Projet>();
            List<Projet> projetsPromotion = new List<Projet>();
            List<Projet> projetsMotsclef = new List<Projet>();
            List<Projet> projetsIntitule = new List<Projet>();
            
            List<Projet> projets = new List<Projet>();

            // On recherche par tous les critères possibles avec la chaîne entrée
            projetsEleve = RechercheParEleve(recherche);
            projetsAnnee = RechercheParAnnee(recherche);
            projetsPromotion = RechercheParPromotion(recherche);
            projetsMotsclef = RechercheParPromotion(recherche);
            projetsIntitule = RechercheParIntitule(recherche);

            // On ajoute à la liste de projet tous les projets récupérés par la recherche par élève
            foreach (var projet in projetsEleve)
            {
                projets.Add(projet);
            }

            // On ajoute à la liste de projet tous les projets récupérés par la recherche par année
            foreach (var projet in projetsAnnee)
            {
                projets.Add(projet);
            }

            // On ajoute à la liste de projet tous les projets récupérés par la recherche par promotion
            foreach (var projet in projetsPromotion)
            {
                projets.Add(projet);
            }

            // On ajoute à la liste de projet tous les projets récupérés par la recherche par mot clé
            foreach (var projet in projetsMotsclef)
            {
                projets.Add(projet);
            }

            // On ajoute à la liste de projet tous les projets récupérés par la recherche par intitulé
            foreach (var projet in projetsIntitule)
            {
                projets.Add(projet);
            }

            return projets;

        }
    }
}
