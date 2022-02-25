using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField]
    GameObject flashlight;

    void OnFlashlight()
    {
        flashlight.SetActive(!flashlight.activeSelf);
    }
}
