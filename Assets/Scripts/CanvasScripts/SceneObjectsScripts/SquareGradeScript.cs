using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGradeScript : MonoBehaviour
{
    //Start is called before the first frame update
    void Start()
    {
        SoundController.beatEvent.AddListener(FlashGrarde);
        GetComponent<LineRenderer>().startColor = new Color(1, 1, 1, 0.05f);
        GetComponent<LineRenderer>().endColor = new Color(1, 1, 1, 0.025f);
    }

    //Update is called once per frame
    void Update()
    {

        if (GetComponent<LineRenderer>().startColor.a > 0.05f)
        {
            GetComponent<LineRenderer>().startColor = new Color(1, 1, 1, GetComponent<LineRenderer>().startColor.a - 0.01f);
        }

        if (GetComponent<LineRenderer>().endColor.a > 0.025f)
        {
            GetComponent<LineRenderer>().endColor = new Color(1, 1, 1, GetComponent<LineRenderer>().endColor.a - 0.01f);
        }
    }

    void FlashGrarde()
    {
        StartCoroutine(FlashWithDelay(SoundController.gradeDelay));
    }

    IEnumerator FlashWithDelay(float gradeDelay)
    {
        yield return new WaitForSeconds(gradeDelay);

        GetComponent<LineRenderer>().startColor = new Color(1, 1, 1, 0.3f);
        GetComponent<LineRenderer>().endColor = new Color(1, 1, 1, 0.15f);
    }
}
