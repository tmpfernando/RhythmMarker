using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SoundController : MonoBehaviour
{
    public static float audioDelay;

    public static AudioSource audioSource;

    public static UnityEvent musicIsPlaying;
    public static UnityEvent musicIsNotPlaying;

    public static List<MusicInfo> Musics;
    public TextAsset musicInfo;

    float multiplier = 2.0f;
    
    public static int musicIndex = 0;

    public GameObject bar;

    int barQuantitie = 10;
    float[] barHeights;
    float[] barHeightsBuffer;
    public float yValue = 0.0f;

    public GameObject plusMessage;

    public static float gradeTransparency = 0.05f;

    bool rhythmAnalysis;
    public static int rhythmScore = 0;
    int rhythmScoreCombo = 0;
    float rhythnScoreCryteria = 0.0f;
    float beatTime = 0.0f;
    float stepTime = 0.0f;
    public bool onBeat2;

    public static int melodyScore = 0;
    public float dancerMelodyValue = 0.0f;
    public float melodyAnalisysDelay = 0.0f;
    public static float melodyValue = 0;
    public static float melodyValueMax = 0;

    List<float> MusicStatus;
    List<float> DancerStatus;


    private void Awake()
    {
        musicIsPlaying = new UnityEvent();
        musicIsNotPlaying = new UnityEvent();

        audioSource = GetComponent<AudioSource>();
        plusMessage = Resources.Load<GameObject>("Prefabs/Overlay/ScoreUpdate");
        bar = Resources.Load<GameObject>("Prefabs/Bar");

    }

    //Start is called before the first frame update
    void Start()
    {

        audioDelay = PlayerPrefs.GetFloat("averageDelay");
        Debug.Log(audioDelay);

        Musics = new List<MusicInfo>();
        LoadMusicInfo();

        InputSetup.anyStepOn.AddListener(ScoringRule);
        InputSetup.playButton.AddListener(PlayMusic);
        InputSetup.previousMusicButton.AddListener(PreviousMusic);
        InputSetup.nextMusicButton.AddListener(NextMusic);

        MusicStatus = new List<float>();
        DancerStatus = new List<float>();

        barHeights = new float[barQuantitie];
        barHeightsBuffer = new float[barQuantitie];

        //Catch and Define AudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = 0.75f;

        musicIndex = 0;
        audioSource.clip = Musics[musicIndex].musicArchive;

        onBeat2 = false;
        rhythmAnalysis = false;

    }


    //Update is called once per frame
    void Update()
    {

        //Updates while the music is playing
        if (audioSource.isPlaying)
        {
            musicIsPlaying?.Invoke();
            //UpdateAudioSpectrumBars();
            BeatVerificationByMusicInfo(musicIndex);
            MelodyVerification();
        }
        else {
            musicIsNotPlaying?.Invoke();
            SetVariablesToZero();
        }

        UpdateGradeColor();

    }

    #region PLAYLIST_ACTIONS

    //Method to start music
    public void PlayMusic()
    {

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            musicIsNotPlaying?.Invoke();
            SetVariablesToZero();
            //CreateAudioSpectrum(false);
        }
        else
        {
            //CreateAudioSpectrum(true);
            audioSource.Play();
            musicIsPlaying?.Invoke();
        }
        Debug.Log("Play Music EVENT");
    }

    //Method to change music
    public void NextMusic()
    {

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            //CreateAudioSpectrum(false);
            musicIsNotPlaying?.Invoke();
            SetVariablesToZero();
        }

        if (musicIndex >= Musics.Count - 1)
        {
            musicIndex = 0;
        }
        else
        {
            musicIndex++;
        }
        audioSource.clip = Musics[musicIndex].musicArchive;
        //CreateAudioSpectrum(true);
        audioSource.Play();
        musicIsPlaying?.Invoke();
        Debug.Log("Next Music EVENT");
    }

    //Method to change music
    public void PreviousMusic()
    {

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            //CreateAudioSpectrum(false);
            musicIsNotPlaying?.Invoke();
            SetVariablesToZero();
        }

        if (musicIndex <= 0)
        {
            musicIndex = Musics.Count - 1;
        }
        else
        {
            musicIndex--;
        }
        audioSource.clip = Musics[musicIndex].musicArchive;
        //CreateAudioSpectrum(true);
        audioSource.Play();
        musicIsPlaying?.Invoke();
        Debug.Log("Previous Music EVENT");
    }

    //Method used to load information from CSV File in Resources folder
    public void LoadMusicInfo()
    {

        musicInfo = Resources.Load<TextAsset>("MusicInformation");
        string[] data = musicInfo.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            if (row[1] != "")
            {
                MusicInfo m = new MusicInfo();

                m.musicName = row[1];
                m.artistName = row[2];
                m.link = row[3];

                string path = "Musics/" + row[1] + " by " + row[2];
                m.musicArchive = Resources.Load<AudioClip>(path);

                int.TryParse(row[4], out m.bpm1);
                float.TryParse(row[5], out m.iniInterval1);
                float.TryParse(row[6], out m.endInterval1);
                //Interval time was in Mile seconds, so, this will be divided by 1000 to convert to seconds
                m.iniInterval1 = m.iniInterval1 / 1000;
                m.endInterval1 = m.endInterval1 / 1000;

                int.TryParse(row[7], out m.bpm2);
                float.TryParse(row[8], out m.iniInterval2);
                float.TryParse(row[9], out m.endInterval2);
                m.iniInterval2 = m.iniInterval2 / 1000;
                m.endInterval2 = m.endInterval2 / 1000;

                int.TryParse(row[10], out m.bpm3);
                float.TryParse(row[11], out m.iniInterval3);
                float.TryParse(row[12], out m.endInterval3);
                m.iniInterval3 = m.iniInterval3 / 1000;
                m.endInterval3 = m.endInterval3 / 1000;

                int.TryParse(row[13], out m.bpm4);
                float.TryParse(row[14], out m.iniInterval4);
                float.TryParse(row[15], out m.endInterval4);
                m.iniInterval4 = m.iniInterval4 / 1000;
                m.endInterval4 = m.endInterval4 / 1000;

                int.TryParse(row[16], out m.bpm5);
                float.TryParse(row[17], out m.iniInterval5);
                float.TryParse(row[18], out m.endInterval5);
                m.iniInterval5 = m.iniInterval5 / 1000;
                m.endInterval5 = m.endInterval5 / 1000;

                int.TryParse(row[19], out m.bpm6);
                float.TryParse(row[20], out m.iniInterval6);
                float.TryParse(row[21], out m.endInterval6);
                m.iniInterval6 = m.iniInterval6 / 1000;
                m.endInterval6 = m.endInterval6 / 1000;

                int.TryParse(row[22], out m.bpm7);
                float.TryParse(row[23], out m.iniInterval7);
                float.TryParse(row[24], out m.endInterval7);
                m.iniInterval7 = m.iniInterval7 / 1000;
                m.endInterval7 = m.endInterval7 / 1000;

                Musics.Add(m);

                Debug.Log(m.musicArchive.name);

            }
        }

    }

    #endregion

    //Change the color of that grade spawned at player position
    public void UpdateGradeColor() {
        //updade grade color

        if (onBeat2)
        {
            foreach (GameObject grade in GameObject.FindGameObjectsWithTag("grade"))
            {
                grade.GetComponent<LineRenderer>().startColor = new Color(1, 1, 1, 0.3f);
                grade.GetComponent<LineRenderer>().endColor = new Color(1, 1, 1, 0.1f);
            }
        }
        else {
            foreach (GameObject grade in GameObject.FindGameObjectsWithTag("grade"))
            {
                if (grade.GetComponent<LineRenderer>().startColor.a > 0.06f)
                {
                    grade.GetComponent<LineRenderer>().startColor = new Color(1, 1, 1, grade.GetComponent<LineRenderer>().startColor.a - 0.01f);
                    grade.GetComponent<LineRenderer>().endColor = new Color(1, 1, 1, grade.GetComponent<LineRenderer>().startColor.a - 0.01f);
                }
            }
        }
    }

    //Change the heights of audio spectrum bars
    public void UpdateAudioSpectrumBars() {

        float[] spectrum0 = new float[1024];
        float[] spectrum1 = new float[1024];

        AudioListener.GetSpectrumData(spectrum0, 0, FFTWindow.Rectangular);
        AudioListener.GetSpectrumData(spectrum1, 1, FFTWindow.Rectangular);

        for (int i = 0; i < barQuantitie; i++)
        {
            
            barHeightsBuffer[i] = barHeights[i];

            barHeights[i] = 0.0f;

            for (int k = Mathf.RoundToInt(Mathf.Pow(2, i)) - 1; k < Mathf.Pow(2, i + 1) - 1; k++)
            {
                barHeights[i] += spectrum0[k] / 2 + spectrum1[k] / 2;
            }

            //decreace slowly
            if (barHeightsBuffer[i] > barHeights[i])
            {
                yValue = barHeights[i] - (barHeights[i] - barHeightsBuffer[i]);
            }
            //increace fast
            else
            {
                yValue = barHeights[i];
            }

            transform.GetChild(i).GetComponent<Transform>().localScale =
                new Vector3(transform.GetChild(i).GetComponent<Transform>().localScale.x,
                multiplier * yValue,
                transform.GetChild(i).GetComponent<Transform>().localScale.z);

            //update bar color
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Clamp(yValue, 0.1f, 1.0f));

            
        }

        // Melodic Analisys
        if (melodyValue > melodyValueMax)
            melodyValueMax = melodyValue;

        //I was using range 7~128 before
        melodyValue = 0.0f;
        for (int k = 0; k < 1023; k++)
        {
            melodyValue += spectrum0[k] / 2 + spectrum1[k] / 2;
        }

    }

    //Rhythm Scorring Rule
    public void ScoringRule()
    {
        //call it when any step made
        if (rhythmAnalysis)
        {
            stepTime = audioSource.time;

            rhythnScoreCryteria = - audioDelay + stepTime - beatTime;

            if (Mathf.Abs(rhythnScoreCryteria) <= 0.15f)
            {
                rhythmScore++;
                rhythmScoreCombo++;
                if (rhythmScoreCombo % 5 == 0 && rhythmScoreCombo > 0)
                    PlusMessage(rhythmScoreCombo, "RHYTHM");
            }
            else
            {
                rhythmScore--;
                rhythmScoreCombo = 0;
                PlusMessage(rhythmScoreCombo, "MISS RITHM");
            }
        }
    }

    //This method is used to analyse beats based on BPM
    public void BeatVerificationByMusicInfo(int musicIndex) {

        if (onBeat2)
        {
            beatTime = audioSource.time;
            onBeat2 = false;
        }

        if (audioSource.time > Musics[musicIndex].iniInterval1 && audioSource.time < Musics[musicIndex].endInterval1)
        {
            if (((audioSource.time - Musics[musicIndex].iniInterval1) % (60.0f / Musics[musicIndex].bpm1) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }

        }
        else if (audioSource.time > Musics[musicIndex].iniInterval2 && audioSource.time < Musics[musicIndex].endInterval2)
        {
            if (((audioSource.time - Musics[musicIndex].iniInterval2) % (60.0f / Musics[musicIndex].bpm2) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }
        }
        else if (audioSource.time > Musics[musicIndex].iniInterval3 && audioSource.time < Musics[musicIndex].endInterval3)
        {
            if (((audioSource.time - Musics[musicIndex].iniInterval3) % (60.0f / Musics[musicIndex].bpm3) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }
        }
        else if (audioSource.time > Musics[musicIndex].iniInterval4 && audioSource.time < Musics[musicIndex].endInterval4)
        {
            if (((audioSource.time - Musics[musicIndex].iniInterval4) % (60.0f / Musics[musicIndex].bpm3) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }
        }
        else if (audioSource.time > Musics[musicIndex].iniInterval5 && audioSource.time < Musics[musicIndex].endInterval5)
        {
            if (((audioSource.time - Musics[musicIndex].iniInterval5) % (60.0f / Musics[musicIndex].bpm3) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }
        }
        else if (audioSource.time > Musics[musicIndex].iniInterval6 && audioSource.time < Musics[musicIndex].endInterval6)
        {
            if (((audioSource.time - Musics[musicIndex].iniInterval6) % (60.0f / Musics[musicIndex].bpm3) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }
        }
        else if (audioSource.time > Musics[musicIndex].iniInterval7 && audioSource.time < Musics[musicIndex].endInterval7)
        {
            if (((audioSource.time - Musics[musicIndex].iniInterval7) % (60.0f / Musics[musicIndex].bpm3) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }
        }
        else {
            rhythmAnalysis = false;
        }

    }

    //This method is used to compare audio clip melody behavior with player arms behavior
    public void MelodyVerification() {

        //Melody value is defined on a specific iterval of audio spectrum
        float melodyValue01 = Mathf.InverseLerp(0.0f, melodyValueMax, melodyValue);
        dancerMelodyValue = -Controller.leftArmRotation + Controller.rightArmRotation;
        melodyAnalisysDelay += Time.deltaTime;

        if (melodyAnalisysDelay > 0.2f)
        {
            MusicStatus.Add(melodyValue01);
            DancerStatus.Add(dancerMelodyValue);
            melodyAnalisysDelay = 0.0f;
        }

        if (MusicStatus.Count > 5)
        {

            int melodyPoint = 0;

            for (int i = 1; i < MusicStatus.Count; i++)
            {

                if (MusicStatus[i] - MusicStatus[i - 1] > 0.0f
                    && DancerStatus[i] - DancerStatus[i - 1] > 0.0f)
                    melodyPoint++;

                else if (MusicStatus[i] - MusicStatus[i - 1] < 0.0f
                    && DancerStatus[i] - DancerStatus[i - 1] < 0.0f)
                    melodyPoint++;

                //else if (MusicStatus[i] - MusicStatus[i - 1] == 0.0f
                //    && DancerStatus[i] - DancerStatus[i - 1] == 0.0f)
                //    melodyPoint++;
            }

            if (melodyPoint > 1)
            {
                melodyScore += melodyPoint;
                PlusMessage(melodyPoint, "MELODY");
            }


            MusicStatus.Clear();
            DancerStatus.Clear();
            melodyValueMax = melodyValueMax / 2;
        }
    }

    //this method is called to create audio spectrum in scene
    public void CreateAudioSpectrum(bool create)
    {
        if (create) {

            float distance = 0.0f;

            for (int i = 0; i < barQuantitie; i++)
            {
                Instantiate(bar, new Vector3(Mathf.RoundToInt(Controller.bodyPos.x) + distance - 2, Mathf.RoundToInt(Controller.bodyPos.y), 0.0f), Quaternion.identity, GameObject.Find("SoundSystem").transform);
                distance += 0.4f;
            }
        }
        else {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("bar"))
            {
                Object.Destroy(obj);
            }
        }
    }

    //Set zero for a few variables that have to be zero when stop music
    public void SetVariablesToZero() {
        melodyScore = 0;
        rhythmScore = 0;
        beatTime = 0.0f;
        stepTime = 0.0f;
        onBeat2 = false;
        rhythmAnalysis = false;
        rhythnScoreCryteria = 0.0f;
        rhythmScoreCombo = 0;
    }

    //Method to sendo combo visual feedbacks
    public void PlusMessage(int amount, string scoreType)
    {
        ScoreUpdate.scoreAmount = amount;
        ScoreUpdate.scoreUpdate = scoreType;
        Instantiate(plusMessage, GameObject.Find("Canvas").transform.position + new Vector3(0, Screen.height / 5, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
    }
}
