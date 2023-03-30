using SokobanBruteforcer;

public class Program
{
    public static void Main()
    {
        //_currentSolutions = SokobanJuniorLayouts.SokobanJunior15Solutions;
        //Level._solutions = _currentSolutions;
        //var initialLevel = new Level(SokobanJuniorLayouts.SokobanJunior15, null, 0);

        var currentSolution = EmptyHolesTest.LevelHolyHow4Solutions;
        Level._solutions = currentSolution;
        var initialLevel = new Level(EmptyHolesTest.LevelHolyHow4, null, 0);
        SokobanSolver.SolveSokobanLevel(initialLevel);


    }
}