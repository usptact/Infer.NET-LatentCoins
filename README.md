# Infer.NET-LatentCoins
Inferring coin biases in a model with latent selector variable.

The generative model:
1. Select coin
2. Toss the selected coin "n" times and record the number of heads.
3. Repeat the experiment "m" times
4. Return the m-vector of head counts

The program infers coin "A" and coin "B" biases given that the selector is unobserved.
