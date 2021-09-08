using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewMusicListMenu : MonoBehaviour
{
    ScrollRect scrollRect;
    public Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        pos = new Vector2(0f, Mathf.Sin(Time.time * 10f) * 100f);

        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();
    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.2f, Screen.height / 1.4f);
        GetComponent<ScrollRect>().horizontalScrollbarSpacing = Screen.width / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
