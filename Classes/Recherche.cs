using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    static class Recherche
    {
        //Méthodes
        
        /*
         * Recherche par élève
         * @param nomEleve Le nom de famille de l'élève concerné par la recherche
         * @return La liste des projets associés à cet élève
         */
        public static List<Projet> RechercheParEleve(String nomEleve)
        {
            Projet[] projets = Catalogue.Projets;
            List<Projet> projetsEleve = new List<Projet>();
            
            // Pour chaque projet
            foreach (var projet in projets)
            {
                // Pour chaque élève associé à ce projet
                foreach (var eleve in projet.Eleves)
                {
                    // Si le nom de l'élève correspond au nom entré
                    if (eleve.Nom.ToLower().Equals(nomEleve.ToLower()) && !projetsEleve.Contains(projet))
                    {
                        projetsEleve.Add(projet);
                    }
                }
            }

            return projetsEleve;
        }

        /*
         * Recherche par année
         * @param L'année concernée par la recherche
         * @return La liste des projets de cette année
         */
        public static List<Projet> RechercheParAnnee(String annee)
        {
            Projet[] projets = Catalogue.Projets;
            List<Projet> projetsAnnee = new List<Projet>();

            // Pour chaque projet
            foreach (var projet in projets)
            {
                // Si la date de fin du projet correspond à l'année entrée
                if (projet.DateFin.GetAnnee() == annee && !projetsAnnee.Contains(projet))
                {
                    projetsAnnee.Add(projet);
                }
            }

            return projetsAnnee;
        }

        /*
         * Recherche par promotion
         * @param promotion La promotion concernée par la recherche
         * @return La liste des projets pour cette promotion
         */
        public static List<Projet> RechercheParPromotion(String promotion)
        {
            Projet[] projets = Catalogue.Projets;
            List<Projet> projetsPromotion = new List<Projet>();

            // Pour chaque projet
            foreach (var projet in projets) 
            {
                // Pour chaque promo associés à ce projet
                foreach (var promo in projet.Promotions)
                {
                    // Si la promotion de l'élève correspond à la promotion entrée
                    if (promo.ToLower().Equals(promotion.ToLower()) && !projetsPromotion.Contains(projet))
                    {
                        projetsPromotion.Add(projet);
                    }
                }
            }

            return projetsPromotion;
        }

        /*
         * Recherche par mot clé
         * @param Le mot clé concerné par la recherche
         * @return La liste des projets correspondant à ce mot clé
         */
        public static List<Projet> RechercheParMotsClefs(String motClef)
        {
            Projet[] projets = Catalogue.Projets;
            List<Projet> projetsMotsClefs = new List<Projet>();

            // Pour chaque projet
            foreach (var projet in projets)
            {
                // Pour chaque mot clé du projet en cours
                foreach (var motcle in projet.MotsCles)
                {
                    // Si le mot clé en cours correspond au mot clé entré
                    if (motcle.ToLower().Equals(motClef.ToLower()) && !projetsMotsClefs.Contains(projet))
                    {
                        projetsMotsClefs.Add(projet);
                    }
                }
            }

            return projetsMotsClefs;
        }

        /*
         * Recherche par intitulé
         * @param intitule L'intitulé cncerné par la recherche
         * @return La liste des projets ayant cet intitulé
         */
        public static List<Projet> RechercheParIntitule(String intitule)
        {
            Projet[] projets = Catalogue.Projets;
            List<Projet> projetsIntitule = new List<Projet>();

            // Création d'un regex pour matcher les projets grâce à leur intitulé
            Regex regexIntitule = new Regex(intitule);

            // Pour tous les projets
            foreach (var projet in projets)
            {
                // Si l'intitulé du projet en cours contient la recherche rentrée
                if (regexIntitule.Match(projet.Intitule).Success && !projetsIntitule.Contains(projet))
                {
                    projetsIntitule.Add(projet);
                }
            }

            return projetsIntitule;
        }

        /*
         * Recherche générale, la fonction cherche le String entré parmi :
         *     - les noms des élèves
         *     - les années
         *     - les promotions
         *     - les mots clé des projets
         *     - les intitulés des projets
         * @param recherche La chaîne de caractère à rechercher
         * @return La liste de tous les projets qui correspondent à cette chaîne
         */
        public static List<Projet> RechercheGenerale(String recherche)
        {
            List<Projet> projetsEleve;
            List<Projet> projetsAnnee;
            List<Projet> projetsPromotion;
            List<Projet> projetsMotsclef;
            List<Projet> projetsIntitule;

            List<Projet> projets = new List<Projet>();

            // On recherche par tous les critères possibles avec la chaîne entrée
            projetsEleve = RechercheParEleve(recherche);
            projetsAnnee = RechercheParAnnee(recherche);
            projetsPromotion = RechercheParPromotion(recherche);
            projetsMotsclef = RechercheParMotsClefs(recherche);
            projetsIntitule = RechercheParIntitule(recherche);

            // On ajoute à la liste de projet tous les projets récupérés par la recherche par élève
            foreach (var projet in projetsEleve)
            {
                if(!projets.Contains(projet)) projets.Add(projet);
            }

            // On ajoute à la liste de projet tous les projets récupérés par la recherche par année
            foreach (var projet in projetsAnnee)
            {
                if (!projets.Contains(projet)) projets.Add(projet);
            }

            // On ajoute à la liste de projet tous les projets récupérés par la recherche par promotion
            foreach (var projet in projetsPromotion)
            {
                if (!projets.Contains(projet)) projets.Add(projet);
            }

            // On ajoute à la liste de projet tous les projets récupérés par la recherche par mot clé
            foreach (var projet in projetsMotsclef)
            {
                if (!projets.Contains(projet)) projets.Add(projet);
            }

            // On ajoute à la liste de projet tous les projets récupérés par la recherche par intitulé
            foreach (var projet in projetsIntitule)
            {
                if (!projets.Contains(projet)) projets.Add(projet);
            }

            return projets;
        }      
    }
}
