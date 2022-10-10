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
        public void DrawMapInEditor()
        {
            MapData mapData = GenerateMapData();

            MapDisplay display = FindObjectOfType<MapDisplay>();
            if (drawMode == DrawMode.NoiseMap)
            {
                display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
            }
            else if (drawMode == DrawMode.ColorMap)
            {
                display.DrawTexture(TextureGenerator.TextureFromColorMap(mapData.coulorMap, mapChunkSize, mapChunkSize));
            }
            else if (drawMode == DrawMode.Mesh)
            {
                display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplayer, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(mapData.coulorMap, mapChunkSize, mapChunkSize));
            }
        }

        MapData GenerateMapData() 
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
            return new MapData(noiseMap,colorMap);
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
    public struct MapData
    {
        public float[,] heightMap;
        public Color[] coulorMap;

        public MapData(float[,] heightMap, Color[] coulorMap )
        {
            this.heightMap = heightMap;
            this.coulorMap = coulorMap;
        }
    }
}
