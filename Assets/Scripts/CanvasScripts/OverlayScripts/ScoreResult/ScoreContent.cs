using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreContent : MonoBehaviour
{
    public bool showScore;

    GameObject scoreResults;

    public Score lastScore;
    public static List<Score> scoreContent;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        showScore = false;

        scoreResults = Resources.Load<GameObject>("Prefabs/Overlay/ScoreResult");

        scoreContent = new List<Score>();

        SoundController.musicIsPlaying.AddListener(MusicIsPlaying);
        SoundController.musicIsNotPlaying.AddListener(MusicIsNotPlaying);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MusicIsPlaying()
    {
        if (SoundController.audioSource.clip.length - SoundController.audioSource.time < 0.5f)
        {
            showScore = true;

            lastScore = new Score(SoundController.Musics[SoundController.musicIndex].musicName
                    + " by " + SoundController.Musics[SoundController.musicIndex].artistName,
                    SoundController.rhythmScore, SoundController.melodyScore);
        }

    }
    public void MusicIsNotPlaying()
    {
        if (showScore)
        {
            if (GameObject.FindGameObjectsWithTag("scoreResult").Length < 1)
            {
                scoreContent.Add(lastScore);
                Instantiate(scoreResults, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            }
            showScore = false;
        }
    }
}
