using UnityEngine;
namespace MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        public int mapWidth;
        public int mapHeight;
        public int noiseScale;
        public bool AutoUpdate;

        public void GenerateMap()
        { 
            float[,] noiseMap = NoiseMapGeneration.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

            MapDisplay display = FindObjectOfType<MapDisplay>();
            display.DrawNoiseMap(noiseMap);
        }
    }
}
