using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreHistory : MonoBehaviour
{
    public static int historySize;

    AudioSource audioSource;

    public static bool showScoreResult;
    bool musicIsEnding;
    UnityEvent musicEnd;


    GameObject scoreResultPanel;

    private void Awake()
    {
        musicEnd = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreResultPanel = Resources.Load<GameObject>("Prefabs/Overlay/ScoreResult");

        historySize = 10;
        musicIsEnding = false;
        showScoreResult = false;

        audioSource = GetComponent<AudioSource>();
        musicEnd.AddListener(ScoreResults);

    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying)
        {
            if (audioSource.clip.length - audioSource.time < 1 && !musicIsEnding)
            {
                musicIsEnding = true;
            }
        }
        else {
            if (musicIsEnding) 
            {
                musicIsEnding = false;
                musicEnd?.Invoke();
            }
        }
    }


    void ScoreSave() 
    {
        if (GameObject.FindGameObjectsWithTag("scoreResult").Length > 0)
        {
            Debug.Log("Already have a Score Result Panel Instantiated.");
        }
        else
        {
            for (int i = historySize - 1; i > 0; i--)
            {

                if (PlayerPrefs.GetString("music" + (i - 1)) != "")
                {
                    PlayerPrefs.SetString("music" + i, PlayerPrefs.GetString("music" + (i - 1)));
                    PlayerPrefs.SetInt("rhythmScore" + i, PlayerPrefs.GetInt("rhythmScore" + (i - 1)));
                    PlayerPrefs.SetInt("melodyScore" + i, PlayerPrefs.GetInt("melodyScore" + (i - 1)));
                }
            }

            PlayerPrefs.SetString("music0", SoundController.Musics[SoundController.musicIndex].musicName + " by " + SoundController.Musics[SoundController.musicIndex].musicName);
            PlayerPrefs.SetInt("rhythmScore0", SoundController.rhythmScore);
            PlayerPrefs.SetInt("melodyScore0", SoundController.melodyScore);

            PlayerPrefs.Save();

        }
    }

    void ScoreResults() 
    {
        ScoreSave();
        Object.Destroy(GameObject.Find("Canvas").transform.GetChild(0).transform.gameObject);
        Instantiate(scoreResultPanel, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
    }
}
