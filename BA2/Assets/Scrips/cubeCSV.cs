using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

using DG.Tweening;

public class cubeCSV : MonoBehaviour
{
    public GameObject Cube;
    public Vector3[] posarrayC = new[] { new Vector3(8f, 1f, 19f), new Vector3(5f, 1f, 27f), new Vector3(6f, 1f, 25f) };
    public string[] CSVInfo;
    
    public string csv;


    float pos;
    // Start is called before the first frame update
    void Start()
    {
        Animations();
        ReadRandomFileline();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.DOMove(new Vector3(8, 0, 18), 5);
    }

    public void Animations()
    {
        Cube.transform.DOPath(posarrayC,5);

    }
    
    public void ReadRandomFileline()
    {
        
        TextAsset CSVText = Resources.Load<TextAsset>("CSV_example");
        string[] csvdata = CSVText.text.Split(new char[] { '\n' });
        Debug.Log(csvdata.Length);
        //int xcount = Random.Range(1, 6);
        for (int i=1; i< csvdata.Length-1;i++)
        {
            CSVInfo = csvdata[i].Split(new char[] { ';', ',' });
        }

    }
    
}
