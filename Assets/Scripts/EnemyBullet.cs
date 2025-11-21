using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    private float direktion;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void GiveDirektion(float startDirection)
    {

        direktion = startDirection;

    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.AddForce(transform.up * 10 * direktion);
    }
}
