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
            Assert.Less(soloveDuration, 10 * 1000, "Sokoban junir level 1 took more than 10 seconds");
        }

        [Test]
        public void SolveModernLevelHolyHow4()
        {
            var currentSolution = EmptyHolesTest.LevelHolyHow4Solutions;
            Level._solutions = currentSolution;
            var initialLevel = new Level(EmptyHolesTest.LevelHolyHow4, null, 0);
            
            bool res = SokobanSolver.SolveSokobanLevel(initialLevel, true);

            Assert.IsTrue(res);
            //steps should be 96
        }

        [Test]
        public void SolveSokobanLevel2()
        {
            var currentSolution = GridLayouts.Level19SolutionIndices;
            Level._solutions = currentSolution;
            var initialLevel = new Level(GridLayouts.Level19, null, 0);

            SolutionVariables._levelInvalidationImprovement = GridLayouts.Level19InvalidationImprovement;
            var sw = Stopwatch.StartNew();
            bool res = SokobanSolver.SolveSokobanLevel(initialLevel, true, 190);
            var soloveDuration = sw.ElapsedMilliseconds;
            Assert.IsTrue(res);
            Assert.Less(soloveDuration, 14 * 1000, "Sokoban junir level 1 took more than 14 seconds");
        }

        [Test]
        public void SolveSokobanLevel21()
        {
            var currentSolution = GridLayouts.Level21SolutionIndices;
            Level._solutions = currentSolution;
            var initialLevel = new Level(GridLayouts.Level21, null, 0);
            
            var sw = Stopwatch.StartNew();
            bool res = SokobanSolver.SolveSokobanLevel(initialLevel, true);
            var soloveDuration = sw.ElapsedMilliseconds;

            Assert.IsTrue(res);
            Assert.Less(soloveDuration, 10 * 1000, "Sokoban junir level 1 took more than 10 seconds");
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