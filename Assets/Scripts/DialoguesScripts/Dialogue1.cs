using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue1 : MonoBehaviour
{
    //This dialogue is the introduce of NPC and the introduction to the walk mechanic
    public enum NPCColor { RED, GREEN, BLUE }
    public NPCColor npcColor;

    public Color color;

    GameObject npcPrefab;


    private void Awake()
    {
        npcPrefab = Resources.Load<GameObject>("Prefabs/NPCs/DancerNPC");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            npcColor = (NPCColor)Random.Range(0, 3);

            if (npcColor == NPCColor.RED)
                color = new Color(0.5f, 0, 0, 1);

            if (npcColor == NPCColor.GREEN)
                color = new Color(0, 0.5f, 0, 1);

            if (npcColor == NPCColor.BLUE)
                color = new Color(0, 0, 0.5f, 1);

            npcPrefab.name = npcColor.ToString();
            
            //Body
            npcPrefab.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color = color;
            //Left Leg
            npcPrefab.transform.GetChild(1).transform.GetComponent<SpriteRenderer>().color = color;
            //Right Leg
            npcPrefab.transform.GetChild(2).transform.GetComponent<SpriteRenderer>().color = color;
            //Head
            npcPrefab.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
            //Left Arm
            npcPrefab.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
            //Right Arm
            npcPrefab.transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;

            Instantiate(npcPrefab);
        }
    }
}
