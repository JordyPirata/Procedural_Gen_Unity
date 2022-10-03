using UnityEngine;

namespace MapGeneration
{
    [System.Serializable]
    public class TerrainType
    {
        public string name;
        public float heigth;
        public Color color;
    }
    public class MapGenerator : MonoBehaviour
    {
        public enum DrawMode {NoiseMap, ColorMap, Mesh}
        public DrawMode drawMode;

        public int mapWidth;
        public int mapHeight;
        public float noiseScale;

        public int octaves;
        [Range(0,1)]
        public float persistance;
        public float lacunarity;

        public int seed;
        public Vector2 offset;

        public float meshHeightMultiplayer;

        [SerializeField]
        private TerrainType[] terrainTypes;

        public bool AutoUpdate;

        public void GenerateMap() 
        { 
            float[,] noiseMap = NoiseMapGeneration.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

            Color[] colorMap = new Color[mapWidth * mapHeight];
            for (int yIndex = 0; yIndex < mapHeight; yIndex++)
            {
                for (int xIndex = 0; xIndex < mapWidth; xIndex++)
                {
                    int colorIndex = yIndex * mapWidth + xIndex;
                    float height = noiseMap[xIndex, yIndex];

                    TerrainType terrainType = ChooseTerrainType(height);
                    colorMap[colorIndex] = terrainType.color;
                }
            }
            MapDisplay display = FindObjectOfType<MapDisplay>();
            if (drawMode == DrawMode.NoiseMap)
            {
                display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
            }
            else if (drawMode == DrawMode.ColorMap)
            {
                display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
            }
            else if (drawMode == DrawMode.Mesh)
            {
                display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplayer),TextureGenerator.TextureFromColorMap(colorMap,mapWidth,mapHeight));
            }
        }

        TerrainType ChooseTerrainType(float heigth)
        {
            foreach (TerrainType terrainType in terrainTypes)
            {
                if (heigth > terrainType.heigth)
                {
                    return terrainType;
                }
            }
            return terrainTypes[terrainTypes.Length - 1];
        }
        private void OnValidate()
        {
            if (mapWidth < 1)
            {
                mapWidth = 1;
            }
            if (mapHeight < 1)
            {
                mapHeight = 1;
            }
            if(lacunarity < 1)
            {
                lacunarity = 1;
            }
            if(octaves < 0)
            {
                octaves = 0;
            }

        }
    }
}
