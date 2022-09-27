using UnityEngine;
namespace MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        public int mapWidth;
        public int mapHeight;
        public float noiseScale;

        public int octaves;
        public float persistance;
        public float lacunarity;

        public bool AutoUpdate;

        public void GenerateMap() 
        { 
            float[,] noiseMap = NoiseMapGeneration.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persistance, lacunarity);

            MapDisplay display = FindObjectOfType<MapDisplay>();
            display.DrawNoiseMap(noiseMap);
        }
    }
}
