using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class MemoryRequestSpawner : MonoBehaviour
{
    public List<GameObject> requestPrefabs = new List<GameObject>(3);
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
            GameObject randomPrefab = requestPrefabs[Random.Range(0, requestPrefabs.Count)];
            GameObject spawnedObject = Instantiate(randomPrefab, transform);
            MemoryRequest memoryRequest = spawnedObject.GetComponent<MemoryRequest>();
            memoryRequest.memoryAddressText.text = "0x" + Random.Range(10, 100).ToString();
            spawnedObject.transform.localPosition = (chipSize.x + chipGap) * maxMemoryRequests * Vector3.right;
            conveyorShifts++;
        }
    }
    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform item = transform.GetChild(i);
            if (!item.GetComponent<MemoryRequest>().grabbing)
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
