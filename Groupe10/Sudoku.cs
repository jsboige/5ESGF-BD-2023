using System;

class Program
{
    static void Main(string[] args)
    {
        int[][] grille = new int[9][];

        for (int i = 0; i < 9; i++)
        {
            grille[i] = new int[9];
            Console.Write("Ligne " + (i + 1) + " du Sudoku : ");
            string entreeLigne = Console.ReadLine();
            string[] valeursLigne = entreeLigne.ToCharArray()
                                         .Where(c => char.IsDigit(c))
                                         .Select(c => c.ToString())
                                         .ToArray();

            if (valeursLigne.Length != 9)
            {
                Console.WriteLine("Ligne invalide. Veuillez entrer 9 valeurs.");
                i--;
                continue;
            }

            for (int j = 0; j < 9; j++)
            {
                if (!int.TryParse(valeursLigne[j], out int valeur))
                {
                    Console.WriteLine("Valeur invalide. Veuillez entrer un entier.");
                    j--;
                    break;
                }

                grille[i][j] = valeur;
            }
        }

        if (ResoudreSudoku(grille))
        {
            Console.WriteLine("Le Sudoku a été résolu :");
            AfficherGrille(grille);
        }
        else
        {
            Console.WriteLine("Aucune solution n'a été trouvée pour ce Sudoku.");
        }
    }

    static bool ResoudreSudoku(int[][] grille)
    {
        int[] celluleVide = TrouverCelluleVide(grille);
        if (celluleVide == null)
        {
            return true;
        }

        int ligne = celluleVide[0];
        int colonne = celluleVide[1];

        for (int num = 1; num <= 9; num++)
        {
            if (EstMouvementValide(grille, celluleVide, num))
            {
                grille[ligne][colonne] = num;

                if (ResoudreSudoku(grille))
                {
                    return true;
                }

                grille[ligne][colonne] = 0;
            }
        }

        return false;
    }

    static int[] TrouverCelluleVide(int[][] grille)
    {
        for (int ligne = 0; ligne < 9; ligne++)
        {
            for (int colonne = 0; colonne < 9; colonne++)
            {
                if (grille[ligne][colonne] == 0)
                {
                    return new int[] { ligne, colonne };
                }
            }
        }

        return null;
    }

    static bool EstMouvementValide(int[][] grille, int[] cellule, int num)
    {
        int ligne = cellule[0];
        int colonne = cellule[1];

        for (int i = 0; i < 9; i++)
        {
            if (grille[i][colonne] == num || grille[ligne][i] == num)
            {
                return false;
            }
        }

        int debutLigne = (ligne / 3) * 3;
        int debutColonne = (colonne / 3) * 3;

        for (int i = debutLigne; i < debutLigne + 3; i++)
        {
            for (int j = debutColonne; j < debutColonne + 3; j++)
            {
                if (grille[i][j] == num)
                {
                    return false;
                }
            }
        }

        return true;
    }

    static void AfficherGrille(int[][] grille)
    {
        Console.WriteLine("-------------------------------");

        for (int ligne = 0; ligne < 9; ligne++)
        {
            Console.Write("| ");

            for (int colonne = 0; colonne < 9; colonne++)
            {
                int valeur = grille[ligne][colonne];
                Console.Write(valeur != 0 ? $"{valeur} " : "  ");

                if ((colonne + 1) % 3 == 0)
                {
                    Console.Write("| ");
                }
            }

            Console.WriteLine();

            if ((ligne + 1) % 3 == 0)
            {
                Console.WriteLine("-------------------------------");
            }
        }
    }
}
