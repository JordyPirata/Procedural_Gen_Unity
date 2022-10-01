using UnityEngine;

namespace MapGeneration
{
    public static class MeshGenerator
    {
        public static void GenerateTerrainMesh(float[,] heigthMap)
        {
            int width = heigthMap.GetLength(0);
            int heigth = heigthMap.GetLength(1);

            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < width; x++)
                {

                }
            }
        }
    }
    public class MeshData
    {
        public Vector3[] vertices;
        public int[] triangles;

        public MeshData(int width, int heigth)
        {

        }
    }
}
