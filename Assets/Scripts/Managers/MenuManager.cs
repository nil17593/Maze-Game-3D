using UnityEngine.SceneManagement;
using UnityEngine;

namespace RoninLabs.Maze3D
{
    /// <summary>
    /// handles menu scene 
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        public void OnPlayButtonClicked()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
