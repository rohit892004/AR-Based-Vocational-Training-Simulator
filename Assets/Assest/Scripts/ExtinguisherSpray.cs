using UnityEngine;

public class ExtinguisherSpray : MonoBehaviour
{
    [Header("Spray")]
    public ParticleSystem sprayParticle;
    public Transform nozzle;

    [Header("Fire Detection")]
    public float sprayRange = 5f;

    private FireTarget currentFire;
    private bool spraying = false;

    void Update()
    {
        if (!spraying)
            return;

        CheckFire();
    }

    public void StartSpray()
    {
        spraying = true;

        if (sprayParticle != null)
            sprayParticle.Play();

        Debug.Log("💨 SPRAY ON");

        CheckFire();
    }

    public void StopSpray()
    {
        spraying = false;

        if (sprayParticle != null)
            sprayParticle.Stop();

        if (currentFire != null)
        {
            currentFire = null;
        }

        Debug.Log("💨 SPRAY OFF");
    }

    void CheckFire()
    {
        if (nozzle == null)
        {
            Debug.LogWarning("⚠️ Nozzle is not assigned!");
            return;
        }

        Ray ray = new Ray(nozzle.position, nozzle.forward);

        Debug.DrawRay(
            nozzle.position,
            nozzle.forward * sprayRange,
            Color.red
        );

        if (Physics.Raycast(ray, out RaycastHit hit, sprayRange))
        {
            FireTarget fire = hit.collider.GetComponentInParent<FireTarget>();

            if (fire != null)
            {
                if (currentFire != fire)
                {
                    currentFire = fire;

                    Debug.Log(
                        "🎯 FIRE HIT: " + fire.gameObject.name
                    );
                }

                fire.Extinguish();

                return;
            }
        }
    }
}