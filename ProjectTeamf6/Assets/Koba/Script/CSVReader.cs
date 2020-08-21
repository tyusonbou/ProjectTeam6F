using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class CSVReader : MonoBehaviour
{
    TextAsset csvFile; //CSVファイル
    List<string[]> csvDatas = new List<string[]>();//中身を入れるリスト
    //public static List<int[]> csvIntDatas = new List<int[]>();
    public static int[,] csvIntDatas = new int[4,4];
    int h, w;
    //Encoding enco;

    // Start is called before the first frame update
    void Start()
    {
        
        //enco = Encoding.GetEncoding("utf-8");
        csvFile = Resources.Load("zombie_ParaCSV") as TextAsset;
        if(csvFile == null)
        {
            Debug.Log("ないよ");
        }
        //Debug.Log(csvFile);
        StringReader reader = new StringReader(csvFile.text);


        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }


        /*
        for(int i = 0; i <= 3; i++)
        {
            for (int j = 0; j <= 3; j++)
            {
                Debug.Log(csvDatas[i][j]);
            }
        }
        */


        //csvIntDatas.Add(int.Parse(csvDatas[]));
        //Debug.Log( int.Parse(csvDatas[0][0]));
        //csvIntDatas[0, 0] = int.Parse(csvDatas[0][0]);
        //Debug.Log(csvIntDatas[0, 0]);
        
        for(int i = 0; i <= 3; i++)
        {
            for(int j = 0; j <= 3; j++)
            {
                csvIntDatas[i,j] = int.Parse(csvDatas[i][j]);
                //Debug.Log(csvIntDatas[i, j]);
            }
        }
        

        /*
        Debug.Log(csvDatas[0].Length);
        Debug.Log(csvDatas[0][1]);
        Debug.Log(csvDatas[1][2]);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
