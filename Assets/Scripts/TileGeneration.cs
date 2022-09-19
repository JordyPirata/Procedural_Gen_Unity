using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MapGeneration
{
    [System.Serializable]
    public class TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }

    public class TileGeneration : MonoBehaviour
    {
        [SerializeField]
        private TerrainType[] terrainTypes;

        [SerializeField]
        NoiseMapGeneration noiseMapGeneration;

        [SerializeField]
        private MeshRenderer titleRenderer;

        [SerializeField]
        private MeshFilter meshFilter;

        [SerializeField]
        private MeshCollider meshCollider;

        [SerializeField]
        private float mapScale;

        void Start()
        {
            GenerateTitle();
        }

        void GenerateTitle()
        {
            Vector3[] meshVertices = this.meshFilter.mesh.vertices;

            int titleDepth = (int)Mathf.Sqrt(meshVertices.Length);
            int titleWidth = titleDepth;
            float[,] heightMap = this.noiseMapGeneration.GenerateNoiseMap(titleDepth, titleWidth, this.mapScale);

            Texture2D titleTexture = BuildTexture(heightMap);
            this.titleRenderer.material.mainTexture = titleTexture;
        }
        private Texture2D BuildTexture(float[,] heightMap)
        {
            int titleDepth = heightMap.GetLength(0);
            int titleWidth = heightMap.GetLength(1);

            Color[] colorMap = new Color[titleDepth * titleWidth];
            for (int zIndex = 0; zIndex < titleDepth; zIndex++)
            {
                for(int xIndex = 0; xIndex < titleDepth; xIndex++)
                {
                    int colorIndex = zIndex * titleWidth + xIndex;
                    float height = heightMap [zIndex, xIndex];

                    TerrainType terrainType = ChooseTerrainType(height);
                    colorMap[colorIndex] = terrainType.color;
                }
            }

            Texture2D tileTexture = new Texture2D(titleWidth, titleDepth);

            tileTexture.wrapMode = TextureWrapMode.Clamp;
            tileTexture.SetPixels(colorMap);
            tileTexture.Apply();

            return tileTexture;
        }

        TerrainType ChooseTerrainType(float height)
        {
            foreach (TerrainType terrainType in terrainTypes)
            {
                if (height < terrainType.height)
                {
                    return terrainType;
                }

            }
            return terrainTypes[terrainTypes.Length - 1];
        }
    }
}