
using System;
using System.Collections.Generic;

namespace KohonenSOM
{
    public class KohonenNetwork
    {
        private readonly Random random = new Random();

        public int MapSize { get; }
        public double[,,] Weights { get; }

        public KohonenNetwork(int mapSize)
        {
            if (mapSize < 2)
                throw new ArgumentException("Rozmiar mapy musi wynosić co najmniej 2.");

            MapSize = mapSize;
            Weights = new double[mapSize, mapSize, 2];

            // Losowa inicjalizacja wag neuronów
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Weights[i, j, 0] = random.NextDouble();
                    Weights[i, j, 1] = random.NextDouble();
                }
            }
        }

        // Znalezienie neuronu zwycięskiego (BMU)
        public (int x, int y) FindWinner(double[] input)
        {
            double minDistance = double.MaxValue;
            int winnerX = 0;
            int winnerY = 0;

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    double dx = input[0] - Weights[i, j, 0];
                    double dy = input[1] - Weights[i, j, 1];

                    double distance = dx * dx + dy * dy;

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        winnerX = i;
                        winnerY = j;
                    }
                }
            }

            return (winnerX, winnerY);
        }

        // Jedna epoka uczenia sieci
        public void TrainEpoch(
            List<double[]> data,
            double learningRate,
            double radius)
        {
            List<double[]> shuffledData = new List<double[]>(data);

            for (int i = shuffledData.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                (shuffledData[i], shuffledData[j]) =
                    (shuffledData[j], shuffledData[i]);
            }
            foreach (double[] input in shuffledData)
            {
                var winner = FindWinner(input);

                for (int i = 0; i < MapSize; i++)
                {
                    for (int j = 0; j < MapSize; j++)
                    {
                        double distanceSquared =
                            Math.Pow(i - winner.x, 2) +
                            Math.Pow(j - winner.y, 2);

                        // Funkcja sąsiedztwa Gaussa
                        double influence = Math.Exp(
                            -distanceSquared / (2 * radius * radius));

                        // Aktualizacja wag neuronu
                        for (int k = 0; k < 2; k++)
                        {
                            Weights[i, j, k] +=
                                learningRate * influence *
                                (input[k] - Weights[i, j, k]);
                        }
                    }
                }
            }
        }

        // Obliczenie błędu kwantyzacji
        public double CalculateError(List<double[]> data)
        {
            if (data.Count == 0)
                return 0;

            double totalError = 0;

            foreach (double[] input in data)
            {
                var winner = FindWinner(input);

                double dx = input[0] - Weights[winner.x, winner.y, 0];
                double dy = input[1] - Weights[winner.x, winner.y, 1];

                totalError += Math.Sqrt(dx * dx + dy * dy);
            }

            return totalError / data.Count;
        }
    }
}
