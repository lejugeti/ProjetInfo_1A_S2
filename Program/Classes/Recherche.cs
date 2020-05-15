using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    static class Recherche
    {
        //Méthodes
        
        /**
         * Recherche par élève
         * @param nomEleve Le nom de famille de l'élève concerné par la recherche
         * @return La liste des projets associés à cet élève
         */
        public static List<Projet> RechercheParEleve(Catalogue catalogue, String nomEleve)
        {
            Projet[] projets = catalogue.Projets;
            List<Projet> projetsEleve = new List<Projet>();
            
            // Pour chaque projet
            foreach (var projet in projets)
            {
                // Pour chaque élève associé à ce projet
                foreach (var eleve in projet.Eleves)
                {
                    // Si le nom de l'élève correspond au nom entré
                    if (eleve.Nom.ToLower().Equals(nomEleve.ToLower()))
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
        public static List<Projet> RechercheParAnnee(Catalogue catalogue, String annee)
        {
            Projet[] projets = catalogue.Projets;
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
        public static List<Projet> RechercheParPromotion(Catalogue catalogue, String promotion)
        {
            Projet[] projets = catalogue.Projets;
            List<Projet> projetsPromotion = new List<Projet>();

            // Pour chaque projet
            foreach (var projet in projets) 
            {
                // Pour chaque promo associés à ce projet
                foreach (var promo in projet.Promotions)
                {
                    // Si la promotion de l'élève correspond à la promotion entrée
                    if (promo.ToLower().Equals(promotion.ToLower()))
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
        public static List<Projet> RechercheParMotsClefs(Catalogue catalogue, String motClef)
        {
            Projet[] projets = catalogue.Projets;
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
        public static List<Projet> RechercheParIntitule(Catalogue catalogue, String intitule)
        {
            Projet[] projets = catalogue.Projets;
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
        public static List<Projet> RechercheGenerale(Catalogue catalogue, String recherche)
        {
            List<Projet> projetsEleve = new List<Projet>();
            List<Projet> projetsAnnee = new List<Projet>();
            List<Projet> projetsPromotion = new List<Projet>();
            List<Projet> projetsMotsclef = new List<Projet>();
            List<Projet> projetsIntitule = new List<Projet>();

            List<Projet> projets = new List<Projet>();

            // On recherche par tous les critères possibles avec la chaîne entrée
            projetsEleve = RechercheParEleve(catalogue, recherche);
            projetsAnnee = RechercheParAnnee(catalogue, recherche);
            projetsPromotion = RechercheParPromotion(catalogue, recherche);
            projetsMotsclef = RechercheParMotsClefs(catalogue, recherche);
            projetsIntitule = RechercheParIntitule(catalogue, recherche);

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

        static void SortByIntitule(List<Projet> projets)
        {
            projets.OrderBy(projet => projet.Intitule);
        }

        static void SortByDate(List<Projet> projets)
        {
            projets.OrderBy(projet => projet.DateFin);
        }
        
    }
}
