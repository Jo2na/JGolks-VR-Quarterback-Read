using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnClic : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Spiel beendet");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
