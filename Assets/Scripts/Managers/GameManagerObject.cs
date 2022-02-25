using UnityEngine;

public static class GameManager
{
    static GameManagerSettings settings = Resources.Load<GameManagerSettings>("GameManager/GameManagerSettings");

    public static GameObject Player; 
    public static float Gravity => settings.Gravity;
    public static LayerMask RaycastMask => settings.RaycastMask;
    public static float ProbeRefreshRate => settings.ProbeRefreshRate;
}

public class GameManagerObject : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;

    private void Awake()
    {
        Debug.Log(GameManager.ProbeRefreshRate);

        //Spawn the player and keep a reference to it
        spawnManager = GetComponent<SpawnManager>();
        GameManager.Player = spawnManager.SpawnPlayer();

        //Set the first Checkpoint
        OnCheckpointReached(GameManager.Player.transform);

        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RespawnPlayer()
    {
        spawnManager.Respawn();
    }

    public void OnCheckpointReached(Transform checkpoint)
    {
        spawnManager.UpdateCheckpointLocation(checkpoint);
    }
}
