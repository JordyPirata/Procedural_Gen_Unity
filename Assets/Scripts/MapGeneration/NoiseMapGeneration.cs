using System;
using Random = System.Random;
using UnityEngine;

namespace MapGeneration
{
    public static class NoiseMapGeneration
    {
        public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight,int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
        {
            float[,] noiseMap = new float[mapWidth, mapHeight];
            Random prng = new Random(seed);
            Vector2[] octaveOffsets = new Vector2[octaves];

            for (int i = 0; i < octaves; i++)
            {
                float offsetY = prng.Next(-100000,100000) + offset.x;
                float offsetX = prng.Next(-100000,100000) + offset.y;
                octaveOffsets[i] = new Vector2(offsetX, offsetY);
            }
            if (scale <= 0)
            {
                throw new Exception("Scale cannot be negative");
            }
            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;

            float halfWidth = mapWidth / 2f;
            float halfHeight = mapHeight / 2f;

            for (int yIndex = 0; yIndex < mapHeight; yIndex++)
            {
                for (int xIndex = 0; xIndex < mapWidth; xIndex++)
                {
                    float amplitude = 1;
                    float frequency = 1;
                    float noiseHeight = 0;

                    for (int i = 0; i < octaves; i++)
                    { 
                        float sampleX = (xIndex-halfWidth + octaveOffsets[i].x) / scale * frequency ;
                        float sampleY = (yIndex- halfHeight + octaveOffsets[i].y) / scale * frequency ; 

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