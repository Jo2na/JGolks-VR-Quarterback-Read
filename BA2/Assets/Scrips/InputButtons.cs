using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class InputButtons : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("Button A is pressed");
            print("A wurde gedrückt");
            SceneManager.LoadScene("MainMenu");


        }
    }
}
