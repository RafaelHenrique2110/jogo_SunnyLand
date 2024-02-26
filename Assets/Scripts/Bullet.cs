using UnityEngine;

public class Bullet : MonoBehaviour {

    void Start(){
        Destroy(gameObject, 3.0f);
    }
    
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * 15 * Time.fixedDeltaTime);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("death", true);
            Destroy(other.gameObject, 0.2f);
            Destroy(gameObject);


        }
    }
}
