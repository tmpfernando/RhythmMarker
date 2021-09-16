using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuVideoDelay : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip rhythmTrack;
    Image image;

    public int bpm;
    public float startBeatTime;
    public bool start;
    public bool testEnded;

    public float[] inputTime;
    public float[] flashTime;

    public float averageVideoDelay;
    public float timeToStart;
    public float timeToEnd;

    public float color;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("videoDelayMenu").Length > 1) 
        {
            Object.Destroy(gameObject);
        }
        else 
        {
            color = 0.1f;

            image = GetComponent<Image>();
            audioSource = GetComponent<AudioSource>();
            rhythmTrack = Resources.Load<AudioClip>("Musics/RhythmTrack");
            audioSource.clip = rhythmTrack;
            audioSource.playOnAwake = false;

            audioSource.volume = 0.01f;

            testEnded = false;
            start = false;
            bpm = 80;
            startBeatTime = 0.005f;

            InputSetup.anyStepOn.AddListener(InputForDelayTest);

            flashTime = new float[16];
            inputTime = new float[16];

            for (int i = 0; i < flashTime.Length; i++)
            {
                flashTime[i] = i * 0.75f + startBeatTime;
            }

            transform.GetChild(1).GetComponent<Text>().text = "PRESS ANY STEP BUTTON (OR SWIPE) TO START.\nTHEN, HIT STEP BUTTON (OR SWIPE) WHEN YOU SEE SCREEN FLASHES\n(HINT: NO NEED TO PRESS THE BUTTON ON ALL FLASHES, TRY TO RECOGNIZE RHYTHM BEFORE STARTING PUSHING THE BUTTON)";

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
                ScreenFlash();

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                else if ((audioSource.clip.length - audioSource.time) <= 0.1f)
                {
                    audioSource.Stop();
                    start = false;
                    CalculateVideoDelay();

                    if(SoundController.gradeDelay < 0 || SoundController.gradeDelay > 1) 
                    {
                        transform.GetChild(1).GetComponent<Text>().text = "SORRY, COULD YOU MAKE THIS TEST AGAIN???\n(HINT: NO NEED TO PRESS THE BUTTON ON ALL FLASHES, TRY TO RECOGNIZE RHYTHM BEFORE STARTING PUSHING THE BUTTON)";
                    }
                    transform.GetChild(1).GetComponent<Text>().text = "CALCULATED DELAY: " + SoundController.gradeDelay + "SECONDS";
                    timeToStart = 3;

                    SoundController.gradeDelay = averageVideoDelay;
                    PlayerPrefs.SetFloat("gradeDelay", averageVideoDelay);
                    PlayerPrefs.Save();

                    Waiting2Seconds();

                    testEnded = true;
                    timeToEnd = 3;

                }
                else if (audioSource.isPlaying)
                {
                    transform.GetChild(1).GetComponent<Text>().text = "HIT STEP BUTTON WHEN SCREEN FLASHES\nENDING IN " + (audioSource.clip.length - audioSource.time).ToString("0.0") + " SECONDS.";
                }
            }
            else
            {
                timeToStart -= Time.deltaTime;
                transform.GetChild(1).GetComponent<Text>().text = "(HINT: NO NEED TO PRESS THE BUTTON ON ALL FLASHES, TRY TO RECOGNIZE RHYTHM BEFORE STARTING PUSHING THE BUTTON)\nSTARTS IN\n" + timeToStart.ToString("0.0") + " SECONDS.";
            }
        }

        if (testEnded)
        {
            timeToEnd -= Time.deltaTime;
        }

    }

    void ScreenFlash() 
    {
        image.color = new Color(color, color, color, 1);

        if ((audioSource.time - startBeatTime) % (60.0f / bpm) <= 0.05f)
        {
            color = 0.5f;
        }
        else
        {
            if (color > 0.1f)
                color -= 0.05f;
        }
    }

    void InputForDelayTest()
    {
        start = true;
        inputTime[Mathf.RoundToInt((audioSource.time / audioSource.clip.length) * 16)] = audioSource.time;
    }

    public void CalculateVideoDelay()
    {
        List<float> distances = new List<float>();

        for (int i = 0; i < 16; i++)
        {
            if (inputTime[i] != 0)
                distances.Add(inputTime[i] - flashTime[i]);
        }

        averageVideoDelay = 0;

        foreach (float distance in distances)
        {
            averageVideoDelay += distance / distances.Count;
        }
    }

    private IEnumerator Waiting2Seconds()
    {
        yield return new WaitForSeconds(2f);
        // Your code here
    }


}
