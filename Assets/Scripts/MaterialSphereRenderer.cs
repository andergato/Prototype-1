using UnityEngine;

public class MaterialSphereRenderer : MonoBehaviour
{
    [Header("Capture Settings")]
    public int resolution = 512;
    public LayerMask previewLayer;

    [Header("Inner Core Settings")]
    // public GameObject innerCorePrefab; // Optional: leave empty to use a default inner sphere
    public float innerScale = 0.55f;
    public Material innerCoreMaterial; // Optional: assign a contrasting material (e.g., matte dark or bright core)

    public Sprite CaptureMaterialSprite(Material targetMaterial, GameObject innerCorePrefab)
    {
        int layerIndex = GetLayerIndex(previewLayer);

        // 1. Create the outer 3D Sphere without a collider
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        SafeDestroy(sphere.GetComponent<Collider>());
        sphere.transform.position = new Vector3(0, -1000f, 0);
        sphere.layer = layerIndex;
        sphere.GetComponent<MeshRenderer>().sharedMaterial = targetMaterial;

        // 1b. Create the inner GameObject inside the sphere
        GameObject innerObj;
        if (innerCorePrefab != null)
        {
            innerObj = Instantiate(innerCorePrefab, sphere.transform.position, Quaternion.identity);
        }
        else
        {
            innerObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        innerObj.transform.localScale = Vector3.one * innerScale;
        SetLayerRecursively(innerObj, layerIndex);

        // Remove any colliders on the inner object so it never affects physics
        foreach (Collider col in innerObj.GetComponentsInChildren<Collider>())
        {
            SafeDestroy(col);
        }

        // Apply optional override material to the inner object
        if (innerCoreMaterial != null && innerObj.TryGetComponent<MeshRenderer>(out MeshRenderer innerRenderer))
        {
            innerRenderer.sharedMaterial = innerCoreMaterial;
        }

        // 2. Set up a dedicated Camera with transparent background
        GameObject camObj = new GameObject("PreviewCamera");
        Camera cam = camObj.AddComponent<Camera>();
        cam.transform.position = sphere.transform.position + new Vector3(0, 0, -1.6f);
        cam.transform.LookAt(sphere.transform.position);
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0, 0, 0, 0);
        cam.cullingMask = previewLayer;
        cam.fieldOfView = 35f;

        // 3. Add Key Light and Rim Light
        GameObject keyLightObj = new GameObject("KeyLight");
        Light keyLight = keyLightObj.AddComponent<Light>();
        keyLight.type = LightType.Directional;
        keyLight.transform.rotation = Quaternion.Euler(40f, -35f, 0f);
        keyLight.intensity = 1.2f;
        keyLight.cullingMask = previewLayer;

        GameObject rimLightObj = new GameObject("RimLight");
        Light rimLight = rimLightObj.AddComponent<Light>();
        rimLight.type = LightType.Directional;
        rimLight.transform.rotation = Quaternion.Euler(-20f, 145f, 0f);
        rimLight.intensity = 0.8f;
        rimLight.color = new Color(0.85f, 0.92f, 1f);
        rimLight.cullingMask = previewLayer;

        // 4. Render to a RenderTexture
        RenderTexture rt = new RenderTexture(resolution, resolution, 24, RenderTextureFormat.ARGB32);
        rt.antiAliasing = 4;
        cam.targetTexture = rt;
        cam.Render();

        // 5. Read pixels into a Texture2D
        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
        tex.Apply();

        // 6. Safely clean up all temporary objects
        RenderTexture.active = null;
        cam.targetTexture = null;
        rt.Release();

        SafeDestroy(rt);
        SafeDestroy(sphere);
        SafeDestroy(innerObj);
        SafeDestroy(camObj);
        SafeDestroy(keyLightObj);
        SafeDestroy(rimLightObj);

        // 7. Return as a Sprite
        return Sprite.Create(tex, new Rect(0, 0, resolution, resolution), new Vector2(0.5f, 0.5f));
    }

    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    private void SafeDestroy(Object obj)
    {
        if (obj == null) return;

        if (Application.isPlaying)
        {
            Destroy(obj);
        }
        else
        {
            DestroyImmediate(obj);
        }
    }

    private int GetLayerIndex(LayerMask mask)
    {
        int layer = 0;
        int bitmask = mask.value;
        while (bitmask > 1) { bitmask >>= 1; layer++; }
        return layer;
    }
}
