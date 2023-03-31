using NUnit.Framework;
using SokobanBruteforcer;
using System.Diagnostics;

namespace Test_SokobanSolver
{
    public class TestSokobanSolver
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SolveSokobanJuniorLevel1()
        {
            var currentSolution = SokobanJuniorLayouts.SokobanJunior1Solutions;
            Level._solutions = currentSolution;
            var initialLevel = new Level(SokobanJuniorLayouts.SokobanJunior1, null, 0);

            var sw = Stopwatch.StartNew();
            bool res = SokobanSolver.SolveSokobanLevel(initialLevel, true);
            var soloveDuration = sw.ElapsedMilliseconds;

            Assert.IsTrue(res);
            Assert.Less(soloveDuration, 3 * 1000, "Sokoban junior level 1 took more than 7 seconds");
            //6.5 -> 2.1~ after optimization
            //21 steps best
        }

        [Test]
        public void SolveModernLevelHolyHow4()
        {
            var currentSolution = EmptyHolesTest.LevelHolyHow4Solutions;
            Level._solutions = currentSolution;
            var initialLevel = new Level(EmptyHolesTest.LevelHolyHow4, null, 0);            
            bool res = SokobanSolver.SolveSokobanLevel(initialLevel, true, 100);
            Assert.IsTrue(res);
            //steps should be 96
        }

        [Test]
        public void SolveSokobanLevel19()
        {
            var currentSolution = GridLayouts.Level19SolutionIndices;
            Level._solutions = currentSolution;
            var initialLevel = new Level(GridLayouts.Level19, null, 0);

            SolutionVariables._levelInvalidationImprovement = GridLayouts.Level19InvalidationImprovement;
            var sw = Stopwatch.StartNew();
            bool res = SokobanSolver.SolveSokobanLevel(initialLevel, true, 190);
            var soloveDuration = sw.ElapsedMilliseconds;
            Assert.IsTrue(res);
            Assert.Less(soloveDuration, 14 * 1000, "Sokoban level 19 took more than 14 seconds");
            //14 seconds -> 7.7 pending items opt -> 4.7 sb solution
        }

        [Test]
        public void SolveSokobanLevel21()
        {
            var currentSolution = GridLayouts.Level21SolutionIndices;
            Level._solutions = currentSolution;
            var initialLevel = new Level(GridLayouts.Level21, null, 0);
            
            var sw = Stopwatch.StartNew();
            bool res = SokobanSolver.SolveSokobanLevel(initialLevel, true, 93);
            var soloveDuration = sw.ElapsedMilliseconds;
            Assert.Less(soloveDuration, 60 * 1000, "Sokoban level 21 took more than 60 seconds");

            Assert.IsTrue(res);
            //1min 56 sec execution 30.03
            //44.3 sec after proper pending items optimization
            
            //32.6 with Snapshot v2 algo

        }

        [Test]
        public void SolveSokobanLevel58()
        {
            var currentSolution = GridLayouts.Level58SolutionIndices;
            Level._solutions = currentSolution;
            var initialLevel = new Level(GridLayouts.Level58, null, 0);

            SolutionVariables._levelInvalidationImprovement = GridLayouts.Level58InvalidationImprovement;
            var sw = Stopwatch.StartNew();
            bool res = SokobanSolver.SolveSokobanLevel(initialLevel, true, 252);
            var soloveDuration = sw.ElapsedMilliseconds;
            Assert.IsTrue(res);
            
        }
    }
}