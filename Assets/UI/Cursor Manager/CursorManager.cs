using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CursorManager : MonoBehaviour
{
    #region Singleton
    public static CursorManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public enum CursorType
    {
        DefaultCursor,
        LoadingCursor
    }
    [System.Serializable]
    struct CustomCursor
    {
        public string cursorName;
        public CursorType cursorType;
        public Sprite[] cursorImages;
        public Texture2D[] cachedImages;
    }

    [SerializeField] Sprite defaultCursor;
    [SerializeField] Sprite defaultHoverCursor;
    [SerializeField] int sizeX;
    [SerializeField] int sizeY;
    [SerializeField] List<CustomCursor> customCursors = new List<CustomCursor>();
    private static bool m_Initialized = false;

    private Coroutine animatingCoroutine;


    Texture2D ResizeTexture(Texture2D sourceTexture, int targetWidth, int targetHeight)
    {
        Texture2D resizedTexture = new Texture2D(targetWidth, targetHeight, TextureFormat.RGBA32, false);
        resizedTexture.filterMode = FilterMode.Point;

        float xRatio = (float)sourceTexture.width / targetWidth;
        float yRatio = (float)sourceTexture.height / targetHeight;

        for (int x = 0; x < targetWidth; x++)
        {
            for (int y = 0; y < targetHeight; y++)
            {
                int xCoord = Mathf.FloorToInt(x * xRatio);
                int yCoord = Mathf.FloorToInt(y * yRatio);

                Color color = sourceTexture.GetPixel(xCoord, yCoord);
                resizedTexture.SetPixel(x, y, color);
            }
        }

        resizedTexture.Apply();
        return resizedTexture;
    }

    Texture2D ConvertToTexture2D(Sprite sprite)
    {
        var spriteTexture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);
        var pixels = sprite.texture.GetPixels(
           (int)sprite.textureRect.x,
           (int)sprite.textureRect.y,
           (int)sprite.textureRect.width,
           (int)sprite.textureRect.height);

        spriteTexture2D.SetPixels(pixels);
        spriteTexture2D.filterMode = FilterMode.Point;
        return spriteTexture2D;
    }

    void CacheTexturesOnStart()
    {
        for (int i = 0; i < customCursors.Count; i++)
        {
            CustomCursor currentCursor = customCursors[i];

            currentCursor.cachedImages = new Texture2D[currentCursor.cursorImages.Length];

            for (int j = 0; j < currentCursor.cursorImages.Length; j++)
            {
                Texture2D upScaledTexture = ConvertToTexture2D(currentCursor.cursorImages[j]);
                upScaledTexture = ResizeTexture(upScaledTexture, sizeX, sizeY);
                currentCursor.cachedImages[j] = upScaledTexture;
            }

            customCursors[i] = currentCursor;
        }
    }


    void Start()
    {
        if (!m_Initialized)
        {
            m_Initialized = true;
            CacheTexturesOnStart();
        }
    }


    private IEnumerator AnimateCursor(CustomCursor customCursor, float animationDelaySeconds)
    {
        for (int i = 0; i < customCursor.cachedImages.Length; i++)
        {
            Cursor.SetCursor(customCursor.cachedImages[i], Vector2.zero, CursorMode.ForceSoftware);
            yield return new WaitForSeconds(animationDelaySeconds);
        }
    }

    int FindCursorByName(string cursorName)
    {
        for (int i = 0; i < customCursors.Count; i++)
        {
            if (customCursors[i].cursorName.Equals(cursorName))
            {
                return i;
            }
        }

        return -1;
    }

    public void SwitchToDefaultCursor()
    {
        if (animatingCoroutine != null)
        {
            StopCoroutine(animatingCoroutine);
            animatingCoroutine = null;
        }

        Texture2D cursorTexture = ConvertToTexture2D(defaultCursor);
        cursorTexture = ResizeTexture(cursorTexture, sizeX, sizeY);

        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void SwitchToDefaultHoverCursor()
    {
        if (animatingCoroutine != null)
        {
            StopCoroutine(animatingCoroutine);
            animatingCoroutine = null;
        }

        Texture2D cursorTexture = ConvertToTexture2D(defaultHoverCursor);
        cursorTexture = ResizeTexture(cursorTexture, sizeX, sizeY);

        Cursor.SetCursor(cursorTexture, new Vector2(2, 2), CursorMode.ForceSoftware);
    }

    public void SetCursorType(CursorType cursorType, float animationDelaySeconds)
    {
        switch (cursorType)
        {
            case CursorType.DefaultCursor:
                SwitchToDefaultCursor();
                break;

            case CursorType.LoadingCursor:
                SwitchToDefaultCursor();
                int cursorIndex = FindCursorByName("LoadingCursor");
                if (cursorIndex == -1)
                { Debug.LogWarning("Cursor not found"); return; }
                if (animatingCoroutine != null)
                {
                    StopCoroutine(animatingCoroutine);
                    animatingCoroutine = null;
                }
                animatingCoroutine = StartCoroutine(AnimateCursor(customCursors[cursorIndex], animationDelaySeconds));
                break;
        }
    }
}
