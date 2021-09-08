using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMusicLinkScript : MonoBehaviour
{
    public string musicName;
    public string artistName;
    public string link;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = musicName + " by " + artistName + "(Open Link)";

        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();
    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.5f, Screen.height / 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCLick() {

        Application.OpenURL(link);
    }

}
