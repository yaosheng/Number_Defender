using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MissileBase : MonoBehaviour {

    public Vector2 target;
    public Aerolite targetAerolite;
    public float flySpeed;
    public GunMount gunMount;
    public SpriteRenderer sr;

    public int keyNumber;

    void Awake( )
    {
        sr = GetComponent<SpriteRenderer>( );
    }

    void OnEnable( )
    {

    }

    void Update( )
    {
        //Fly( );
        TrackFly( );
    }

    void Fly( )
    {
        Vector3 velocity = flySpeed * target;
        transform.position += velocity * Time.deltaTime;
        transform.up = target;
    }

    void TrackFly( )
    {
        if(targetAerolite.gameObject.activeSelf && targetAerolite != null) {
            Vector3 velocity = flySpeed * (targetAerolite.transform.position - transform.position).normalized;
            transform.position += velocity * Time.deltaTime;
            transform.up = targetAerolite.transform.position - transform.position;
        }
        else {
            Vector3 velocity = flySpeed * transform.up;
            transform.position += velocity * Time.deltaTime;
        }
    }
    
    public void GetGunMount(GunMount gm)
    {
        this.gunMount = gm;
    }

    public void GetTargetAerolite(Aerolite al )
    {
        this.targetAerolite = al;
    }

    public void GetDirection( Vector2 target )
    {
        this.target = target;
    }

    void OnTriggerEnter2D( Collider2D other )
    {
        if(other.tag == "Aerolite") {
            Aerolite al = other.GetComponent<Aerolite>( );
            Number n = al.number;
            if(n.number != 1) {
                Vector2 vector = this.transform.position - other.transform.position;
                al.ChangeRotate( );
                al.ChangeMaterial( );
                //al.GetMoveDirection(vector);
                //al.StartCollide(vector);
                Recover( );
                ChangeNumber(al, vector);
            }
        }
    }

    void ChangeNumber(Aerolite al, Vector2 vector)
    {
        Number number = al.number;

        if((number.number % keyNumber) == 0) {
            if(number.number == keyNumber) {
                al.Recover( );
            }
            else {
                int temp = (int)(number.number / keyNumber);
                al.SetUpNumber(temp);
                al.GetMoveDirection(vector);
                al.StartCollide(vector);
                //al.SetGunMount(gunMount);
            }
        }
        else {
            List<int> intList = new List<int>( );
            List<Vector3> vectorList = new List<Vector3>( );
            if(number.number > keyNumber) {
                //version 3
                int tempNumber = number.number - keyNumber;

                intList.Add(keyNumber);
                Vector3 randomV = al.transform.position + new Vector3(Random.Range(-0.10f, 0.10f), Random.Range(-0.10f, 0.10f), 0);
                vectorList.Add(randomV);

                al.SetUpNumber(tempNumber);
                //al.SetGunMount(gunMount);

                //intList.Add(tempNumber);
                //Vector3 randomV1 = al.transform.position + new Vector3(Random.Range(-0.10f, 0.10f), Random.Range(-0.10f, 0.10f), 0);
                //vectorList.Add(randomV1);
                //al.Recover( );
            }
            else {
                for(int i = 0; i < number.number; i++) {
                    intList.Add(1);
                    Vector3 randomV = al.transform.position + new Vector3(Random.Range(-0.10f, 0.10f), Random.Range(-0.10f, 0.10f), 0);
                    vectorList.Add(randomV);
                }
                al.Recover( );
            }
            al.am.SplitAerolite(vectorList.ToArray(), intList.ToArray( ));
        }
    }

    void Recover( )
    {
        gunMount.missilePool.Enqueue(this);
        this.gameObject.SetActive(false);
    }
}

//int multiple = Mathf.FloorToInt(number.number / keyNumber);//商
//int residue = number.number % keyNumber;//餘數

//version 1
//for(int i = 0; i < multiple; i++) {
//    intList.Add(keyNumber);
//    Vector3 randomV = al.transform.position + new Vector3(Random.Range(-0.10f, 0.10f), Random.Range(-0.10f, 0.10f), 0);
//    vectorList.Add(randomV);
//}
//intList.Add(residue);
//Vector3 randomV1 = al.transform.position + new Vector3(Random.Range(-0.10f, 0.10f), Random.Range(-0.10f, 0.10f), 0);
//vectorList.Add(randomV1);

//version 2
//int[ ] intArray;
//if(multiple == 1) {
//    intArray = new int[1];
//    intArray[0] = residue;
//}
//else {
//    intArray = new int[2];
//    intArray[0] = multiple;
//    intArray[1] = residue;
//}

//for(int i = 0; i < intArray.Length; i++) {
//    intList.Add(intArray[i]);
//    Vector3 tempVector = al.transform.position + new Vector3(Random.Range(-0.10f, 0.10f), Random.Range(-0.10f, 0.10f), 0);
//    vectorList.Add(tempVector);
//}