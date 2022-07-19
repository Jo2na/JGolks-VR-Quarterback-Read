using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catchBall : MonoBehaviour
{
    
    public GameManger gameManger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (gameManger.ballThrown == true)
        {
            if (collision.gameObject.tag == "Ball")
            {
                gameManger.ballCatch = true;
                gameManger.catcher = this.name;
                
            }
        }

    }
}
