using UnityEngine;

namespace MapGeneration
{
    public static class MeshGenerator
    {
        public static MeshData GenerateTerrainMesh(float[,] heigthMap, float heigthMultiplier, AnimationCurve curve)
        {
            int width = heigthMap.GetLength(0);
            int heigth = heigthMap.GetLength(1);
            float topLeftX = (width - 1) / -2f;
            float topLeftZ = (heigth - 1) / 2f;

            MeshData meshData = new MeshData(width, heigth);
            int vertexIndex = 0;

            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, curve.Evaluate(heigthMap[x,y]) * heigthMultiplier, topLeftZ - y);
                    meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)heigth);

                    if (x < width -1 && y < heigth -1)
                    {
                        meshData.AddTriangle(vertexIndex,vertexIndex + width + 1,vertexIndex + width);
                        meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                    }
                    vertexIndex++;
                }
            }
            return meshData;
        }
    }
    public class MeshData
    {
        public Vector3[] vertices;
        public int[] triangles;
        public Vector2[] uvs;
        int triangleIndex;
        public MeshData(int meshWidth, int meshHeight)
        {
            vertices = new Vector3[meshWidth * meshHeight];
            uvs = new Vector2[meshWidth * meshHeight];   
            triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
        }
        public void AddTriangle(int a, int b, int c)
        {
            triangles[triangleIndex] = a;
            triangles[triangleIndex + 1] = b;
            triangles[triangleIndex + 2] = c;
            triangleIndex += 3;
        }

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}
