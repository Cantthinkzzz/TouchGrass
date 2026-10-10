using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ZVisible : MonoBehaviour
{
    public float Offset;
    private GameObject _player;
    private SpriteRenderer _sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (_player.transform.position.z + Offset > transform.position.z && Vector3.Distance(_player.transform.position, transform.position) < 3)
        {
            _sprite.color = new Color(1, 1, 1, 0.7f);
        }
        else
        {
            _sprite.color = new Color(1, 1, 1, 1);
        }
    }
}
