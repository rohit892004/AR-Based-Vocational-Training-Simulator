using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScpt : MonoBehaviour
{

    public GameObject fire;
    public GameObject fire1;
      private bool extinguished = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
    }
    /// <summary>
    /// OnParticleTrigger is called when any particles in a particle system
    /// meet the conditions in the trigger module.
    /// </summary>
    private void OnParticleCollision(GameObject other)
    {
        if (extinguished)
            return;

        if (other.CompareTag("FireSpray"))
        {
            extinguished = true;

            if (fire != null)
                fire.SetActive(false);

        

            Debug.Log("🔥 FIRE EXTINGUISHED!");
        }

        if(other.CompareTag("FireSpray1"))
        {
            extinguished = true;

            if(fire1 != null)
            {
                fire1.SetActive(false);
                Debug.Log("Fire1");
            }

        }
    }
    
}
