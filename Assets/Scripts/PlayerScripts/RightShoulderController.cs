using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightShoulderController : MonoBehaviour
{
    public float z = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update() {

        if (z > Controller.rightArmRotation)
            z = z - 0.1f * Mathf.Abs(z - Controller.rightArmRotation);

        if (z < Controller.rightArmRotation)
            z = z + 0.1f * Mathf.Abs(z - Controller.rightArmRotation);

        transform.eulerAngles = new Vector3(0.0f, 0.0f, z);
    }
}
