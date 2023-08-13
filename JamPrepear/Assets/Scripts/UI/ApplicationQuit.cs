using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ApplicationQuit : MonoBehaviour
    {
        public void Trigger_Restart()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }        
        
        public void Trigger_Quit()
        {
            Application.Quit();
        }
    }
}