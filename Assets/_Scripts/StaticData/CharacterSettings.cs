using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName ="CharacterSettings", menuName = "StaticData/Character Settings Data")]
public class CharacterSettings : ScriptableObject
{
    [Header("Move Character")]
    public float MovementForceOnAir = 2;
    public float MovementForceOnGround = 1.5f;
    public float JumpForce = 50;

    [Header("Death parameter")]
    public Color HeroDeathColor;
    public float FrequencyDeath;
    public float MaxShakeAmount;

}
