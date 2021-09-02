using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDelaysScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, 5 * Screen.height / 10);
    }

    void OnClick() {
    
    }
}
