using UnityEngine;

public class GunDouble : MonoBehaviour, StrategyGun {
    
    public GameObject obj;
    float time = 0;
    public void Fire(Transform t) {
        if (time < Time.time){
            Vector3 dir = Vector3.right * 0.5f;
            Instantiate(obj, t.position + dir, t.rotation);  
            Instantiate(obj, t.position - dir, t.rotation);  
            time = Time.time+.5f;
        }
    }
}
