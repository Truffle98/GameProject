using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxExperience(int maxExperience)
    {
        slider.maxValue = maxExperience;
        slider.value = 0;
    }

    public void SetExperience(int experience)
    {
        slider.value = experience;
    }
}
