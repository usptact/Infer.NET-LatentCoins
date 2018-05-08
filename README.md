# Infer.NET-LatentCoins
Inferring coin biases in a model with a latent selector variable.

The generative model:
0. Create coin "A" and coin "B"
1. Pick a coin with some probability
2. Toss the selected coin "n" times and record the number of heads.
3. Repeat the experiment "m" times
4. Return the m-vector of head counts

The program infers coin "A" and coin "B" biases given that the selector is unobserved. The coin picking probability is inferred as an auxilliary task.
