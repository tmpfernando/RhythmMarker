using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPreviousScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 8, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 8, Screen.height / 10);
    }

    public void OnClick()
    {
        InputSetup.previousMusicButton.Invoke();
    }
}
