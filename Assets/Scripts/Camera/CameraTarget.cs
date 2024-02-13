using System;
using UnityEngine;

namespace Camera
{
    [Serializable]
    public class CameraTarget
    {
        public CameraPosition Position;
        public Transform Target = null!;

        public CameraSwitchOption[] SwitchOptions = Array.Empty<CameraSwitchOption>();
    }
}