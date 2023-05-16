using Microsoft.Spark.Sql;
using System;
using System.IO;
using System.Linq;
using static Microsoft.Spark.Sql.Functions;

namespace HelloSpark
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello Spark!");

			// Create Spark session
			SparkSession spark =
				SparkSession
					.Builder()
					.AppName("word_count_sample")
					.GetOrCreate();

			// Create initial DataFrame
			//string filePath = args[0];
			//Console.WriteLine($"Current Dir:{Environment.CurrentDirectory}");
			//Console.WriteLine($"Current Files: {Directory.GetFiles(Environment.CurrentDirectory).Aggregate((s, s1) => s+s1)}");

			string filePath = "dbfs:/input.txt";
			if (args.Length>0)
			{
				filePath = args[0];
			}
			
			DataFrame dataFrame = spark.Read().Text(filePath);

			//Count words
			DataFrame words =
				dataFrame
					.Select(Microsoft.Spark.Sql.Functions.Split(Col("value"), " ").Alias("words"))
					.Select(Explode(Col("words")).Alias("word"))
					.GroupBy("word")
					.Count()
					.OrderBy(Col("count").Desc());

			// Display results
			words.Show();

			// Stop Spark session
			spark.Stop();
		}
	}
}
