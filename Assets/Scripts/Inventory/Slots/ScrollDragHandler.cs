using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollDragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IScrollHandler
{
    private GameObject _scrollView;

    void Start()
    {
        // Получаем компонент ScrollRect у родительского объекта
        _scrollView = transform.parent.gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Отключаем прокрутку
        _scrollView.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Включаем прокрутку
        _scrollView.SetActive(true);
    }

    public void OnScroll(PointerEventData eventData)
    {
        // Включаем прокрутку
        _scrollView.SetActive(true);
    }
}