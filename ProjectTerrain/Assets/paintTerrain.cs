using System.Collections; // импортируем наши библиотечки
using UnityEngine;

public class paintTerrain : MonoBehaviour // название нашего файла
{
    [System.Serializable]
    public class SplatHeights
    {
        public int textureIndex; // здесь мы храним индекс текстуры
        public int startingHeight; // здесь мы определяем высоту
    }

    public SplatHeights[] splatHeights; // хранит альфа значения необходимые для количества отображения текстуры в данный момент 

    void Start() // функция котороя создает наш шаблон
    {
        TerrainData terrainData = Terrain.activeTerrain.terrainData;
        float[, ,] splatmapData = new float[terrainData.alphamapWidth,
                                                    terrainData.alphamapHeight,
                                                    terrainData.alphamapLayers];
        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {

                float terrainHeight = terrainData.GetHeight(y,x);
                
                float[] splat = new float[splatHeights.Length];

                for(int i = 0; i < splatHeights.Length; i++)
                {
                    if(i == splatHeights.Length-1 && terrainHeight >= splatHeights[i].startingHeight)
                        splat[i] = 1;
                     else if(terrainHeight >= splatHeights[i].startingHeight && terrainHeight <= splatHeights[i+1].startingHeight)
                        splat[i] = 1;
                }

                for(int j = 0; j < splatHeights.Length; j++)
                {
                    splatmapData[x, y, j] = splat[j];
                }
            }
        }  

        terrainData.SetAlphamaps(0, 0, splatmapData);
    }

}
// ВНИМАНИЕ!: ПОЛНАЯ ИНСТРУКЦИЯ И ОБЪЯСНЕНИЕ ДАННОГО СКРИПТА НАХОДИТЬСЯ ПО ЭТОЙ ССЫЛКИ:
// https://www.youtube.com/watch?v=aUcWm1k0xDc