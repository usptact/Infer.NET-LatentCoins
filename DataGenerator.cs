using Microsoft.ML.Probabilistic.Distributions;

namespace LatentCoins
{
    public class DataGenerator
    {
        public DataGenerator()
        {
        }

		public static int[] GenerateLatent(int numExperiments, int numTrials,
		                                   double selectProb, double biasA, double biasB)
		{
			int[] data = new int[numExperiments];

			Bernoulli selector = new Bernoulli(selectProb);
			Binomial coinA = new Binomial(numTrials, biasA);
			Binomial coinB = new Binomial(numTrials, biasB);

			for (int i = 0; i < numExperiments; i++)
			{
				if (selector.Sample())
					data[i] = coinA.Sample();
				else
					data[i] = coinB.Sample();
			}

			return data;
		}

		public static void GenerateComplete(int numExperiments, int numTrials,
                                            double selectProb, double biasA, double biasB,
		                                    out int[] data, out int[] assignment)
		{
			data = new int[numExperiments];
			assignment = new int[numExperiments];

            Bernoulli selector = new Bernoulli(selectProb);
            Binomial coinA = new Binomial(numTrials, biasA);
            Binomial coinB = new Binomial(numTrials, biasB);

            for (int i = 0; i < numExperiments; i++)
            {
                if (selector.Sample())
				{
					assignment[i] = 0;
					data[i] = coinA.Sample();
				}
				else
				{
					assignment[i] = 1;
					data[i] = coinB.Sample();
				}
            }
		}

    }
}
