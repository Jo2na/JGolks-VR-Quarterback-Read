using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public Text tutorialText;
    [TextArea(3, 10)]
    public string[] sentences;
    public int numOfSentences;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //particlsy = GetComponent<ParticleSystem>();
        

        sentences = new string[9];
        sentences[0] = "Welcome to the tutorial!\nPoint the laser at the button \nand press the trigger button \nwith your index finger to continue";
        sentences[1] = "To pick up the ball,\npoint at the ball and press the trigger\nbutton with your middle finger.";
        sentences[2] = "When you pick up the ball, a power bar appears.\nThrow at the right time to apply the desired power.";
        sentences[3] = "Press A with your right thumb \nto make the ball appear in front of you again.";
        sentences[4] = "With the right joystick you rotate!";
        sentences[5] = "Use the left joystick to go!";
        sentences[6] = "To practice throw the ball through the rings!";
        sentences[7] = "Have fun!";
        sentences[8] = " ";


        numOfSentences = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartText()
    {
        
        
        if (numOfSentences < sentences.Length+1)
        {
            tutorialText.text = sentences[numOfSentences]; 
            numOfSentences++;
        }
        
        if(numOfSentences == sentences.Length)
        {
            animator.SetBool("isOpen", false);
            numOfSentences = 0;
        }
       
        
    }

    public void StartTutorial()
    {
        animator.SetBool("isOpen", true);
        tutorialText.text = sentences[numOfSentences];
        numOfSentences++;

    }



}
