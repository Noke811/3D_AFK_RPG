using System.Collections.Generic;
using UnityEngine;

public class PreviewGenerator : MonoBehaviour
{
    private Camera modelCam;
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] Transform pivot;
    [SerializeField] LayerMask layerMask;
    private int layer_UIModel;

    private Dictionary<ItemData, Sprite> previewCache;

    private void Awake()
    {
        modelCam = GetComponentInChildren<Camera>();

        previewCache = new Dictionary<ItemData, Sprite>();
        layer_UIModel = GetSingleLayer(layerMask);
    }

    // 아이템 프리뷰 이미지 가져오기
    public Sprite GetPreview(ItemData item)
    {
        if (previewCache.TryGetValue(item, out Sprite cached))
            return cached;

        return GeneratePreview(item);
    }

    // 아이템 프리뷰 이미지 생성
    private Sprite GeneratePreview(ItemData item)
    {
        // 1. 인스턴스 생성
        GameObject instance = Instantiate(item.ItemPrefab, pivot);
        instance.transform.localPosition = Vector3.zero;
        SetLayerRecursive(instance.transform, layer_UIModel);

        // 2. 찍기
        modelCam.targetTexture = renderTexture;
        modelCam.Render();

        // 3. Texture2D로 저장
        RenderTexture.active = renderTexture;
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;

        // 4. Sprite로 변환
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        previewCache[item] = sprite;

        // 5. 정리
        Destroy(instance);

        return sprite;
    }

    // 자식 오브젝트의 레이어까지 바꾸는 재귀 함수
    private void SetLayerRecursive(Transform t, int layer)
    {
        t.gameObject.layer = layer;
        foreach (Transform child in t)
            SetLayerRecursive(child, layer);
    }

    // 단일 레이어마스크 정수형 레이어로 변환
    private int GetSingleLayer(LayerMask mask)
    {
        if (!IsSingleLayer(mask))
        {
            throw new System.Exception("LayerMask가 단일 레이어가 아닙니다.");
        }

        int value = mask.value;
        int layer = -1;

        while (value != 0)
        {
            value = value >> 1;
            layer++;
        }

        return layer;
    }

    // 단일 레이어인지 확인
    private bool IsSingleLayer(LayerMask mask)
    {
        int value = mask.value;
        return value != 0 && (value & (value - 1)) == 0;
    }
}
