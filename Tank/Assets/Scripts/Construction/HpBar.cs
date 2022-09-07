using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider bar;

    public void SetValue(float v)
    {
        bar.value = v;
    }
}
