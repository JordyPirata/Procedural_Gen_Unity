using UnityEngine;
namespace MapGeneration
{
    public class MapGenrator : MonoBehaviour
    {
        public int mapHeight;
        public int mapWidth;
        public int noiseScale;
        public void GenerateMap()
        { 
            float[,] noiseMap = NoiseMapGeneration.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

            MapDisplay display = FindObjectOfType<MapDisplay>();
            display.DrawNoiseMap(noiseMap);

        }
    }
}
