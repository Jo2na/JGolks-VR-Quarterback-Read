using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensePlayer
{
    private string playerName;
    private Vector3[] position;
    private bool isContinue;

    //Constructor
    public OffensePlayer(string name, Vector3[] pos, bool cont)
    {
        playerName = name;
        position = pos;
        isContinue = cont;
    }

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    public Vector3[] Position
    {
        get { return position; }
        set { position = value; }
    }

    public bool IsContinue
    {
        get { return isContinue; }
        set { isContinue = value; }
    }
}
