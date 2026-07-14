using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Reset() => GetComponent<Collider>().isTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        gameManager.HandleWin();
    }
}