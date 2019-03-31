using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameCtr : MonoBehaviour
{
    public float _movespeed;
    public static GameCtr gamectr;
    private void Awake()
    {
        gamectr = this;
        map = new int[Length, Length];
        grid = new GameObject[Length, Length];
    }
    public Transform root;
    public int Length;
    public float offset;
    public float start;
    public int[,] map;
    public GameObject[,] grid;
    public List<int> number = new List<int>();
    public void CreateMap()
    {
        for (int i = 0; i < Length*Length-1; i++)
        {
            number.Add(i/4+1);
        }
        for (int i = 0; i < root.childCount; i++)
        {
            map[i/ Length, i% Length] = 1;
            grid[i / Length, i % Length] = root.GetChild(i).gameObject;
       
            int index = Random.Range(0, number.Count);
         
            root.GetChild(i).GetComponent<Number>().type =  number[index];
            root.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Number_"+number[index]);
            number.Remove(number[index]);
        }

    }
    public void ResetGame()
    {
        number.Clear();
        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                map[i, j] = 0;
            }
        }
        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                if (grid[i, j] != null)
                    grid[i, j].GetComponent<Number>().ResetNumber();
             
                grid[i, j] = null;
            }
        }
    }

    private void Start()
    {
        
    }
    public int[] FindPos(int x,int y)
    {
        if (x < 0 || x > 3)
            return null;
        if (y < 0 || y > 3)
            return null;

        if (map[x, y] != 1)
            return new int[] {  x, y };

        return null;

    }
   
    public  void FindEmpty(int x,int y)
    {
        
        int[] movepos = null;
        if(FindPos(x + 1, y)!=null)
        {
            movepos = FindPos(x + 1, y);

        }
        if (FindPos(x - 1, y) != null)
        {
            movepos = FindPos(x - 1, y);

        }
        if (FindPos(x, y + 1) != null)
        {
            movepos = FindPos(x , y+1);

        }
        if (FindPos(x, y - 1) != null)
        {
            movepos = FindPos(x , y-1);

        }

        if (movepos == null)
            return;
        int movex = movepos[0];
        int movey = movepos[1];

      
    
        grid[x, y].GetComponent<RectTransform>().DOAnchorPos(new Vector3(start + movey * offset, -(start + movex * offset), 0), _movespeed);
        map[x, y] = 0;
        grid[x, y].GetComponent<Number>().x = movex;
        grid[x, y].GetComponent<Number>().y = movey;
        map[movex, movey] = 1;
        grid[movex, movey] = grid[x, y];
        grid[x, y] = null;

        if(CheckWin())
        {
            UIManager._instance.CloseView<NumberGameView>();
        }
    }
    public bool CheckWin()
    {
     
        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                if(grid[i,j]==null)
                {
                    if (i == Length-1 && j == Length-1)
                        return true;
                    else
                        return false;
                }
               
                if ( grid[i,j].GetComponent<Number>().type!= i+1)
                {
                    return false;
                }
                Debug.Log(grid[i, j].GetComponent<Number>().type);

            }
        }
        return true;
    }
    
}
