using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public int coin;
    public int score;

    public Text coinNumber;
    public Text scoreNumber;
    public ParticleSystem coinParticle;
    public Queue<ParticleSystem> queueParticles = new Queue<ParticleSystem>();

	// Use this for initialization
	void Start () {
        coinNumber.text = "0".ToString( );
        scoreNumber.text = "0".ToString( );
    }
	
	// Update is called once per frame
	void Update () {
        coinNumber.text = coin.ToString( );
        scoreNumber.text = score.ToString( );
    }

    public void GetCoinParticle(Vector3 position)
    {
        ParticleSystem ps;
        if(queueParticles.Count > 0) {
            ps = queueParticles.Dequeue( );
        }
        else {
            ps = Instantiate(coinParticle) as ParticleSystem;
        }
        ps.transform.position = position;
        ps.gameObject.SetActive(true);
        ps.transform.SetParent(this.transform);
    }


}
