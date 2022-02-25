using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthStats", menuName = "Scriptable Objects/HealthStats")]
public class HealthStats : ScriptableObject
{
    [SerializeField] bool killable = true;
    public bool Killable { get => killable; }

    [Space(15)]
    [SerializeField] int maxHealthPoints = 100;
    public int MaxHealthPoints { get => maxHealthPoints; }

    [SerializeField] int lives = 3;
    public int Lives { get => lives; }
}
