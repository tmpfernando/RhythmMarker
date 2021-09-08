using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSetup : MonoBehaviour
{
    
    public static UnityEvent anyStepOn;

    public static UnityEvent stepUP;
    public static UnityEvent stepDown;
    public static UnityEvent stepLeft;
    public static UnityEvent stepRight;

    public static UnityEvent leftArmPosition0;
    public static UnityEvent leftArmPosition1;
    public static UnityEvent leftArmPosition2;
    public static UnityEvent rightArmPosition0;
    public static UnityEvent rightArmPosition1;
    public static UnityEvent rightArmPosition2;

    public static UnityEvent menuButton;
    public static UnityEvent playButton;
    public static UnityEvent nextMusicButton;
    public static UnityEvent previousMusicButton;
    public static UnityEvent interactButton;

    private bool stepYdone;
    private bool stepXdone;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public float timeForSwipe;
    public Vector2 screenCenter;
    public Vector2 firstSetPosition;
    public Vector2 tapDirection;
    public Vector2 holdDirection;

    public string armsPosition;

    public GameObject circleTouch;

    private void Awake()
    {
        anyStepOn = new UnityEvent();

        stepUP = new UnityEvent();
        stepDown = new UnityEvent();
        stepLeft = new UnityEvent();
        stepRight = new UnityEvent();

        leftArmPosition0 = new UnityEvent();
        leftArmPosition1 = new UnityEvent();
        leftArmPosition2 = new UnityEvent();
        rightArmPosition0 = new UnityEvent();
        rightArmPosition1 = new UnityEvent();
        rightArmPosition2 = new UnityEvent();

        menuButton = new UnityEvent();
        playButton = new UnityEvent();
        nextMusicButton = new UnityEvent();
        previousMusicButton = new UnityEvent();
        interactButton = new UnityEvent();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        timeForSwipe = 0.15f;
        stepYdone = false;
        stepXdone = false;

        firstSetPosition = new Vector2(Screen.width / 2, Screen.height / 2);

        armsPosition = "zeroPosition";

    }

    // Update is called once per frame
    void Update()
    {
        //Swipe();
        Keyboard();
        Joypad();
        Touch();

        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKey(KeyCode.F4))
        {
            Application.Quit();
            QuitGame();
        }
    }

    public void Touch()
    {
        
        Touch[] touches = Input.touches;

        for (int i = 0; i < Input.touches.Length; i++)
        {
            float initialTime = Time.time;

            if (touches[i].phase == TouchPhase.Ended && Time.time - initialTime <= timeForSwipe)
            {
                //TapCondition

                tapDirection = touches[i].position - touches[i].rawPosition;
                tapDirection.Normalize();

                Debug.Log("TAP: " + touches[i].deltaPosition + " || " + firstSetPosition);

                //Instantiate(circleTouch, touches[i].position, Quaternion.identity, GameObject.Find("Overlay").transform);

                if (tapDirection.y > 0 && tapDirection.x > -0.5f && tapDirection.x < 0.5f)
                {
                    //tap on top of screen
                    anyStepOn?.Invoke();
                    stepUP?.Invoke();
                }
                else if (tapDirection.y < 0 && tapDirection.x > -0.5f && tapDirection.x < 0.5f)
                {
                    //Tap on bottom of screen
                    anyStepOn?.Invoke();
                    stepDown?.Invoke();
                }
                else if (tapDirection.x < 0 && tapDirection.y > -0.5f && tapDirection.y < 0.5f)
                {
                    //Tap on left side of screen
                    anyStepOn?.Invoke();
                    stepLeft?.Invoke();
                }
                else if (tapDirection.x > 0 && tapDirection.y > -0.5f && tapDirection.y < 0.5f)
                {
                    //Tap on right side of screen
                    anyStepOn?.Invoke();
                    stepRight?.Invoke();
                }

                
            }
            else if (touches[i].phase == TouchPhase.Moved || touches[i].phase == TouchPhase.Stationary)
            {
                //Moved Condition
                holdDirection = touches[i].deltaPosition;
                holdDirection.Normalize();
                Debug.Log("HOLD: " + holdDirection);

                //Instantiate(circleTouch, touches[i].position, Quaternion.identity, GameObject.Find("Overlay").transform);
                
                
                if(holdDirection.y > 0.725f && holdDirection.y < 1.000f && holdDirection.x > -0.225f && holdDirection.x < 0.225f)
                {
                    armsPosition = "balletDancer";
                }
                else if (holdDirection.y > 0.225f && holdDirection.y < 0.725f && holdDirection.x > -0.725f && holdDirection.x < -0.225f)
                {
                    armsPosition = "trafficWardenLeft";
                }
                else if (holdDirection.y > -0.725f && holdDirection.y < -0.225f && holdDirection.x > -0.725f && holdDirection.x < -0.225f)
                {
                    armsPosition = "arbitratorLeft";
                }
                else if (holdDirection.y > -0.225f && holdDirection.y < 0.225f && holdDirection.x > -1.000f && holdDirection.x < -0.725f)
                {
                    armsPosition = "armLeft";
                }
                else if (holdDirection.y > -1.000f && holdDirection.y < -0.725f && holdDirection.x > -0.225f && holdDirection.x < 0.225f)
                {
                    armsPosition = "crossPosition";
                }
                else if (holdDirection.y > -0.725f && holdDirection.y < -0.225f && holdDirection.x > 0.225f && holdDirection.x < 0.725f)
                {
                    armsPosition = "arbitratorRight";
                }
                else if (holdDirection.y > -0.225f && holdDirection.y < 0.225f && holdDirection.x > 0.725f && holdDirection.x < 1.000f)
                {
                    armsPosition = "armRight";
                }
                else if (holdDirection.y > 0.225f && holdDirection.y < 0.725f && holdDirection.x > 0.225f && holdDirection.x < 0.725f)
                {
                    armsPosition = "trafficWardenRight";
                }
                else if (touches[i].phase == TouchPhase.Ended)
                {
                    armsPosition = "zeroPosition";
                }
            }
            ArmsPosition(armsPosition);
        }
    }

    public void ArmsPosition(string armsPosition) {

        if (armsPosition == "balletDancer")
        {
            //Ballet Dancer Position
            leftArmPosition2?.Invoke();
            rightArmPosition2?.Invoke();
        }
        else if (armsPosition == "trafficWardenLeft")
        {
            //Traffic Warden Left
            leftArmPosition1?.Invoke();
            rightArmPosition2?.Invoke();
        }
        else if (armsPosition == "arbitratorLeft")
        {
            //Arbitrator Left
            leftArmPosition2?.Invoke();
            rightArmPosition0?.Invoke();
        }
        else if (armsPosition == "armLeft")
        {
            //Arm Left
            leftArmPosition1?.Invoke();
            rightArmPosition0?.Invoke();
        }
        else if (armsPosition == "crossPosition")
        {
            //Cross Position
            leftArmPosition1?.Invoke();
            rightArmPosition1?.Invoke();
        }
        else if (armsPosition == "arbitratorRight")
        {
            //Arbitrator Right
            leftArmPosition0?.Invoke();
            rightArmPosition2?.Invoke();
        }
        else if (armsPosition == "armRight")
        {
            //Arm Right
            leftArmPosition0?.Invoke();
            rightArmPosition1?.Invoke();
        }
        else if (armsPosition == "trafficWardenRight")
        {
            //Traffic Warden Right
            leftArmPosition2?.Invoke();
            rightArmPosition1?.Invoke();
        }
        else if (armsPosition == "zeroPosition")
        {
            //Zero Position
            leftArmPosition0?.Invoke();
            rightArmPosition0?.Invoke();
        }

    }

    public void Keyboard()
    {

        //Menu Button
        if (Input.GetKeyDown(KeyCode.Escape))
            menuButton?.Invoke();

        //Play/Pause Button
        if (Input.GetKeyDown(KeyCode.Space))
            playButton?.Invoke();

        //Next Music Button
        if (Input.GetKeyDown(KeyCode.Period))
            nextMusicButton?.Invoke();

        //Previous Music Button
        if (Input.GetKeyDown(KeyCode.Comma))
            previousMusicButton?.Invoke();

        //Interaction Button
        if (Input.GetKeyDown(KeyCode.Return))
            interactButton?.Invoke();

        //steps buttons
        if (Input.GetKeyDown(KeyCode.W))
        {
            stepUP?.Invoke();
            anyStepOn?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            stepDown?.Invoke();
            anyStepOn?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            stepLeft?.Invoke();
            anyStepOn?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            stepRight?.Invoke();
            anyStepOn?.Invoke();
        }

        //Arms moves controlled by keyboard
        if (Input.GetKey(KeyCode.U))
            leftArmPosition2?.Invoke();
        else if (Input.GetKey(KeyCode.J))
            leftArmPosition1?.Invoke();
        else
            leftArmPosition0?.Invoke();

        //Arms moves controlled by keyboard
        if (Input.GetKey(KeyCode.I))
            rightArmPosition2?.Invoke();
        else if (Input.GetKey(KeyCode.K))
            rightArmPosition1?.Invoke();
        else
            rightArmPosition0?.Invoke();

        
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            //Traffic Warden Left
            leftArmPosition1?.Invoke();
            rightArmPosition2?.Invoke();
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            //Traffic Warden Right
            leftArmPosition2?.Invoke();
            rightArmPosition1?.Invoke();
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            //Arbitrator Left
            leftArmPosition2?.Invoke();
            rightArmPosition0?.Invoke();
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            //Arbitrator Right
            leftArmPosition0?.Invoke();
            rightArmPosition2?.Invoke();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            //Cross Position
            leftArmPosition1?.Invoke();
            rightArmPosition1?.Invoke();
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //Ballet Dancer Position
            leftArmPosition2?.Invoke();
            rightArmPosition2?.Invoke();
        }
        
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Arm Left
            leftArmPosition1?.Invoke();
            rightArmPosition0?.Invoke();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //Arm Right
            leftArmPosition0?.Invoke();
            rightArmPosition1?.Invoke();
        }
        

    }

    public void Joypad()
    {
        //Menu Button
        if (Input.GetButtonDown("BACK"))
            menuButton?.Invoke();

        //Play/Pause Button
        if (Input.GetButtonDown("START"))
            playButton?.Invoke();

        //Next Music Button
        if (Input.GetButtonDown("LB"))
            previousMusicButton?.Invoke();

        //Previous Music Button
        if (Input.GetButtonDown("RB"))
            nextMusicButton?.Invoke();


        //Interaction Button
        if (Input.GetButtonDown("AButton"))
            interactButton?.Invoke();



        //Arms moves controlled by joypad buttons
        if ((Input.GetButton("YButton") && Input.GetButton("XButton"))
            || (Input.GetAxis("RightAnalogX") < 0.0f && Input.GetAxis("RightAnalogY") > 0.0f))
        {
            //Trafic Warden Left Position
            leftArmPosition1?.Invoke();
            rightArmPosition2?.Invoke();
        }
        else if ((Input.GetButton("YButton") && Input.GetButton("BButton"))
           || (Input.GetAxis("RightAnalogX") > 0.0f && Input.GetAxis("RightAnalogY") > 0.0f))
        {
            //Trafic Warden Right Position
            leftArmPosition2?.Invoke();
            rightArmPosition1?.Invoke();
        }
        else if ((Input.GetButton("XButton") && Input.GetButton("BButton"))
           || (Input.GetAxis("RightAnalogX") == 0.0f && Input.GetAxis("RightAnalogY") < 0.0f))
        {
            //Crux Position
            leftArmPosition1?.Invoke();
            rightArmPosition1?.Invoke();
        }
        else if ((Input.GetButton("AButton") && Input.GetButton("BButton"))
           || (Input.GetAxis("RightAnalogX") > 0.0f && Input.GetAxis("RightAnalogY") < 0.0f))
        {
            //Arbitrator Right Position
            leftArmPosition0?.Invoke();
            rightArmPosition1?.Invoke();
        }
        else if ((Input.GetButton("AButton") && Input.GetButton("XButton"))
           || (Input.GetAxis("RightAnalogX") < 0.0f && Input.GetAxis("RightAnalogY") < 0.0f))
        {
            //Arbitrator Left Position
            leftArmPosition1?.Invoke();
            rightArmPosition0?.Invoke();
        }
        else if ((Input.GetButton("YButton"))
           || (Input.GetAxis("RightAnalogX") == 0.0f && Input.GetAxis("RightAnalogY") > 0.0f))
        {
            //Ballet Dancer Position
            leftArmPosition2?.Invoke();
            rightArmPosition2?.Invoke();
        }
        else if ((Input.GetButton("XButton"))
            || (Input.GetAxis("RightAnalogX") < 0.0f && Input.GetAxis("RightAnalogY") == 0.0f))
        {
            //Arm Left Position
            leftArmPosition2?.Invoke();
            rightArmPosition0?.Invoke();
        }
        else if ((Input.GetButton("BButton"))
            || (Input.GetAxis("RightAnalogX") > 0.0f && Input.GetAxis("RightAnalogY") == 0.0f))
        {
            //Arm Right Position
            leftArmPosition0?.Invoke();
            rightArmPosition2?.Invoke();
        }
        else
        {
            //Arms moves controlled by joypad triggers
            if (Input.GetAxis("LeftTrigger") > 0.99f) leftArmPosition2?.Invoke();
            else if (Input.GetAxis("LeftTrigger") > 0.0f) leftArmPosition1?.Invoke();

            //Arms moves controlled by joypad triggers
            if (Input.GetAxis("RightTrigger") > 0.99f) rightArmPosition2?.Invoke();
            else if (Input.GetAxis("RightTrigger") > 0.0f) rightArmPosition1?.Invoke();
        }

        //Block any Y axis movement while non setted to zero on Joypad
        if (Input.GetAxis("DPADY") == 0 && Input.GetAxis("LeftAnalogY") == 0)
            stepYdone = false;

        //Block any X axis movement while non setted to zero on Joypad
        if (Input.GetAxis("DPADX") == 0 && Input.GetAxis("LeftAnalogX") == 0)
            stepXdone = false;

        if ((Input.GetAxis("DPADY") > 0 || Input.GetAxis("LeftAnalogY") > 0) && !stepYdone)
        {
            anyStepOn?.Invoke();
            stepUP?.Invoke();
            stepYdone = true;

        }
        if ((Input.GetAxis("DPADY") < 0 || Input.GetAxis("LeftAnalogY") < 0) && !stepYdone)
        {
            anyStepOn?.Invoke();
            stepDown?.Invoke();
            stepYdone = true;

        }
        if ((Input.GetAxis("DPADX") > 0 || Input.GetAxis("LeftAnalogX") > 0) && !stepXdone)
        {
            anyStepOn?.Invoke();
            stepRight?.Invoke();
            stepXdone = true;

        }
        if ((Input.GetAxis("DPADX") < 0 || Input.GetAxis("LeftAnalogX") < 0) && !stepXdone)
        {
            anyStepOn?.Invoke();
            stepLeft?.Invoke();
            stepXdone = true;

        }
    }

    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("up swipe");
                anyStepOn?.Invoke();
                stepUP?.Invoke();

            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                anyStepOn?.Invoke();
                Debug.Log("down swipe");
                stepDown?.Invoke();

            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                anyStepOn?.Invoke();
                Debug.Log("left swipe");
                stepLeft?.Invoke();

            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                anyStepOn?.Invoke();
                Debug.Log("right swipe");
                stepRight?.Invoke();

            }
        }
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
