using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textEditorScript : MonoBehaviour
{

    public TextMeshProUGUI TMProTextSpace;

    public void ChangeStackNumber(string stackNum)
    {
        TMProTextSpace.SetText(stackNum);
    }

    public void SetLevel(string level)
    {
        TMProTextSpace.SetText(level);
    }

}
