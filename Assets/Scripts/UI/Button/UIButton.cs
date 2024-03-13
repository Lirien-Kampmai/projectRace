using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectCar
{
    namespace UI
    {
        public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
        {
            [SerializeField] private bool Interactable;

            private bool focus = false;
            public bool Focus => focus;

            public UnityEvent ClickButton;

            public event UnityAction<UIButton> PointerEnter;
            public event UnityAction<UIButton> PointerExit;
            public event UnityAction<UIButton> PointerClick;

            public virtual void SetOnFocus()
            {
                if (Interactable == false) return;

                focus = true;
            }

            public virtual void SetOffFocus()
            {
                if (Interactable == false) return;

                focus = false;
            }

            public virtual void OnPointerEnter(PointerEventData eventData)
            {
                if (Interactable == false) return;

                //SetOnFocus();

                PointerEnter?.Invoke(this);
            }

            public virtual void OnPointerExit(PointerEventData eventData)
            {
                if (Interactable == false) return;

                //SetOffFocus();

                PointerExit?.Invoke(this);
            }

            public virtual void OnPointerClick(PointerEventData eventData)
            {
                if (Interactable == false) return;

                PointerClick?.Invoke(this);
                ClickButton?.Invoke();
            }
        }
    }
}


