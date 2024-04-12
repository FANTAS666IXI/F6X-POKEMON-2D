using UnityEngine;

public class HPBar : MonoBehaviour
{
    [Header("HP Bar Parameters")]
    [SerializeField] GameObject health;

    public void SetHP(float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }
}