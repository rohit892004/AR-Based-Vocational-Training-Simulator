using UnityEngine;

public class FireTarget : MonoBehaviour
{
    [Header("Fire Objects")]
    public GameObject fire;
    public GameObject bigSmoke;
    public GameObject floorSmoke;

    private bool extinguished = false;

    public void Extinguish()
    {
        if (extinguished)
            return;

        extinguished = true;

        if (fire != null)
            fire.SetActive(false);

        if (bigSmoke != null)
            bigSmoke.SetActive(false);

        if (floorSmoke != null)
            floorSmoke.SetActive(false);

        Debug.Log("🔥 FIRE EXTINGUISHED: " + gameObject.name);
    }

    public void ResetFire()
    {
        extinguished = false;

        if (fire != null)
            fire.SetActive(true);

        if (bigSmoke != null)
            bigSmoke.SetActive(true);

        if (floorSmoke != null)
            floorSmoke.SetActive(true);
    }
}