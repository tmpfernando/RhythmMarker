using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject menuBox;

    public static Vector3 leftLegPos = new Vector3(-0.1f, -0.2f, 0);
    public static Vector3 rightLegPos = new Vector3(0.1f, -0.2f, 0);
    public static Vector3 bodyPos = new Vector3(0.0f, -0.0f, 0);

    public static float leftArmRotation = 0.0f;
    public static float rightArmRotation = 0.0f;

    public static float stepVelocity;
    public static float slideVelocity;
    public static float stepLenght = 0.15f;
    public static float stepTolerance = 0.05f;

    public static bool supportLeftLeg = true;

    public static bool stepXDone = false;
    public static bool stepYDone = false;

    GameObject Grade;
    public GameObject squareGrade;

    public static Vector3 lastPosition = new Vector3(10.0f, 10.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {

        leftLegPos.Set(GameObject.Find("LeftLeg").GetComponent<Transform>().position.x,
            GameObject.Find("LeftLeg").GetComponent<Transform>().position.y,
            GameObject.Find("LeftLeg").GetComponent<Transform>().position.z);

        rightLegPos.Set(GameObject.Find("RightLeg").GetComponent<Transform>().position.x,
            GameObject.Find("RightLeg").GetComponent<Transform>().position.y,
            GameObject.Find("RightLeg").GetComponent<Transform>().position.z);

        bodyPos.Set(GameObject.Find("Body").GetComponent<Transform>().position.x,
            GameObject.Find("Body").GetComponent<Transform>().position.y,
            GameObject.Find("Body").GetComponent<Transform>().position.z);

        Grade = new GameObject("Grade");

        gradeGenerator(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("BACK"))
        {
            if (GameObject.FindGameObjectsWithTag("menu").Length > 0)
            {
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("menu"))
                    Object.Destroy(g);
            }
            else
            {
                Instantiate(menuBox, GameObject.Find("Canvas").GetComponent<RectTransform>().localPosition, Quaternion.identity, GameObject.Find("Canvas").GetComponent<Transform>().transform);
                
            }
            
        }

        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKey(KeyCode.F4))
        {
            Application.Quit();
            QuitGame();
        }

        //calling body position correction
        updateBodyPosition();

        //Reset step condition for X Axys
        if (Input.GetAxis("DPADX") == 0.0f && Input.GetAxis("LeftAnalogX") == 0.0f)
            stepXDone = false;

        //Reset step condition for Y Axys
        if (Input.GetAxis("DPADY") == 0.0f && Input.GetAxis("LeftAnalogY") == 0.0f)
            stepYDone = false;

        //Legs Moves
        if (supportLeftLeg)
        {
            if (Input.GetKeyDown(KeyCode.W)
                || (Input.GetAxis("DPADY") > 0.0F && stepYDone == false)
                || (Input.GetAxis("LeftAnalogY") > 0.0F && stepYDone == false))
            {
                supportLeftLeg = false;
                stepYDone = true;
                BodyController.bodyBump();
                leftLegPos.y = rightLegPos.y + stepLenght;
                gradeGenerator(GameObject.Find("Body").transform.position);
            }
            if (Input.GetKeyDown(KeyCode.S)
                || (Input.GetAxis("DPADY") < 0.0F && stepYDone == false)
                || (Input.GetAxis("LeftAnalogY") < 0.0F && stepYDone == false))
            {
                supportLeftLeg = false;
                stepYDone = true;
                BodyController.bodyBump();
                leftLegPos.y = rightLegPos.y - stepLenght;
                gradeGenerator(GameObject.Find("Body").transform.position);
            }
            if (Input.GetKeyDown(KeyCode.D)
                || (Input.GetAxis("DPADX") > 0.0F && stepXDone == false)
                || (Input.GetAxis("LeftAnalogX") > 0.0F && stepXDone == false))
            {
                supportLeftLeg = false;
                stepXDone = true;
                BodyController.bodyBump();
                leftLegPos.x = rightLegPos.x - 0.2f + stepLenght;
                gradeGenerator(GameObject.Find("Body").transform.position);
            }
            if (Input.GetKeyDown(KeyCode.A)
                || (Input.GetAxis("DPADX") < 0.0F && stepXDone == false)
                || (Input.GetAxis("LeftAnalogX") < 0.0F && stepXDone == false))
            {
                supportLeftLeg = false;
                stepXDone = true;
                BodyController.bodyBump();
                leftLegPos.x = rightLegPos.x - 0.2f - stepLenght;
                gradeGenerator(GameObject.Find("Body").transform.position);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W)
                || (Input.GetAxis("DPADY") > 0.0F && stepYDone == false)
                || (Input.GetAxis("LeftAnalogY") > 0.0F && stepYDone == false))
            {
                supportLeftLeg = true;
                stepYDone = true;
                BodyController.bodyBump();
                rightLegPos.y = leftLegPos.y + stepLenght;
                gradeGenerator(GameObject.Find("Body").transform.position);
            }
            if (Input.GetKeyDown(KeyCode.S)
                || (Input.GetAxis("DPADY") < 0.0F && stepYDone == false)
                || (Input.GetAxis("LeftAnalogY") < 0.0F && stepYDone == false))
            {
                supportLeftLeg = true;
                stepYDone = true;
                BodyController.bodyBump();
                rightLegPos.y = leftLegPos.y - stepLenght;
                gradeGenerator(GameObject.Find("Body").transform.position);
            }
            if (Input.GetKeyDown(KeyCode.D)
                || (Input.GetAxis("DPADX") > 0.0F && stepXDone == false)
                || (Input.GetAxis("LeftAnalogX") > 0.0F && stepXDone == false))
            {
                supportLeftLeg = true;
                stepXDone = true;
                BodyController.bodyBump();
                rightLegPos.x = leftLegPos.x + 0.2f + stepLenght;
                gradeGenerator(GameObject.Find("Body").transform.position);
            }
            if (Input.GetKeyDown(KeyCode.A)
                || (Input.GetAxis("DPADX") < 0.0F && stepXDone == false)
                || (Input.GetAxis("LeftAnalogX") < 0.0F && stepXDone == false))
            {
                supportLeftLeg = true;
                stepXDone = true;
                BodyController.bodyBump();
                rightLegPos.x = leftLegPos.x + 0.2f - stepLenght;
                gradeGenerator(GameObject.Find("Body").transform.position);
            }
        }

        //Arms moves controlled by joypad triggers
        if (Input.GetKey(KeyCode.U) || Input.GetAxis("LeftTrigger") > 0.99f) leftArmRotation = -180.0f;
        else if (Input.GetKey(KeyCode.J) || Input.GetAxis("LeftTrigger") > 0.0f) leftArmRotation = -90.0f;
        else leftArmRotation = 0.0f;

        //Arms moves controlled by joypad triggers
        if (Input.GetKey(KeyCode.I) || Input.GetAxis("RightTrigger") > 0.99f) rightArmRotation = 180.0f;
        else if (Input.GetKey(KeyCode.K) || Input.GetAxis("RightTrigger") > 0.0f) rightArmRotation = 90.0f;
        else rightArmRotation = 0.0f;

        //Arms moves controlled by joypad buttons
        if ((Input.GetButton("YButton") && Input.GetButton("XButton"))
            || (Input.GetAxis("RightAnalogX") < 0.0f && Input.GetAxis("RightAnalogY") > 0.0f))
        {
            //Trafic Warden Left Position
            leftArmRotation = -90.0f;
            rightArmRotation = 180.0f;
        }
        else if ((Input.GetButton("YButton") && Input.GetButton("BButton"))
           || (Input.GetAxis("RightAnalogX") > 0.0f && Input.GetAxis("RightAnalogY") > 0.0f))
        {
            //Trafic Warden Right Position
            leftArmRotation = -180.0f;
            rightArmRotation = 90.0f;
        }
        else if ((Input.GetButton("XButton") && Input.GetButton("BButton"))
           || (Input.GetAxis("RightAnalogX") == 0.0f && Input.GetAxis("RightAnalogY") < 0.0f))
        {
            //Crux Position
            leftArmRotation = -90.0f;
            rightArmRotation = 90.0f;
        }
        else if ((Input.GetButton("AButton") && Input.GetButton("BButton"))
           || (Input.GetAxis("RightAnalogX") > 0.0f && Input.GetAxis("RightAnalogY") < 0.0f))
        {
            //Arbitrator Right Position
            leftArmRotation = 0.0f;
            rightArmRotation = 90.0f;
        }
        else if ((Input.GetButton("AButton") && Input.GetButton("XButton"))
           || (Input.GetAxis("RightAnalogX") < 0.0f && Input.GetAxis("RightAnalogY") < 0.0f))
        {
            //Arbitrator Left Position
            leftArmRotation = -90.0f;
            rightArmRotation = 0.0f;
        }
        else if ((Input.GetButton("YButton"))
           || (Input.GetAxis("RightAnalogX") == 0.0f && Input.GetAxis("RightAnalogY") > 0.0f))
        {
            //Ballet Dancer Position
            leftArmRotation = -180.0f;
            rightArmRotation = 180.0f;
        }
        else if ((Input.GetButton("XButton"))
            || (Input.GetAxis("RightAnalogX") < 0.0f && Input.GetAxis("RightAnalogY") == 0.0f))
        {
            //Arm Left Position
            leftArmRotation = -180.0f;
            rightArmRotation = 0.0f;
        }
        else if ((Input.GetButton("BButton"))
            || (Input.GetAxis("RightAnalogX") > 0.0f && Input.GetAxis("RightAnalogY") == 0.0f))
        {
            //Arm Right Position
            leftArmRotation = 0.0f;
            rightArmRotation = 180.0f;
        }

    }

    public void updateBodyPosition()
    {
        //method to translate body to corret position (between legs)
        bodyPos = new Vector3(leftLegPos.x / 2.0f + rightLegPos.x / 2.0f,
                    0.2f + leftLegPos.y / 2.0f + rightLegPos.y / 2.0f,
                    0.0f);
    }

    public void gradeGenerator(Vector3 actualPosition)
    {

        //Round Position to snap into the grid
        actualPosition = new Vector3(Mathf.Round(actualPosition.x),
            Mathf.Round(actualPosition.y),
            Mathf.Round(actualPosition.z));

        //Destruction of distant grade squares
        foreach (GameObject objectMaybeDestroy in GameObject.FindGameObjectsWithTag("grade"))
        {
            if (Vector3.Distance(objectMaybeDestroy.transform.position, actualPosition) > 3.5f)
            {
                Destroy(objectMaybeDestroy);
            }
        }

        //Actual possitions to spawns grade squares
        if (actualPosition != lastPosition)
        {
            Vector3[] positions = new Vector3[12];

            positions[0] = new Vector3(actualPosition.x - 1, actualPosition.y + 1, actualPosition.z);
            positions[1] = new Vector3(actualPosition.x, actualPosition.y + 1, actualPosition.z);
            positions[2] = new Vector3(actualPosition.x - 2, actualPosition.y, actualPosition.z);
            positions[3] = new Vector3(actualPosition.x - 1, actualPosition.y, actualPosition.z);
            positions[4] = new Vector3(actualPosition.x, actualPosition.y, actualPosition.z);
            positions[5] = new Vector3(actualPosition.x + 1, actualPosition.y, actualPosition.z);
            positions[6] = new Vector3(actualPosition.x - 2, actualPosition.y - 1, actualPosition.z);
            positions[7] = new Vector3(actualPosition.x - 1, actualPosition.y - 1, actualPosition.z);
            positions[8] = new Vector3(actualPosition.x, actualPosition.y - 1, actualPosition.z);
            positions[9] = new Vector3(actualPosition.x + 1, actualPosition.y - 1, actualPosition.z);
            positions[10] = new Vector3(actualPosition.x - 1, actualPosition.y - 2, actualPosition.z);
            positions[11] = new Vector3(actualPosition.x, actualPosition.y - 2, actualPosition.z);

            //Instantiate of grade squares only at free space 
            for (int i = 0; i < positions.Length; i++)
            {
                bool verify = true;
                foreach (GameObject gradePos in GameObject.FindGameObjectsWithTag("grade"))
                {
                    if (gradePos.transform.position == positions[i])
                        verify = false;
                }
                if (verify)
                    Instantiate(squareGrade, positions[i], Quaternion.identity, GameObject.Find("Grade").transform);
            }

            foreach (GameObject grade in GameObject.FindGameObjectsWithTag("grade"))
            {
                grade.GetComponent<LineRenderer>().startColor = new Color(1, 1, 1, SoundController.gradeTransparency);
                grade.GetComponent<LineRenderer>().endColor = new Color(1, 1, 1, SoundController.gradeTransparency);
            }


        }
        lastPosition = actualPosition;
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