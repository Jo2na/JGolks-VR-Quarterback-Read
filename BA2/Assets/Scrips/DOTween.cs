using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTween : MonoBehaviour
{    
    public GameObject Red;
    public Vector3[] posarrayBall = new[] { new Vector3(15f, 1f, 8f), new Vector3(15f, 1f, 30f) };
    public GameObject Blue;
    public Vector3[] posarray = new[] { new Vector3(12f, 1f, 8f), new Vector3(16f, 1f, 8f), new Vector3(12f, 1f, 30f) };


    // Start is called before the first frame update
    void Start()
    {
        DOTAnimation();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DOTAnimation()
    {
        
        Red.transform.DOPath(posarrayBall, 4).SetEase(Ease.OutQuad).SetSpeedBased(true);
        Blue.transform.DOLocalPath(posarray, 4).SetEase(Ease.OutCubic).SetSpeedBased(true).OnWaypointChange(MyCallback);
        
    }

    public void MyCallback(int waypointIndex)
    {
        Debug.Log("Waypoint");
        
    }

}
