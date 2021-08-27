using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update size of Texts on CANVAS
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 20 , Screen.height / 3);
        GetComponent<RectTransform>().transform.position = new Vector2( 10 , Screen.height - Screen.height / 3 - 10);

        scoreText.text =
            "Rhythm Analisys: " + SoundController.rhythmAnalysis +
            "\nRhythm Time Error: " + (SoundController.stepTime - SoundController.beatTime).ToString("00.000") + "s " +
            "\nMusic is Playing: " + SoundController.audioSource.isPlaying.ToString() +
            "\nBeat Status: " + SoundController.onBeat2.ToString() +
            "\nMelody Score: " + SoundController.melodyScore +
            "\nRhythm Score: " + SoundController.rhythmScore
            ;
    }
}
