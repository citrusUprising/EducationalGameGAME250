using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBox : MonoBehaviour
{
    bool isFilledIn = false;
    bool isSafe = false;

    [SerializeField] Toggle toggleYes;
    [SerializeField] Toggle toggleNo;
    // Start is called before the first frame update
    public void checkYes()
    {
        if (!isFilledIn)
        {
            isSafe = true;
            isFilledIn = true;
            toggleYes.SetIsOnWithoutNotify(true);
            toggleNo.SetIsOnWithoutNotify(false);
            return;
        }


        if (!isSafe)
        {
            isSafe = true;
            isFilledIn = true;
            toggleYes.SetIsOnWithoutNotify(true);
            toggleNo.SetIsOnWithoutNotify(false);
        }
        else if (isSafe)
        {
            isSafe = false;
            isFilledIn = false;
            toggleYes.SetIsOnWithoutNotify(false);
            toggleNo.SetIsOnWithoutNotify(false);
        }
    }

    public void checkNo()
    {

        if (!isFilledIn)
        {
            isSafe = false;
            isFilledIn = true;
            toggleYes.SetIsOnWithoutNotify(false);
            toggleNo.SetIsOnWithoutNotify(true);
            return;
        }

        if (!isSafe)
        {
            isSafe = false;
            isFilledIn = false;
            toggleNo.SetIsOnWithoutNotify(false);
            toggleYes.SetIsOnWithoutNotify(false);
        }
        else if (isSafe)
        {
            isSafe = false;
            isFilledIn = true;
            toggleNo.SetIsOnWithoutNotify(true);
            toggleYes.SetIsOnWithoutNotify(false);
        }
    }
}
