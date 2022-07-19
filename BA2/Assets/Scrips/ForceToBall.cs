using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ForceToBall : MonoBehaviour
{
    
    public Slider powerbar;

    
    public bool PowerBarOn;
    bool powerIsIncreasing;
    float barChangeSpeed = 2;
    float maxPowerBarValue = 100;
    float currentPowerBarValue;
    public float fill;
    public float pwb;
    public bool slideractive;

    public UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable myXRGrabInteraction;

    // Start is called before the first frame update
    void Start()
    {
        PowerBarOn = true;
        
        StartCoroutine(UpdatePowerBar());
        slideractive = false;
        powerbar.gameObject.SetActive(slideractive);

    }

    // Update is called once per frame
    void Update()
    {
        powerbar.gameObject.SetActive(slideractive);
        if (slideractive == true)
        {
            pwb = powerbar.value;
        }
    }
   
    public void InteractableSelectEnter()
    {
        slideractive = true;
        powerbar.gameObject.SetActive(slideractive);
    }
    public void InteractableSelectExit()
    {
        slideractive = false;
        powerbar.gameObject.SetActive(slideractive);
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
            myXRGrabInteraction.throwVelocityScale = fill * maxPowerBarValue;
            yield return new WaitForSeconds(0.02f);

        }
        
        yield return null;
    }


}
