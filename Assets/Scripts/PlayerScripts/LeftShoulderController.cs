using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftShoulderController : MonoBehaviour
{
    public float z = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update() {

        if (z > Controller.leftArmRotation)
            z = z - 0.1f * Mathf.Abs(z - Controller.leftArmRotation);

        if (z < Controller.leftArmRotation)
            z = z + 0.1f * Mathf.Abs(z - Controller.leftArmRotation);

        transform.eulerAngles = new Vector3(0.0f, 0.0f, z);

    }

}
