using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IDragHandler
{
    public GunMount currentGunMount;

    void Start( )
    {

    }

    void FixedUpdate( )
    {
        InputUpdate( );
    }

    public void OnDrag( PointerEventData data )
    {
        Debug.Log("drag");
    }

    void InputUpdate( )
    {
        //if(Input.GetButton("Fire1")) {
        //    Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    RaycastHit2D hit = Physics2D.Raycast(new Vector2(vector.x, vector.y), Vector2.zero, LayerMask.NameToLayer("Earth"));

        //    if(hit) {
        //        if(hit.collider.tag == "GunMount") {
        //            currentGunMount = hit.collider.GetComponent<GunMount>( );
        //            currentGunMount.ShowLine( );
        //            currentGunMount.RayCastHit( );
        //        }
        //    }
        //    else {
        //        currentGunMount.CloseLine( );
        //    }
        //    if(hit.collider != null && hit.collider.tag == "GunMount") {
        //        currentGunMount = hit.collider.GetComponent<GunMount>( );
        //        currentGunMount.ShowLine( );
        //        currentGunMount.RayCastHit( );
        //    }
        //    if(hit.collider != null && hit.collider.tag == "Aerolite" && currentGunMount != null) {
        //        Debug.Log(hit.collider.name);
        //        Aerolite al = hit.collider.GetComponent<Aerolite>( );
        //        //currentGunMount.ShootingMissile(hit.collider.transform.position - currentGunMount.transform.position);
        //        currentGunMount.ShootingMissile(al);
        //    }
        //}

    }
}
