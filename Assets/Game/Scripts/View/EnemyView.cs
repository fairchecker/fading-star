using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private float _health;
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 2, 0), 2.5f * Time.deltaTime);
    }

    public void Initialize(float health)
    {
        _health = health;
    }

    public void DealDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
