using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFXManager : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _coinPrefab;
    [SerializeField] GameObject _targetGameObject;
    [SerializeField] float _speed;

    public void SpawnCoin(Vector3 startPosition)
    {
        GameObject coin = Instantiate(_coinPrefab, _canvas.transform);
        CoinFXAnimation moveToUI = coin.GetComponent<CoinFXAnimation>();
        moveToUI.InitFXCoin(_targetGameObject);
        RectTransform coinRectTransform = coin.GetComponent<RectTransform>();
        coinRectTransform.anchoredPosition = WorldToOverlayPoint(startPosition, _canvas, Camera.main);
    }

    private Vector2 WorldToOverlayPoint(Vector3 worldPoint, Canvas canvas, Camera mainCamera)
    {
        RectTransform canvasRectTransform = _canvas.GetComponent<RectTransform>();
        Vector2 adjustedPosition = mainCamera.WorldToScreenPoint(worldPoint);

        adjustedPosition.x *= canvasRectTransform.rect.width / (float)mainCamera.pixelWidth;
        adjustedPosition.y *= canvasRectTransform.rect.height / (float)mainCamera.pixelHeight;

        return adjustedPosition - canvasRectTransform.sizeDelta / 2f;
    }

}
