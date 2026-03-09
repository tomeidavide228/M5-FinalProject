using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [SerializeField] private UnityEvent onLeverPull;
    [SerializeField] private GameObject _pressIcon;
    private Animator _animation;
    private bool _isPlayerInRange;

    private void Start()
    {
        _animation = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            _animation.SetTrigger("Pull");
            onLeverPull?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            _pressIcon.SetActive(true);
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pressIcon.SetActive(false);
            _isPlayerInRange = false;
        }
    }
}
