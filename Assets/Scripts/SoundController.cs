using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicalPhraseStatus { withoutPhrase, withinPhrase };
public enum Beat { onBeat, offBeat }

public class SoundController : MonoBehaviour
{
    public GameObject plusMessage;

    public static List<MusicInfo> Musics = new List<MusicInfo>();
    public TextAsset musicInfo;

    public MusicalPhraseStatus musicalPhraseStatus;
    public Beat beat;

    public static bool rhythmAnalysis;
    public static int melodyScore = 0;
    public static int rhythmScore = 0;
    public int rhythmScoreCombo = 0;
    public static float rhythnScoreCryteria = 0.0f;
    public static float beatTime = 0.0f;
    public static float stepTime = 0.0f;
    public bool stepXDone = false;
    public bool stepYDone = false;

    public float multiplier = 2.0f;

    public static AudioSource audioSource;
    public AudioClip[] audioClip = new AudioClip[23];
    public static int musicIndex = 0;

    public float timeCount;

    public static bool onBeat2;
    public bool musicalPhrase;

    public GameObject bar;
    int barQuantitie = 10;
    public float[] barHeights;
    public float[] barHeightsBuffer;
    public float yValue = 0.0f;

    public static float gradeTransparency = 0.05f;

    public float melodyValue = 0.0f;
    public float melodyValue01 = 0.0f;
    public float melodyValueMax = 0.0f;
    public float dancerMelodyValue = 0.0f;

    public List<float> MusicStatus;
    public List<float> DancerStatus;

    public float melodyAnalisysDelay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

        LoadMusicInfo();

        barHeights = new float[barQuantitie];
        barHeightsBuffer = new float[barQuantitie];

        musicalPhraseStatus = MusicalPhraseStatus.withoutPhrase;
        beat = Beat.offBeat;


        //Catch and Define AudioSource
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = 0.75f;
        audioSource.clip = audioClip[musicIndex];

        onBeat2 = false;
        rhythmAnalysis = false;

        //Defines size of Texts on CANVAS
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Change music
        if (Input.GetButtonDown("LB") || Input.GetKeyDown(KeyCode.Comma)) {
            audioSource.Stop();
            SetVariablesToZero();
            if (musicIndex <= 0)
            {
                musicIndex = audioClip.Length - 1;
            }
            else
            {
                musicIndex--;
            }
            audioSource.clip = audioClip[musicIndex];
            audioSource.Play();
            CreateAudioSpectrum(true);
        } else if (Input.GetButtonDown("RB") || Input.GetKeyDown(KeyCode.Period)) {
            audioSource.Stop();
            SetVariablesToZero();
            CreateAudioSpectrum(false);
            if (musicIndex >= audioClip.Length - 1) {
                musicIndex = 0;
            }
            else {
                musicIndex++;
            }
            audioSource.clip = audioClip[musicIndex];
            audioSource.Play();
            CreateAudioSpectrum(true);
        }

        //Play and Stop Music
        if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("START"))
        {
            if (audioSource.isPlaying) {
                audioSource.Stop();
                SetVariablesToZero();
            }
            else {
                audioSource.Play();
                CreateAudioSpectrum(true);
            }
        }

        //Updates while the music is playing
        if (audioSource.isPlaying)
        {
            UpdateAudioSpectrumBars();
            BeatVerificationByMusicInfo(musicIndex);
            MelodyVerification();
        }
        else {
            SetVariablesToZero();
        }
        UpdateGradeColor();
    }

    //Method used to load information from CSV File in Resources folder
    public void LoadMusicInfo() {

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

            }
        }

        foreach (MusicInfo m in Musics)
        {
            Debug.Log(m.musicName + " by " + m.artistName + " has been added to Music List");
        }
    }

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

            GameObject.Find("SoundSystem").transform.GetChild(i).GetComponent<Transform>().localScale =
                new Vector3(GameObject.Find("SoundSystem").transform.GetChild(i).GetComponent<Transform>().localScale.x,
                multiplier * yValue,
                GameObject.Find("SoundSystem").transform.GetChild(i).GetComponent<Transform>().localScale.z);

            //update bar color
            GameObject.Find("SoundSystem").transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Clamp(yValue, 0.1f, 1.0f));

            
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

    //This method is used to analyse beats based on BPM
    public void BeatVerificationByMusicInfo(int musicIndex) {

        if (onBeat2)
        {
            beatTime = audioSource.time;
            onBeat2 = false;
        }

        if (audioSource.time > Musics[musicIndex].iniInterval1 && audioSource.time < Musics[musicIndex].endInterval1)
        {
            RhythmScorringRule();
            if (((audioSource.time - Musics[musicIndex].iniInterval1) % (60.0f / Musics[musicIndex].bpm1) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }

        }
        else if (audioSource.time > Musics[musicIndex].iniInterval2 && audioSource.time < Musics[musicIndex].endInterval2)
        {
            RhythmScorringRule();
            if (((audioSource.time - Musics[musicIndex].iniInterval2) % (60.0f / Musics[musicIndex].bpm2) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }
        }
        else if (audioSource.time > Musics[musicIndex].iniInterval3 && audioSource.time < Musics[musicIndex].endInterval3)
        {
            RhythmScorringRule();
            if (((audioSource.time - Musics[musicIndex].iniInterval3) % (60.0f / Musics[musicIndex].bpm3) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }
        }
        else if (audioSource.time > Musics[musicIndex].iniInterval4 && audioSource.time < Musics[musicIndex].endInterval4)
        {
            RhythmScorringRule();
            if (((audioSource.time - Musics[musicIndex].iniInterval4) % (60.0f / Musics[musicIndex].bpm3) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }
        }
        else if (audioSource.time > Musics[musicIndex].iniInterval5 && audioSource.time < Musics[musicIndex].endInterval5)
        {
            RhythmScorringRule();
            if (((audioSource.time - Musics[musicIndex].iniInterval5) % (60.0f / Musics[musicIndex].bpm3) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }
        }
        else if (audioSource.time > Musics[musicIndex].iniInterval6 && audioSource.time < Musics[musicIndex].endInterval6)
        {
            RhythmScorringRule();
            if (((audioSource.time - Musics[musicIndex].iniInterval6) % (60.0f / Musics[musicIndex].bpm3) <= 0.05f))
            {
                rhythmAnalysis = true;
                onBeat2 = true;
            }
        }
        else if (audioSource.time > Musics[musicIndex].iniInterval7 && audioSource.time < Musics[musicIndex].endInterval7)
        {
            RhythmScorringRule();
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

    public void RhythmScorringRule() {
        //ACTUAL Rhythm scoring rule

        if (Input.GetKeyDown(KeyCode.W)
                || Input.GetKeyDown(KeyCode.A)
                || Input.GetKeyDown(KeyCode.S)
                || Input.GetKeyDown(KeyCode.D)
                || (Input.GetAxis("LeftAnalogX") > 0.0F && stepXDone == false)
                || (Input.GetAxis("LeftAnalogX") < 0.0F && stepXDone == false)
                || (Input.GetAxis("LeftAnalogY") > 0.0F && stepYDone == false)
                || (Input.GetAxis("LeftAnalogY") < 0.0F && stepYDone == false)
                || (Input.GetAxis("DPADX") > 0.0F && stepXDone == false)
                || (Input.GetAxis("DPADX") < 0.0F && stepXDone == false)
                || (Input.GetAxis("DPADY") > 0.0F && stepYDone == false)
                || (Input.GetAxis("DPADY") < 0.0F && stepYDone == false))
        {
            stepTime = audioSource.time;
            stepXDone = true;
            stepYDone = true;

            rhythnScoreCryteria = stepTime - beatTime;

            if (Mathf.Abs(rhythnScoreCryteria) <= 0.2f)
            {
                rhythmScore++;
                rhythmScoreCombo++;
                if (rhythmScoreCombo % 5 == 0)
                    PlusMessage(rhythmScoreCombo, "RHYTHM");
            }
            else
            {
                rhythmScore--;
                rhythmScoreCombo = 0;
                PlusMessage(rhythmScoreCombo, "MISS RITHM");
            }
        }

        //Reset step condition for X Axys
        if (Input.GetAxis("DPADX") == 0.0f && Input.GetAxis("LeftAnalogX") == 0.0f)
            stepXDone = false;

        //Reset step condition for Y Axys
        if (Input.GetAxis("DPADY") == 0.0f && Input.GetAxis("LeftAnalogY") == 0.0f)
            stepYDone = false;

        //END of Actual Rhythm scoring rule
    }

    //This method is used to compare audio clip melody behavior with player arms behavior
    public void MelodyVerification() {


        if (audioSource.time > 0.0f && audioSource.time < 6000.0f)
        {
            musicalPhraseStatus = MusicalPhraseStatus.withinPhrase;
        }
        else
        {
            musicalPhraseStatus = MusicalPhraseStatus.withoutPhrase;
        }

        if (musicalPhraseStatus == MusicalPhraseStatus.withinPhrase)
        {
            //Debug.LogError("tá rolando");

            //Melody value is defined on a specific iterval of audio spectrum
            melodyValue01 = Mathf.InverseLerp(0.0f, melodyValueMax, melodyValue);
            dancerMelodyValue = -Controller.leftArmRotation + Controller.rightArmRotation;
            melodyAnalisysDelay += Time.deltaTime;

            if (melodyAnalisysDelay > 0.2f) {
                MusicStatus.Add(melodyValue01);
                DancerStatus.Add(dancerMelodyValue);
                melodyAnalisysDelay = 0.0f;
            }

            if (MusicStatus.Count > 4) {

                int melodyPoint = 0;

                for (int i = 1; i < MusicStatus.Count; i++) {

                    if (MusicStatus[i] - MusicStatus[i - 1] > 0.0f
                        && DancerStatus[i] - DancerStatus[i - 1] > 0.0f)
                        melodyPoint++;

                    else if (MusicStatus[i] - MusicStatus[i - 1] < 0.0f
                        && DancerStatus[i] - DancerStatus[i - 1] < 0.0f)
                        melodyPoint++;

                    else if (MusicStatus[i] - MusicStatus[i - 1] == 0.0f
                        && DancerStatus[i] - DancerStatus[i - 1] == 0.0f)
                        melodyPoint++;
                }

                if (melodyPoint > 1) {
                    melodyScore+= melodyPoint;
                    PlusMessage(melodyPoint, "MELODY");
                }
                    

                MusicStatus.Clear();
                DancerStatus.Clear();
                melodyValueMax = melodyValueMax / 2;
            }
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
                Destroy(obj);
            }
        }
    }

    //Set zero for a few variables that have to be zero when stop music
    public void SetVariablesToZero() {
        timeCount = 0.0f;
        melodyScore = 0;
        rhythmScore = 0;
        beatTime = 0.0f;
        stepTime = 0.0f;
        onBeat2 = false;
        rhythmAnalysis = false;
        rhythnScoreCryteria = 0.0f;
        CreateAudioSpectrum(false);
        rhythmScoreCombo = 0;
    }

    public void PlusMessage(int amount, string scoreType) {
        ScoreUpdate.scoreAmount = amount;
        ScoreUpdate.scoreUpdate = scoreType;
        Instantiate(plusMessage, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
    }
}
