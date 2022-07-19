using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAnimation : MonoBehaviour
{
    public TestCSV readerCSV;
    public List<DefensePlayer> List;
    // Start is called before the first frame update
    void Start()
    {
        List = readerCSV.DefensePlayerList;
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
