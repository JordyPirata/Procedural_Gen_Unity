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

        public const int mapChunkSize = 241;
        [Range(0, 6)]
        public int levelOfDetail;
        public float noiseScale;

        public int octaves;
        [Range(0,1)]
        public float persistance;
        public float lacunarity;

        public int seed;
        public Vector2 offset;

        public float meshHeightMultiplayer;
        public AnimationCurve meshHeightCurve;

        [SerializeField]
        private TerrainType[] terrainTypes;

        public bool AutoUpdate;

        public void GenerateMap() 
        { 
            float[,] noiseMap = NoiseMapGeneration.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

            Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
            for (int yIndex = 0; yIndex < mapChunkSize; yIndex++)
            {
                for (int xIndex = 0; xIndex < mapChunkSize; xIndex++)
                {
                    int colorIndex = yIndex * mapChunkSize + xIndex;
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
                display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
            }
            else if (drawMode == DrawMode.Mesh)
            {
                display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplayer,meshHeightCurve,levelOfDetail),TextureGenerator.TextureFromColorMap(colorMap,mapChunkSize,mapChunkSize));
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
