using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PopupScript : MonoBehaviour
    {
        public void ButtonClickConfirm()
        {
            TaskControllerDragScript.Instance.BackToMenu();
        }
        public void ButtonClickCancel()
        {
            TaskControllerDragScript.Instance.ClosePopup();
        }
    }
}