using UnityEngine;
using UnityEngine.Tilemaps;

namespace MVCMPlatformer
{
    public class GeneratorLevelView : MonoBehaviour
    {
        public Tilemap _tilemap;
        public Tile _tile;
        public int _mapHeight;
        public int _mapWidth;

        [Range(0, 100)] public int _fillPrecent;
        [Range(0, 100)] public int _smoothPrecent;

        public bool _borders;
    }
}