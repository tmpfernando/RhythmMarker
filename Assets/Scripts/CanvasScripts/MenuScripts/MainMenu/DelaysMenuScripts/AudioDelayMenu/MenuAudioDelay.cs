using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudioDelay : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip rhythmTrack;

    public int bpm;
    public float startBeatTime;
    public bool start;
    public bool testEnded;

    public float[] inputTime;
    public float[] musicTime;

    public float averageAudioDelay;
    public float timeToStart;
    public float timeToEnd;

    
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("audioDelayMenu").Length > 1)
        {
            Object.Destroy(gameObject);
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
            rhythmTrack = Resources.Load<AudioClip>("Musics/RhythmTrack");
            audioSource.clip = rhythmTrack;
            audioSource.playOnAwake = false;

            testEnded = false;
            start = false;
            bpm = 80;
            startBeatTime = 0.005f;

            InputSetup.anyStepOn.AddListener(InputForDelayTest);

            musicTime = new float[16];
            inputTime = new float[16];

            for (int i = 0; i < musicTime.Length; i++)
            {
                musicTime[i] = i * 0.75f + startBeatTime;
            }

            transform.GetChild(1).GetComponent<Text>().text = "PRESS ANY STEP BUTTON (OR SWIPE) TO START.\nTHEN, HIT STEP BUTTON (OR SWIPE) WHEN YOU HEAR THE BEAT\n(HINT: NO NEED TO PRESS THE BUTTON ON ALL BEATS, TRY TO RECOGNIZE RHYTHM BEFORE STARTING PUSHING THE BUTTON)";

            timeToStart = 3;
            timeToEnd = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (start && !testEnded)
        {
            
            if (timeToStart <= 0)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                else if((audioSource.clip.length - audioSource.time) <= 0.1f)
                {
                    audioSource.Stop();
                    start = false;
                    CalculateAudioDelay();
                    transform.GetChild(1).GetComponent<Text>().text = "CALCULATED DELAY: " + averageAudioDelay;
                    timeToStart = 3;

                    SoundController.audioDelay = averageAudioDelay;
                    PlayerPrefs.SetFloat("averageDelay", averageAudioDelay);
                    PlayerPrefs.Save();

                    Waiting2Seconds();

                    testEnded = true;
                    timeToEnd = 3;
                }
                else if (audioSource.isPlaying)
                {
                    transform.GetChild(1).GetComponent<Text>().text = "HIT STEP BUTTON WHEN YOU HEAR THE BEAT\nENDING IN " + (audioSource.clip.length - audioSource.time).ToString("0.0") + " SECONDS.";
                }
            }
            else
            {
                timeToStart -= Time.deltaTime;
                transform.GetChild(1).GetComponent<Text>().text = "(HINT: NO NEED TO PRESS THE BUTTON ON ALL BEATS, TRY TO RECOGNIZE RHYTHM BEFORE STARTING PUSHING THE BUTTON)\nSTARTS IN\n" + timeToStart.ToString("0.0") + " SECONDS.";
            }
        }

        if (testEnded) 
        {
            timeToEnd -= Time.deltaTime;
        }

    }

    void InputForDelayTest()
    {
        start = true;  
        inputTime[Mathf.RoundToInt((audioSource.time / audioSource.clip.length) * 16)] = audioSource.time;
    }

    public void CalculateAudioDelay() 
    {

        List<float> distances = new List<float>();

        for (int i = 0; i < 16; i++)
        {
            if(inputTime[i] != 0)
                distances.Add(inputTime[i] - musicTime[i]);
        }

        averageAudioDelay = 0;

        foreach (float distance in distances) 
        {
            averageAudioDelay += distance / distances.Count;
        }
    }

    private IEnumerator Waiting2Seconds()
    {
        yield return new WaitForSeconds(2f);
        // Your code here
    }


}
