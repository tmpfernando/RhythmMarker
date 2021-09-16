using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public static Vector3 camTarget;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        camTarget = new Vector3(Controller.bodyPos.x, Controller.bodyPos.y, Controller.bodyPos.z);

        speed = 0.6f * Vector3.Distance(Controller.bodyPos, transform.position);

        if (transform.position.y > camTarget.y)
            transform.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
        if (transform.position.y < camTarget.y)
            transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
        if (transform.position.x > camTarget.x)
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        if (transform.position.x < camTarget.x)
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);

    }
}
