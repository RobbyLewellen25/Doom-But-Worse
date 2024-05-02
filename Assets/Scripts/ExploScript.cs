using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float range = 3.5f;
    void Start()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(range, 100, range), Quaternion.identity);

        foreach (var hitCollider in hitColliders)
        {
            Damage(hitCollider);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //THIS JANK WAY OF DOING DAMAGE IS THE WAY THE ORIGINAL DOOM DID IT
    void Damage (Collider other)
    {
        Vector3 explosionPosition = transform.position;
        Vector3 otherPosition = other.transform.position;

        float xDistance = Mathf.Abs(explosionPosition.x - otherPosition.x);
        float zDistance = Mathf.Abs(explosionPosition.z - otherPosition.z);

        float maxDistance = Mathf.Max(xDistance, zDistance);

        Vector3 boxSize = new Vector3(3.5f, 100f, 3.5f);
        float maxBoxDistance = Mathf.Max(boxSize.x, boxSize.z);

        // Map maxDistance in range [0, 20] to damage in range [160, 20]
        float damage = Mathf.Lerp(8, 1, maxDistance / maxBoxDistance) * 20;

        if (other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.DamagePlayer((int)damage);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            Enemy enemyHealth = other.gameObject.GetComponent<Enemy>();
            enemyHealth.TakeDamage((int)damage);
        }
    }
}
