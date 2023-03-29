using UnityEngine;
using UnityEngine.UI;

public class CraneAttributeBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _max = 100;

    private float _value;

    [SerializeField]
    private float _lossPerTick;

    [SerializeField]
    private Image _bar;

    private void Awake()
    {
        Fullfill();
    }

    public void Tick()
    {
        _value -= _lossPerTick;
        _value = Mathf.Clamp(_value, 0, _max);
        SyncUi();
    }

    public void Fullfill()
    {
        _value = _max;
        SyncUi();
    }

    void SyncUi()
    {
        _bar.fillAmount = _value / _max;
    }
}
