using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("mainMenu").Length > 1)
        {
            Object.Destroy(gameObject);
        }
        else
        {
            transform.GetChild(0).GetComponent<Button>().Select();
            ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
            ScreenSizeAdjustments();
        }
    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
