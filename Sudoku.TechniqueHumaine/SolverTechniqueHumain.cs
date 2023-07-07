using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Spark.Sql;
using Sudoku.Shared;
using System.Runtime.ConstrainedExecution;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            var nbWorkers = 2;
            var nbCoresPerWorker = 5; var nbLignesMax = 100000;
            var chronometre = Stopwatch.StartNew();
            var filePath = Path.Combine(Environment.CurrentDirectory, "sudoku.csv");
                        
            SparkSession spark = SparkSession
            .Builder()
            .AppName("Norvig Solver Spark")
            .Config("spark.executor.cores", nbCoresPerWorker)
            .Config("spark.executor.instances", nbWorkers)
            .GetOrCreate();
            DataFrame dataFrame = spark
            .Read()
            .Option("header", true)
            //.Option("inferSchema", true)
            .Schema("quizzes string, solutions string")
            .Csv(filePath);
            DataFrame milleSudoku = dataFrame.Limit(nbLignesMax);


            int[,] sudoku = new int[9, 9] {
{ 5, 3, 0, 0, 7, 0, 0, 0, 0 },
{ 6, 0, 0, 1, 9, 5, 0, 0, 0 },
{ 0, 9, 8, 0, 0, 0, 0, 6, 0 },
{ 8, 0, 0, 0, 6, 0, 0, 0, 3 },
{ 4, 0, 0, 8, 0, 3, 0, 0, 1 },
{ 7, 0, 0, 0, 2, 0, 0, 0, 6 },
{ 0, 6, 0, 0, 0, 0, 2, 8, 0 },
{ 0, 0, 0, 4, 1, 9, 0, 0, 5 },
{ 0, 0, 0, 0, 8, 0, 0, 7, 9 }
};

            Console.WriteLine("Sudoku avant résolution :");
            AfficherSudoku(sudoku);

            if (ResoudreSudoku(sudoku))
            {
                Console.WriteLine("Sudoku après résolution :");
                AfficherSudoku(sudoku);
            }

            else
            {
                Console.WriteLine("Impossible de résoudre le sudoku.");
            }

            Console.ReadKey();
        }

        public static string SolveSudoku(string strSudoku)
        {
            var sudoku = SudokuGrid.ReadSudoku(strSudoku);
            //var norvigSolver = new NorvigSolver();
            var sudokuResolu = norvigSolver.Solve(sudoku);
           
        
            string game = "";
            foreach (var ligneSudoku in sudokuResolu.Cellules)
            {
                foreach (var celluleSudoku in ligneSudoku)
                {
                    game = game + celluleSudoku.ToString();
                }
            }
            return game;
        }
  

static bool ResoudreSudoku(int[,] sudoku)
        {
            int ligne = 0;
            int colonne = 0;

            if (!TrouverCaseVide(sudoku, ref ligne, ref colonne))
            {
                return true; // Sudoku résolu
            }

            for (int chiffre = 1; chiffre <= 9; chiffre++)
            {
                if (EstValide(sudoku,

                ligne, colonne, chiffre))
                {
                    sudoku[ligne, colonne] = chiffre;

                    if (ResoudreSudoku(sudoku))
                    {
                        return true;
                    }

                    sudoku[ligne, colonne] = 0; // Réinitialiser la case
                }
            }

            return false; // Aucune solution trouvée
        }

        static bool TrouverCaseVide(int[,] sudoku, ref int ligne, ref int colonne)
        {

            for (ligne = 0; ligne < 9; ligne++)
            {
                for (colonne = 0; colonne < 9; colonne++)
                {
                    if (sudoku[ligne, colonne] == 0)
                    {
                        return true;
                    }
                }
            }

            return false; // Aucune case vide trouvée
        }

        static bool EstValide(int[,] sudoku, int ligne, int colonne, int chiffre)
        {
            // Vérifier la ligne
            for (int

            i = 0; i < 9; i++)
            {
                if (sudoku[ligne, i] == chiffre)
                {
                    return false;
                }
            }

            // Vérifier la colonne
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[i, colonne] == chiffre)
                {
                    return false;
                }
            }

            // Vérifier la région 3x3
            int regionLigne = ligne - ligne % 3;
            int regionColonne

            = colonne - colonne % 3;

            for (int i = regionLigne; i < regionLigne + 3; i++)
            {
                for (int j = regionColonne; j < regionColonne + 3; j++)
                {
                    if (sudoku[i, j] == chiffre)
                    {
                        return false;
                    }
                }
            }

            return true; // Le chiffre est valide
        }

        static void AfficherSudoku(int[,] sudoku)
        {
            for (int ligne

            = 0; ligne < 9; ligne++)
            {
                for (int colonne = 0; colonne < 9; colonne++)
                {
                    Console.Write(sudoku[ligne, colonne] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}


