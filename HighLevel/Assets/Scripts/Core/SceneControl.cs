using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    public class SceneControl : MonoBehaviour
    {

        public void NextScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
    }
}
