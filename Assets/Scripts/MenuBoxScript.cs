using UnityEngine;
using System.Collections;
using UnityEngine.UI;  // Required when Using UI elements.

public class MenuBoxScript : MonoBehaviour
{
    public GameObject menuBox;
    public Button CloseMenuButton;
    public Button QuitButton;


    // Start is called before the first frame update
    void Start()
    {
        

        menuBox = gameObject;
        CloseMenuButton = menuBox.transform.GetChild(0).gameObject.GetComponent<Button>();
        QuitButton = menuBox.transform.GetChild(1).gameObject.GetComponent<Button>();

        CloseMenuButton.Select();

    }

    // Update is called once per frame
    void Update()
    {
        CloseMenuButton.GetComponent<RectTransform>().localPosition = new Vector3(0, CloseMenuButton.GetComponent<RectTransform>().sizeDelta.y - Screen.height / 2, 0);
        CloseMenuButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Clamp(Screen.width / 4, 100, Screen.width / 4), Mathf.Clamp(Screen.height / 6, 40, Screen.height / 6));

        QuitButton.GetComponent<RectTransform>().localPosition = new Vector3(QuitButton.GetComponent<RectTransform>().sizeDelta.x + 10, QuitButton.GetComponent<RectTransform>().sizeDelta.y - Screen.height / 2, 0);
        QuitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Clamp(Screen.width / 4, 100, Screen.width / 4), Mathf.Clamp(Screen.height / 6, 40, Screen.height / 6));

    }

    public void CloseMenuClick()
    {

        Object.Destroy(gameObject);

    }
    public void QuitGameClick()
    {

        Application.Quit();
        QuitGame();

    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
