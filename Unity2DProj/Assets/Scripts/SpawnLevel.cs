using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public string level = "level1";
    
    
    public GameObject wall;
    public Transform player1;
    public Transform player2;
    public Transform floor_valid;
    public Transform coin;
    
    //public Transform floor_obstacle;
    //public Transform floor_checkpoint;

    public const string sfloor_valid = "x";
    public const string splayer1 = "1";
    public const string splayer2 = "2";
    public const string scoin = "c";
    //public const string sfloor_checkpoint = "2";
    //public const string sstart = "S";

    void Start()
    {
        int level = PlayerPrefs.GetInt("level") + 1;
        string[][] jagged = readFile("Assets/Resources/level" + level + ".txt");
        Debug.Log("test");
        Debug.Log(jagged[0][0]);
        // create planes based on matrix
        for (int y = 0; y < jagged.Length; y++)
        {
            for (int x = 0; x < jagged[0].Length; x++)
            {
                switch (jagged[y][x])
                {
                    case sfloor_valid:
                        Instantiate(floor_valid, new Vector3(x, -y, 0), Quaternion.identity);
                        break;
                    case splayer1:
                        Instantiate(player1, new Vector3(x, -y, 0), Quaternion.identity);
                        break;
                    case splayer2:
                        Instantiate(player2, new Vector3(x, -y, 0), Quaternion.identity);
                        break;
                    case scoin:
                        Instantiate(coin, new Vector3(x, -y, 0), Quaternion.identity);
                        break;

                }
            }
        }

        transform.position = new Vector3(jagged[0].Length / 2, transform.position.y - jagged.Length / 2, -1 *  jagged[0].Length/2);
    }

    /*
    void spawnLevel()
    {
        string path = "Assets/Resources/level1.txt";

        TextAsset t1 = (TextAsset)Resources.Load("level1", typeof(TextAsset));

        string x = t1.text;
        Debug.Log(x);

        int i, j;
        x = x.Replace("\n", "");
        for(i = 0; i < x.Length; i++)
        {
            if (x[i] == 'x')
            {
                int column, row;
                column = i % 10;
                row = i / 10;
                GameObject t;
                t = (GameObject)(Instantiate(wall, new Vector2(50 - column * 4, 50 - row * 4), Quaternion.identity));
            }
        }
        
    }*/


    string[][] readFile(string file)
    {
        string text = System.IO.File.ReadAllText(file);
        string[] lines = Regex.Split(text, "\r\n");
        int rows = lines.Length;

        string[][] levelBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] stringsOfLine = Regex.Split(lines[i], " ");
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }
}
