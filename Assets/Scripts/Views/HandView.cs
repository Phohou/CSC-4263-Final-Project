using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer; // Hand position container
    private readonly List<CardView> cards = new(); // List of card views in hand

    private void Awake()
    {
        if (splineContainer == null)
        {
            splineContainer = GetComponentInChildren<SplineContainer>();
        }
    }

    public IEnumerator AddCard(CardView cardView)
    {
        cards.Add(cardView);
        yield return UpdateCardPositions(0.15f);    // Update card positions with animation every time a card is added
    }

    private IEnumerator UpdateCardPositions(float duration)
    {
        if (splineContainer == null) yield break;
        if (cards.Count == 0) yield break; // No cards to update
        float cardSpacing = 1f / 10f; // Spacing between cards on the spline (adjust as needed)
        float firstCardPosition = 0.5f - (cardSpacing * (cards.Count - 1) / 2f); // Center the cards on the spline
        Spline spline = splineContainer.Spline;

        for (int i = 0; i < cards.Count; i++)
        {
            float targetPosition = firstCardPosition + (i * cardSpacing);
            Vector3 splinePosition = splineContainer.transform.TransformPoint(spline.EvaluatePosition(targetPosition));
            Vector3 forward = spline.EvaluateTangent(targetPosition);
            Vector3 up = spline.EvaluateUpVector(targetPosition);
            Quaternion targetRotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
            cards[i].transform.DOMove(splinePosition + 0.01f * i * Vector3.back, duration);
            cards[i].transform.DORotate(targetRotation.eulerAngles, duration);
        }

        yield return new WaitForSeconds(duration); // Wait for the animation to complete

    }

}
