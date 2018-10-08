using System;
using Microsoft.ML.Probabilistic.Distributions;

namespace LatentCoins
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			int numExperiments = 1000;
			int numTrials = 10;
			double selectProb = 0.7;
			double biasA = 0.3;
			double biasB = 0.6;

			int[] trainingData = DataGenerator.GenerateLatent(numExperiments, numTrials,
													          selectProb, biasA, biasB);
            
			ModelData modelData = new ModelData
			{
				selectorDist = new Beta(1, 1),
				successADist = new Beta(1, 2),      // breaking symmetry
				successBDist = new Beta(2, 1)       // -- same here --
			};

			Model model = new Model();
			model.CreateModel();
			model.SetModelData(modelData);

			ModelData posteriors = model.InferModelData(trainingData, numTrials);

			Console.WriteLine("TRUE select probability = {0}", selectProb);
			Console.WriteLine("TRUE coin A bias = {0}", biasA);
			Console.WriteLine("TRUE coib B bias = {0}\n", biasB);

			Console.WriteLine("ESTIM select probability = {0}", posteriors.selectorDist.GetMean());
			Console.WriteLine("ESTIM coin A bias = {0}", posteriors.successADist.GetMean());
			Console.WriteLine("ESTIM coin B bias = {0}", posteriors.successBDist.GetMean());

            Console.ReadKey();
        }
    }
}
