using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelnumber;
    [Button]
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelnumber);
    }
}
    