using UnityEngine;

public class SCR_RandomBattles : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private int _minimumEnemy;
    [SerializeField] private int _maximumEnemy;
    [SerializeField][Range(1, 100)] private int _battleChance;
    [SerializeField] private int _minimumDistance;
    [SerializeField] private int _maximumDistance;
    private Vector2 _position;

    private void Start()
    {
        _position = transform.position;

        for (int i = 0; i < _maximumEnemy; i++)
        {
            if (i < _minimumEnemy)
            {
                InstantiateEnemy();
            }
            else
            {
                int random = Random.Range(1, 101);

                if (random <= _battleChance)
                {
                    InstantiateEnemy();
                }
            }
        }
    }

    private void InstantiateEnemy()
    {
        int random = Random.Range(1, 3);
        float x = random == 1 ? Random.Range(_position.x + _minimumDistance, _position.x + _maximumDistance): Random.Range(_position.x - _minimumDistance, _position.x - _maximumDistance);
        random = Random.Range(1, 3);
        float y = random == 1 ? Random.Range(_position.y + _minimumDistance, _position.y + _maximumDistance) : Random.Range(_position.y - _minimumDistance, _position.y - _maximumDistance);
        Instantiate(_enemy, new Vector2(x, y), Quaternion.identity, transform);
    }
}