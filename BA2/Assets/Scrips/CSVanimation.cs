using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;


public class CSVanimation : MonoBehaviour
{   public GameObject FootballPlayer;
    public Vector3 posStart;
    public Vector3 posTarget;

    public GameObject LineOS;
    public Vector3 posLOS;
    private float lerpFraction = 0; // 0 und 1 wo zwischen start und traget

    public float speed = 0.85f;//0.25f
    //public float lerpTime = 1f;
    private float currentLerpTime;
    //public float x = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
        ReadCSVFile();
    }


    // Update is called once per frame
    void Update()
    {
        currentLerpTime += (Time.deltaTime * speed);
        if (currentLerpTime < 1 && currentLerpTime >0)
        {
            lerpFraction = currentLerpTime * currentLerpTime * (3f - 2f * currentLerpTime);
        }
            
        //float t = currentLerpTime / lerpTime;
        
        //FootballPlayer.transform.position = Vector3.Lerp(posStart, posTarget, t);
        //lerpFraction += (Time.deltaTime * speed); 


        // Next, we apply the new lerp values to the target transform position.
        //FootballPlayer.transform.position
        //= Vector3.Lerp(posStart, posTarget, lerpFraction);

        //lerpFraction += (Time.deltaTime * x);
        FootballPlayer.transform.position = Vector3.Lerp(posStart, posTarget, lerpFraction);

        LineOS.transform.position = new Vector3(posLOS.x, posLOS.y, posLOS.z);
    }

    void ReadCSVFile()
    {
        //Debug.Log("Start reading CSV File");
        //StreamReader strReader = new StreamReader("C:\\Bachelor Thesis\\BA2\\Assets\\CSV File\\Book2.csv");
        StreamReader strReader = new StreamReader("C:\\Bachelor Thesis\\CSV File\\Book1.csv");
        //StreamReader strReader = new StreamReader("C:\\Bachelor Thesis\\CSV File\\Book1.csv");


        bool endOfFile = false;
        while(!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String == null)
            {
                //Debug.Log("CSV File End");
                endOfFile = true;
                break;
            }

            var data_values = data_String.Split(',');
            if (data_values[2] == "1")
            {
                //Debug.Log("Update Koordinaten");
                posStart.x = float.Parse(data_values[3]);
                posStart.z = float.Parse(data_values[4]);

                posTarget.x = float.Parse(data_values[5]);
                posTarget.z = float.Parse(data_values[6]);

                posLOS.z = float.Parse(data_values[6]);

                //Debug.Log("Player: " + data_values[0].ToString() + " Pos Start : [" + data_values[1].ToString() + " " + data_values[2].ToString() + " " + data_values[3].ToString() + "]");
            }

            }
            
        }
    }