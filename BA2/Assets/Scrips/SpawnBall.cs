using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnBall : MonoBehaviour
{
    public GameObject prefab;
    public InputActionReference spawnReference = null;
    public GameObject player;
    public Rigidbody myRB;
    

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        // if button x pressed call Method Press
        spawnReference.action.started += Press;
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void Press(InputAction.CallbackContext context)
    {
        //GameObject Ball = Instantiate(prefab); // Gameobject wird erstellt
       
        // aif der x axse neeben dem player transformiert
        prefab.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z+1);
        myRB.velocity = new Vector3(0, 0, 0);

    }
}
