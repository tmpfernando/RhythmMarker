using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBodyController : MonoBehaviour
{

    enum StepDirection { up, down, left, right }
    StepDirection stepDirection;

    enum ArmsPosition { stand, armLeft, armRight, crux, arbitratorLeft, arbitratorRight, trafficWardenLeft, trafficWardenRight, balletDancer }
    ArmsPosition armsPosition;

    public Vector3 leftLegPos = new Vector3(-0.1f, -0.2f, 0);
    public Vector3 rightLegPos = new Vector3(0.1f, -0.2f, 0);
    public Vector3 bodyPos = new Vector3(0.0f, -0.0f, 0);
    public float leftArmZ;
    public float rightArmZ;

    public float leftArmRotation = 0.0f;
    public float rightArmRotation = 0.0f;

    public bool supportLeftLeg = true;
    public bool makeStep = false;
    public float makeStepDelay;

    public bool makeArmPosition = false;
    public float makeArmPositionDelay;

    public float stepVelocity;
    public float slideVelocity;
    public float stepLenght = 0.15f;
    public float stepTolerance = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        SoundController.beatEvent.AddListener(MakeStep);

        //Seting position for body members variables
        leftLegPos.Set(GameObject.Find("LeftLeg").GetComponent<Transform>().position.x,
            GameObject.Find("LeftLeg").GetComponent<Transform>().position.y,
            GameObject.Find("LeftLeg").GetComponent<Transform>().position.z);

        rightLegPos.Set(GameObject.Find("RightLeg").GetComponent<Transform>().position.x,
            GameObject.Find("RightLeg").GetComponent<Transform>().position.y,
            GameObject.Find("RightLeg").GetComponent<Transform>().position.z);

        bodyPos.Set(GameObject.Find("Body").GetComponent<Transform>().position.x,
            GameObject.Find("Body").GetComponent<Transform>().position.y,
            GameObject.Find("Body").GetComponent<Transform>().position.z);

    }

    

    // Update is called once per frame
    void Update()
    {

        if ( Vector2.Distance(GameObject.Find("Player").transform.GetChild(0).transform.position, transform.position) > 4)
        {
            Vector3 newPosition = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);

            bodyPos = GameObject.Find("Player").transform.GetChild(0).transform.position + newPosition;
            leftLegPos = GameObject.Find("Player").transform.GetChild(1).transform.position + newPosition;
            rightLegPos = GameObject.Find("Player").transform.GetChild(0).transform.position + newPosition;
        }

        slidestepajust(supportLeftLeg);

        stepVelocity = 30.0f * Vector3.Distance(bodyPos, transform.position);

        if (transform.position.y > bodyPos.y) transform.Translate(Vector3.down * Time.deltaTime * stepVelocity, Space.World);
        if (transform.position.y < bodyPos.y) transform.Translate(Vector3.up * Time.deltaTime * stepVelocity, Space.World);
        if (transform.position.x > bodyPos.x) transform.Translate(Vector3.left * Time.deltaTime * stepVelocity, Space.World);
        if (transform.position.x < bodyPos.x) transform.Translate(Vector3.right * Time.deltaTime * stepVelocity, Space.World);

        //method to translate body to corret position (between legs)
        bodyPos = new Vector3(leftLegPos.x / 2.0f + rightLegPos.x / 2.0f,
                    0.2f + leftLegPos.y / 2.0f + rightLegPos.y / 2.0f,
                    0.0f);

        ArmsRotation(SoundController.melodyValue, SoundController.melodyValueMax);
        UpdateLegsPosition();
        UpdateArmsRotation();

    }

    void MakeStep()
    {
        stepDirection = (StepDirection)Random.Range(0, 4);

        if (stepDirection == StepDirection.up)
        {
            //Up Step
            if (supportLeftLeg)
                leftLegPos.y = rightLegPos.y + stepLenght;
            else
                rightLegPos.y = leftLegPos.y + stepLenght;

            supportLeftLeg = !supportLeftLeg;
        }
        else if (stepDirection == StepDirection.down)
        {
            //Down Step
            if (supportLeftLeg)
                leftLegPos.y = rightLegPos.y - stepLenght;
            else
                rightLegPos.y = leftLegPos.y - stepLenght;

            supportLeftLeg = !supportLeftLeg;
        }
        else if (stepDirection == StepDirection.left)
        {
            //Left Step
            if (supportLeftLeg)
                leftLegPos.x = rightLegPos.x - 0.2f - stepLenght;
            else
                rightLegPos.x = leftLegPos.x + 0.2f - stepLenght;

            supportLeftLeg = !supportLeftLeg;
        }
        else if (stepDirection == StepDirection.right)
        {
            //Right Step
            if (supportLeftLeg)
                leftLegPos.x = rightLegPos.x - 0.2f + stepLenght;
            else
                rightLegPos.x = leftLegPos.x + 0.2f + stepLenght;

            supportLeftLeg = !supportLeftLeg;
        }
    }

    public void bodyBump()
    {
        bodyPos.y += 0.2f;
    }

    public void slidestepajust(bool supportLeftLeg)
    {
        slideVelocity = 4.0f * Vector3.Distance(leftLegPos, rightLegPos);

        if (supportLeftLeg)
        {
            if (leftLegPos.y > rightLegPos.y + stepTolerance)
                leftLegPos.y -= slideVelocity * Time.deltaTime;
            if (leftLegPos.y < rightLegPos.y - stepTolerance)
                leftLegPos.y += slideVelocity * Time.deltaTime;
            if (leftLegPos.x > rightLegPos.x - 0.2f + stepTolerance)
                leftLegPos.x -= slideVelocity * Time.deltaTime;
            if (leftLegPos.x < rightLegPos.x - 0.2f - stepTolerance)
                leftLegPos.x += slideVelocity * Time.deltaTime;
        }
        else
        {
            if (rightLegPos.y > leftLegPos.y + stepTolerance)
                rightLegPos.y -= slideVelocity * Time.deltaTime;
            if (rightLegPos.y < leftLegPos.y - stepTolerance)
                rightLegPos.y += slideVelocity * Time.deltaTime;
            if (rightLegPos.x > leftLegPos.x + 0.2f + stepTolerance)
                rightLegPos.x -= slideVelocity * Time.deltaTime;
            if (rightLegPos.x < leftLegPos.x + 0.2f - stepTolerance)
                rightLegPos.x += slideVelocity * Time.deltaTime;
        }
    }

    public void ArmsRotation(float melodyValue, float melodyValueMax) 
    {

        if (makeArmPositionDelay > 0.2f)
        {
            makeArmPosition = true;
            makeArmPositionDelay = 0;
        }
        else
        {
            makeArmPosition = false;
            makeArmPositionDelay += Time.deltaTime;
        }

        if(makeArmPosition)
        {
            if (melodyValue > 3 * melodyValueMax / 4)
            {
                armsPosition = ArmsPosition.balletDancer;
            }
            else if (melodyValue > 2 * melodyValueMax / 4)
            {
                armsPosition = (ArmsPosition)Random.Range(6, 8);
            }
            else if (melodyValue > 1 * melodyValueMax / 4)
            {
                armsPosition = (ArmsPosition)Random.Range(3, 6);
            }
            else if (melodyValue > 0)
            {
                armsPosition = (ArmsPosition)Random.Range(1, 3);
            }
            else
            {
                armsPosition = ArmsPosition.stand;
            }

            if (armsPosition == ArmsPosition.balletDancer)
            {
                leftArmRotation = -180f;
                rightArmRotation = 180f;
            }
            else if (armsPosition == ArmsPosition.trafficWardenLeft)
            {
                leftArmRotation = -90f;
                rightArmRotation = 180f;
            }
            else if (armsPosition == ArmsPosition.trafficWardenRight)
            {
                leftArmRotation = -180f;
                rightArmRotation = 90f;
            }
            else if (armsPosition == ArmsPosition.arbitratorLeft)
            {
                leftArmRotation = -180f;
                rightArmRotation = 0f;
            }
            else if (armsPosition == ArmsPosition.arbitratorRight)
            {
                leftArmRotation = 0f;
                rightArmRotation = 180f;
            }
            else if (armsPosition == ArmsPosition.crux)
            {
                leftArmRotation = -90f;
                rightArmRotation = 90f;
            }
            else if (armsPosition == ArmsPosition.armLeft)
            {
                leftArmRotation = -90f;
                rightArmRotation = 0f;
            }
            else if (armsPosition == ArmsPosition.armRight)
            {
                leftArmRotation = 0f;
                rightArmRotation = 90f;
            }
            else if (armsPosition == ArmsPosition.stand)
            {
                leftArmRotation = 0f;
                rightArmRotation = 0f;
            }
        }
    }

    public void UpdateLegsPosition() 
    {
        //Update Left Leg Position
        stepVelocity = 30.0f * Vector3.Distance(leftLegPos, transform.parent.transform.GetChild(1).transform.position);

        if (transform.parent.transform.GetChild(1).transform.position.y > leftLegPos.y)
            transform.parent.transform.GetChild(1).transform.Translate(Vector3.down * Time.deltaTime * stepVelocity, Space.World);
        if (transform.parent.transform.GetChild(1).transform.position.y < leftLegPos.y)
            transform.parent.transform.GetChild(1).transform.Translate(Vector3.up * Time.deltaTime * stepVelocity, Space.World);
        if (transform.parent.transform.GetChild(1).transform.position.x > leftLegPos.x)
            transform.parent.transform.GetChild(1).transform.Translate(Vector3.left * Time.deltaTime * stepVelocity, Space.World);
        if (transform.parent.transform.GetChild(1).transform.position.x < leftLegPos.x)
            transform.parent.transform.GetChild(1).transform.Translate(Vector3.right * Time.deltaTime * stepVelocity, Space.World);

        //Update Right Leg Position
        stepVelocity = 30.0f * Vector3.Distance(rightLegPos, transform.parent.transform.GetChild(2).transform.position);

        if (transform.parent.transform.GetChild(2).transform.position.y > rightLegPos.y)
            transform.parent.transform.GetChild(2).transform.Translate(Vector3.down * Time.deltaTime * stepVelocity, Space.World);
        if (transform.parent.transform.GetChild(2).transform.position.y < rightLegPos.y)
            transform.parent.transform.GetChild(2).transform.Translate(Vector3.up * Time.deltaTime * stepVelocity, Space.World);
        if (transform.parent.transform.GetChild(2).transform.position.x > rightLegPos.x)
            transform.parent.transform.GetChild(2).transform.Translate(Vector3.left * Time.deltaTime * stepVelocity, Space.World);
        if (transform.parent.transform.GetChild(2).transform.position.x < rightLegPos.x)
            transform.parent.transform.GetChild(2).transform.Translate(Vector3.right * Time.deltaTime * stepVelocity, Space.World);
    }

    public void UpdateArmsRotation() 
    {

        if (leftArmZ > leftArmRotation)
            leftArmZ = leftArmZ - 0.1f * Mathf.Abs(leftArmZ - leftArmRotation);

        if (leftArmZ < leftArmRotation)
            leftArmZ = leftArmZ + 0.1f * Mathf.Abs(leftArmZ - leftArmRotation);

        transform.GetChild(1).transform.eulerAngles = new Vector3(0.0f, 0.0f, leftArmZ);

        if (rightArmZ > rightArmRotation)
            rightArmZ = rightArmZ - 0.1f * Mathf.Abs(rightArmZ - rightArmRotation);

        if (rightArmZ < rightArmRotation)
            rightArmZ = rightArmZ + 0.1f * Mathf.Abs(rightArmZ - rightArmRotation);

        transform.GetChild(2).transform.eulerAngles = new Vector3(0.0f, 0.0f, rightArmZ);

    }

    public void teleportNearPlayer() 
    {

    }

}
