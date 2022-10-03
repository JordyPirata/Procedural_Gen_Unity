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

        void Start()
        {
            chunkSize = MapGenerator.mapChunkSize - 1;
            chunkVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);

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
                }
            }
        }
        public class TerrainChunk
        {


        }
    }
}
