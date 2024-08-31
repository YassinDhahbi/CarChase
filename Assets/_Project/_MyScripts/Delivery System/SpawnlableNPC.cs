using UnityEngine;

[System.Serializable]
public class SpawnlableNPC
{
    [SerializeField] private Sprite _npcPortrait;
    public Sprite NPCPortrait => _npcPortrait;
    [SerializeField] private GameObject _npcPrefab;
    public GameObject NPCPrefab => _npcPrefab;
}