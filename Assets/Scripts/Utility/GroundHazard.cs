using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundHazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}