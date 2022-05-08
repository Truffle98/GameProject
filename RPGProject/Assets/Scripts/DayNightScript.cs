using UnityEngine;


public class DayNightScript : MonoBehaviour
{
    [SerializeField] private Gradient lightColor;
    [SerializeField] public UnityEngine.Rendering.Universal.Light2D light;

    private int days;
    public float time = 50;
    private bool canChangeDay = true;

    public int GetDays() {
        return days;
    }

    public void DayChanged() {

        return;
    }

    private void Update() {
        if (time > 500) {
            time = 0;
        }

        if ((int)time == 250 && canChangeDay) {
            
            canChangeDay = false;
            DayChanged();
            days++;

        }

        if ((int) time == 255) {
            canChangeDay = true;
        }

        time += Time.deltaTime;
        light.color = lightColor.Evaluate(time * 0.002f);
    }
}
