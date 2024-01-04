using System;
using System.Collections.Generic;

class Solution
{
    public double F1 { get; set; }
    public double F2 { get; set; }
    public bool IsDominated { get; set; }
    public List<Solution> DominatingSet { get; set; }

    public Solution(double f1, double f2)
    {
        F1 = f1;
        F2 = f2;
        IsDominated = false;
        DominatingSet = new List<Solution>();
    }
}

class Program
{
    static void Main()
    {
        List<Solution> solutions = new List<Solution>
        {
            //
        };

        List<List<Solution>> fronts = FindParetoFrontsKung(solutions);

        Console.WriteLine("Liczba znalezionych frontów: " + fronts.Count);

        // Wyświetl pierwsze pięć frontów
        for (int i = 0; i < Math.Min(5, fronts.Count); i++)
        {
            Console.WriteLine($"Front {i + 1}:");
            foreach (var solution in fronts[i])
            {
                Console.WriteLine($"({solution.F1}, {solution.F2})");
            }
            Console.WriteLine();
        }
    }

    static List<List<Solution>> FindParetoFrontsKung(List<Solution> solutions)
    {
        List<List<Solution>> fronts = new List<List<Solution>>();
        foreach (var solution in solutions)
        {
            foreach (var otherSolution in solutions)
            {
                if (solution != otherSolution)
                {
                    if (IsDominatedBy(solution, otherSolution))
                    {
                        solution.IsDominated = true;
                        otherSolution.DominatingSet.Add(solution);
                    }
                }
            }
        }

        while (solutions.Count > 0)
        {
            List<Solution> currentFront = new List<Solution>();
            foreach (var solution in solutions)
            {
                if (!solution.IsDominated)
                {
                    currentFront.Add(solution);
                    foreach (var dominatedSolution in solution.DominatingSet)
                    {
                        dominatedSolution.IsDominated = false;
                    }
                }
            }

            foreach (var solution in currentFront)
            {
                solutions.Remove(solution);
            }

            fronts.Add(currentFront);
        }

        return fronts;
    }

    static bool IsDominatedBy(Solution solution1, Solution solution2)
    {
        return solution1.F1 <= solution2.F1 && solution1.F2 <= solution2.F2 && (solution1.F1 < solution2.F1 || solution1.F2 < solution2.F2);
    }
}
