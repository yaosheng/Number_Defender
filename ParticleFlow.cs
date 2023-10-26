using UnityEngine;
using System.Collections;

public class ParticleFlow : MonoBehaviour {

    public ParticleSystem.Particle[ ] mParticles;
    public ParticleSystem ps;
    public Transform tran;

    private GameManager gm;
    //private float timer;
    private int num;

    void Start( )
    {
        gm = Object.FindObjectOfType(typeof(GameManager)) as GameManager;
        ps = GetComponent<ParticleSystem>( );
        tran = GameObject.FindGameObjectWithTag("CoinGoal").transform;
    }

    void LateUpdate( )
    {
        if(mParticles == null || mParticles.Length < ps.maxParticles) {
            mParticles = new ParticleSystem.Particle[ps.maxParticles];
        }
        int numParticlesAlive = ps.GetParticles(mParticles);
        Debug.Log("particles number :" + numParticlesAlive);

        for(int i = 0; i < numParticlesAlive; i++) {
            if(mParticles[i].lifetime < 0.75f) {
                mParticles[i].velocity += (tran.position - mParticles[i].position) * 5;
            }
            if(Vector3.Distance(tran.position, mParticles[i].position) < 0.75f) {
                gm.coin++;
                num++;
                mParticles[i].lifetime = 0;
            }
        }
        ps.SetParticles(mParticles, numParticlesAlive);

        //if( numParticlesAlive > 0 && num == numParticlesAlive) {
        //    Recover( );
        //}
    }

    void Recover( )
    {
        num = 0;
        this.gameObject.SetActive(false);
        gm.queueParticles.Enqueue(ps);
    }
//#if UNITY_ANDROID

//#endif
}
