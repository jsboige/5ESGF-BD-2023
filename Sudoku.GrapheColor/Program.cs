using Microsoft.Spark.Sql;
using Sudoku.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.GrapheColor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sudoku Graphe color solver SPARK");

            SparkSession spark =
                SparkSession
                    .Builder()
                    .AppName("sudokuSolver_interactive_spark")
                    .GetOrCreate();

            string filePath = "dbfs:/Sudoku_file.txt";
            if (args.Length > 0)
            {
                filePath = args[0];
            }

            DataFrame dataFrame = spark.Read().Text(filePath);


            string[] sudokuData = dataFrame.SelectExpr("CAST(value AS STRING)").Collect().Select(row => row.GetAs<string>("value")).ToArray();

            // Convertir le DataFrame en une représentation utilisable pour résoudre le Sudoku
            // Par exemple, extraire les lignes du DataFrame et les stocker dans une liste ou un tableau
            List<SudokuGrid> sudokuGrids = SudokuGrid.ReadSudokuFile(filePath);

            // Vous pouvez accéder à chaque SudokuGrid individuellement
            foreach (SudokuGrid sudokuGrid in sudokuGrids)
            {
                // Utilisez le SudokuGrid pour résoudre le Sudoku ou effectuer d'autres opérations
                // Par exemple, affichez la grille Sudoku
                Console.WriteLine(sudokuGrid.ToString());

                // Créez une instance de GrapheColorSolver
                ISudokuSolver solver = new GrapheColorSolver();

                // Utilisez le solver pour résoudre le SudokuGrid
                SudokuGrid solvedGrid = solver.Solve(sudokuGrid);

                // Affichez la grille Sudoku résolue
                Console.WriteLine(solvedGrid.ToString());
            }

            spark.Stop();
        }


    }

}
