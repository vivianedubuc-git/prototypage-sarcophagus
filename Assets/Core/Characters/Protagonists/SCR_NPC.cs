using UnityEngine;

public class SCR_NPC : MonoBehaviour
{
    private int _totalSpaceshipPieces = 0;
    [SerializeField] private int _goalSpaceshipPieces = 1;

    public void Interact(SCR_Inventory inventory)
    {
        for (int i = inventory.lItems.Count - 1; i >= 0; i--)
        {
            if(inventory.lItems[i].item.itemType == ItemType.valueables)
            {
                _totalSpaceshipPieces++;
                inventory.lItems.RemoveAt(i);

                if(_totalSpaceshipPieces >= _goalSpaceshipPieces)
                {
                    SCR_MapManager.instance.ChangeScene("Victory");
                }
            }
        }
    }
}