using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomGenerate : MonoBehaviour
{
    [SerializeField] GameObject path1;
    [SerializeField] GameObject path2;
    [SerializeField] GameObject path3;
    [SerializeField] int generateNumber;
    void Start()
    {
        generateNumber = Random.Range(1, 4);
        if (generateNumber == 1)
        {
            path1.SetActive(true);
        }


        if (generateNumber == 2)
        {
            path2.SetActive(true);
        }

        if (generateNumber == 3)
        {
            path3.SetActive(true);
        }
    }

   
}
