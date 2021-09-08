using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScoreReultList : MonoBehaviour
{
    public string scoreResultText;
    int countScoresToShow;

    public GameObject score;

    public float scorePositonY;
    //Start is called before the first frame update
    void Start()
    {
        countScoresToShow = 0;

        //Load the prefab that represent the score item
        score = Resources.Load<GameObject>("Prefabs/Overlay/Score");

        //instantiate the score itens on screen
        for (int i = 0; i < ScoreHistory.historySize; i++)
        {
            //check if string is not empty
            if (PlayerPrefs.GetString("music" + i) != "")
            {
                countScoresToShow++;

                //set the text that will be spawned on each score item
                scoreResultText = "Music: " + PlayerPrefs.GetString("music" + i) + "\nRhythm Score: " + PlayerPrefs.GetInt("rhythmScore" + i) + ", Melody Score: " + PlayerPrefs.GetInt("melodyScore" + i);

                //instantiate each score iten
                Instantiate(score, new Vector2(0, scorePositonY), Quaternion.identity, GetComponent<RectTransform>());
                //rename each score iten
                score.name = "score" + i;
                //Set the text of each score iten
                score.transform.GetChild(0).GetComponent<Text>().text = scoreResultText;

            }
        }

        //Set a position for Score Result Panel
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (countScoresToShow + 2) * (Screen.height / 8));

        //listener to check if screen resolution was chaged
        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);

        //call the method to ajust iten to match whit screen size
        ScreenSizeAdjustments();

    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        scorePositonY = countScoresToShow * (Screen.height / 16);

        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (countScoresToShow + 2) * (Screen.height / 8));

        for (int i = 0; i < countScoresToShow; i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().localPosition = new Vector2(0, scorePositonY);
            transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.4f, Screen.height / 8);

            scorePositonY -= Screen.height / 8;
        }
    }
}
