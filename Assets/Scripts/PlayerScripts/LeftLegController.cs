using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLegController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {

        Controller.stepVelocity = 30.0f * Vector3.Distance(Controller.leftLegPos, transform.position);

        if (transform.position.y > Controller.leftLegPos.y)
            transform.Translate(Vector3.down * Time.deltaTime * Controller.stepVelocity, Space.World);
        if (transform.position.y < Controller.leftLegPos.y)
            transform.Translate(Vector3.up * Time.deltaTime * Controller.stepVelocity, Space.World);
        if (transform.position.x > Controller.leftLegPos.x)
            transform.Translate(Vector3.left * Time.deltaTime * Controller.stepVelocity, Space.World);
        if (transform.position.x < Controller.leftLegPos.x)
            transform.Translate(Vector3.right * Time.deltaTime * Controller.stepVelocity, Space.World);
    }
}
