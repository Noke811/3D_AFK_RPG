using System.Collections.Generic;
using UnityEngine;

public class PreviewGenerator : MonoBehaviour
{
    private Camera modelCam;
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] Transform pivot;
    [SerializeField] LayerMask uiModelLayer;
    private int _uiModelLayer = 6;

    private Dictionary<ItemData, Sprite> previewCache;

    private void Awake()
    {
        modelCam = GetComponentInChildren<Camera>();

        previewCache = new Dictionary<ItemData, Sprite>();
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
        SetLayerRecursive(instance.transform, _uiModelLayer);

        // 2. 카메라 설정
        modelCam.transform.position = pivot.position + new Vector3(0, 0, -1);
        modelCam.transform.LookAt(pivot);

        // 3. 찍기
        modelCam.Render();

        // 4. Texture2D로 저장
        RenderTexture.active = renderTexture;
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;

        // 5. Sprite로 변환
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        previewCache[item] = sprite;

        // 6. 정리
        Destroy(instance);

        return sprite;
    }

    // 자식 오브젝트의 레이어까지 바꾸는 재귀 함수
    void SetLayerRecursive(Transform t, int layer)
    {
        t.gameObject.layer = layer;
        foreach (Transform child in t)
            SetLayerRecursive(child, layer);
    }
}
