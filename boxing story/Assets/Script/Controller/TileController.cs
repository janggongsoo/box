using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {
    Tile[,] tiles;

    public Sprite tileImage;
    // Use this for initialization
    void Start () {
        tiles = new Tile[Constants.Height, Constants.Width];
        

        for (int i = 0; i < Constants.Height; i++)
        {
            for (int j = 0; j < Constants.Width; j++)
            {
                // i = y , j = x

                GameObject tileObj = new GameObject("Tile_" + i + "_" + j);
                tileObj.transform.position = new Vector3((-1*i) +j, (i*0.5f) + (j*0.5f), (i * 0.5f) + (j * 0.5f));
                tileObj.transform.SetParent(transform);

                tileObj.AddComponent<SpriteRenderer>().sprite = tileImage;
                tiles[i, j] = new Tile(i, j, tileObj);
            }
        }
        
    }
	
    public Tile getTile(int _x, int _y)
    {
        if(!isRangeOut(_x,_y))
        {
            return tiles[_y, _x];
        }
        return new Tile();
    }

    public void SetTileBuff(int range, int posX, int posY, BuffType type, int value)
    {
        for(int i = -range; i <= range; i++)
        {
            for(int j = -range; j <= range; j++)
            {
                if (i == 0 && j == 0) continue;

                int _x = posX + j;
                int _y = posY + i;
                if (isRangeOut(_y, _x)) continue;

                tiles[_y, _x].AddBuffList(type, value);
            }
        }

    }
    private bool isRangeOut(int x,int y)
    {
        return (x < 0) || (y < 0) || (x >= Constants.Width) || (y >= Constants.Height);
    }

	
}
