using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionsMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();
    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.2f, Screen.height / 1.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
