using TMPro;
using UnityEngine;

namespace UI
{
    public class FontStyleController : MonoBehaviour
    {
        public void SetUnderline(TMP_Text text)
        {
            text.fontStyle = FontStyles.Underline;
        }
        
        public void SetNormal(TMP_Text text)
        {
            text.fontStyle = FontStyles.Normal;
        }
        
        public void SetBold(TMP_Text text)
        {
            text.fontStyle = FontStyles.Bold;
        }
    }
}