using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("musicListMenu").Length > 1) 
        {
            Object.Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
