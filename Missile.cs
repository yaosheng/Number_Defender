using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    public Vector2 target;

    void Update( )
    {

    }

    void Fly( )
    {

    }

    public void Direction(Vector2 direction )
    {
        this.target = direction;
    }

    void OnTriggerEnter2D( Collider2D other )
    {
        if(other.tag == "Aerolite") {
            //Aerolite al = other.GetComponent<Aerolite>( );
            ////Vector2 point = other
            //Vector2 normal = other.collider. .normal;
            //StartCoroutine(al.GetRecoil(-normal));
        }
    }

    //void OnCollisionEnter2D( Collision2D coll )
    //{
    //    if(coll.collider.tag == "Aerolite") {
    //        Aerolite al = coll.collider.GetComponent<Aerolite>( );
    //        //Vector2 point = coll
    //    }
    //}
}
