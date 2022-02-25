using UnityEngine;
using UnityEngine.VFX;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] CharacterMovement character;
    [SerializeField] VisualEffect effect;

    [Tooltip("Minimum speed required before activating the particles")]
    [SerializeField] float minSpeed;

    private void Update()
    {
        if (!character.IsFalling && character.Velocity.magnitude >= minSpeed) effect.Play();
        else effect.Stop();
    }
}
