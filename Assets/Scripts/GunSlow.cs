using UnityEngine;

public class GunSlow : MonoBehaviour, StrategyGun {
    
    public GameObject obj;
    float time = 0;
    public void Fire(Transform t) {
        if (time < Time.time){
            Instantiate(obj, t.position, t.rotation);  
            time = Time.time+1.0f;
        }
    }
}
