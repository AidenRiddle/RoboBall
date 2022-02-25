using UnityEngine;

[CreateAssetMenu(fileName = "BulletStats", menuName = "Scriptable Objects/BulletStats")]
public class BulletStats : ScriptableObject
{
    [SerializeField]
    int damage;
    public int Damage { get => damage; }

    [SerializeField]
    float speed;
    public float Speed { get => speed; }

    [Tooltip("Maximum distance the projectile will travel before being destroyed")]
    [SerializeField]
    int travelDistance;
    public int TravelDistance { get => travelDistance; }
}
