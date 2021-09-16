using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{


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

    public GameObject squareGrade;
    public GameObject blackSurfaceOverlay;

    public static Vector3 lastPosition = new Vector3(10.0f, 10.0f, 0.0f);

    void Awake() {

        squareGrade = Resources.Load<GameObject>("Prefabs/SceneObjects/SquareGrade");
        
    }

    // Start is called before the first frame update
    void Start()
    {

        GradeGenerator(Vector3.zero);

        leftLegPos.Set(GameObject.Find("LeftLeg").GetComponent<Transform>().position.x,
            GameObject.Find("LeftLeg").GetComponent<Transform>().position.y,
            GameObject.Find("LeftLeg").GetComponent<Transform>().position.z);

        rightLegPos.Set(GameObject.Find("RightLeg").GetComponent<Transform>().position.x,
            GameObject.Find("RightLeg").GetComponent<Transform>().position.y,
            GameObject.Find("RightLeg").GetComponent<Transform>().position.z);

        bodyPos.Set(GameObject.Find("Body").GetComponent<Transform>().position.x,
            GameObject.Find("Body").GetComponent<Transform>().position.y,
            GameObject.Find("Body").GetComponent<Transform>().position.z);

        InputSetup.stepUP.AddListener(StepUpAction);
        InputSetup.stepDown.AddListener(StepDownAction);
        InputSetup.stepLeft.AddListener(StepLeftAction);
        InputSetup.stepRight.AddListener(StepRightAction);

        InputSetup.leftArmPosition0.AddListener(LeftArmPosition0);
        InputSetup.rightArmPosition0.AddListener(RightArmPosition0);
        InputSetup.leftArmPosition1.AddListener(LeftArmPosition1);
        InputSetup.rightArmPosition1.AddListener(RightArmPosition1);
        InputSetup.leftArmPosition2.AddListener(LeftArmPosition2);
        InputSetup.rightArmPosition2.AddListener(RightArmPosition2);

    }

    // Update is called once per frame
    void Update()
    {

        //calling body position correction
        UpdateBodyPosition();

    }

    public void UpdateBodyPosition()
    {
        //method to translate body to corret position (between legs)
        bodyPos = new Vector3(leftLegPos.x / 2.0f + rightLegPos.x / 2.0f,
                    0.2f + leftLegPos.y / 2.0f + rightLegPos.y / 2.0f,
                    0.0f);
    }


    #region PLAYER_BODY_MOVES

    void StepUpAction()
    { 
        BodyController.bodyBump();

        if (supportLeftLeg)
            leftLegPos.y = rightLegPos.y + stepLenght;
        else
            rightLegPos.y = leftLegPos.y + stepLenght;

        supportLeftLeg = !supportLeftLeg;

        GradeGenerator(GameObject.Find("Body").transform.position);
        Debug.Log("STEP UP");
    }

    void StepDownAction()
    {
        BodyController.bodyBump();

        if (supportLeftLeg)
            leftLegPos.y = rightLegPos.y - stepLenght;
        else
            rightLegPos.y = leftLegPos.y - stepLenght;

        supportLeftLeg = !supportLeftLeg;

        GradeGenerator(GameObject.Find("Body").transform.position);
        Debug.Log("STEP DOWN");
    }

    void StepLeftAction()
    {
        BodyController.bodyBump();

        if (supportLeftLeg)
            leftLegPos.x = rightLegPos.x - 0.2f - stepLenght;
        else
            rightLegPos.x = leftLegPos.x + 0.2f - stepLenght;

        supportLeftLeg = !supportLeftLeg;

        GradeGenerator(GameObject.Find("Body").transform.position);
        Debug.Log("STEP LEFT");
    }

    void StepRightAction()
    {
        BodyController.bodyBump();

        if (supportLeftLeg)
            leftLegPos.x = rightLegPos.x - 0.2f + stepLenght;
        else
            rightLegPos.x = leftLegPos.x + 0.2f + stepLenght;

        supportLeftLeg = !supportLeftLeg;

        GradeGenerator(GameObject.Find("Body").transform.position);
        Debug.Log("STEP RIGHT");
    }

    void LeftArmPosition0()
    {
        leftArmRotation = 0;
    }

    void RightArmPosition0()
    {
        rightArmRotation = 0;
    }

    void LeftArmPosition1()
    {
        leftArmRotation = -90;
    }

    void RightArmPosition1()
    {
        rightArmRotation = 90;
    }

    void LeftArmPosition2()
    {
        leftArmRotation = -180;
    }

    void RightArmPosition2()
    {
        rightArmRotation = 180;
    }

    #endregion

    public void GradeGenerator(Vector3 actualPosition)
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

        }
        lastPosition = actualPosition;
    }


}