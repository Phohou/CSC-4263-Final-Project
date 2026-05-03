using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class TestSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private HandView handView;
    [SerializeField] private CardData cardData;
    void Start()
    {
        if (handView == null)
        {
            handView = FindFirstObjectByType<HandView>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current?.spaceKey.wasPressedThisFrame == true)
        {
            if (handView == null || cardData == null)
            {
                Debug.LogError("TestSystem is missing HandView or CardData reference.");
                return;
            }
            Card card = new Card(cardData);
            CardView cardView = CardViewCreator.Instance.CreateCardView(card, handView.transform.position, Quaternion.identity);
            StartCoroutine(handView.AddCard(cardView));
        }
    }
}
