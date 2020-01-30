using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentAi : MonoBehaviour
{   
    enum AgentState {
        Idle,
        StartMining,
        Mining,
        StartWalkingToMine,
        WalkingToMine,
        StartWalkingToBase,
        WalkingToBase,
        StartDumping,
        Dumping
    }

    private AgentState state;
    public GameObject target;
    public GameObject spawn;
    public GameObject resource;
    private float miningTimer = 5f;
    public float miningSpeed = 5f;
    private int inventoryQuantity;
    public int inventorySize = 5;
    private NavMeshAgent nav;

    private bool IsFull {
        get {
            return inventoryQuantity >= inventorySize;
        }
    }

    private bool IsEmpty {
        get {
            return inventoryQuantity == 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        state = AgentState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state) {
            case AgentState.Idle:
                state = AgentState.StartWalkingToMine;
                break;
            case AgentState.StartWalkingToMine:
                Debug.Log("Start walking to mine.");
                nav.SetDestination(target.transform.position);
                state = AgentState.WalkingToMine;
                break;
            case AgentState.WalkingToMine:
                if(Vector3.Distance(target.transform.position, transform.position) <= 15f) {
                    Debug.Log("Arrived at mine.");
                    state = AgentState.StartMining;
                }
                break;
            case AgentState.StartMining:
                Debug.Log("Start mining.");
                miningTimer = 0;
                state = AgentState.Mining;
                break;
            case AgentState.Mining:
                miningTimer += Time.deltaTime;
                if(miningTimer >= miningSpeed) {
                    inventoryQuantity++;
                    miningTimer = 0;
                    Debug.Log("Item mined.");
                }
                if(inventoryQuantity >= inventorySize) {
                    Debug.Log("Done mining, inventory is full.");
                    state = AgentState.StartWalkingToBase;
                }
                break;
            case AgentState.StartWalkingToBase:
                Debug.Log("Start walking to base.");
                nav.SetDestination(spawn.transform.position);
                state = AgentState.WalkingToBase;
                break;
            case AgentState.WalkingToBase:
                if(Vector3.Distance(spawn.transform.position, transform.position) < 25f) {
                    Debug.Log("Arrived at base.");
                    state = AgentState.StartDumping;
                }
                break;
            case AgentState.StartDumping:
                Debug.Log("Start dumping.");  
                StartCoroutine(DropItem());
                state = AgentState.Dumping;
                break;
            case AgentState.Dumping:
                if(IsEmpty) {
                    Debug.Log("Done dumping.");
                    state = AgentState.StartWalkingToMine;
                }
                break;
        }
    }

    private IEnumerator DropItem() {
        while(inventoryQuantity > 0) {
            var positionOffset = new Vector3(0, 8, 0);
            var obj = GameObject.Instantiate(resource, transform.position + positionOffset, Quaternion.identity);
            var rb = obj.GetComponent<Rigidbody>();
            obj.GetComponent<Rigidbody>().velocity = 12f*(new Vector3(2, 1, 0));
            obj.GetComponent<SphereCollider>().radius = 0.001f;
            inventoryQuantity--;
            Debug.Log("Dropped item.");
            yield return new WaitForSeconds(0.3f);                        
        }
    }
}
