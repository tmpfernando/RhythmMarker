using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenResolutionCheck : MonoBehaviour
{

    public static UnityEvent screenResolutionChange;
    private Vector2 screenResolution;

    private void Awake()
    {
        screenResolutionChange = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ScreenCheck", 1, 1);
        
        screenResolution = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ScreenCheck() {

        if (screenResolution != new Vector2(Screen.width, Screen.height))
        {
            screenResolutionChange?.Invoke();
            Debug.Log("Screen Resolution Change!!!");
        }

        screenResolution = new Vector2(Screen.width, Screen.height);
    }
}
