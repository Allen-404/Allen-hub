using UnityEngine;

public class TankShooting : MonoBehaviour
{
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public AudioSource m_ShootingAudio;
    public AudioClip m_FireClip;
    public float fireInverval = 0.4f;
    float _firedTimestamp;
    bool _isFiring;

    private void Start()
    {
        _isFiring = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _isFiring = true;
        if (Input.GetKeyUp(KeyCode.Space))
            _isFiring = false;

        if (_isFiring)
        {
            if (Time.time - _firedTimestamp > fireInverval)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        _firedTimestamp = Time.time;

        var shell = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation);
        ShellExplosion se = shell.GetComponent<ShellExplosion>();
        se.host = this.transform;

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }
}