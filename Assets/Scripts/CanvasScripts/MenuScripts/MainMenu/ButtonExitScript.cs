using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 6, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(5 * (Screen.width / 6), (Screen.height / 10));
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
