using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            int sudokuCount = 0;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Length != 81)
                        {
                            Console.WriteLine($"La ligne \"{line}\" ne contient pas un Sudoku valide. Ignorer.");
                            continue;
                        }

                        int[,] sudokuGrid = ParseSudokuGrid(line);

                        // Résoudre le Sudoku en utilisant l'algorithme de backtracking
                        if (SolveSudoku(sudokuGrid))
                        {
                            sudokuCount++;
                            // Afficher la grille Sudoku résolue
                            Console.WriteLine($"Grille Sudoku {sudokuCount} résolue :");
                            PrintSudokuGrid(sudokuGrid);
                        }
                        else
                        {
                            Console.WriteLine($"Impossible de résoudre le Sudoku {sudokuCount + 1}.");
                        }
                    }
                }

                Console.WriteLine($"Tous les Sudoku ont été résolus. Nombre total de Sudoku résolus : {sudokuCount}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Le fichier \"{filePath}\" est introuvable.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }
        }

        private static int[,] ParseSudokuGrid(string line)
        {
            int[,] sudokuGrid = new int[9, 9];
            int index = 0;
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    char ch = line[index++];
                    int value = ch - '0'; // Convert char to int
                    sudokuGrid[row, col] = value;
                }
            }
            return sudokuGrid;
        }

        private static string ChoosePath(string filePath1, string filePath2)
        {
            while (true)
            {
                // Demande à l'utilisateur de choisir un Sudoku
                Console.WriteLine("Choisir Sudoku : \n1-Soduku 1 \n2-Soduku 2");

                // Lit l'entrée utilisateur et tente de la convertir en entier
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 1)
                        return filePath1; // Renvoie filePath1 si l'entrée est 1
                    if (input == 2)
                        return filePath2; // Renvoie filePath2 si l'entrée est 2
                }

                // Entrée invalide, affiche un message d'erreur et recommence la boucle
                Console.WriteLine("Veuillez vérifier votre choix !");
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
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sb.Append(sudokuGrid[i, j]).Append(" ");
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
        }

        private static bool SolveSudoku(int[,] sudokuGrid)
        {
            List<int> emptyCells = FindEmptyCells(sudokuGrid);
            return SolveSudokuRecursive(sudokuGrid, emptyCells, 0);
        }

        private static bool SolveSudokuRecursive(int[,] sudokuGrid, List<int> emptyCells, int index)
        {
            if (index == emptyCells.Count)
            {
                // Toutes les cellules sont remplies, le Sudoku est résolu
                return true;
            }

            int cell = emptyCells[index];
            int row = cell / 9;
            int col = cell % 9;

            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(sudokuGrid, row, col, num))
                {
                    sudokuGrid[row, col] = num;
                    if (SolveSudokuRecursive(sudokuGrid, emptyCells, index + 1))
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

        private static List<int> FindEmptyCells(int[,] sudokuGrid)
        {
            List<int> emptyCells = new List<int>();
            for (int cell = 0; cell < 81; cell++)
            {
                int row = cell / 9;
                int col = cell % 9;
                if (sudokuGrid[row, col] == 0)
                {
                    emptyCells.Add(cell);
                }
            }
            return emptyCells;
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
