using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera m_aimVirtualCamera;
    [SerializeField] private float m_normalSensitivity;
    [SerializeField] private float m_aimSensitivity;
    [SerializeField] private LayerMask m_aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform m_bulletProjectile;
    [SerializeField] private Transform m_spawnBulletPosition;

    private ThirdPersonController m_thirdPersonController;
    private StarterAssetsInputs m_starterAssetsInputs;
    private Animator m_animator;

    private void Awake()
    {
        m_thirdPersonController = GetComponent<ThirdPersonController>();
        m_starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        m_animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, m_aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }
       
        if (m_starterAssetsInputs.aim)
        {
            m_aimVirtualCamera.gameObject.SetActive(true);
            m_thirdPersonController.SetSensitivity(m_aimSensitivity);
            m_thirdPersonController.SetRotateToMove(false);
            m_animator.SetLayerWeight(1, Mathf.Lerp(m_animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            m_aimVirtualCamera.gameObject.SetActive(false);
            m_thirdPersonController.SetSensitivity(m_normalSensitivity);
            m_thirdPersonController.SetRotateToMove(true);
            m_animator.SetLayerWeight(1, Mathf.Lerp(m_animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));


        }

        if (m_starterAssetsInputs.shoot)
        {
            Vector3 aimDir = (mouseWorldPosition - m_spawnBulletPosition.position).normalized;
            Instantiate(m_bulletProjectile, m_spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            m_starterAssetsInputs.shoot = false;
        }

    }
}
