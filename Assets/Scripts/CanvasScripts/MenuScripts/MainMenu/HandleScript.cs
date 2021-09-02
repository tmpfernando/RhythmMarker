using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10, 0);
    }
}
