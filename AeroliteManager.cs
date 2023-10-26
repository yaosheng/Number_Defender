using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AeroliteManager : MonoBehaviour {

    public Aerolite aerolite;
    public Queue<Aerolite> aerolites = new Queue<Aerolite>( );
    public List<Aerolite> aeroliteGroup = new List<Aerolite>( );
    public Sprite[ ] numbers;
    public Material originalMaterial;
    public Material hitMaterial;

    private float timer;
    private int step;
    public float setTime;

    void Start( )
    {
        timer = setTime;
    }

    void FixedUpdate( )
    {
        timer += Time.fixedDeltaTime;
        //Debug.Log("timer :" + timer);
        if(timer > setTime) {
            timer = 0;
            GenerateAerolite( );
        }
    }

    public void GenerateAerolite( )
    {
        Aerolite al;
        if(aerolites.Count > 0) {
            al = aerolites.Dequeue( );
        }
        else {
            al = Instantiate(aerolite) as Aerolite;
        }
        al.gameObject.SetActive(true);
        al.SetRenderColor(new Color32(255, 255, 255, 255));
        //al.rb.position = new Vector2(Random.Range(-2.00f, 2.00f), 6);
        al.transform.position = new Vector3(Random.Range(-2.00f, 2.00f), 6, 0);
        al.SetUpNumber(Random.Range(2, 9));
        al.name += step.ToString( );
        step++;
        al.transform.SetParent(this.transform);
    }

    public void SplitAerolite(Vector3[] positions, int[] numbers)
    {
        for(int i = 0; i < numbers.Length; i++) {
            Aerolite al;
            if(aerolites.Count > 0) {
                al = aerolites.Dequeue( );
            }
            else {
                al = Instantiate(aerolite) as Aerolite;
            }
            //Rigidbody2D rb = al.GetComponent<Rigidbody2D>( );
            al.gameObject.SetActive(true);
            //al.rb.position = positions[i];
            al.transform.position = positions[i];
            al.SetUpNumber(numbers[i]);
            al.name += step.ToString( );
            step++;
            al.transform.SetParent(this.transform);
        }
    }
}
