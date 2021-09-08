using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScoreHistoryScript : MonoBehaviour
{
    public GameObject ScoreResultsOverlay;

    // Start is called before the first frame update
    void Start()
    {
        ScoreResultsOverlay = Resources.Load<GameObject>("Prefabs/Overlay/ScoreResult");
        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();
    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, 7 * Screen.height / 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {

        Instantiate(ScoreResultsOverlay, transform.parent.transform.parent.transform);
        Object.Destroy(transform.parent.gameObject);

    }
}
