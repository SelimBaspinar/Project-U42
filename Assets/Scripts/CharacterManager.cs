using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotationSpeed = 150f;
    [SerializeField] float targetRotationSpeed = 5f;

    private bool isAttacking = false;
    private bool isTeleporting = false;
    private Vector3 teleportPosition;
    private Vector3 targetPosition;
    private bool isRotating = false;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        Vector3 rotation = new Vector3(0f, horizontalInput * rotationSpeed * Time.deltaTime, 0f);
        transform.Rotate(rotation);

        if (Input.GetMouseButton(0) && !isAttacking)
        {
            isAttacking = true;
            //TODO saldırı animasyonu eklenecek
        }

        if (Input.GetMouseButton(1) && !isTeleporting)
        {
            isTeleporting = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                teleportPosition = hit.point;
                //TODO ışınlanma animasyonu eklenecek
            }
            TeleportAnimationFinished();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    targetPosition = hit.point;
                    targetPosition.y = transform.position.y;
                    isRotating = true;
                }
            }
        }

        if (isRotating)
        {
            Vector3 targetDirection = targetPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            if (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, targetRotationSpeed * Time.deltaTime);
            }
            else
            {
                isRotating = false;
            }
        }
    }

    public void AttackAnimationFinished()
    {
        isAttacking = false;
        //TODO saldırı animasyonu durdurulacak
    }

    public void TeleportAnimationFinished()
    {
        isTeleporting = false;
        transform.position = teleportPosition;
        //TODO ışınlanma animasyonu durdurulacak
    }
}
