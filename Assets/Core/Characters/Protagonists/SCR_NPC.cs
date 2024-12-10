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

    Animator _animator;
    private string _hasPieceText = "Thank you for the spaceship piece";
    private string _hasNoPieceText = "Well? Time to go find these spaceship pieces!";
    private TextMeshProUGUI _textRetroaction;

    private void Start()
    {
        _textRetroaction = _retroactionText.GetComponentInChildren<TextMeshProUGUI>();
        _animator = GetComponentInChildren<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        SCR_MC SCR_MC = other.gameObject.GetComponentInParent<SCR_MC>();

        if(SCR_MC != null){
            _animator.SetTrigger("StartIdleVariation");
        }

    }

    public void Interact(SCR_Inventory inventory)
    {
        int hasPiece = 0;

        for (int i = inventory.lItems.Count - 1; i >= 0; i--)
        {
            if (inventory.lItems[i].item.itemType == ItemType.valueables)
            {
                hasPiece++;
                _totalSpaceshipPieces++;
                inventory.lItems.RemoveAt(i);

                if (_totalSpaceshipPieces >= _goalSpaceshipPieces)
                {
                    SCR_MapManager.instance.ChangeScene("Victory");
                }
            }
        }

        if (hasPiece > 1)
        {
            _textRetroaction.text = _hasPieceText + "s!";
        }
        else if (hasPiece > 0)
        {
            _textRetroaction.text = _hasPieceText + "!";
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