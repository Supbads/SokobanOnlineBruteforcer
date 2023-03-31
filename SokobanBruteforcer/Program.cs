using SokobanBruteforcer;

public class Program
{
    public static void Main()
    {
        //ParallelismExample();


        //var currentSolution = SokobanJuniorLayouts.SokobanJunior15Solutions;
        //Level._solutions = currentSolution;
        //SolutionVariables._levelInvalidationImprovement = SokobanJuniorLayouts.Level15InvalidationImprovement;
        //var initialLevel = new Level(SokobanJuniorLayouts.SokobanJunior15, null, 0);


        //lvl hack
        //if (GridLayouts.Levle57InvalidationImprovement(boxIndices))
        //{
        //    wallChecksFuncInvalidationsHack++;
        //    return true;
        //}        

        //var currentSolution = EmptyHolesTest.LevelHolyHow4Solutions;
        //Level._solutions = currentSolution;
        //var initialLevel = new Level(EmptyHolesTest.LevelHolyHow4, null, 0);

        var currentSolution = EmptyHolesTest.LevelHolyHow4Solutions;
        Level._solutions = currentSolution;
        var initialLevel = new Level(EmptyHolesTest.LevelHolyHow4, null, 0);
        bool res = SokobanSolver.SolveSokobanLevel(initialLevel, true, 100);



        //var currentSolution = GridLayouts.Level58SolutionIndices;
        //Level._solutions = currentSolution;
        //SolutionVariables._levelInvalidationImprovement = GridLayouts.Level58InvalidationImprovement;
        //var initialLevel = new Level(GridLayouts.Level58, null, 0);
        //SokobanSolver.SolveSokobanLevel(initialLevel, false, 252);


    }

    private static void ParallelismExample()
    {
        var numbers = Enumerable.Range(1, 30); // 1 , 2 ,3 

        int sum = 0;
        var lockObj = new object();

        Parallel.ForEach(
            numbers,
            new ParallelOptions { MaxDegreeOfParallelism = 2 },
            () => 0,
            (num, state, accumulator) =>
            {
                Console.WriteLine($"current Number: {num}");
                accumulator += num; return accumulator;
            },

            (fin) =>
            {
                Console.WriteLine($"Fin: {fin}");
                lock (lockObj)
                {
                    Console.WriteLine($"Fin: {fin} inside lock");
                    Thread.Sleep(2000);

                    sum += fin;
                    Console.WriteLine($"Fin: {fin} releases the lock");
                }
                //Console.WriteLine($"Fin: {fin} outside lock");
            });

        Console.WriteLine("Final sum is: " + sum);
    }
}