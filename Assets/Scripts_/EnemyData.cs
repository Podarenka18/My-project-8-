using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/New enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Basic Status")] 
    public string enemyName;
    public float maxHealth = 100f;

    [Header("Vision")] 
    public GameObject enemyPrefab;
    
    [Header("Dropped rewards")]
    public int currentReward = 5;
}
