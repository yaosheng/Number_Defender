using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour
{
    private AeroliteManager am;
    private SpriteRenderer sr;
    private UIManager uiManager;
    public float maxHealth;
    public float health;

    void Awake( )
    {
        am = Object.FindObjectOfType(typeof(AeroliteManager)) as AeroliteManager;
        sr = GetComponent<SpriteRenderer>( );
        uiManager = Object.FindObjectOfType(typeof(UIManager)) as UIManager;
    }

    void Start( )
    {
        health = maxHealth;
    }

    void OnTriggerEnter2D( Collider2D other )
    {
        if(other.tag == "Aerolite") {
            StartCoroutine(ChangeMaterial( ));

            Aerolite al = other.GetComponent<Aerolite>( );
            int temp = al.number.number;
            if(health - temp > 0) {
                health -= temp;
                uiManager.HitEarth(health, maxHealth);
            }
            else {
                uiManager.HitEarth(0, maxHealth);
                uiManager.ShowRestartUI( );
                Time.timeScale = 0;
            }
            Debug.Log("hit Earth");
        }
    }

    IEnumerator ChangeMaterial( )
    {
        if(sr.sharedMaterial == am.originalMaterial) {
            sr.sharedMaterial = am.hitMaterial;
        }
        yield return new WaitForSeconds(0.15f);
        if(sr.sharedMaterial == am.hitMaterial) {
            sr.sharedMaterial = am.originalMaterial;
        }
    }
}
