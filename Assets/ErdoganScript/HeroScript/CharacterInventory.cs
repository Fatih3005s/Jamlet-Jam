using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [Header("CharacterController")]
    private CharacterController characterController;

    [Header("Weapon Durabilities")]
    private int maxDurabilityCount;
    public int Durability;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        maxDurabilityCount = PlayerPrefs.GetInt("maxDurability", 3);
    }
   
}
