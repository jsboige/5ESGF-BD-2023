﻿using System.Collections.Generic;
using System.Linq;

namespace GeneticSharp.Extensions
{
    /// <summary>
    /// Evaluates a sudoku chromosome for completion by counting duplicates in rows, columns, boxes, and differences from the target mask
    /// </summary>
    public class SudokuFitness : IFitness
    {
        /// <summary>
        /// The target Sudoku Mask to solve.
        /// </summary>
        private readonly SudokuBoard _targetSudokuBoard;

        public SudokuFitness(SudokuBoard targetSudokuBoard)
        {
            _targetSudokuBoard = targetSudokuBoard;
        }

        /// <summary>
        /// Evaluates a chromosome according to the IFitness interface. Simply reroutes to a typed version.
        /// </summary>
        /// <param name="chromosome"></param>
        /// <returns></returns>
        public double Evaluate(IChromosome chromosome)
        {
            return Evaluate((SudokuChromosomeBase)chromosome);
        }

        /// <summary>
        /// Evaluates a ISudokuChromosome by summing over the fitnesses of its corresponding Sudoku boards.
        /// </summary>
        /// <param name="chromosome">a Chromosome that can build Sudokus</param>
        /// <returns>the chromosome's fitness</returns>
        public double Evaluate(SudokuChromosomeBase chromosome)
        {
            List<double> scores = new List<double>();

            var sudokus = chromosome.GetSudokus();
            foreach (var sudoku in sudokus)
            {
                scores.Add(Evaluate(sudoku));
            }
            return scores.Sum();
        }

        /// <summary>
        /// Evaluates a single Sudoku board by counting the duplicates in rows, boxes
        /// and the digits differing from the target mask.
        /// </summary>
        /// <param name="testSudokuBoard">the board to evaluate</param>
        /// <returns>the number of mistakes the Sudoku contains.</returns>
        public double Evaluate(SudokuBoard testSudokuBoard)
        {
            var sudokuGrid = testSudokuBoard.toSudokuGrid();
            var toReturn = sudokuGrid.NbErrors(sudokuGrid);

            return -toReturn;
        }
    }
}