using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    float bg_x;
    float bg_y;
    // Start is called before the first frame update
    void Start()
    {
        bg_x = GameObject.Find("Background").GetComponent<Transform>().position.x;
        bg_y = GameObject.Find("Background").GetComponent<Transform>().position.y;
        Vector3 backgroundPosition = new Vector3(bg_x, bg_y, -10f);
        this.transform.position = backgroundPosition; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
