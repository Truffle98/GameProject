using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumberScript : MonoBehaviour
{
    public TextMeshPro textMeshPro;

    public void SetDamageNumber(string damage)
    {
        textMeshPro.SetText(damage);
    }
}
