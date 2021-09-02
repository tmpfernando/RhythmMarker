using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tapCircleScript : MonoBehaviour
{
    float timeToEnd;
    // Start is called before the first frame update
    void Start()
    {
        timeToEnd = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeToEnd -= 0.05f;
        if(timeToEnd < 0)
        {
            Object.Destroy(gameObject);
        }
    }
}
