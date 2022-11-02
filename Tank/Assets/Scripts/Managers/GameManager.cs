using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Tank playerTankPrefab;
    public Tank playerTank;

    public RoundsDefinition roundsDef;

    public CameraControl m_CameraControl;
    public Transform spawnSpot;
    private int m_RoundNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;

  
    const float k_MaxDepenetrationVelocity = float.PositiveInfinity;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // This line fixes a change to the physics engine.
        Physics.defaultMaxDepenetrationVelocity = k_MaxDepenetrationVelocity;

        m_StartWait = new WaitForSeconds(roundsDef.m_StartDelay);
        m_EndWait = new WaitForSeconds(roundsDef.m_EndDelay);

        SpawnAllTanks();
        SetCameraTargets();
    }

    private void SpawnAllTanks()
    {
        playerTank = Instantiate(playerTankPrefab, spawnSpot.position, spawnSpot.rotation);
    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[1];
        targets[0] = playerTank.transform;
        m_CameraControl.m_Targets = targets;
    }
}