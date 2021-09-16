using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOrganizer : MonoBehaviour
{
    List<GameObject> bodies;
    List<GameObject> orderedBodies;
    List<float> bodiesHeights;

    // Start is called before the first frame update
    void Start()
    {

        InputSetup.anyStepOn.AddListener(LayerUpdate);
        SoundController.beatEvent.AddListener(LayerUpdate);

        bodies = new List<GameObject>();
        orderedBodies = new List<GameObject>();
        bodiesHeights = new List<float>();

    }

    void LayerUpdate() 
    {
        //Clear lists
        bodies.Clear();
        orderedBodies.Clear();
        bodiesHeights.Clear();

        //Fill bodies list with all players and npcs bodies
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("playerBody"))
        {
            bodies.Add(obj);
            bodiesHeights.Add(obj.transform.position.y);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("npcBody"))
        {
            bodies.Add(obj);
            bodiesHeights.Add(obj.transform.position.y);
        }

        //if have more than one body in scene it will fill ordered bodies list
        

        while (bodies.Count > 1) 
        {
            float heightest = -9999999999999;
            int heightestIndex = 0;

            for (int i = 0; i < bodies.Count; i++)
            {
                if (bodiesHeights[i] > heightest)
                {
                    heightest = bodiesHeights[i];
                    heightestIndex = i;
                }
            }

            orderedBodies.Add(bodies[heightestIndex]);
            bodies.Remove(bodies[heightestIndex]);
            bodiesHeights.Remove(bodiesHeights[heightestIndex]);
        }

        orderedBodies.Add(bodies[0]);

        for(int i = 0; i < orderedBodies.Count; i++) 
        {
            //body object
            orderedBodies[i].GetComponent<SpriteRenderer>().sortingOrder = i;
            //head object
            orderedBodies[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = i;
            //left arm object
            orderedBodies[i].transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = i;
            //right arm object
            orderedBodies[i].transform.GetChild(2).transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = i;
            //left leg object
            orderedBodies[i].transform.parent.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = i;
            //right leg object
            orderedBodies[i].transform.parent.transform.GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = i;
        }

    }
}
