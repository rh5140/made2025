using UnityEngine;
using UnityEngine.Events;

public class ProtagRevive : MonoBehaviour
{
    public UnityEvent onReviveTriggerEnter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        onReviveTriggerEnter?.Invoke();
    }
}
