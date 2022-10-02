using UnityEngine;

namespace MapGeneration
{
    public static class MeshGenerator
    {
        public static void GenerateTerrainMesh(float[,] heigthMap)
        {
            int width = heigthMap.GetLength(0);
            int heigth = heigthMap.GetLength(1);

            MeshData meshData = new MeshData(width,heigth);
            int vertexIndex = 0;

            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    meshData.vertices[vertexIndex] = new Vector3(x, heigthMap[x,y], y);

                    vertexIndex++;
                }
            }
        }
    }
    public class MeshData
    {
        public Vector3[] vertices;
        public int[] triangles;

        int triangleIndex;
        public MeshData(int meshWidth, int meshHeight)
        {
            vertices = new Vector3[meshWidth * meshHeight];
            triangles = new int[(meshWidth-1)*(meshWidth-1)*6];
        }
        public void AddTriangles(int a, int b, int c)
        {
            triangles[triangleIndex] = a;
            triangles[triangleIndex + 1] = b;
            triangles[triangleIndex + 2] = c;
            triangleIndex += 3;
        }
    }
}
