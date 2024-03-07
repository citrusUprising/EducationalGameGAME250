using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hoverOverCabinet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        Color temp = this.GetComponent<SpriteRenderer>().color;
        temp.a = 0;
        //Debug.Log ("I am invisible");
        this.GetComponent<SpriteRenderer>().color = temp;
    }

    void OnMouseExit()
    {
        Color temp = this.GetComponent<SpriteRenderer>().color;
        temp.a = 255;
        //Debug.Log ("I am visible");
        this.GetComponent<SpriteRenderer>().color = temp;
    }
}
