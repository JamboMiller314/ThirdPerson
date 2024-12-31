using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera m_aimVirtualCamera;

    private StarterAssetsInputs m_starterAssetsInputs;

    private void Awake()
    {
        m_starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        
    }
}
