using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public static class NoiseMapGeneration
    {
        public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int octaves, float persistance, float lacunarity)
        {
            float[,] noiseMap = new float[mapWidth, mapHeight];
            
            if (scale <= 0)
            {
                throw new Exception("Scale cannot be negative");
            }

            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;
            for (int yIndex = 0; yIndex < mapHeight; yIndex++)
            {
                for (int xIndex = 0; xIndex < mapWidth; xIndex++)
                {
                    float amplitude = 1;
                    float frequency = 1;
                    float noiseHeight = 0;

                    for (int i = 0; i < octaves; i++)
                    { 
                        float sampleX = xIndex / scale * frequency;
                        float sampleY = yIndex / scale * frequency; 

                        float noise = Mathf.PerlinNoise(sampleX, sampleY) * 2 -1;
                        noiseHeight += noise * amplitude;

                        amplitude *= persistance;
                        frequency *= lacunarity;
                    }
                    if (noiseHeight > maxNoiseHeight)
                    {
                        maxNoiseHeight = noiseHeight;
                    }
                    else if (noiseHeight < minNoiseHeight)
                    {
                        minNoiseHeight = noiseHeight;
                    }
                    noiseMap[xIndex, yIndex] = noiseHeight;
                }
            }
            for (int yIndex = 0; yIndex < mapHeight; yIndex++)
            {
                for (int xIndex = 0; xIndex < mapWidth; xIndex++)
                {
                    noiseMap[xIndex, yIndex] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[xIndex,yIndex]);
                }
            }
            return noiseMap;
        }
    }
}