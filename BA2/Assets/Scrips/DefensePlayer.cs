using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePlayer
{
    private string playerName;
    private Vector3 position;
    private string followTarget;
    private bool hasTarget;

    public DefensePlayer(string name, Vector3 pos, string ftarget, bool target)
    {
        playerName = name;
        position = pos;
        followTarget = ftarget;
        hasTarget = target;
    }

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    public string FollowTarget
    {
        get { return followTarget; }
        set { followTarget = value; }
    }

    public bool HasTarget
    {
        get { return hasTarget; }
        set { hasTarget = value; }
    }
}
