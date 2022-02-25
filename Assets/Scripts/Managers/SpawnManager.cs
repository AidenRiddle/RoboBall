using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnLocation;

    public GameObject SpawnPlayer()
    {
        if (spawnLocation == null)
        {
            spawnLocation = new GameObject("Checkpoint").transform;
            spawnLocation.SetPositionAndRotation(transform.position, transform.rotation);
            Debug.Log("<color=lime>No spawn location attached.</color>\nCreating default character at location " + spawnLocation.position);
        }
        return Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
    }

    public void Respawn()
    {
        GameManager.Player.transform.SetPositionAndRotation(spawnLocation.position, spawnLocation.rotation);
        GameManager.Player.SetActive(true);
    }

    public void UpdateCheckpointLocation(Transform newLocation)
    {
        spawnLocation.SetPositionAndRotation(newLocation.position, newLocation.rotation);
    }
}
