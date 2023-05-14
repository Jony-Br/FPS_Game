using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private Camera _camera;

    private float _xRot;
    private float _yRot;

    void LateUpdate()
    {
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                return;
            }

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                _xRot += touch.deltaPosition.x * _sensitivity * Time.deltaTime;
                _yRot += touch.deltaPosition.y * _sensitivity * Time.deltaTime;
                _yRot = Mathf.Clamp(_yRot, -90, 90);

                transform.rotation = Quaternion.Euler(-_yRot, _xRot, 0);
                _playerBody.transform.rotation = Quaternion.Euler(Vector3.up * _xRot);
            }

        }
    }
}
