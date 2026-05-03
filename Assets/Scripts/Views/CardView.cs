using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private SpriteRenderer imageSRC;
    [SerializeField] private GameObject wrappper;
    public Card Card { get; private set; }

    public void Setup(Card card)
    {
        if (card == null)
        {
            Debug.LogError("CardView.Setup called with null card.");
            return;
        }
        if (title == null || description == null || mana == null || imageSRC == null)
        {
            Debug.LogError("CardView is missing TMP/SpriteRenderer references.");
            return;
        }
        Card = card;
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();
        imageSRC.sprite = card.Image;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (wrappper != null)
        {
            wrappper.SetActive(false);
        }
        Vector3 pos = new(transform.position.x, -3, 0);
        if (CardViewHoverSystem.Instance != null)
        {
            CardViewHoverSystem.Instance.Show(Card, pos);
        }
        Debug.Log($"Mouse entered card: {Card.Title} at position {pos}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CardViewHoverSystem.Instance != null)
        {
            CardViewHoverSystem.Instance.Hide();
        }
        if (wrappper != null)
        {
            wrappper.SetActive(true);
        }
    }

}