using TMPro;
using UnityEngine;

public class SCR_NPC : MonoBehaviour
{
    [SerializeField] private GameObject _retroactionText;
    [SerializeField] private GameObject _interactionText;
    public GameObject interactionText { get { return _interactionText; } }
    [SerializeField] private int _goalSpaceshipPieces;
    private int _totalSpaceshipPieces = 0;
    private int _waitTime = 3;
    private string _hasPieceText = "Thank you!";
    private string _hasNoPieceText = "You have nothing for me!";
    private TextMeshProUGUI _textRetroaction;

    private void Start()
    {
        _textRetroaction = _retroactionText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Interact(SCR_Inventory inventory)
    {
        bool hasPiece = false;

        for (int i = inventory.lItems.Count - 1; i >= 0; i--)
        {
            if (inventory.lItems[i].item.itemType == ItemType.valueables)
            {
                hasPiece = true;
                _totalSpaceshipPieces++;
                inventory.lItems.RemoveAt(i);

                if (_totalSpaceshipPieces >= _goalSpaceshipPieces)
                {
                    SCR_MapManager.instance.ChangeScene("Victory");
                }
            }
        }

        if (hasPiece)
        {
            _textRetroaction.text = _hasPieceText;
        }
        else
        {
            _textRetroaction.text = _hasNoPieceText;
        }

        _retroactionText.SetActive(true);
        Invoke("DeactivateRetroaction", _waitTime);
    }

    private void DeactivateRetroaction()
    {
        _textRetroaction.text = "";
        _retroactionText.SetActive(false);
    }
}