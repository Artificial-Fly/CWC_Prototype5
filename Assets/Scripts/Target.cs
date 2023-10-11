using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody targetRb;
    public ParticleSystem explosionParticle;
    float minSpeed = 12, maxSpeed = 16, maxTorque = 12, xRange = 4, ySpawnPos = -0.75f;
    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        if(!gameManager.isGameOver){
            targetRb = GetComponent<Rigidbody>();
            targetRb.AddForce(RandomForce(), ForceMode.Impulse);
            targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());//cause an object to rotate randomly
            transform.position = RandomSpawnPos();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameOver){
            targetRb.isKinematic = true;
        }
    }

    Vector3 RandomForce(){
        return Vector3.up * Random.Range(minSpeed,maxSpeed);
    }
    Vector3 RandomSpawnPos(){
        return new Vector3(Random.Range(-xRange,xRange), ySpawnPos);
    }
    float RandomTorque(){
        return Random.Range(-maxTorque,maxTorque);
    }
    void OnMouseDown(){
        if(!gameManager.isGameOver){
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            if(gameObject.CompareTag("Bad")){gameManager.UpdateLives(-1);}
        }
    }

    void OnTriggerEnter(Collider other){
        if(!gameObject.CompareTag("Bad")){gameManager.UpdateLives(-1);}
        Destroy(gameObject);        
    }
}
