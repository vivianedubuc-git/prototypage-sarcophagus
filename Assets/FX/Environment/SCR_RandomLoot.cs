using UnityEngine;

public class SCR_RandomLoot : MonoBehaviour
{
    [SerializeField] private GameObject[] _tLoot;
    [SerializeField] private int[] _lootChance;
    [SerializeField] private int _minimumDistance;
    [SerializeField] private int _maximumDistance;
    private Vector2 _position;

    private void Start()
    {
        _position = transform.position;

        for (int i = 0; i < _tLoot.Length; i++)
        {
            int random = Random.Range(1, 101);

            if (random <= _lootChance[i])
            {
                InstantiateLoot(i);
            }
        }
    }

    private void InstantiateLoot(int position)
    {
        int random = Random.Range(1, 3);
        float x = random == 1 ? Random.Range(_position.x + _minimumDistance, _position.x + _maximumDistance) : Random.Range(_position.x - _minimumDistance, _position.x - _maximumDistance);
        random = Random.Range(1, 3);
        float y = random == 1 ? Random.Range(_position.y + _minimumDistance, _position.y + _maximumDistance) : Random.Range(_position.y - _minimumDistance, _position.y - _maximumDistance);
        Instantiate(_tLoot[position], new Vector2(x, y), Quaternion.identity, transform);
    }
}