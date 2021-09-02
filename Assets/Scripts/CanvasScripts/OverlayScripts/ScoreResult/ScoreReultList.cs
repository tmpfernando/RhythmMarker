using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreReultList : MonoBehaviour
{
    public string scoreResultText;

    public GameObject score;

    public float scorePositonY;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (ScoreContent.scoreContent.Count + 2) * (Screen.height / 8));
        
        score = Resources.Load<GameObject>("Prefabs/Overlay/Score");


        for (int i = 0 ; i < ScoreContent.scoreContent.Count; i++)
        {
            scoreResultText = "Music: " + ScoreContent.scoreContent[i].musicName +
                "\nRhythm Score: " + ScoreContent.scoreContent[i].rhythmScore + ", Melody Score: " + ScoreContent.scoreContent[i].melodyScore;
            
            Instantiate(score, new Vector2(0, scorePositonY), Quaternion.identity, GetComponent<RectTransform>());
            score.transform.GetChild(0).GetComponent<Text>().text = scoreResultText;
        }

    }

    // Update is called once per frame
    void Update()
    {
        scorePositonY = ScoreContent.scoreContent.Count * (Screen.height / 16);

        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (ScoreContent.scoreContent.Count + 2) * (Screen.height / 8));

        for (int i = 0; i < ScoreContent.scoreContent.Count; i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, scorePositonY);
            transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.4f, Screen.height / 8);

            scorePositonY -= Screen.height / 8;
        }
    }
}
