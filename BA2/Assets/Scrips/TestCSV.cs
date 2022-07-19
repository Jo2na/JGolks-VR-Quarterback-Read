using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;


public class TestCSV : MonoBehaviour
{
    private string[] CSVOffense;
    private List<OffensePlayer> offensePlayerList = new List<OffensePlayer>();
    
    private string[] CSVDefense;
    public List<DefensePlayer> defensePlayerList = new List<DefensePlayer>();
    
    public Vector3 linePos;
    public Vector3 ballPos;
    
    void Start()
    {   
        ReadRandomFileLine("Offense_Lookalike", ref CSVOffense);
        GetOffensePosition();

        ReadRandomFileLine("Defense_Lookalike", ref CSVDefense);
        GetDefensePosition();

        GetFieldPosition();

    }

    void Update()
    {
    }
    
    public List<OffensePlayer> OffensePlayerList
    {
        get { return offensePlayerList; }
        set { offensePlayerList = value; }
    }

    public List<DefensePlayer> DefensePlayerList
    {
        get { return defensePlayerList; }
        set { defensePlayerList = value; }
    }
    // Vectoren von Line of Scrimmage und dem Ball
    public void GetFieldPosition()
    {
        linePos.z = (float.Parse(CSVOffense[1]) / 1.094f);
        linePos = new Vector3(0.05f, 0.02f, linePos.z-0.12f);

        for (int b = 0; b<CSVOffense.Length-1; b++)
        {
            if (CSVOffense[b] == "Center")
            {
                ballPos = new Vector3(float.Parse(CSVOffense[b + 1]) / 1.094f, 0.08f, (float.Parse(CSVOffense[b + 2]) - 0.65f) / 1.094f);
            }
        }
        
    }
    //Zufällig ausgewählte CSV File Zeile wird in ein Array eingelesen
    public void ReadRandomFileLine(string filename, ref string[] CSVDatei)
    {
        TextAsset CSVText = Resources.Load<TextAsset>(filename);
        string[] csvdata = CSVText.text.Split(new char[] { '\n' });
        //Debug.Log(csvdata.Length); // ist immer eine zeile länger 
        int xcount = UnityEngine.Random.Range(1, csvdata.Length - 1); // Range(0,3) --> mögliche Zufallszahlen: 0,1,2 von 1 da die erste Zeile erklärungen enthält
        CSVDatei = csvdata[xcount].Split(new char[] { ';', ',' });

    }
    public void GetOffensePosition()
    {
        string playerName;
        
        List<Vector3> posList = new List<Vector3>();
        Vector3[] posArray;
        bool isContinue = false;
        bool isFloat;

        for (int b = 2; b < CSVOffense.Length; b++)
        {
            playerName = CSVOffense[b];
            do
            {   
                Vector3 position = new Vector3(0,1f,0);
                if(playerName == "Center" || playerName == "Left Guard" || playerName == "Right Guard" || playerName == "Left Tackle" || playerName == "Right Tackle")
                {
                    position = new Vector3(0, 0, 0);
                }

                b++;
                position.x = float.Parse(CSVOffense[b]) / 1.094f;
                b++;
                position.z = float.Parse(CSVOffense[b]) / 1.094f;
                posList.Add(position);

                if (b >= CSVOffense.Length - 1)
                    break;
                isFloat = float.TryParse(CSVOffense[b + 1], out float pos);
                if (isFloat == false)
                {
                    if (CSVOffense[b + 1] == "break")
                    {
                        b++;
                        isFloat = true;
                    }
                    if (CSVOffense[b + 1] == "end")
                    {
                        b++;
                    }
                    if (CSVOffense[b + 1] == "continue")
                    {
                        b++;
                        isContinue = true;

                    }
                }
            } while (isFloat == true);

            posArray = posList.ToArray();
            if (isContinue == true)
                ContinouMove(ref posArray, ref posList);


            offensePlayerList.Add(new OffensePlayer(playerName, posArray, isContinue));
            posList.Clear();
            isContinue = false;
        }

    }

    //Soll der Spieler seinen Laufweg weiterführen, wird hier die feiterführende Koordinate am Spielfeldran berechnet
    //und zum Positions Array hinzugefühgt
    public void ContinouMove(ref Vector3[] posarry, ref List<Vector3> posVecList)
    { //spielbreite und länge muss noch angepasst werden

        Vector3 vecA = posarry[posarry.Length - 1];                               // letzte Koordinate
        Vector3 vecB = posarry[posarry.Length - 2];                             // vorletzte Koordinta

        double anK = vecA.x - vecB.x;                                              // Ankathete
        double gegK = vecA.z - vecB.z;                                              // Gegenkathete
        double radians = Math.Atan(gegK / anK);                                    // Winkel

        float vecZ = 100; //maxi Spielfeldlänge
        double nAnK = (vecZ - vecA.z) / Math.Tan(radians);                           // Neue Ankathete 
        float vecX = Convert.ToSingle(nAnK + vecA.x);                                                     // neue Ankathete + letzte x Koordinate couable to float
        if (vecX > 48)
        {
            vecX = 47; //maxi Spielfeld breite
            double nGegK = (vecX - vecA.x) * Math.Tan(radians);                           // Neue Gegenkathete
            vecZ = Convert.ToSingle(nGegK + vecA.z);                                                     // neue Gegenkathete + letzte z Koordinate
        }

        posVecList.Add(new Vector3(vecX, 1f, vecZ));
        posarry = posVecList.ToArray();
    }


    //Name, Position und zudeckene Gegenspieler(string + bool) wird in einer Liste der Klasse DefensePlayer gespeichert
    public void GetDefensePosition()
    {
        string playerName;
        string followTarget = "";
        bool success, succes2, hasTarget = false;
        
        for (int i = 2; i < CSVDefense.Length; i++)
        {
            playerName = CSVDefense[i];

            Vector3 position = new Vector3(0, 1f, 0);
            if (playerName == "TL" || playerName == "N" || playerName == "TR" || playerName == "B")
            {
                position = new Vector3(0, 0, 0);
            }

            i++;
            position.x = float.Parse(CSVDefense[i]) / 1.094f;
            i++;
            position.z = float.Parse(CSVDefense[i]) / 1.094f;

            if (i < CSVDefense.Length - 1)
            {
                success = float.TryParse(CSVDefense[i + 1], out float num);
                if (i < CSVDefense.Length - 2)
                {
                    succes2 = float.TryParse(CSVDefense[i + 2], out num);
                }
                else succes2 = false;
                if (success == false && succes2 == false)
                {
                    i++;
                    followTarget = CSVDefense[i];
                    hasTarget = true;
                }
            }


            defensePlayerList.Add(new DefensePlayer(playerName, position, followTarget, hasTarget));
            success = false;
            succes2 = false;
            hasTarget = false;

        }
    }
   
}