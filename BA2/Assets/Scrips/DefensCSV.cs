using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime;

public class DefensCSV : MonoBehaviour
{
    public string[] CSVDefense;
    public float pos;

    //Defensive Line
    public GameObject TL; 
    public Vector3 posTL;
    public string followTargetTL;

    public GameObject N;
    public Vector3 posN;
    public string followTargetN;

    public GameObject TR;
    public Vector3 posTR;
    public string followTargetTR;

    public GameObject CL;
    public Vector3 posCL;
    public string followTargetCL;

    public GameObject SS;
    public Vector3 posSS;
    public string followTargetSS;

    public GameObject SD;
    public Vector3 posSD;
    public string followTargetSD;

    public GameObject B;
    public Vector3 posB;
    public string followTargetB;

    public GameObject M;
    public Vector3 posM;
    public string followTargetM;

    public GameObject W;
    public Vector3 posW;
    public string followTargetW;

    public GameObject FS;
    public Vector3 posFS;
    public string followTargetFS;

    public GameObject CR;
    public Vector3 posCR;
    public string followTargetCR;
    

    public GameObject getS;
    public GameObject getZ;
    public GameObject getH;
    public GameObject getX;
    public GameObject getCR; 
    
    
    IDictionary<string, string> pathTarget = new Dictionary<string, string>();
    // Start is called before the first frame update
    void Start()
    {
        pathTarget.Add("X", "FootballPlayer/Offensive/X");
        pathTarget.Add("S", "FootballPlayer/Offensive/S");
        pathTarget.Add("H", "FootballPlayer/Offensive/H");
        pathTarget.Add("Z", "FootballPlayer/Offensive/Z");
        pathTarget.Add("B", "FootballPlayer/Offensive/B");

       ReadRandomFileLine("Defense_Lookalike", ref CSVDefense);
       SetUpDefense();
       
    }

    // Update is called once per frame
    void Update()
    {
        
        decken(CL, followTargetCL);
        decken(SS, followTargetSS);
        decken(SD, followTargetSD);
        decken(CR, followTargetCR);
       
    }
    
    public void decken(GameObject follower, string followTarget)
    {
        string fTstr = followTarget.Substring(0, 1); // da bei dem string followTarget mehr eingelesen wurden z.b. leerzeichen
        GameObject target = GameObject.Find(pathTarget[fTstr]);
        
        float speed = 4f;
        var distance = Vector3.Distance(follower.transform.position, target.transform.position);
        if (distance < 1) //damit die spieler nicht durch einander laufen
            speed = -4;
        if (distance < 2 && distance > 1) //abstandhalter
            speed = 0;
        if (distance > 1.5) 
            speed = 4;
        follower.transform.position = Vector3.MoveTowards(follower.transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void GetDefensePosition(int i, ref Vector3 pos, ref string followTarget)
    {
        bool success;
        bool succes2;
        float num;

        i++;
        pos.x = float.Parse(CSVDefense[i]) / 1.094f;
        i++;
        pos.z = float.Parse(CSVDefense[i]) / 1.094f;
        
        if (i < CSVDefense.Length - 1)
        {
            success = float.TryParse(CSVDefense[i + 1], out num);
            if (i < CSVDefense.Length - 2)
            {
                succes2 = float.TryParse(CSVDefense[i + 2], out num);
            }
            else succes2 = false;
            if (success == false && succes2 == false)
            {
                i++;
                followTarget = CSVDefense[i];
            }
        }
        
    }

    public void SetUpDefense() //csv in yards!!  m / 1.094f = yards
    {


        //O-Line + Quarterback
        for (int i = 2; i< CSVDefense.Length; i++)
        {
            string PlayerName = CSVDefense[i];
            
            switch (PlayerName)
            {
                case "TL":
                    GetDefensePosition(i, ref posTL, ref followTargetTL);

                    break; 
                case "N":
                    GetDefensePosition(i, ref posN, ref followTargetN);
                    
                    break;
                case "TR":
                    GetDefensePosition(i, ref posTR, ref followTargetTR);
                    
                    break;
                case "CL":
                    GetDefensePosition(i, ref posCL, ref followTargetCL);
                    
                    break;
                case "SS":
                    GetDefensePosition(i, ref posSS, ref followTargetSS);
                    
                    break;
                case "SD":
                    GetDefensePosition(i, ref posSD, ref followTargetSD);
                    
                    break;
                case "B":
                    GetDefensePosition(i, ref posB, ref followTargetB);
                    
                    break;
                case "M":
                    GetDefensePosition(i, ref posM, ref followTargetM); 
                    
                    break;
                case "W":
                    GetDefensePosition(i, ref posW, ref followTargetW); 
                    
                    break;
                case "FS":
                    GetDefensePosition(i, ref posFS, ref followTargetFS);

                    break;
                case "CR":
                    GetDefensePosition(i, ref posCR, ref followTargetCR);
                    //decken(CR, followTargetCR);

                    break;
                default:
                    break;
            }      
        }
        

        TL.transform.position = new Vector3(posTL.x, 1, posTL.z);
        N.transform.position = new Vector3(posN.x, 1, posN.z);
        TR.transform.position = new Vector3(posTR.x, 1, posTR.z);
        CL.transform.position = new Vector3(posCL.x, 1, posCL.z);
        SD.transform.position = new Vector3(posSD.x, 1, posSD.z);
        SS.transform.position = new Vector3(posSS.x, 1, posSS.z);
        B.transform.position = new Vector3(posB.x, 1, posB.z);
        M.transform.position = new Vector3(posM.x, 1, posM.z);
        W.transform.position = new Vector3(posW.x, 1, posW.z);
        FS.transform.position = new Vector3(posFS.x, 1, posFS.z);
        CR.transform.position = new Vector3(posCR.x, 1, posCR.z);

        /*/
        GameObject Ball = Instantiate(Footballplayer); // Gameobject wird erstellt


        */


    }

    // Liest 1 zufällige CSV Datei Zeile in CSVInfo Array
    // TextAsset dialogueAsset = Resources.Load<TextAsset>("Dialogue/Dialogue#");
    //string dialogue = dialogueAsset.text;
    /*
    public void ReadRandomFileline()
    {
        TextAsset CSVText = Resources.Load<TextAsset>("Defense_Lookalike");
        string[] csvdata = CSVText.text.Split(new char[] { '\n' });
        //Debug.Log(csvdata.Length);
        //int xcount = Random.Range(1, 6);
        for (int i = 1; i < csvdata.Length - 1; i++)
        {
            CSVDefense = csvdata[i].Split(new char[] { ';', ',' });
        }

    }
    */
    public void ReadRandomFileLine(string filename, ref string[] CSVDatei)
    {
        //string filename = "Defense_Lookalike";
        TextAsset CSVText = Resources.Load<TextAsset>(filename);
        string[] csvdata = CSVText.text.Split(new char[] { '\n' });
        //Debug.Log(csvdata.Length); // ist immer eine zeile länger 
        int xcount = UnityEngine.Random.Range(1, csvdata.Length - 1); // Range(0,3) --> mögliche Zufallszahlen: 0,1,2 von 1 da die erste Zeile erklärungen enthält
        CSVDatei = csvdata[xcount].Split(new char[] { ';', ',' });

    }
}
