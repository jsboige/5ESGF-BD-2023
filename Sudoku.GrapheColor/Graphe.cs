using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Shared;

namespace Sudoku.GrapheColor
{
    /// <summary>
    /// La classe Graphe repr�sente un graphe dans son ensemble
    /// </summary>
    public class Graphe
    {
        /// Le r�seau est constitu� d'une collection de sommets
        List<Sommet> m_sommets;
        SudokuGrid m_grid;
        public const int m_ordre = 81;

        /// La construction du r�seau se fait � partir d'une grille Sudoku
        public Graphe(SudokuGrid grid)
        {
            m_grid = grid.CloneSudoku();
            m_sommets = new List<Sommet>();
            initGraphe();
        }

        // Ajout de l'ensemble des sommets dans le graphe
        void initGraphe()
        {
            m_sommets = new List<Sommet>();
            int ligne, colonne;
            for (int i = 0; i < m_ordre; i++)
            {
                ligne = (int)(i / 9);
                colonne = i % 9;
                m_sommets.Add(new Sommet(i, m_grid.Cells[ligne][colonne]));
            }
            foreach (Sommet s in m_sommets)
            {
                s.determineAdjacents(m_sommets);
            }
        }

        // Retourne l'objet GridSudoku contenant la grille Sudoku � valider du graphe
        public SudokuGrid getGrid()
        {
            return m_grid;
        }

        public void displayGrid()
        {
            Console.WriteLine("----------------------------------");
            int i = 0;
            foreach (Sommet s in m_sommets)
            {
                if (i % 3 == 0)
                    Console.Write("| ");
                Console.Write("{0,2:#0} ", s.getCouleur());
                i++;
                if (i % 9 == 0)
                    Console.WriteLine("|");
                if (i % 27 == 0)
                    Console.WriteLine("----------------------------------");
            }
            Console.WriteLine();
        }

        public void algoNaifOptimise()
        {
            // Si la premi�re case contient d�j� une couleur,
            // on lance l'algorithme sur cette couleur
            if (m_sommets.First().getCouleur() != 0)
                attribuerCouleurGraphe(0, 0, m_sommets.First().getCouleur());
            else
            {
                // On lance l'algorithme en testant l'ensemble des couleurs de 1 � 9
                // sur la premi�re case
                for (int colour = 1; colour <= 9; colour++)
                {
                    if (attribuerCouleurGraphe(0, 0, colour))
                    {
                        break;
                    }
                }
            }
            // On a trouv� la solution. On met � jour la grille
            for (int i = 0; i < m_sommets.Count; i++)
                m_grid.Cells[(int)(i / 9)][i % 9] = m_sommets.ElementAt(i).getCouleur();
        }

        // Cette fonction renvoie :
        //      - true si le graphe a pu �tre enti�rement compl�t�
        //      - false sinon
        bool attribuerCouleurGraphe(int nbOk, int nbCouleurs, int couleur)
        {
            // On parcourt la liste des sommets
            Sommet s = m_sommets.ElementAt(nbOk);

            int couleurBackup = 0;
            // On v�rifie si la case a d�j� une couleur issue de la grille initiale
            // Si oui, on sauvegarde sa couleur
            if (s.getCouleur() != 0)
                couleurBackup = s.getCouleur();
            else
            {
                // On teste la couleur pass�e en param�tre aupr�s des adjacents
                if (s.testCouleur(couleur))
                {
                    // La couleur n'est pas d�j� utilis�e par un adjacent
                    // On sauvegarde l'absence de couleur
                    couleurBackup = 0;
                    // Puis on affecte la couleur � la case
                    s.setCouleur(couleur);
                }
            }
            // On v�rifie si la case a d�sormais une couleur
            if (s.getCouleur() != 0)
            {
                // On met � jour le nombre de couleurs
                nbCouleurs++;
                nbOk++;
                // Si nbOk == m_ordre, on a rempli la grille Sudoko
                if (nbOk == m_ordre)
                    return true;
                // Si le nombre de couleurs est � 9, on a une ligne compl�te
                // On r�initialise le nombre de couleurs � 0
                nbCouleurs = nbCouleurs % 9;

                // Si la case suivante contient d�j� une couleur,
                // on lance l'algorithme sur cette couleur
                if (m_sommets.ElementAt(nbOk).getCouleur() != 0)
                {
                    if (attribuerCouleurGraphe(nbOk, nbCouleurs, m_sommets.ElementAt(nbOk).getCouleur()))
                        return true;
                }
                else
                {
                    // On lance l'algorithme en testant l'ensemble des couleurs de 1 � 9
                    // sur la case suivante
                    for (int colour = 1; colour <= 9; colour++)
                    {
                        if (attribuerCouleurGraphe(nbOk, nbCouleurs, colour))
                            return true;
                    }
                }

                // Si on arrive ici, c'est que la r�solution a �chou�
                // On restaure la couleur initiale de la case (ou l'absence de couleur)
                s.setCouleur(couleurBackup);
            }
            return (nbOk == m_ordre);
        }
    }
}