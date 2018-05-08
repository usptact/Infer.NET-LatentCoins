using System;
using MicrosoftResearch.Infer.Distributions;

namespace LatentCoins
{
    public struct ModelData
    {
		public Beta selectorDist;
		public Beta successADist;
		public Beta successBDist;
    }
}
