using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hoverOverCabinet : MonoBehaviour
{

    [SerializeField] private GameObject openSounds;
    [SerializeField] private GameObject closeSounds;
    AudioSource open;
    AudioSource close;
    // Start is called before the first frame update
    void Start()
    {
        open = openSounds.GetComponent<AudioSource>();
        close = closeSounds.GetComponent<AudioSource>();
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
        open.Stop();
        close.Stop();
        open.Play(0);
    }

    void OnMouseExit()
    {
        Color temp = this.GetComponent<SpriteRenderer>().color;
        temp.a = 255;
        //Debug.Log ("I am visible");
        this.GetComponent<SpriteRenderer>().color = temp;
        open.Stop();
        close.Stop();
        close.Play(0);
    }
}
