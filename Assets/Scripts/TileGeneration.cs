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
        private MeshRenderer titleRenderer;

        [SerializeField]
        private MeshFilter meshFilter;

        [SerializeField]
        private MeshCollider meshCollider;
  
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GenerateTitle();
            }
        }
        [SerializeField]
        NoiseMapGeneration noiseMapGeneration;

        [SerializeField]
        private float mapScale;

        [SerializeField]
        private Wave[] waves;

        void GenerateTitle()
        {
            Vector3[] meshVertices = this.meshFilter.mesh.vertices;

            int titleDepth = (int)Mathf.Sqrt(meshVertices.Length);
            int titleWidth = titleDepth;

            float offsetX = -this.gameObject.transform.position.x;
            float offsetZ = -this.gameObject.transform.position.z;

            float[,] heightMap = this.noiseMapGeneration.GenerateNoiseMap(titleDepth, titleWidth, this.mapScale, offsetX, offsetZ, waves);

            Texture2D titleTexture = BuildTexture(heightMap);
            this.titleRenderer.material.mainTexture = titleTexture;

            UpdateMeshVertices(heightMap);
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

        [SerializeField]
        private TerrainType[] terrainTypes;
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

        [SerializeField]
        private float heightMultiplayer;
        [SerializeField]
        private AnimationCurve heightCurve;
        private void UpdateMeshVertices(float[,] heightMap)
        {
            int titleDepth = heightMap.GetLength(0);
            int titleWidth = heightMap.GetLength(1);

            Vector3[] meshVertices = this.meshFilter.mesh.vertices;

            int vertexIndex = 0;
            for (int zIndex = 0; zIndex < titleDepth; zIndex++)
            {
                for (int xIndex = 0; xIndex < titleWidth; xIndex++)
                {
                    float height = heightMap[zIndex,xIndex];

                    Vector3 vertex = meshVertices[vertexIndex];
                    meshVertices[vertexIndex] = new Vector3(vertex.x, heightCurve.Evaluate(height) * heightMultiplayer, vertex.z);

                    vertexIndex++;
                }
            }
            this.meshFilter.mesh.vertices = meshVertices;
            this.meshFilter.mesh.RecalculateBounds();
            this.meshFilter.mesh.RecalculateNormals();

            this.meshCollider.sharedMesh = this.meshFilter.mesh;
        }
    }
}
