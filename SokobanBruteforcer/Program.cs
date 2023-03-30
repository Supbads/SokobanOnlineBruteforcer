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

        var currentSolution = SokobanJuniorLayouts.SokobanJunior1Solutions;
        Level._solutions = currentSolution; //improvement not needed
        var initialLevel = new Level(SokobanJuniorLayouts.SokobanJunior1, null, 0);

        //lvl 2 hack
        //if (boxIndices.x == 1 || boxIndices.x == 6 || boxIndices.y == 1 || boxIndices.y == 6)
        //{
        //    return true;
        //}

        //var currentSolution = EmptyHolesTest.LevelHolyHow4Solutions;
        //Level._solutions = currentSolution;
        //var initialLevel = new Level(EmptyHolesTest.LevelHolyHow4, null, 0);

        SokobanSolver.SolveSokobanLevel(initialLevel);


    }
}