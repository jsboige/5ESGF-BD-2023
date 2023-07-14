using System;
using System.Linq;
using System.IO;
using Microsoft.Spark.Sql;


/*namespace Sudokus_avec_la_méthode_SA
{
    class Program
    {
        static void Main()
        {
            string filePath = @"C:\Users\A0152\OneDrive\Bureau\BIGDATA\Easy50.txt";

            
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                
                lines[i] = lines[i].Replace(".", "0").Trim();

                
                string[] sublines = new string[9];
                for (int j = 0; j < 9; j++)
                {
                    int length = Math.Min(lines[i].Length - j * 9, 9);
                    sublines[j] = lines[i].Substring(j * 9, length);
                }

                
                string joinedLine = string.Join("\n", sublines);

                
                lines[i] = joinedLine;
            }

         
            }
    }
}*/



namespace Sudokus_avec_la_méthode_SA
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SparkSession spark = SparkSession
                .Builder()
                .AppName("Sudoku Solver")
                .GetOrCreate();

            
            DataFrame sudokuDF = spark
                .Read()
                .Text(@"..\..\..\..\Puzzles\Sudoku_Easy51.txt");

            
            DataFrame processedDF = sudokuDF
                .Select(Functions.Trim(Functions.RegexpReplace(sudokuDF["value"], "\\.", "0")).Alias("processed"));

            
            DataFrame sublinesDF = processedDF
                .Select(Functions.Expr("substring(processed, (pos - 1) * 9 + 1, 9) AS sublines"))
                .WithColumn("pos", Functions.Explode(Functions.PosExplode(Functions.Split(processedDF["processed"], "(?<=\\G.{9})"))));

            
            DataFrame joinedLineDF = sublinesDF
                .GroupBy("pos")
                .Agg(Functions.ConcatWs("\n", Functions.CollectList(sublinesDF["sublines"])).Alias("joinedLine"))
                .OrderBy("pos");

            
            //string startingSudoku = joinedLineDF.Take(95)[94].GetAs<string>("joinedLine");
            //Console.WriteLine(startingSudoku);

            
            //string seventhLine = joinedLineDF.Take(7)[6].GetAs<string>("joinedLine");
            //Console.WriteLine(seventhLine);

            
            spark.Stop();
        }
    }
}

