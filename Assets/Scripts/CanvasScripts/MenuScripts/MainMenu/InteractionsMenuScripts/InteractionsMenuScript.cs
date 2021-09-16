using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionsMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("interactionsMenu").Length > 1)
            Object.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
