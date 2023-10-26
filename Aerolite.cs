using UnityEngine;
using System.Collections;

public class Aerolite : MonoBehaviour
{
    private int rotateDirection = 0;
    private float rotateSpeed = 0;
    private float dropSpeed;
    private bool isCollided = false;
    private Vector2 moveDirection;
    [SerializeField]
    private SpriteRenderer[ ] digitals;
    [SerializeField]
    private ParticleSystem smoke;
    private Collider2D col;
    private GameManager gm;

    public Number number;
    public AeroliteManager am;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public GunMount gunMount;

    void Awake( )
    {
        gm = Object.FindObjectOfType(typeof(GameManager)) as GameManager;
        rb = GetComponent<Rigidbody2D>( );
        sr = GetComponentInChildren<SpriteRenderer>( );
        am = Object.FindObjectOfType(typeof(AeroliteManager)) as AeroliteManager;
        col = GetComponent<Collider2D>( );
        number = GetComponentInChildren<Number>( );
    }

    void Start( )
    {
        smoke.gameObject.SetActive(false);
        dropSpeed = 0.5f;
        ChangeRotate( );
    }

    void OnEnable( )
    {
        am.aeroliteGroup.Add(this);
        gunMount = null;
    }

    void Update( )
    {
        RotateUpdate( );
        CheckNumeber1( );
    }

    void FixedUpdate( )
    {
        FlyUpdate( );
    }

    void FlyUpdate( )
    {
        if(!isCollided) {
            Drop( );
        }
        else {
            Move(moveDirection);
        }
    }

    void Drop( )
    {
        if(number.number != 1) {
            rb.MovePosition(rb.position + Vector2.down * dropSpeed * Time.deltaTime);
        }
        else {
            rb.MovePosition(rb.position + Vector2.down * dropSpeed * 20.0f * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D( Collider2D other )
    {
        if(other.tag == "Earth") {
            Recover( );
        }
    }

    void CheckNumeber1( )
    {
        if(number.number == 1) {
            smoke.gameObject.SetActive(true);
            col.isTrigger = true;
        }
        else {
            col.isTrigger = false;
        }
    }

    void RotateUpdate( )
    {
        switch(rotateDirection) {
            case 0:
            sr.transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
            break;
            case 1:
            sr.transform.Rotate(Vector3.back * Time.deltaTime * rotateSpeed);
            break;
        }
    }

    IEnumerator ChangeMaterial_C( )
    {
        if(sr.sharedMaterial == am.originalMaterial) {
            sr.sharedMaterial = am.hitMaterial;
        }
        yield return new WaitForSeconds(0.03f);
        if(sr.sharedMaterial == am.hitMaterial) {
            sr.sharedMaterial = am.originalMaterial;
        }
    }

    void MakeExplosion( )
    {

    }

    void ChangeNewAerolite( )
    {

    }

    IEnumerator StartCollide_C( Vector2 direction )
    {
        isCollided = true;
        yield return new WaitForSeconds(0.75f);
        isCollided = false;
    }

    void Move( Vector2 direction )
    {
        rb.MovePosition(rb.position - direction * 1.15f * Time.deltaTime);
    }

    public void SetUpNumber(int temp)
    {
        number.SetNumber(temp);
    }

    public void ChangeRotate( )
    {
        rotateSpeed = Random.Range(20, 200);
        rotateDirection = Random.Range(0, 2);
    }

    public void ChangeMaterial( )
    {
        StartCoroutine(ChangeMaterial_C( ));
    }
    public void StartCollide( Vector2 direction )
    {
        StartCoroutine(StartCollide_C(direction));
    }

    public void GetMoveDirection(Vector2 direction )
    {
        this.moveDirection = direction;
    }

    public void SetRenderColor(Color32 color )
    {
        sr.color = color;
    }

    public void SetGunMount(GunMount gm)
    {
        this.gunMount = gm;
    }

    public void Recover( )
    {
        gm.GetCoinParticle(this.transform.position);
        gm.score += 100;
        isCollided = false;
        this.gameObject.layer = 0;
        sr.sharedMaterial = am.originalMaterial;
        smoke.gameObject.SetActive(false);
        //if(gunMount != null) {
        //    gunMount.RemoveGoalAerolite( );
        //}
        am.aerolites.Enqueue(this);
        am.aeroliteGroup.Remove(this);
        gameObject.SetActive(false);
    }
}

//void OnCollisionEnter2D( Collision2D coll )
//{
//    if(coll.collider.tag == "Aerolite") {
//        //Debug.Log("hit aerolite");
//    }
//}

//void OnCollisionStay2D( Collision2D coll )
//{
//    if(coll.collider.tag == "Aerolite") {
//        //Debug.Log("stay aerolite");
//        rb.Sleep( );
//    }
//}

//void OnCollisionExit2D( Collision2D coll )
//{
//    if(coll.collider.tag == "Aerolite") {
//        //Debug.Log("exit aerolite");
//        rb.WakeUp( );
//    }
//}