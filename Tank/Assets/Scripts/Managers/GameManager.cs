using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public RoundsDefinition roundsDef;

    public CameraControl m_CameraControl;
    public Text m_MessageText;
    public GameObject m_TankPrefab;
    public Transform spawnSpot;
    private int m_RoundNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;

    public GameObject playerTank;
    const float k_MaxDepenetrationVelocity = float.PositiveInfinity;

    private void Start()
    {
        // This line fixes a change to the physics engine.
        Physics.defaultMaxDepenetrationVelocity = k_MaxDepenetrationVelocity;

        m_StartWait = new WaitForSeconds(roundsDef.m_StartDelay);
        m_EndWait = new WaitForSeconds(roundsDef.m_EndDelay);

        SpawnAllTanks();
        SetCameraTargets();
        m_MessageText.text = "";
    }

    private void SpawnAllTanks()
    {
        playerTank = Instantiate(m_TankPrefab, spawnSpot.position, spawnSpot.rotation);
    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[1];
        targets[0] = playerTank.transform;
        m_CameraControl.m_Targets = targets;
    }
}