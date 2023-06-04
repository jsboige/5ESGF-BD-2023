using Microsoft.Spark.Sql;
using System;

namespace Sudoku.Backtracking
{
    class Program
    {
        // User Defined Function (UDF) pour résoudre un Sudoku
        static UDF<string, string> ResolveSudokuUDF = new UDF<string, string>(sudoku =>
        {
            // Convertir la chaîne de caractères du Sudoku en une matrice 9x9
            int[,] grid = ConvertSudokuStringToGrid(sudoku);

            // Résoudre le Sudoku à l'aide de l'algorithme de backtracking
            bool isSolved = SolveSudoku(grid);

            // Convertir la matrice résolue en une chaîne de caractères représentant le Sudoku résolu
            string solvedSudoku = ConvertGridToSudokuString(grid);

            // Retourner le Sudoku résolu
            return solvedSudoku;
        });

        static void Main(string[] args)
        {
            // Initialiser SparkSession
            var spark = SparkSession.Builder()
                .AppName("SudokuSolver")
                .GetOrCreate();

            // Charger le fichier de Sudokus
            DataFrame sudokuData = spark.Read().Csv("C:/Doc/Mon Projet/sudokus.csv");

            // Résoudre les Sudokus
            DataFrame solvedSudokus = sudokuData.SelectExpr("sudoku", "ResolveSudokuUDF(sudoku) as solved_sudoku");

            // Afficher les Sudokus résolus
            solvedSudokus.Show();

            // Arrêter la session Spark
            spark.Stop();
        }

        // Résoudre le Sudoku à l'aide de l'algorithme de backtracking
        static bool SolveSudoku(int[,] grid)
        {
            int row = 0;
            int col = 0;
            bool isComplete = FindUnassignedLocation(grid, ref row, ref col);

            // Si toutes les cellules sont remplies, le Sudoku est résolu
            if (!isComplete)
                return true;

            // Essayer différentes valeurs dans la cellule non attribuée
            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(grid, row, col, num))
                {
                    // Assigner la valeur num dans la cellule
                    grid[row, col] = num;

                    // Récursivement résoudre le reste du Sudoku
                    if (SolveSudoku(grid))
                        return true;

                    // Si l'assignation de num ne mène pas à une solution, revenir en arrière et essayer une autre valeur
                    grid[row, col] = 0;
                }
            }

            // Aucune valeur n'a conduit à une solution valide
            return false;
        }

        // Vérifier si une valeur peut être assignée à une cellule en respectant les règles du Sudoku
        static bool IsSafe(int[,] grid, int row, int col, int num)
        {
            // Vérifier si le nombre num existe déjà dans la même rangée
            for (int i = 0; i < 9; i++)
            {
                if (grid[row, i] == num)
                    return false;
            }

            // Vérifier si le nombre num existe déjà dans la même colonne
            for (int j = 0; j < 9; j++)
            {
                if (grid[j, col] == num)
                    return false;
            }

            // Vérifier si le nombre num existe déjà dans le même bloc 3x3
            int startRow = row - row % 3;
            int startCol = col - col % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (grid[i + startRow, j + startCol] == num)
                        return false;
                }
            }

            // La valeur num peut être assignée en toute sécurité
            return true;
        }

        // Trouver une cellule non attribuée dans le Sudoku
        static bool FindUnassignedLocation(int[,] grid, ref int row, ref int col)
        {
            for (row = 0; row < 9; row++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (grid[row, col] == 0)
                        return true;
                }
            }

            // Toutes les cellules sont attribuées
            return false;
        }

        // Convertir la chaîne de caractères du Sudoku en une matrice 9x9
        static int[,] ConvertSudokuStringToGrid(string sudoku)
        {
            int[,] grid = new int[9, 9];

            // Parcourir chaque ligne du Sudoku
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    // Convertir le caractère en entier et attribuer la valeur à la cellule correspondante
                    grid[row, col] = int.Parse(sudoku[row * 9 + col].ToString());
                }
            }

            return grid;
        }

        // Convertir la matrice résolue en une chaîne de caractères représentant le Sudoku résolu
        static string ConvertGridToSudokuString(int[,] grid)
        {
            string sudoku = "";

            // Parcourir chaque cellule du Sudoku
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    // Ajouter la valeur de la cellule à la chaîne de caractères
                    sudoku += grid[row, col].ToString();
                }
            }

            return sudoku;
        }
    }
}
