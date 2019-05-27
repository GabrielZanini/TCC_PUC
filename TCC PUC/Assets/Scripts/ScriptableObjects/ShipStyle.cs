using UnityEngine;


[CreateAssetMenu(fileName = "New Style", menuName = "Spaceship/Style")]
public class ShipStyle : ScriptableObject
{
    [Header("Ship")]
    public Color shipColor = Color.blue;

    [Header("Bullets")]
    public Color inBulletColor = Color.white;
    public Color outBulletColor = Color.blue;

    [Header("Shield")]
    public Material shieldMaterial;

}
