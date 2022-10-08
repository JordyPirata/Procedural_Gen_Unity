using UnityEngine;
using System.Collections.Generic;

namespace MapGeneration
{
    public class EndlessTerrain : MonoBehaviour
    {
        public const float maxViewDst = 300;
        public Transform viewer;

        public static Vector2 viwerPosition;
        int chunkSize;
        int chunkVisibleInViewDst;

        Dictionary<Vector2, TerrainChunk> terrainChunkDiccionary = new Dictionary<Vector2, TerrainChunk>();
        List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

        void Start()
        {
            chunkSize = MapGenerator.mapChunkSize - 1;
            chunkVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);

        }
        private void Update()
        {
            viwerPosition = new Vector2(viewer.position.x,viewer.position.z);
            UpdateVisibleChunks();
        }
        void UpdateVisibleChunks()
        {
            int currentChunkCoordX = Mathf.RoundToInt(viwerPosition.x / chunkSize);
            int currentChunkCoordY = Mathf.RoundToInt(viwerPosition.y / chunkSize);

            for (int yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
            {
                for (int xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
                {
                    Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                    if (terrainChunkDiccionary.ContainsKey(viewedChunkCoord))
                    {
                        terrainChunkDiccionary[viewedChunkCoord].UpdateTerrainChunk();
                    }
                    else
                    {
                        terrainChunkDiccionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize));
                    }
                }
            }
        }
        public class TerrainChunk
        {
            GameObject meshObject;
            Vector2 position;
            Bounds bounds;
            public TerrainChunk(Vector2 coord, int size)
            {
                position = coord * size;
                bounds = new Bounds(position,Vector2.one * size);
                Vector3 positionV3 = new Vector3(position.x,0,position.y);

                meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
                meshObject.transform.position = positionV3;
                meshObject.transform.localScale = Vector3.one * size / 10f;
                SetVisible(false);
            }
            public void UpdateTerrainChunk()
            {
                float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viwerPosition));
                bool visible = viewerDstFromNearestEdge <= maxViewDst;
                SetVisible(visible);
            }
            public void SetVisible(bool visible)
            {
                meshObject.SetActive(visible);
            }
        }
    }
}
