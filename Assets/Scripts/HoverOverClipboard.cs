using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOverClipboard : MonoBehaviour
{    
    
    private bool isClosed = true;
    private bool isHovering = false;
    private bool isAnimating = false;
    private float animationTime = .5f;
    [SerializeField] Vector3 closedPos;
    [SerializeField] Vector3 openedPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (isHovering && isClosed)
        {
            open();
        }
        if (!isHovering && !isClosed)
        {
            close();
        }
    }

    public void enter()
    {
        isHovering = true;
    }

    public void exit()
    {
        isHovering = false;
    }

    void open() 
    {
        if (isAnimating)
            return;
        
        isClosed = false;
        StartCoroutine(LerpPosition(openedPos, animationTime));
    }

    void close()
    {
        if (isAnimating)
            return;
        
        isClosed = true;
        StartCoroutine(LerpPosition(closedPos, animationTime));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        float t = 0;
        Vector3 startPosition = GetComponent<RectTransform>().anchoredPosition;
        isAnimating = true;

        while (time < duration)
        {
            t = time/duration;
            t = easeOutQuint(t);
            GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }
        GetComponent<RectTransform>().anchoredPosition = targetPosition;
        isAnimating = false;
    }
    
    // ty easings.net
    float easeOutExpo(float x) 
    {
        return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
    }

    float easeOutBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;

        return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
    }
    float easeOutQuint(float x)
    {
        return 1 - Mathf.Pow(1 - x, 5);
    }    
}
