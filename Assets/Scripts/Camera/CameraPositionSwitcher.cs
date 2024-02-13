using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    public class CameraPositionSwitcher : MonoBehaviour
    {
        private CameraTarget? m_target;
        private DateTime m_targetSetTime = DateTime.Now;

        [SerializeField]
        private float m_lerpSeconds = 0.5f;

        [SerializeField]
        private List<CameraTarget> m_cameraTargets = new List<CameraTarget>();

        private void Start()
        {
            m_target = FindTargetAtPosition(CameraPosition.Monitor)!;
            transform.position = m_target.Target.position;
            transform.rotation = m_target.Target.rotation;
        }

        // Update is called once per frame
        private void Update()
        {
            if (m_target != null)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) TryChangeCameraToConnectedPosition(CameraSwitchKey.Left);
                if (Input.GetKeyDown(KeyCode.RightArrow)) TryChangeCameraToConnectedPosition(CameraSwitchKey.Right);
                if (Input.GetKeyDown(KeyCode.UpArrow)) TryChangeCameraToConnectedPosition(CameraSwitchKey.Up);
                if (Input.GetKeyDown(KeyCode.DownArrow)) TryChangeCameraToConnectedPosition(CameraSwitchKey.Down);
                if (Input.GetKeyDown(KeyCode.Escape)) TryChangeCameraToConnectedPosition(CameraSwitchKey.Escape);

                DoCameraLerp();
            }
        }

        private void TryChangeCameraToConnectedPosition(CameraSwitchKey key)
        {
            if (m_target == null) return;

            foreach (CameraSwitchOption switchOption in m_target.SwitchOptions)
            {
                if (switchOption.Key == key)
                {
                    CameraTarget? target = FindTargetAtPosition(switchOption.Position);
                    if (target != null)
                    {
                        m_target = target;
                        m_targetSetTime = DateTime.Now;
                    }
                    return;
                }
            }
        }

        private void DoCameraLerp()
        {
            if (m_target == null) return;

            Vector3 currentPosition = transform.position;
            Quaternion currentRotation = transform.rotation;

            Vector3 targetPosition = m_target.Target.position;
            Quaternion targetRotation = m_target.Target.rotation;

            DateTime endTime = m_targetSetTime + TimeSpan.FromSeconds(m_lerpSeconds);
            DateTime currentTime = DateTime.Now;
            TimeSpan timeLeft = endTime - currentTime;
            float timePassedSeconds = (m_lerpSeconds - (float)timeLeft.TotalSeconds);
            float alpha = timePassedSeconds / m_lerpSeconds;

            Vector3 interpolatedPosition = Vector3.Lerp(currentPosition, targetPosition, alpha);
            Quaternion interpolatedRotation = Quaternion.Lerp(currentRotation, targetRotation, alpha);

            transform.position = interpolatedPosition;
            transform.rotation = interpolatedRotation;
        }

        private CameraTarget? FindTargetAtPosition(CameraPosition position)
        {
            foreach (CameraTarget target in m_cameraTargets)
            {
                if (target.Position == position)
                    return target;
            }

            return null;
        }
    }
}
