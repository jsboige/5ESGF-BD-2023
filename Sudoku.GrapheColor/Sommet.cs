using System.Collections.Generic;

namespace Sudoku.GrapheColor
{
    /// <summary>
    /// La classe Sommet repr�sente un sommet du graphe
    /// Les cases adjacences sont d�duites des positions ligne,colonne des Sommets
    /// </summary>
    public class Sommet
    {
        // Voisinage : liste d'adjacence
        List<Sommet> m_adjacents;

        // Donn�es sp�cifiques du sommet
        int m_id;               // Indice de la case dans la grille (de 0 � 80)
        int m_couleur;          // Couleur affect�e (ou 0 si pas encore de couleur)
        int m_ligne, m_colonne; // Position dans la grille
        int m_bloc;             // Num�ro du bloc d'appartenance (carr� 3x3)

        // Constructeur
        public Sommet(int id, int couleur)
        {
            m_id = id;
            m_couleur = couleur;
            m_ligne = (int)(m_id / 9);
            m_colonne = m_id % 9;
            if (m_ligne < 3)
                m_bloc = (int)(m_colonne / 3);
            else if (m_ligne < 6)
                m_bloc = 3 + (int)(m_colonne / 3);
            else if (m_ligne < 9)
                m_bloc = 6 + (int)(m_colonne / 3);
            m_adjacents = new List<Sommet>();
        }

        // Retourne la couleur (num�ro de couleur) actuellement affect� au sommet
        // Par convention la valeur 0 indique "pas encore de couleur affect�e"
        public int getCouleur()
        {
            return m_couleur;
        }

        // Affecte une couleur au sommet
        public void setCouleur(int couleur)
        {
            m_couleur = couleur;
        }

        // Teste l'affectation d'une couleur
        // Retourne vrai si la couleur n'est pas en conflit avec un sommet adjacent
        // faux sinon
        public bool testCouleur(int couleur)
        {
            foreach (Sommet s in m_adjacents)
                if (s.m_couleur == couleur)
                    return false;
            return true;
        }

        // M�thode de d�termination des sommets adjacents
        public void determineAdjacents(List<Sommet> sommets)
        {
            m_adjacents = new List<Sommet>();
            foreach (Sommet s in sommets)
            {
                if (s != this)
                {
                    if (s.m_ligne == m_ligne || s.m_colonne == m_colonne || s.m_bloc == m_bloc)
                        m_adjacents.Add(s);
                }
            }
        }
    }
}