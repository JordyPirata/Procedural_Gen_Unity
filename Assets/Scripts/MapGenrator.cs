namespace MapGeneration
{
    public class MapGenrator
    {
        public int mapDepth;
        public int mapWidth;
        public int scale;
        public void GenerateMap()
        {
            float[,] noiseMap = NoiseMapGeneration.GenerateNoiseMap(mapDepth, mapWidth, scale);

        }
    }
}
