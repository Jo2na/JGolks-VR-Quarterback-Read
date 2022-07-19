using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManger : MonoBehaviour
{
    public TestCSV testCSV;

    public GameObject offenseTeam;
    public GameObject offensePrefab;    
    public GameObject defenseTeam;
    public GameObject defensePrefab;

    public GameObject XROrgin;
    public GameObject lineOS;
    public GameObject Ball;
    public GameObject startButton;
    public GameObject nextButton;

    public bool animationStart;

    public bool ballThrown;
    public bool ballOnGround = false;
    public bool ballCatch = false;
    public string catcher;

    private float maxSpeedOfense = 6.22f;
    private float maxSpeedDefense = 6.16f;

    public float speedProzent = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        SetUpField();
        SetOffense(offensePrefab, offenseTeam);
        SetDefense(defensePrefab, defenseTeam);

        startButton.SetActive(true);
        nextButton.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if (animationStart == true)
        {
            foreach (DefensePlayer player in testCSV.DefensePlayerList)
            {
                if(player.HasTarget == true)
                    decken(player.PlayerName, player.FollowTarget);
            }
        }

        /*if (ballOnGround==true && ballThrown == true)//
        {
            nextButton.SetActive(true);
        }
        */
        if (ballCatch == true && ballThrown == true)//
        {
            nextButton.SetActive(true);
        }
    }


    public void BallThrown() 
    {
        ballThrown = true;
        
    }

    public void Catch()
    {
        if (ballCatch == true)
        {
            GameObject go = GameObject.Find("OffenseTeam/" + catcher);
            Ball.transform.DOMove(go.transform.position, 0);
            // Ball.transform.position = Vector3.MoveTowards(Ball.transform.position, go.transform.position, speed * Time.deltaTime);
            Debug.Log(catcher);
            
        }
    }
   
    public void SetUpField()
    {   
        Vector3 linePos = testCSV.linePos;
        Vector3 ballPos = testCSV.ballPos;
        lineOS.transform.DOMove(linePos, 0);
        Ball.transform.DOMove(ballPos, 0);
    }

    // Alle Defensiven Spieler werdeb erstellt und auf die Startposition gesetzt
    public void SetDefense(GameObject prefab, GameObject team)
    {
        foreach (DefensePlayer player in testCSV.DefensePlayerList)
        {
            GameObject clone;
            clone = Instantiate(prefab);
            clone.transform.DOMove(player.Position, 0);
            clone.transform.SetParent(team.transform);
            clone.name = (player.PlayerName);

        }

    }


    // Alle Offensive Spieler werden erstellt und auf die Startposition gesetzt
    public void SetOffense(GameObject prefab, GameObject team)
    {
        foreach (OffensePlayer player in testCSV.OffensePlayerList)
        {
             
            if (player.PlayerName == "Q")
            {
                XROrgin.transform.DOMove(player.Position[0], 0);
            }
            else
            {
                GameObject clone;
                clone = Instantiate(prefab);
                clone.transform.DOMove(player.Position[0], 0);
                clone.transform.SetParent(team.transform);
                clone.name = (player.PlayerName);
            }


        }

    }
    //Animiert die Spielzüge der Offensiven
    public void PlayOffense()
    {
        foreach (OffensePlayer player in testCSV.OffensePlayerList)
        {
            if (player.PlayerName == "Q")
                continue;
            GameObject target = GameObject.Find("OffenseTeam/" + player.PlayerName);
            target.transform.DOPath(player.Position, maxSpeedOfense*speedProzent).SetSpeedBased(true);

        }
        animationStart = true;

        startButton.SetActive(false);

    }

    // Soll der Defensive Spieler jemanden decken werden hier die beiden Gameobjects auf dem Spielfeld gesucht.
    // Und der Defensive Spieler wird zum Offensiven Spieler hinbewgt.
    public void decken(string playerName, string followTarget)
    {
        followTarget = followTarget.TrimEnd();
        GameObject follower = GameObject.Find("DefenseTeam/" + playerName);
        GameObject target = GameObject.Find("OffenseTeam/" + followTarget);

        float speed = maxSpeedDefense * speedProzent;
        var distance = Vector3.Distance(follower.transform.position, target.transform.position);

        if (distance < 1) //damit die spieler nicht durch einander laufen
        {
            //speed = -speedOfense;
            Vector3 pos = new Vector3(follower.transform.position.x, follower.transform.position.y, follower.transform.position.z-1);
            follower.transform.DOMove(pos,0);
        }

        
        if (distance < 2 && distance > 1) //abstandhalter
            speed = 0;
        if (distance > 1.5)
            speed = maxSpeedDefense * speedProzent;
        follower.transform.position = Vector3.MoveTowards(follower.transform.position, target.transform.position, speed * Time.deltaTime);


    }
}
