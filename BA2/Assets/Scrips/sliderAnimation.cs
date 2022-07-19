using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderAnimation : MonoBehaviour
{
    public Slider powerbar;

    public bool PowerBarOn;
    bool powerIsIncreasing;
    public float barChangeSpeed = 1;
    float maxPowerBarValue = 100;
    float currentPowerBarValue;
    public float fill;
    public float pwb;
    public bool slideractive;

    // Start is called before the first frame update
    void Start()
    {

        PowerBarOn = true;
        StartCoroutine(UpdatePowerBar());
        slideractive = true;
        powerbar.gameObject.SetActive(slideractive);

    }

    // Update is called once per frame
    void Update()
    {
        powerbar.gameObject.SetActive(slideractive);
        if (slideractive == true)
        {
           
           pwb = powerbar.value;
           powerbar.gameObject.SetActive(true);
        }

        

    }
    

    IEnumerator UpdatePowerBar()
    {
        while (PowerBarOn)
        {
            if (!powerIsIncreasing)
            {
                currentPowerBarValue -= barChangeSpeed;
                if (currentPowerBarValue <= 0)
                {
                    powerIsIncreasing = true;
                }
            }
            if (powerIsIncreasing)
            {
                currentPowerBarValue += barChangeSpeed;
                if (currentPowerBarValue >= maxPowerBarValue)
                {
                    powerIsIncreasing = false;
                }
            }

            float fill = currentPowerBarValue / maxPowerBarValue;
            powerbar.value = fill;
            
            yield return new WaitForSeconds(0.02f);



        }
        
        yield return null;


    }

}
