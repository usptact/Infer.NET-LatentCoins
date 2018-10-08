using Microsoft.ML.Probabilistic.Distributions;

namespace LatentCoins
{
    public struct ModelData
    {
		public Beta selectorDist;
		public Beta successADist;
		public Beta successBDist;
    }
}
