using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonWindow : MonoBehaviour
{

    public ParticleSystem particlsy;

    // Start is called before the first frame update
    void Start()
    {
        
        particlsy.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            //Destroy(gameObject);
            Debug.Log(collision.gameObject.name + " enter " + gameObject.name);
            particlsy.Play();
        }
    }

   


    }
