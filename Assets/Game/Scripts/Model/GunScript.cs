using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] int bulletsCount = 5;
    [SerializeField] float reloadTime = 0.5f;
    [SerializeField] Transform shootPos;
    [SerializeField] Mesh bulletMesh;
    [SerializeField] Vector3 bulletSize = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] Material bulletMaterial;
    [SerializeField] float bulletSpeed = 100f;
    [SerializeField] float spreadAngle = 10f;

    bool canShoot = true;

    public void Shoot()
    {
        if (!canShoot) return;

        for (int i = 0; i < bulletsCount; i++)
        {
            GameObject tmp = new GameObject("Bullet");
            tmp.transform.position = shootPos.position;

            // Основное направление выстрела (от оружия)
            Vector3 direction = (shootPos.forward).normalized;

            // Добавляем случайное отклонение
            Quaternion randomSpread = Quaternion.Euler(
                Random.Range(-spreadAngle, spreadAngle), // Отклонение по вертикали
                Random.Range(-spreadAngle, spreadAngle) + 90, // Отклонение по горизонтали
                0f); 

            direction = randomSpread * direction; // Применяем разброс

            // Устанавливаем направление пули
            tmp.transform.forward = direction;

            // Добавляем компонент Bullet
            Bullet bulletComponent = tmp.AddComponent<Bullet>();
            bulletComponent.speed = bulletSpeed;

            var collider = tmp.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            var rigidbody = tmp.AddComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.freezeRotation = true;

            tmp.AddComponent<MeshRenderer>().material = bulletMaterial;
            tmp.AddComponent<MeshFilter>().mesh = bulletMesh;
            tmp.transform.localScale = bulletSize;
            tmp.tag = "bullet";
            Destroy(tmp, 10);
        }

        canShoot = false;
        Invoke("Reload", reloadTime);
    }
    private void Reload()
    {
        canShoot = true;
    }
    
}

public class Bullet : MonoBehaviour
{
    public float speed = 100f;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet") return;
        if (other.transform.CompareTag("enemy")) 
        {
            other.gameObject.GetComponent<EnemyView>().DealDamage(50);
        }
        Destroy(gameObject);
    }
}
