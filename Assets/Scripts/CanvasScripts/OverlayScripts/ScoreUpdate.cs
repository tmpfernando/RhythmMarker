using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
    public static int scoreAmount;
    public static string scoreUpdate;
    public float timeToDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        if (scoreUpdate == "MISS RITHM") {
            scoreUpdate = "MISS RHYTHM\n\n\n";
            GetComponent<Text>().color = new Color(1, 0, 0, 1);
        }
        else if (scoreUpdate == "RHYTHM") {
            scoreUpdate = scoreAmount + " [RHYTHM COMBO]\n\n\n";
            if (scoreAmount > 100)
                GetComponent<Text>().color = new Color(0, 1, 0, 1);
            else if (scoreAmount > 80)
                GetComponent<Text>().color = new Color(0.3f, 1, 0.3f, 1);
            else if (scoreAmount > 50)
                GetComponent<Text>().color = new Color(0.6f, 1, 0.6f, 1);
            else
                GetComponent<Text>().color = new Color(1, 1, 1, 1);
        }
        else if (scoreUpdate == "MELODY") {
            scoreUpdate = scoreAmount + " [MELODY COMBO]\n\n\n";
            if (scoreAmount > 4)
                GetComponent<Text>().color = new Color(0, 1, 0, 1);
            else if (scoreAmount > 3)
                GetComponent<Text>().color = new Color(0.5f, 1, 0.5f, 1);
            else if (scoreAmount > 2)
                GetComponent<Text>().color = new Color(1, 1, 1, 1);
            else
                GetComponent<Text>().color = new Color(1, 1, 1, 1);
        }
        GetComponent<Text>().text = scoreUpdate;

        timeToDeath = 0.0f;

        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 3, Screen.height / 6);
    }

    // Update is called once per frame
    void Update()
    {

        if (timeToDeath >= 2.0f) {
            Object.Destroy(gameObject);
        }

        timeToDeath += Time.deltaTime;

        GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, GetComponent<Text>().color.a  - 0.5f * Time.deltaTime);
        transform.Translate(Vector3.up * Time.deltaTime * 100, Space.World);

    }
}
