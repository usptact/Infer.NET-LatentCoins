using System;

namespace LatentCoins
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			int numExperiments = 5;
			int numTrials = 10;
			double selectProb = 0.5;
			double biasA = 0.3;
			double biasB = 0.6;

			int[] data = DataGenerator.GenerateLatent(numExperiments, numTrials,
													  selectProb, biasA, biasB);
        }
    }
}
