using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class MemoryRequestSpawner : MonoBehaviour
{
    public List<GameObject> requestPrefabs = new List<GameObject>(3);
    public List<GameObject> powerUps = new List<GameObject>(1);
    [ReadOnly]
    public Transform[] itemsOnBelt;
    public Vector3 chipSize = Vector3.one;
    public float chipGap = 0.1f;
    public int maxMemoryRequests = 5;
    public ConveyorBelt belt;
    public float chipLerpSpeed = 12f;
    public static MemoryRequestSpawner global;
    public int conveyorShifts = 0;
    void Start()
    {
        global = this;
        StartCoroutine(spawnInitialChips());
    }

    public IEnumerator spawnInitialChips()
    {
        for (int i = 0; i < maxMemoryRequests; i++)
        {
            spawnMemoryRequest();
            yield return new WaitForSeconds(0.2f);
        }
    }

    [Button]
    public void spawnMemoryRequest()
    {
        itemsOnBelt = transform.Cast<Transform>().ToArray();
        if (itemsOnBelt.Length <= maxMemoryRequests)
        {
            GameObject randomPrefab;
            if (Random.value < 0.075f && powerUps.Count > 0)
            {
                randomPrefab = powerUps[Random.Range(0, powerUps.Count)];
                GameObject spawnedObject = Instantiate(randomPrefab, transform);
                spawnedObject.transform.localPosition = (chipSize.x + chipGap) * maxMemoryRequests * Vector3.right;
            }
            else
            {
                randomPrefab = requestPrefabs[Random.Range(0, requestPrefabs.Count)];
                GameObject spawnedObject = Instantiate(randomPrefab, transform);
                MemoryRequest memoryRequest = spawnedObject.GetComponent<MemoryRequest>();
                memoryRequest.memoryAddressText.text = "0x" + Random.Range(10, 100).ToString();
                spawnedObject.transform.localPosition = (chipSize.x + chipGap) * maxMemoryRequests * Vector3.right;
            }
            conveyorShifts++;
        }
    }

    [Button]
    public void DefragQueue()
    {
        itemsOnBelt = transform.Cast<Transform>().ToArray();

        var sorted = itemsOnBelt
            .Select(t =>
            {
                var memReq = t.GetComponent<MemoryRequest>();
                var powerUp = t.GetComponent<PowerUp>();
                int level = 0;
                if (memReq != null)
                    level = (int)memReq.memoryLevel;
                else if (powerUp != null)
                    level = int.MinValue;
                return new { Transform = t, Level = level };
            })
            .OrderBy(x => x.Level)
            .Select(x => x.Transform)
            .ToList();

        for (int i = 0; i < sorted.Count; i++)
        {
            sorted[i].SetSiblingIndex(i);
        }

        itemsOnBelt = transform.Cast<Transform>().ToArray();
    }
    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform item = transform.GetChild(i);
            if (item.GetComponent<MemoryRequest>() != null)
            {
                if (!item.GetComponent<MemoryRequest>().grabbing)
                {
                    item.localPosition = Vector3.Lerp(item.localPosition, (chipSize.x + chipGap) * i * Vector3.right, Time.deltaTime * chipLerpSpeed);
                }
            }
            else if (item.GetComponent<PowerUp>())
            {
                item.localPosition = Vector3.Lerp(item.localPosition, (chipSize.x + chipGap) * i * Vector3.right, Time.deltaTime * chipLerpSpeed);
            }
        }
        belt.conveyorPosition = Mathf.Lerp(belt.conveyorPosition, (chipSize.x + chipGap) * conveyorShifts, Time.deltaTime * chipLerpSpeed);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < maxMemoryRequests; i++)
        {
            Gizmos.DrawWireCube(transform.position + (Vector3.right * (chipSize.x + chipGap) * i), chipSize);
        }
    }
}
