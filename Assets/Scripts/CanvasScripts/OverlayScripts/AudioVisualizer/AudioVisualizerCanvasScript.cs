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
        } else 
        {

            SoundController.musicIsPlaying.AddListener(MusicIsPlaying);
            SoundController.musicIsNotPlaying.AddListener(MusicIsNotPlaying);

            rectTransformLeft = transform.GetChild(0).GetComponentInChildren<RectTransform>();
            rectTransformRight = transform.GetChild(1).GetComponentInChildren<RectTransform>();

            audioSpectrum0 = new float[64];
            audioSpectrum1 = new float[64];

            fftWindow = FFTWindow.Blackman;

            melodyValueLeftBuffer = 0;
            melodyValueRightBuffer = 0;


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

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);

        AudioListener.GetSpectrumData(audioSpectrum0, 0, fftWindow);
        AudioListener.GetSpectrumData(audioSpectrum0, 1, fftWindow);

        melodyValueLeft = melodyValueLeftBuffer;
        melodyValueRight = melodyValueRightBuffer;

        //calculate audio spectrum
        for (int i = 0; i < audioSpectrum0.Length; i++)
        {
            melodyValueLeft += Mathf.Sqrt(Mathf.Pow(audioSpectrum0[i], 2));
            melodyValueRight += Mathf.Sqrt(Mathf.Pow(audioSpectrum0[i], 2));
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

        //set the bars size
        rectTransformLeft.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 * melodyValueRight);
        rectTransformRight.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 * melodyValueRight);

        //calculate melodic value
        SoundController.melodyValue = melodyValueLeft / 2 + melodyValueRight / 2;

        // Melodic Analisys
        if (SoundController.melodyValue > SoundController.melodyValueMax)
            SoundController.melodyValueMax = SoundController.melodyValue;

        //Updates widths of bars do 1/14 of screen widith
        rectTransformLeft.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 14);
        rectTransformRight.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 14);

        //Corrects the position of bars
        rectTransformLeft.anchoredPosition = new Vector2(0, 0);
        rectTransformLeft.anchoredPosition = new Vector2(0, 0);
    }

    void MusicIsNotPlaying() {

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        SoundController.melodyValue = 0;

    }

}
