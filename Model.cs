using Microsoft.ML.Probabilistic.Models;
using Microsoft.ML.Probabilistic.Distributions;

namespace LatentCoins
{
    public class Model
	{
		protected Range experimentRange;

		protected Variable<int> nExperiments;
		protected Variable<int> nTrials;
        
		protected Variable<Beta> selectorPrior;
		protected Variable<Beta> successAPrior;
		protected Variable<Beta> successBPrior;

		protected Variable<double> probSelection;
		protected Variable<double> probSuccessA;
		protected Variable<double> probSuccessB;

		protected VariableArray<int> data;

		protected InferenceEngine engine;

        public Model()
        {
        }

		public virtual void CreateModel()
		{
			engine = new InferenceEngine();

			nExperiments = Variable.New<int>().Named("numExperiments");
			nTrials = Variable.New<int>().Named("numTrials");

			experimentRange = new Range(nExperiments).Named("experiment");         

			selectorPrior = Variable.New<Beta>();
			successAPrior = Variable.New<Beta>();
			successBPrior = Variable.New<Beta>();

			probSelection = Variable.Random<double, Beta>(selectorPrior).Named("probSelection");
			probSuccessA = Variable.Random<double, Beta>(successAPrior).Named("probSuccessA");
			probSuccessB = Variable.Random<double, Beta>(successBPrior).Named("probSuccessB");

			data = Variable.Array<int>(experimentRange).Named("data");
            
			using (Variable.ForEach(experimentRange))
			{
				Variable<bool> select = Variable.Bernoulli(probSelection);

				using (Variable.If(select))
					data[experimentRange] = Variable.Binomial(nTrials, probSuccessA);
				using (Variable.IfNot(select))
					data[experimentRange] = Variable.Binomial(nTrials, probSuccessB);
			}
		}

		public virtual void SetModelData(ModelData modelData)
		{
			selectorPrior.ObservedValue = modelData.selectorDist;
			successAPrior.ObservedValue = modelData.successADist;
			successBPrior.ObservedValue = modelData.successBDist;
		}

		public ModelData InferModelData(int[] trainingData, int numTrials)
		{
			ModelData posteriors = new ModelData();

			nExperiments.ObservedValue = trainingData.Length;
			nTrials.ObservedValue = numTrials;

			data.ObservedValue = trainingData;

			posteriors.selectorDist = engine.Infer<Beta>(probSelection);
			posteriors.successADist = engine.Infer<Beta>(probSuccessA);
			posteriors.successBDist = engine.Infer<Beta>(probSuccessB);

			return posteriors;
		}
    }
}
