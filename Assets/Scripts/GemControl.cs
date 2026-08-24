using UnityEngine;

public class GemControl : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 2f;
    [SerializeField] AudioSource gemCollect;
    [SerializeField] int gemScore = 100;
    private bool isCollected = false;
    private Transform playerTransform;

    void Start()
    {
        // Ensure Gem has a Trigger Collider even if missing from scene prefab
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            SphereCollider sphere = gameObject.AddComponent<SphereCollider>();
            sphere.isTrigger = true;
            sphere.radius = 1.5f;
        }
        else
        {
            col.isTrigger = true;
        }
    }

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);

        if (!isCollected)
        {
            if (playerTransform == null)
            {
                PlayerControls pc = Object.FindFirstObjectByType<PlayerControls>();
                if (pc != null)
                {
                    playerTransform = pc.transform;
                }
                else
                {
                    GameObject pObj = GameObject.FindWithTag("Player");
                    if (pObj != null) playerTransform = pObj.transform;
                }
            }

            if (playerTransform != null)
            {
                float dist = Vector3.Distance(transform.position, playerTransform.position);
                if (dist <= 2.2f)
                {
                    CollectGem();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        CollectGem();
    }

    public void CollectGem()
    {
        if (isCollected) return;
        isCollected = true;

        ScoreControl.totalScore += gemScore;
        if (gemCollect != null)
        {
            gemCollect.Play();
        }
        Destroy(gameObject);
    }
}
