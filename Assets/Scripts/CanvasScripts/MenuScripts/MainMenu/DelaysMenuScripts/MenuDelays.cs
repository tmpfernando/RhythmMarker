using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDelays : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("delaysMenu").Length > 1)
            Object.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
