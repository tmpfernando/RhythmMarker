using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMenuScript : MonoBehaviour
{
    public GameObject blackSurfaceOverlay;
    public GameObject menuPanel;

    // Start is called before the first frame update
    void Start()
    {
        
        InputSetup.menuButton.AddListener(OnClick);
        blackSurfaceOverlay = Resources.Load<GameObject>("Prefabs/Overlay/BlackSurfaceOverlay");
        menuPanel = Resources.Load<GameObject>("Prefabs/Menus/MenuMain");

        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (GameObject.FindGameObjectsWithTag("mainMenu").Length > 0)
        {
            Instantiate(blackSurfaceOverlay, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            Object.Destroy(GameObject.FindGameObjectWithTag("mainMenu").gameObject);
        }
        else 
        {
            Instantiate(menuPanel, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            Object.Destroy(transform.parent.gameObject);
        }
    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 6, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 6, 9 * (Screen.height / 10));
    }
}
