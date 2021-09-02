using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuScript : MonoBehaviour
{

    public static UnityEvent audioVisualizerON;
    public static UnityEvent audioVisualizerOFF;

    public static List<GameObject> Interactions;


    public GameObject audioVisualizer;
    // Start is called before the first frame update
    void Start()
    {
        audioVisualizer = Resources.Load<GameObject>("Prefabs/Overlay/AudioVisualizer");
        Instantiate(audioVisualizer, transform.position, Quaternion.identity, transform);
    } 

    // Update is called once per frame
    void Update()
    {

    }

}
