using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class catchBall : MonoBehaviour
{
    
    public GameManger gameManger;
    public GameObject Ball;
    public Vector3 pos;
    public bool catched = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (catched == true)
        {
            pos = transform.position;
            pos.z = pos.z - 1;
            gameManger.chatchPos = pos;
            //Ball.transform.DOMove(pos,0);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (gameManger.ballThrown == true)
        {
            
            if (collision.gameObject.tag == "Ball")
            {   
                catched = true;
                gameManger.ballCatch = true;
                //gameManger.catcher = this.name;
                
                
            }
        }

    }
}
