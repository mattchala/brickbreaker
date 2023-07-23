using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public int level;

    static public int unlockedProgress = 3;
    // Start is called before the first frame update
    void Start()
    {
        unlockedProgress = 4;
        // Debug.Log(locks.Length);
        Debug.Log("level #: " + level.ToString() + "prog: " + LevelManager.unlockedProgress.ToString());
        if (level <= (LevelManager.unlockedProgress))
        {
            this.gameObject.SetActive(false);
            Debug.Log("Unlocked");
        }
    }
}
