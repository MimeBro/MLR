using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour
{
    public string sceneToLoad;
    public List<OldUnit> monsterPool;
    public bool used;
    
    public void LoadNodeScene()
    {
        if(used) return;
        used = true;
        SceneManager.LoadScene(sceneToLoad);
    }
    
}
