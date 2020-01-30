using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InsectAI : MonoBehaviour
{
    private float timer;
    public float walkTime = 10f;
    public float idleTime = 5f;
    
    public float minWalkDistance = 5f;
    public float maxWalkDistance = 25f;
    public Animator animator;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = this.transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= walkTime + idleTime) {
            var x = this.transform.position.x;
            var y = this.transform.position.y;
            var z = this.transform.position.z;

            
            var randomOffsetX = Random.Range(minWalkDistance, maxWalkDistance);
            var randomOffsetZ = Random.Range(minWalkDistance, maxWalkDistance);

            targetPosition = new Vector3(x + randomOffsetX, y, z + randomOffsetZ);
            timer = 0;
        }
        animator.SetBool("isWalking", timer < walkTime);
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Mathf.Clamp(timer / walkTime, 0f, 1f));
    }
}
