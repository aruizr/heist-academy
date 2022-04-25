using UnityEngine;

namespace Utilities
{
    public class Logger : MonoBehaviour
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void Log(GameObject obj)
        {
            Debug.Log(obj);
        }
    
        public void Log(Vector2 message)
        {
            Debug.Log(message);
        }
    }
}