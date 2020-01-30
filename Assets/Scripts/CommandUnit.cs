using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandUnit : MonoBehaviour
{
    public enum PlayerColor { Blue, Red }
    public enum PlayerUnit { Gatherer, Soldier }

    public GameObject soldierSpawnPoint;
    public GameObject gathererSpawnPoint;
    public GameObject orbDropOffSite;
    public GameObject objectPlayerGatherer;
    public GameObject objectPlayerSoldier;
    public PlayerColor playerColor;
    public PlayerUnit queuedUnit;
    public int energy;

    private float unitSpawnTimeoutTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HarvestEnergy());
        queuedUnit = PlayerUnit.Gatherer;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnUnit();
    }

    private void SpawnUnit()
    {
        if (unitSpawnTimeoutTimer > 0)
        {
            unitSpawnTimeoutTimer -= Time.deltaTime;
            return;
        }

        // Keep spawning units if there is energy available to do so.
        switch (queuedUnit)
        {
            case PlayerUnit.Gatherer:
                if (energy >= 20)
                {
                    // Spawn a new gatherer and subtract the energy.
                    // Set the next queued unit to soldier.
                }
                break;
            case PlayerUnit.Soldier:
                if (energy >= 50)
                {
                    // Spawn a new soldier and subtract the energy.
                    // Set the next queued unit to gatherer.
                }
                break;
            default:
                queuedUnit = PlayerUnit.Gatherer;
                break;
        }
    }

    private IEnumerator HarvestEnergy() {
        while(true) {
            // Check all the orbs on the drop-off site, and increment
            // the energy which is gathered from them.
            yield return new WaitForSeconds(1);
        }
    }
}
