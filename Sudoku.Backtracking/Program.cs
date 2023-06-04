
using System;
using System.IO;
namespace Sudoku.Backtracking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Vérifier si le chemin vers le fichier Sudoku est fourni en argument
            if (args.Length == 0)
            {
                Console.WriteLine("Veuillez fournir le chemin vers le fichier Sudoku en argument.");
                return;
            }
            string filePath = args[0];
            // Charger la grille Sudoku à partir du fichier
            int[,] sudokuGrid = LoadSudokuGridFromFile(filePath);
            // Afficher la grille Sudoku d'origine
            Console.WriteLine("Grille Sudoku d'origine :");
            PrintSudokuGrid(sudokuGrid);
            // Résoudre le Sudoku en utilisant l'algorithme de backtracking
            if (SolveSudoku(sudokuGrid))
            {
                // Afficher la grille Sudoku résolue
                Console.WriteLine("Grille Sudoku résolue :");
                PrintSudokuGrid(sudokuGrid);
            }
            else
            {
                Console.WriteLine("Impossible de résoudre le Sudoku.");
            }
        }
        private static int[,] LoadSudokuGridFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int[,] sudokuGrid = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                string line = lines[i];
                string[] values = line.Split(' ');
                for (int j = 0; j < 9; j++)
                {
                    sudokuGrid[i, j] = int.Parse(values[j]);
                }
            }
            return sudokuGrid;
        }
        private static void PrintSudokuGrid(int[,] sudokuGrid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(sudokuGrid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        private static bool SolveSudoku(int[,] sudokuGrid)
        {
            int row = 0;
            int col = 0;
            if (!FindEmptyCell(sudokuGrid, ref row, ref col))
            {
                // Toutes les cellules sont remplies, le Sudoku est résolu
                return true;
            }
            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(sudokuGrid, row, col, num))
                {
                    sudokuGrid[row, col] = num;
                    if (SolveSudoku(sudokuGrid))
                    {
                        return true;
                    }
                    // Annuler l'assignation précédente si la solution n'est pas valide
                    sudokuGrid[row, col] = 0;
                }
            }
            // Aucune valeur ne convient, retourner en arrière
            return false;
        }
        private static bool FindEmptyCell(int[,] sudokuGrid, ref int row, ref int col)
        {
            for (row = 0; row < 9; row++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (sudokuGrid[row, col] == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private static bool IsSafe(int[,] sudokuGrid, int row, int col, int num)
        {
            // Vérifier si la valeur est déjà présente dans la même ligne
            for (int j = 0; j < 9; j++)
            {
                if (sudokuGrid[row, j] == num)
                {
                    return false;
                }
            }
            // Vérifier si la valeur est déjà présente dans la même colonne
            for (int i = 0; i < 9; i++)
            {
                if (sudokuGrid[i, col] == num)
                {
                    return false;
                }
            }
            // Vérifier si la valeur est déjà présente dans la même région 3x3
            int startRow = row - row % 3;
            int startCol = col - col % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (sudokuGrid[startRow + i, startCol + j] == num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
