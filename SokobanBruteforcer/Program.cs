using SokobanBruteforcer;

public class Program
{
    public static void Main()
    {
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

        SokobanSolver.SolveSokobanLevel(initialLevel, true, 252);


    }
}