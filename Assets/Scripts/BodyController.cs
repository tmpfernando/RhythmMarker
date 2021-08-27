using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        slidestepajust(Controller.supportLeftLeg);

        Controller.stepVelocity = 30.0f * Vector3.Distance(Controller.bodyPos, transform.position);

        if (transform.position.y > Controller.bodyPos.y) transform.Translate(Vector3.down * Time.deltaTime * Controller.stepVelocity, Space.World);
        if (transform.position.y < Controller.bodyPos.y) transform.Translate(Vector3.up * Time.deltaTime * Controller.stepVelocity, Space.World);
        if (transform.position.x > Controller.bodyPos.x) transform.Translate(Vector3.left * Time.deltaTime * Controller.stepVelocity, Space.World);
        if (transform.position.x < Controller.bodyPos.x) transform.Translate(Vector3.right * Time.deltaTime * Controller.stepVelocity, Space.World);
    }

    public static void bodyBump()
    {
        Controller.bodyPos.y += 0.2f;
    }

    public void slidestepajust(bool supportLeftLeg)
    {
        Controller.slideVelocity = 4.0f * Vector3.Distance(Controller.leftLegPos, Controller.rightLegPos);

        if (supportLeftLeg)
        {
            if (Controller.leftLegPos.y > Controller.rightLegPos.y + Controller.stepTolerance)
                Controller.leftLegPos.y -= Controller.slideVelocity * Time.deltaTime;
            if (Controller.leftLegPos.y < Controller.rightLegPos.y - Controller.stepTolerance)
                Controller.leftLegPos.y += Controller.slideVelocity * Time.deltaTime;
            if (Controller.leftLegPos.x > Controller.rightLegPos.x - 0.2f + Controller.stepTolerance)
                Controller.leftLegPos.x -= Controller.slideVelocity * Time.deltaTime;
            if (Controller.leftLegPos.x < Controller.rightLegPos.x - 0.2f - Controller.stepTolerance)
                Controller.leftLegPos.x += Controller.slideVelocity * Time.deltaTime;
        }
        else
        {
            if (Controller.rightLegPos.y > Controller.leftLegPos.y + Controller.stepTolerance)
                Controller.rightLegPos.y -= Controller.slideVelocity * Time.deltaTime;
            if (Controller.rightLegPos.y < Controller.leftLegPos.y - Controller.stepTolerance)
                Controller.rightLegPos.y += Controller.slideVelocity * Time.deltaTime;
            if (Controller.rightLegPos.x > Controller.leftLegPos.x + 0.2f + Controller.stepTolerance)
                Controller.rightLegPos.x -= Controller.slideVelocity * Time.deltaTime;
            if (Controller.rightLegPos.x < Controller.leftLegPos.x + 0.2f - Controller.stepTolerance)
                Controller.rightLegPos.x += Controller.slideVelocity * Time.deltaTime;
        }
    }

}
