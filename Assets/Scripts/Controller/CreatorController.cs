using UnityEngine.Tilemaps;
using UnityEngine;

namespace MVCMPlatformer
{
    public class GenereatorController
    {
        private Tilemap _tilemap;
        private Tile _tile;
        private int _mapHeight;
        private int _mapWidth;

        private int _fillPrecent;
        private int _smoothPrecent;

        private bool _borders;

        private int[,] _map;

        public GenereatorController(GeneratorLevelView view)
        {
            _tilemap = view._tilemap;
            _tile = view._tile;
            _mapHeight = view._mapHeight;
            _mapWidth = view._mapWidth;

            _fillPrecent = view._fillPrecent;
            _smoothPrecent = view._smoothPrecent;

            _borders = view._borders;

            _map = new int [_mapWidth, _mapHeight];
        }

        public void FillMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (x == 0 || x == _mapWidth - 1 || y==0 || y==_mapHeight-1)
                    {
                        if (_borders)
                        {
                            _map[x,y] = 1;
                        }
                    }
                    else
                    {
                        _map[x, y] = Random.Range(0, 100) < _fillPrecent ? 1 : 0;
                    }
                }
            }
        }

        public void SmoothMap()
        {
            for (int x =0; x<_mapWidth;x++)
            {
                for (int y =0; y<_mapHeight;y++)
                {
                    int neighbour = GetNeighbour(x,y);
                    if (neighbour<4)
                    {
                        _map[x, y] = 1;
                    }
                    else if (neighbour < 4)
                    {
                        _map[x, y] = 0;
                    }
                }
            }
        }
        public int GetNeighbour(int x,int y)
        {
            int neighbour = 0;
            for (int gridX = x-1; x <= x+1; x++)
            {
                for (int gridY = y-1; y <= y+1; y++)
                {
                    if (gridX>=0&&gridX<_mapWidth && gridY>=0&&gridY<_mapHeight)
                    {
                        if(gridX!=x || gridY!=y)
                        {
                            neighbour += _map[gridX, gridY];
                        }
                    }
                    else
                    {
                        neighbour++;
                    }
                }
            }
                    return neighbour;
        }
        public void DrawTiles()
        {
            if (_map == null) return;
            else
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    for (int y = 0; y < _mapHeight; y++)
                    {
                        if (_map[x,y]==1)
                        {
                            Vector3Int tilePosition = new Vector3Int(-_mapWidth/2+x,_mapHeight/2 + y,0);
                            _tilemap.SetTile(tilePosition,_tile);
                        }
                    }
                }
            }
        }
        public void Start()
        {
            FillMap();
            for (int i = 0; i < _smoothPrecent; i++)
            {
                SmoothMap();
            }
            DrawTiles();
        }
    }
}