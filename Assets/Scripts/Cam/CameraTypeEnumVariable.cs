using Codetox.Variables;
using UnityEngine;

namespace Cam
{
    [CreateAssetMenu(fileName = nameof(CameraTypeEnumVariable), menuName = "Trashy Games/Variables/Enums/Camera Type")]
    public class CameraTypeEnumVariable : Variable<CameraType>
    {
    }
}