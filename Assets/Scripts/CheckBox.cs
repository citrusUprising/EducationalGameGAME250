using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;

public class CheckBox : MonoBehaviour
{
    bool isFilledIn = false;
    bool isSafe = false;

    [SerializeField] Toggle toggleYes;
    [SerializeField] Toggle toggleNo;
    [SerializeField] Image wrongVerdict;
    [SerializeField] Image correctVerdict;
    [SerializeField] TMP_Text notesText;

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

    public void grade(bool rightAnswer)
    {
        Assert.IsTrue(isFilledIn);

        if (isSafe == rightAnswer)
        {
            correctVerdict.enabled = true;
        }
        else
        {
            wrongVerdict.enabled = true;
        }
    }
}
