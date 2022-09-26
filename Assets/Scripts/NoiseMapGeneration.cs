using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public static class NoiseMapGeneration
    {
        public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
        {
            float[,] noiseMap = new float[mapWidth, mapHeight];
            
            if (scale <= 0)
            {
                throw new Exception("Scale cannot be negative");
            }
            for (int zIndex = 0; zIndex < mapWidth; zIndex++)
            {
                for (int xIndex = 0; xIndex < mapHeight; xIndex++)
                {
                    float sampleX = xIndex / scale;
                    float sampleZ = zIndex / scale;

                    float noise = Mathf.PerlinNoise(sampleX, sampleZ);

                    noiseMap[zIndex, xIndex] = noise;
                }
            }

            return noiseMap;
        }
    }
}
