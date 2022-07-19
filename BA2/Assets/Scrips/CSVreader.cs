using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using DG.Tweening;
using UnityEngine.InputSystem;



public class CSVreader : MonoBehaviour
{
    //CSV filie in Array
    public string[] CSVOffense;
    public string[] CSVDefense;
    
    public GameObject LineOS;
    public float LOS;
    public GameObject Ball;


    public string Spielzug;
    public float pos;

    //Offensive Line + Quarterback haben nur Startpunkt
    public GameObject Quarterback; // XROrgin ME
    public Vector3 posQB;

    public GameObject Center;
    public List<Vector3> posVecListCenter = new List<Vector3>(); // empty now
    public Vector3 posCenter;

    public GameObject LeftTackle;
    public Vector3 posLTackle;

    public GameObject LeftGuard;
    public Vector3 posLGuard;

    public GameObject RightTakle;
    public Vector3 posRTackle;

    public GameObject RightGuard;
    public Vector3 posRGuard;

    // Receivers 
    public GameObject Z;
    public List<Vector3> posVecListZ = new List<Vector3>();
    public Vector3[] posarrayZ;
    public bool continueZ;
    
    public GameObject S;
    public List<Vector3> posVecListS = new List<Vector3>();
    public Vector3[] posarrayS;
    public bool continueS;

    public GameObject H;
    public List<Vector3> posVecListH = new List<Vector3>();
    public Vector3[] posarrayH;
    public bool continueH;

    public GameObject X;
    public List<Vector3> posVecListX = new List<Vector3>();
    public Vector3[] posarrayX;
    public bool continueX;

    //Runningback
    public GameObject BOf;
    public List<Vector3> posVecListB = new List<Vector3>();
    public Vector3[] posarrayB;
    public bool continueB;


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


    IDictionary<string, string> pathTarget = new Dictionary<string, string>();


    public InputActionReference spawnReference = null;
    public GameObject startButton;

    //ContinueMove()
    public float vecZ;
    public float vecX;


    public Vector3[] posarry;
    public List<Vector3> posVecList;

    private bool isAnimation = false;

    // Start is called before the first frame update
    void Start()
    {   
        ReadRandomFileLine("Offense_Lookalike", ref CSVOffense); // eine CSV zeile in Array
        SetUpStatic();
        SetUpDynamic();
        startButton.SetActive(true);
        //AnimationStart();

        pathTarget.Add("X", "FootballPlayer/Offensive/X");
        pathTarget.Add("S", "FootballPlayer/Offensive/S");
        pathTarget.Add("H", "FootballPlayer/Offensive/H");
        pathTarget.Add("Z", "FootballPlayer/Offensive/Z");
        pathTarget.Add("B", "FootballPlayer/Offensive/B");

        ReadRandomFileLine("Defense_Lookalike", ref CSVDefense);
        SetUpDefense();


        //Knöpfe
        spawnReference.action.started += AnimationControllerButton;
        //spawnReference.action.started -= AnimationAgain;

        


    }

    // Update is called once per frame
    void Update()
    {
       
        if (isAnimation == true)
        {
            decken(CL, followTargetCL);
            decken(SS, followTargetSS);
            decken(SD, followTargetSD);
            decken(CR, followTargetCR);
        }
        

    }

    public void SpielzugFertig()
    {

    }
    private void AnimationAgain(InputAction.CallbackContext contex)
    {
        //RunningPlayer();
    }
    private void AnimationControllerButton(InputAction.CallbackContext contex)
    {
        Ball.transform.DOMove(new Vector3(posCenter.x, 0.08f, posCenter.z - 0.65f), 0);
        Ball.transform.DOMove(new Vector3(posCenter.x, 0.08f, posCenter.z - 1f), 1);


        Z.transform.DOPath(posarrayZ, 2, PathType.Linear).SetSpeedBased(true).SetEase(Ease.OutCubic);
        S.transform.DOPath(posarrayS, 4, PathType.Linear).SetSpeedBased(true).SetEase(Ease.OutCubic);
        H.transform.DOPath(posarrayH, 2).SetSpeedBased(true).SetEase(Ease.OutCubic);
        X.transform.DOPath(posarrayX, 2).SetSpeedBased(true).SetEase(Ease.OutCubic);
        BOf.transform.DOPath(posarrayB, 5).SetSpeedBased(true).SetEase(Ease.OutCubic);

        isAnimation = true;

    }

    public void AnimationStart()
    {
        Ball.transform.DOMove(new Vector3(posCenter.x, 0.08f, posCenter.z - 0.65f), 0);
        Ball.transform.DOMove(new Vector3(posCenter.x, 0.08f, posCenter.z - 1f), 1);


        Z.transform.DOPath(posarrayZ, 2, PathType.Linear).SetSpeedBased(true).SetEase(Ease.OutCubic);
        S.transform.DOPath(posarrayS, 4, PathType.Linear).SetSpeedBased(true).SetEase(Ease.OutCubic);
        H.transform.DOPath(posarrayH, 2).SetSpeedBased(true).SetEase(Ease.OutCubic);
        X.transform.DOPath(posarrayX, 2).SetSpeedBased(true).SetEase(Ease.OutCubic);
        BOf.transform.DOPath(posarrayB, 5).SetSpeedBased(true).SetEase(Ease.OutCubic);

        isAnimation = true;

        startButton.SetActive(false);
    }
    public void ContinouMove(Vector3[] posarry, List<Vector3> posVecList)
    { //spielbreite und länge muss noch angepasst werden

        Vector3 vecA = posarry[posarry.Length - 1];                               // letzte Koordinate
        Vector3 vecB = posarry[posarry.Length - 2];                             // vorletzte Koordinta

        double anK = vecA.x - vecB.x;                                              // Ankathete
        double gegK = vecA.z - vecB.z;                                              // Gegenkathete
        double radians = Math.Atan(gegK / anK);                                    // Winkel

        vecZ = 100; //maxi Spielfeldlänge
        double nAnK = (vecZ - vecA.z) / Math.Tan(radians);                           // Neue Ankathete 
        vecX = Convert.ToSingle(nAnK + vecA.x);                                                     // neue Ankathete + letzte x Koordinate couable to float
        if (vecX > 48)
        {
            vecX = 41; //maxi Spielfeld breite
            double nGegK = (vecX - vecA.x) * Math.Tan(radians);                           // Neue Gegenkathete
            vecZ = Convert.ToSingle(nGegK + vecA.z);                                                     // neue Gegenkathete + letzte z Koordinate
        }
        posVecList.Add(new Vector3(vecX, 1f, vecZ));
        //posarry = posVecList.ToArray();
        //!!!! zuweisung funktioniert nicht ???? kein Plan :((
        //posarrayS = posVecList.ToArray();
    }

    public void GetDynamicPosition(int b, ref List<Vector3> posList, ref bool isContinue)
    {
        bool IsFloat;

        do
        {
            b++;
            float Valuex = float.Parse(CSVOffense[b]);
            b++;
            float Valuey = float.Parse(CSVOffense[b]);
            posList.Add(new Vector3(Valuex, 1f, Valuey));

            if (b >= CSVOffense.Length - 1)
                break;
            IsFloat = float.TryParse(CSVOffense[b + 1], out pos);
            if (IsFloat == false)
            {
                if (CSVOffense[b + 1] == "break")
                {
                    b++;
                    IsFloat = true;
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
        } while (IsFloat == true);
    }
    public void SetUpDynamic()
    {
        //aus csvinfo array --> spielerbezogene vector3 array 
        for (int b = 3; b < CSVOffense.Length; b++)
        {
            string name = CSVOffense[b];

            switch (name)
            {
                case "Z":
                    GetDynamicPosition(b, ref posVecListZ, ref continueZ);

                    break;
                case "S":
                    GetDynamicPosition(b, ref posVecListS, ref continueS);

                    break;
                case "H":
                    GetDynamicPosition(b, ref posVecListH, ref continueH);

                    break;
                case "X":
                    GetDynamicPosition(b, ref posVecListX, ref continueX);

                    break;
                case "B":
                    GetDynamicPosition(b, ref posVecListB, ref continueB);
                    break;
                default:
                    break;
            }

        }
        posarrayZ = posVecListZ.ToArray();
        posarrayS = posVecListS.ToArray();
        posarrayH = posVecListH.ToArray();
        posarrayX = posVecListX.ToArray();
        posarrayB = posVecListB.ToArray();

        if (continueZ == true)
        {
            ContinouMove(posarrayZ, posVecListZ);
        }
        if (continueS == true)
        {
            ContinouMove(posarrayS, posVecListS);
        }
        if (continueX == true)
        {
            ContinouMove(posarrayX, posVecListX);
        }
        if (continueH == true)
        {
            ContinouMove(posarrayH, posVecListH);
        }
        if (continueB == true)
        {
            ContinouMove(posarrayB, posVecListB);
        }
        //nochmal aktualisieren, da in ContinoMove es irgenwie nicht funktioniert
        posarrayZ = posVecListZ.ToArray();
        posarrayS = posVecListS.ToArray();
        posarrayH = posVecListH.ToArray();
        posarrayX = posVecListX.ToArray();
        posarrayB = posVecListB.ToArray();


        //animation.SetSpeedBaseed(bool isSpeedBased = true);
        //Z.transform.DOPath(posarray, 2, PathType.Linear).SetSpeedBased(true);


        BOf.transform.DOMove(posVecListB[0], 0);
        S.transform.DOMove(posVecListS[0], 0);
        Z.transform.DOMove(posVecListZ[0], 0);
        H.transform.DOMove(posVecListH[0], 0);
        X.transform.DOMove(posVecListX[0], 0);
    }




    //statische Spielelemente (O-line, Line of Scrimmage, Quarterback)
    public void SetUpStatic() //csv in yards!!  m / 1.094f = yards
    {
        //Spielzug name
        Spielzug = CSVOffense[0];

        //Line Of Scrimmage 
        LOS = float.Parse(CSVOffense[1]) / 1.094f;
        LineOS.transform.position = new Vector3(0.05f, 0.02f, LOS-0.03f);

        //O-Line + Quarterback
        for (int i = 1; i < CSVOffense.Length; i++)
        {
            string PlayerName = CSVOffense[i];

            switch (PlayerName)
            {
                case "Center":
                    i++;
                    posCenter.x = float.Parse(CSVOffense[i]) / 1.094f;
                    i++;
                    posCenter.z = float.Parse(CSVOffense[i]) / 1.094f;
                    break;
                case "Left Tackle":
                    i++;
                    posLTackle.x = float.Parse(CSVOffense[i]) / 1.094f;
                    i++;
                    posLTackle.z = float.Parse(CSVOffense[i]) / 1.094f;
                    break;
                case "Right Tackle":
                    i++;
                    posRTackle.x = float.Parse(CSVOffense[i]) / 1.094f;
                    i++;
                    posRTackle.z = float.Parse(CSVOffense[i]) / 1.094f;
                    break;
                case "Left Guard":
                    i++;
                    posLGuard.x = float.Parse(CSVOffense[i]) / 1.094f;
                    i++;
                    posLGuard.z = float.Parse(CSVOffense[i]) / 1.094f;
                    break;
                case "Right Guard":
                    i++;
                    posRGuard.x = float.Parse(CSVOffense[i]) / 1.094f;
                    i++;
                    posRGuard.z = float.Parse(CSVOffense[i]) / 1.094f;
                    break;
                case "Q":
                    i++;
                    posQB.x = float.Parse(CSVOffense[i]) / 1.094f;
                    i++;
                    posQB.z = float.Parse(CSVOffense[i]) / 1.094f;
                    break;
                default:
                    break;
            }
        }
        
        Center.transform.position = posCenter;

        LeftTackle.transform.position = new Vector3(posLTackle.x, 0, posLTackle.z);
        LeftGuard.transform.position = new Vector3(posLGuard.x, 0, posLGuard.z);
        RightTakle.transform.position = new Vector3(posRTackle.x, 0, posRTackle.z);
        RightGuard.transform.position = new Vector3(posRGuard.x, 0, posRGuard.z);
        Quarterback.transform.position = new Vector3(posQB.x, 0, posQB.z);


        Ball.transform.DOMove(new Vector3(posCenter.x, 0.08f, LOS+0.05f), 0);
        
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
        for (int i = 2; i < CSVDefense.Length; i++)
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
    // Liest 1 zufällige CSV Datei Zeile in CSVInfo Array
    // TextAsset dialogueAsset = Resources.Load<TextAsset>("Dialogue/Dialogue#");
    //string dialogue = dialogueAsset.text;
    public void ReadRandomFileLine()
    {
        TextAsset CSVText = Resources.Load<TextAsset>("Offense_Lookalike");
        string[] csvdata = CSVText.text.Split(new char[] { '\n' });
        //Debug.Log(csvdata.Length); // ist immer eine zeile länger 
        int xcount = UnityEngine.Random.Range(1, csvdata.Length - 1); // Range(0,3) --> mögliche Zufallszahlen: 0,1,2 von 1 da die erste Zeile erklärungen enthält
        CSVOffense = csvdata[xcount].Split(new char[] { ';', ',' });

    }













    // BRAUCHE ICH NICHT MEHR !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    /*
    //public string[] LOLLO;
    public void test()
    {
        TextAsset CSVText = Resources.Load<TextAsset>("CSV_example");
        LOLLO = CSVText.text.Split(new char[] { '\n' });
    }

   



    //liest das gesamte File Achtung!! csvarray ist hard gecodet !!
    //stream reader funktioniert auf android nicht 
    public void ReadFile()
    {
        StreamReader strReader = new StreamReader("C:/Bachelor Thesis/BA2/Assets/CSV File/CSV_lookalike.csv");

        bool endOfFile = false;
            
        while (!endOfFile)
        {

            string data_Line = strReader.ReadLine();
            if (data_Line == null)
            {
                Debug.Log("CSV File End");
                endOfFile = true;
                //break;
            }
            string [] line = data_Line.Split(';');
            
            int collum = line.Length;
            string[,] csvarray = new string[2, 200];

            int row = 0;

            for (int i=0; i<collum; i++)
            {
                csvarray[row, i] = line[i];
            }
            Debug.Log(csvarray[row, 0]);
            row++;
           
            Array.Clear(line, 0, line.Length);

        }
    }
    public void StartPosition(int a, ref List<string> posList)
    {
        bool IsFloat;
        do
        {
            a++;
            posList.Add(CSVInfo[a]);
            if (a >= CSVInfo.Length - 1)
                break;
            IsFloat = float.TryParse(CSVInfo[a + 1], out pos);
        } while (IsFloat == true);
    }

    public void StaticPosition(int a, ref List<Vector3> posList)
    {
        bool IsFloat;

        do
        {
            a++;
            float Valuex = float.Parse(CSVInfo[a]) / 1.094f;
            a++;
            float Valuey = float.Parse(CSVInfo[a]) / 1.094f;
            posList.Add(new Vector3(Valuex, 1f, Valuey));

            if (a >= CSVInfo.Length - 1)
                break;
            IsFloat = float.TryParse(CSVInfo[a + 1], out pos);

        } while (IsFloat == true);
    }
    // Startpositionen receivers z,s,h,x und runningback b
    public void RunningPlayer()
    {
        Ball.transform.position = new Vector3(posCenter.x, 0, posCenter.z - 0.65f);

        for (int a = 2; a < CSVInfo.Length; a++)
        {
            string name = CSVInfo[a];


            switch (name)
            {
                case "Center":
                    //StartPosition(a, ref posListCenter);
                    StaticPosition(a, ref posVecListCenter);
                    break;
                case "Z":
                    StartPosition(a, ref posListZ);

                    break;
                case "S":
                    StartPosition(a, ref posListS);
                    break;
                case "H":
                    StartPosition(a, ref posListH);

                    break;
                case "X":
                    StartPosition(a, ref posListX);

                    break;
                case "B":
                    StartPosition(a, ref posListB);
                    break;
                default:
                    break;
            }

        }
        posStartZ.x = float.Parse(posListZ[0]) / 1.094f;
        posStartZ.z = float.Parse(posListZ[1]) / 1.094f;
        posStartS.x = float.Parse(posListS[0]) / 1.094f;
        posStartS.z = float.Parse(posListS[1]) / 1.094f;
        posStartH.x = float.Parse(posListH[0]) / 1.094f;
        posStartH.z = float.Parse(posListH[1]) / 1.094f;
        posStartX.x = float.Parse(posListX[0]) / 1.094f;
        posStartX.z = float.Parse(posListX[1]) / 1.094f;
        posStartB.x = float.Parse(posListB[0]) / 1.094f;
        posStartB.z = float.Parse(posListB[1]) / 1.094f;
        posStartB.y = 1;
        posStartX.y = 1;
        posStartH.y = 1;
        posStartS.y = 1;
        posStartZ.y = 1;

        Z.transform.position = new Vector3(posStartZ.x, posStartZ.y, posStartZ.z);
        S.transform.position = new Vector3(posStartS.x, posStartS.y, posStartS.z);
        H.transform.position = new Vector3(posStartH.x, posStartH.y, posStartH.z);
        X.transform.position = new Vector3(posStartX.x, posStartX.y, posStartX.z);
        B.transform.position = new Vector3(posStartB.x, posStartB.y, posStartB.z);
        //Center.transform.DOMove(new Vector3(posCenter.x, 0, posCenter.z - 1f), 1);
        Center.transform.DOMove(posVecListCenter[0], 0); // ,0 snapt direkt zur position ,1 bewegt sich zur position
    }
    */




}
