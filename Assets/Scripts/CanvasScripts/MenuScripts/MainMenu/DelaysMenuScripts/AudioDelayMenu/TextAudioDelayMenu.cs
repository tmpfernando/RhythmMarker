using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAudioDelayMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScreenSizeAdjustments() {

        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.6f, Screen.height / 1.6f);

    }

}

