using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Rigidbody m_bulletRigidBody;

    private void Awake()
    {
        m_bulletRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 10f;
        m_bulletRigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
