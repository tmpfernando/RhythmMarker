using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollVIewInteractionsMenu : MonoBehaviour
{
    ScrollRect scrollRect;
    public Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        pos = new Vector2(0f, Mathf.Sin(Time.time * 10f) * 100f);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.2f, Screen.height / 1.4f);
        GetComponent<ScrollRect>().horizontalScrollbarSpacing = Screen.width / 2;
    }
}
