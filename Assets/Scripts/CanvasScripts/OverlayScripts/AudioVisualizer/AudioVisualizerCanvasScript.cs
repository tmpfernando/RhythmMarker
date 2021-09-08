using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVisualizerCanvasScript : MonoBehaviour
{
    float[] audioSpectrum0;
    float[] audioSpectrum1;

    public float melodyValueLeft;
    public float melodyValueRight;
    public float melodyValueLeftBuffer;
    public float melodyValueRightBuffer;

    RectTransform rectTransformLeft;
    RectTransform rectTransformRight;

    public FFTWindow fftWindow;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("audioVisualizerCanvas").Length > 1) 
        {
            Destroy(gameObject);
        } 
        else 
        {
            ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
            SoundController.musicIsPlaying.AddListener(MusicIsPlaying);
            SoundController.musicIsNotPlaying.AddListener(MusicIsNotPlaying);

            rectTransformLeft = transform.GetChild(0).GetComponentInChildren<RectTransform>();
            rectTransformRight = transform.GetChild(1).GetComponentInChildren<RectTransform>();

            audioSpectrum0 = new float[64];
            audioSpectrum1 = new float[64];

            fftWindow = FFTWindow.Blackman;

            melodyValueLeftBuffer = 0;
            melodyValueRightBuffer = 0;

            ScreenSizeAdjustments();
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Melodic Analisys
        if (SoundController.melodyValue > SoundController.melodyValueMax)
            SoundController.melodyValueMax = SoundController.melodyValue;

    }

    void MusicIsPlaying()
    {

        //Show Audio Visualizer bars
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);

        //Get Auido Information
        AudioListener.GetSpectrumData(audioSpectrum0, 0, fftWindow);
        AudioListener.GetSpectrumData(audioSpectrum1, 1, fftWindow);

        //Set values that will be used to define bars heights
        melodyValueLeft = melodyValueLeftBuffer;
        melodyValueRight = melodyValueRightBuffer;

        //calculate audio spectrum
        for (int i = 0; i < audioSpectrum0.Length; i++)
        {
            melodyValueLeft += Mathf.Sqrt(Mathf.Pow(audioSpectrum0[i], 2));
            melodyValueRight += Mathf.Sqrt(Mathf.Pow(audioSpectrum1[i], 2));
        }

        //set the bars heights
        if (melodyValueLeft > melodyValueLeftBuffer)
            rectTransformLeft.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200 * melodyValueLeft);
        else
            rectTransformLeft.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200 * melodyValueLeft - (melodyValueLeftBuffer - melodyValueLeft) / 2);

        if (melodyValueRight > melodyValueRightBuffer)
            rectTransformRight.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200 * melodyValueRight);
        else
            rectTransformRight.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200 * melodyValueRight - (melodyValueRightBuffer - melodyValueRight) / 2);

        //calculate melodic value
        SoundController.melodyValue = melodyValueLeft / 2 + melodyValueRight / 2;

        // Melodic Analisys
        if (SoundController.melodyValue > SoundController.melodyValueMax)
            SoundController.melodyValueMax = SoundController.melodyValue;

    }

    void MusicIsNotPlaying() 
    {

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        SoundController.melodyValue = 0;

    }

    void ScreenSizeAdjustments()
    {
        //Updates widths of bars do 1 / 14 of screen widith
        rectTransformLeft.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 20);
        rectTransformRight.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 20);

        //Corrects the position of bars
        rectTransformLeft.anchoredPosition = new Vector2(-Screen.width / 2.2f, 0);
        rectTransformRight.anchoredPosition = new Vector2(+Screen.width / 2.2f, 0);
    }
}
