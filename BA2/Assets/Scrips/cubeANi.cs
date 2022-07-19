using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;

public class cubeANi : MonoBehaviour
{
    public GameObject cube;
    public Vector3[] array = new[] { new Vector3(30f, 0.5f, 21f), new Vector3(36f, 0.5f, 21f), new Vector3(36f, 0.5f, 32f) };
    public GameObject follower;
    [SerializeField] private float speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        //iTween.MoveTo(cube, iTween.Hash("path", array, "speed",4, "easetype", iTween.EaseType.easeOutExpo, "onupdate","onvalueChange"));
        cube.transform.DOLocalPath(array, 2).SetEase(Ease.OutBack).SetSpeedBased(true).OnWaypointChange(MyCallback);
    }
    void onvalueChange()
    {
       // Debug.Log("Waypoint");
    }
    // Update is called once per frame
    void Update()
    {
        follower.transform.position = Vector3.MoveTowards(follower.transform.position, cube.transform.position, speed * Time.deltaTime);
    }
    public void MyCallback(int waypointIndex)
    {
        if (waypointIndex == 2)
        {
            //Debug.Log("Waypoint");
            var milliseconds = 200;
            Thread.Sleep(milliseconds);
            //Debug.Log("Start");
        }
        




    }
}
