using UnityEngine;

public class LevelMaintain : MonoBehaviour
{
    public static int levelNumber;
    [SerializeField] int internalNumber;
    void Start()
    {
        internalNumber = levelNumber;
    }

}
