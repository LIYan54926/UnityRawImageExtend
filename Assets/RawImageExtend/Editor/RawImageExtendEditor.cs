using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.SceneManagement;
using System;
using UnityEngine.Rendering;

namespace UnityEditor.UI
{
    /// <summary>
    /// Editor class used to edit UI Images.
    /// </summary>
    [CustomEditor(typeof(RawImageExtend), true)]
    [CanEditMultipleObjects]
    /// <summary>
    ///   Custom editor for RawImage.
    ///   Extend this class to write a custom editor for a RawImage-derived component.
    /// </summary>
    public class RawImageExtendEditor : GraphicEditor
    {
        SerializedProperty m_Texture;
        SerializedProperty m_UVRect;

        GUIContent m_UVRectContent;

        //SerializedProperty m_MaskMap;
        //SerializedProperty m_MaskType;
        //SerializedProperty m_MaskWidth;
        //SerializedProperty m_MaskHeight;
        //SerializedProperty m_MaskSides;
        //SerializedProperty m_MaskRoundness;
        //SerializedProperty m_RotationUV;
        //SerializedProperty m_TwirlStrength;
        //SerializedProperty m_TwirlOffset;
        //SerializedProperty m_MaskSmoothStepLeft;
        //SerializedProperty m_MaskSmoothStepRight;

        //GUIContent m_MaskSmoothStepLeftContent;
        //GUIContent m_MaskSmoothStepRightContent;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_UVRectContent = EditorGUIUtility.TrTextContent("UV Rect");
            m_Texture = serializedObject.FindProperty("m_Texture");
            m_UVRect = serializedObject.FindProperty("m_UVRect");
            m_Maskable = serializedObject.FindProperty("m_Maskable");

            //Mask Property
            //m_MaskMap = serializedObject.FindProperty("m_MaskMap");
            //m_MaskType = serializedObject.FindProperty("m_MaskType");
            //m_MaskWidth = serializedObject.FindProperty("m_MaskWidth");
            //m_MaskHeight = serializedObject.FindProperty("m_MaskHeight");
            //m_MaskSides = serializedObject.FindProperty("m_MaskSides"); ;
            //m_MaskRoundness = serializedObject.FindProperty("m_MaskRoundness"); ;
            //m_RotationUV = serializedObject.FindProperty("m_MaskRotationUV");
            //m_TwirlStrength = serializedObject.FindProperty("m_MaskTwirlStrength");
            //m_TwirlOffset = serializedObject.FindProperty("m_MaskTwirlOffset");
            //m_MaskSmoothStepLeft = serializedObject.FindProperty("m_MaskSmoothLeft");
            //m_MaskSmoothStepRight = serializedObject.FindProperty("m_MaskSmoothRight"); ;

            //m_MaskSmoothStepLeftContent = EditorGUIUtility.TrTextContent("Left  x < y  x:[0,1] y:[0,1]");
            //m_MaskSmoothStepRightContent = EditorGUIUtility.TrTextContent("Right  x > y  x:[0,1] y:[0,1]");

            SetShowNativeSize(true);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Texture);

            AppearanceControlsGUI();
            RaycastControlsGUI();
            EditorGUILayout.PropertyField(m_Maskable);
            EditorGUILayout.PropertyField(m_UVRect, m_UVRectContent);
            SetShowNativeSize(false);
            NativeSizeButtonGUI();

            //EditorGUILayout.Space();
            //RawImageExtend rawImage = (target as RawImageExtend);

            //if (rawImage.material.shader.name == "UI/Polygon Mask")
            //{

            //    EditorGUI.BeginChangeCheck();
            //    EditorGUILayout.PropertyField(m_MaskType);

            //    if (EditorGUI.EndChangeCheck())
            //    {
            //        serializedObject.ApplyModifiedProperties();
            //        SetMaskTypeKeyword(rawImage.material, rawImage.maskType.ToString());

            //    }

            //    switch (m_MaskType.enumValueIndex)
            //    {
            //        case 1:
            //            EditorGUI.BeginChangeCheck();
            //            EditorGUILayout.PropertyField(m_MaskMap);
            //            if (EditorGUI.EndChangeCheck())
            //            {
            //                serializedObject.ApplyModifiedProperties();
            //                rawImage.material.SetTexture(RawImageExtend._MaskMap, rawImage.maskMap);

            //            }
            //            break;
            //        case 2:
            //            EditorGUI.BeginChangeCheck();
            //            EditorGUILayout.PropertyField(m_MaskWidth);
            //            EditorGUILayout.PropertyField(m_MaskHeight);
            //            if (EditorGUI.EndChangeCheck())
            //            {
            //                serializedObject.ApplyModifiedProperties();
            //                rawImage.material.SetFloat(RawImageExtend._MaskWidth, rawImage.maskWidth);
            //                rawImage.material.SetFloat(RawImageExtend._MaskHeight, rawImage.maskHeight);

            //            }
            //            break;
            //        case 3:
            //            EditorGUI.BeginChangeCheck();
            //            EditorGUILayout.PropertyField(m_MaskWidth);
            //            EditorGUILayout.PropertyField(m_MaskHeight);
            //            EditorGUILayout.PropertyField(m_MaskSides);
            //            if (EditorGUI.EndChangeCheck())
            //            {
            //                serializedObject.ApplyModifiedProperties();
            //                rawImage.material.SetFloat(RawImageExtend._MaskWidth, rawImage.maskWidth);
            //                rawImage.material.SetFloat(RawImageExtend._MaskHeight, rawImage.maskHeight);
            //                rawImage.material.SetInteger(RawImageExtend._MaskSides, rawImage.maskSides);

            //            }
            //            break;
            //        case 4:
            //            EditorGUI.BeginChangeCheck();
            //            EditorGUILayout.PropertyField(m_MaskWidth);
            //            EditorGUILayout.PropertyField(m_MaskHeight);
            //            if (EditorGUI.EndChangeCheck())
            //            {
            //                serializedObject.ApplyModifiedProperties();
            //                rawImage.material.SetFloat(RawImageExtend._MaskWidth, rawImage.maskWidth);
            //                rawImage.material.SetFloat(RawImageExtend._MaskHeight, rawImage.maskHeight);

            //            }
            //            break;
            //        case 5:
            //            EditorGUI.BeginChangeCheck();
            //            EditorGUILayout.PropertyField(m_MaskWidth);
            //            EditorGUILayout.PropertyField(m_MaskHeight);
            //            EditorGUILayout.PropertyField(m_MaskSides);
            //            EditorGUILayout.PropertyField(m_MaskRoundness);
            //            if (EditorGUI.EndChangeCheck())
            //            {
            //                serializedObject.ApplyModifiedProperties();
            //                rawImage.material.SetFloat(RawImageExtend._MaskWidth, rawImage.maskWidth);
            //                rawImage.material.SetFloat(RawImageExtend._MaskHeight, rawImage.maskHeight);
            //                rawImage.material.SetInteger(RawImageExtend._MaskSides, rawImage.maskSides);
            //                rawImage.material.SetFloat(RawImageExtend._MaskRoundness, rawImage.maskRoundness);

            //            }
            //            break;
            //        case 6:
            //            EditorGUI.BeginChangeCheck();
            //            EditorGUILayout.PropertyField(m_MaskWidth);
            //            EditorGUILayout.PropertyField(m_MaskHeight);
            //            EditorGUILayout.PropertyField(m_MaskRoundness);
            //            if (EditorGUI.EndChangeCheck())
            //            {
            //                serializedObject.ApplyModifiedProperties();
            //                rawImage.material.SetFloat(RawImageExtend._MaskWidth, rawImage.maskWidth);
            //                rawImage.material.SetFloat(RawImageExtend._MaskHeight, rawImage.maskHeight);
            //                rawImage.material.SetFloat(RawImageExtend._MaskRoundness, rawImage.maskRoundness);

            //            }
            //            break;
            //        default:
            //            break;
            //    }

            //    if (m_MaskType.enumValueIndex != 0)
            //    {
            //        EditorGUILayout.LabelField("Mask UV");
            //        EditorGUI.BeginChangeCheck();
            //        EditorGUILayout.PropertyField(m_RotationUV);
            //        EditorGUILayout.PropertyField(m_TwirlStrength);
            //        EditorGUILayout.PropertyField(m_TwirlOffset);
            //        if (EditorGUI.EndChangeCheck())
            //        {
            //            serializedObject.ApplyModifiedProperties();
            //            rawImage.material.SetVector(RawImageExtend._MaskPolygonUV, rawImage.maskPolygonUV);

            //        }

            //        EditorGUILayout.LabelField("Smooth ");
            //        EditorGUILayout.LabelField("smoothstep(x, y, m) - smoothstep(x, y, m)");
            //        EditorGUI.BeginChangeCheck();
            //        EditorGUILayout.PropertyField(m_MaskSmoothStepLeft, m_MaskSmoothStepLeftContent);
            //        EditorGUILayout.PropertyField(m_MaskSmoothStepRight, m_MaskSmoothStepRightContent);
            //        if (EditorGUI.EndChangeCheck())
            //        {
            //            serializedObject.ApplyModifiedProperties();
            //            rawImage.material.SetVector(RawImageExtend._MaskSmooth, rawImage.maskSmooth);

            //        }

            //    }
            //}

            serializedObject.ApplyModifiedProperties();

        }

        //void SetMaskTypeKeyword(Material material, string keyword)
        //{
        //    string[] enumNames = Enum.GetNames(typeof(MaskType));
        //    for (int i = 0; i < enumNames.Length; i++)
        //    {
        //        if (enumNames[i] == keyword)
        //        {
        //            material.EnableKeyword("_OVERLAY_" + keyword);
        //        }
        //        else
        //        {
        //            material.DisableKeyword("_OVERLAY_" + enumNames[i]);
        //        }
        //    }
        //}

        void SetShowNativeSize(bool instant)
        {
            base.SetShowNativeSize(m_Texture.objectReferenceValue != null, instant);
        }

        private static Rect Outer(RawImageExtend rawImage)
        {
            Rect outer = rawImage.uvRect;
            outer.xMin *= rawImage.rectTransform.rect.width;
            outer.xMax *= rawImage.rectTransform.rect.width;
            outer.yMin *= rawImage.rectTransform.rect.height;
            outer.yMax *= rawImage.rectTransform.rect.height;
            return outer;
        }

        /// <summary>
        /// Allow the texture to be previewed.
        /// </summary>

        public override bool HasPreviewGUI()
        {
            RawImageExtend rawImage = target as RawImageExtend;
            if (rawImage == null)
                return false;

            var outer = Outer(rawImage);
            return outer.width > 0 && outer.height > 0;
        }

        /// <summary>
        /// Draw the Image preview.
        /// </summary>

        public override void OnPreviewGUI(Rect rect, GUIStyle background)
        {
            RawImageExtend rawImage = target as RawImageExtend;
            Texture tex = rawImage.mainTexture;
            if (tex == null)
                return;

            var outer = Outer(rawImage);
            SpriteDrawUtilityExtend.DrawSprite(tex, rect, outer, rawImage.uvRect, rawImage.canvasRenderer.GetColor());
        }

        /// <summary>
        /// Info String drawn at the bottom of the Preview
        /// </summary>

        public override string GetInfoString()
        {
            RawImageExtend rawImage = target as RawImageExtend;

            // Image size Text
            string text = string.Format("RawImageExtend Size: {0}x{1}",
                Mathf.RoundToInt(Mathf.Abs(rawImage.rectTransform.rect.width)),
                Mathf.RoundToInt(Mathf.Abs(rawImage.rectTransform.rect.height)));

            return text;
        }

    }

    /// <summary>
    /// This script adds the UI menu options to the Unity Editor.
    /// </summary>

    static internal class MenuOptionsExtend
    {
        private const string kUILayerName = "UI";

        private const string kStandardSpritePath = "UI/Skin/UISprite.psd";
        private const string kBackgroundSpritePath = "UI/Skin/Background.psd";
        private const string kInputFieldBackgroundPath = "UI/Skin/InputFieldBackground.psd";
        private const string kKnobPath = "UI/Skin/Knob.psd";
        private const string kCheckmarkPath = "UI/Skin/Checkmark.psd";
        private const string kDropdownArrowPath = "UI/Skin/DropdownArrow.psd";
        private const string kMaskPath = "UI/Skin/UIMask.psd";

        static private DefaultControls.Resources s_StandardResources;

        static private DefaultControls.Resources GetStandardResources()
        {
            if (s_StandardResources.standard == null)
            {
                s_StandardResources.standard = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
                s_StandardResources.background = AssetDatabase.GetBuiltinExtraResource<Sprite>(kBackgroundSpritePath);
                s_StandardResources.inputField = AssetDatabase.GetBuiltinExtraResource<Sprite>(kInputFieldBackgroundPath);
                s_StandardResources.knob = AssetDatabase.GetBuiltinExtraResource<Sprite>(kKnobPath);
                s_StandardResources.checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite>(kCheckmarkPath);
                s_StandardResources.dropdown = AssetDatabase.GetBuiltinExtraResource<Sprite>(kDropdownArrowPath);
                s_StandardResources.mask = AssetDatabase.GetBuiltinExtraResource<Sprite>(kMaskPath);
            }
            return s_StandardResources;
        }

        private static void SetPositionVisibleinSceneView(RectTransform canvasRTransform, RectTransform itemTransform)
        {
            SceneView sceneView = SceneView.lastActiveSceneView;

            // Couldn't find a SceneView. Don't set position.
            if (sceneView == null || sceneView.camera == null)
                return;

            // Create world space Plane from canvas position.
            Vector2 localPlanePosition;
            Camera camera = sceneView.camera;
            Vector3 position = Vector3.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRTransform, new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2), camera, out localPlanePosition))
            {
                // Adjust for canvas pivot
                localPlanePosition.x = localPlanePosition.x + canvasRTransform.sizeDelta.x * canvasRTransform.pivot.x;
                localPlanePosition.y = localPlanePosition.y + canvasRTransform.sizeDelta.y * canvasRTransform.pivot.y;

                localPlanePosition.x = Mathf.Clamp(localPlanePosition.x, 0, canvasRTransform.sizeDelta.x);
                localPlanePosition.y = Mathf.Clamp(localPlanePosition.y, 0, canvasRTransform.sizeDelta.y);

                // Adjust for anchoring
                position.x = localPlanePosition.x - canvasRTransform.sizeDelta.x * itemTransform.anchorMin.x;
                position.y = localPlanePosition.y - canvasRTransform.sizeDelta.y * itemTransform.anchorMin.y;

                Vector3 minLocalPosition;
                minLocalPosition.x = canvasRTransform.sizeDelta.x * (0 - canvasRTransform.pivot.x) + itemTransform.sizeDelta.x * itemTransform.pivot.x;
                minLocalPosition.y = canvasRTransform.sizeDelta.y * (0 - canvasRTransform.pivot.y) + itemTransform.sizeDelta.y * itemTransform.pivot.y;

                Vector3 maxLocalPosition;
                maxLocalPosition.x = canvasRTransform.sizeDelta.x * (1 - canvasRTransform.pivot.x) - itemTransform.sizeDelta.x * itemTransform.pivot.x;
                maxLocalPosition.y = canvasRTransform.sizeDelta.y * (1 - canvasRTransform.pivot.y) - itemTransform.sizeDelta.y * itemTransform.pivot.y;

                position.x = Mathf.Clamp(position.x, minLocalPosition.x, maxLocalPosition.x);
                position.y = Mathf.Clamp(position.y, minLocalPosition.y, maxLocalPosition.y);
            }

            itemTransform.anchoredPosition = position;
            itemTransform.localRotation = Quaternion.identity;
            itemTransform.localScale = Vector3.one;
        }

        private static void PlaceUIElementRoot(GameObject element, MenuCommand menuCommand)
        {
            GameObject parent = menuCommand.context as GameObject;
            bool explicitParentChoice = true;
            if (parent == null)
            {
                parent = GetOrCreateCanvasGameObject();
                explicitParentChoice = false;

                // If in Prefab Mode, Canvas has to be part of Prefab contents,
                // otherwise use Prefab root instead.
                PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
                if (prefabStage != null && !prefabStage.IsPartOfPrefabContents(parent))
                    parent = prefabStage.prefabContentsRoot;
            }
            if (parent.GetComponentsInParent<Canvas>(true).Length == 0)
            {
                // Create canvas under context GameObject,
                // and make that be the parent which UI element is added under.
                GameObject canvas = MenuOptionsExtend.CreateNewUI();
                canvas.transform.SetParent(parent.transform, false);
                parent = canvas;
            }

            // Setting the element to be a child of an element already in the scene should
            // be sufficient to also move the element to that scene.
            // However, it seems the element needs to be already in its destination scene when the
            // RegisterCreatedObjectUndo is performed; otherwise the scene it was created in is dirtied.
            SceneManager.MoveGameObjectToScene(element, parent.scene);

            Undo.RegisterCreatedObjectUndo(element, "Create " + element.name);

            if (element.transform.parent == null)
            {
                Undo.SetTransformParent(element.transform, parent.transform, "Parent " + element.name);
            }

            GameObjectUtility.EnsureUniqueNameForSibling(element);

            // We have to fix up the undo name since the name of the object was only known after reparenting it.
            Undo.SetCurrentGroupName("Create " + element.name);

            GameObjectUtility.SetParentAndAlign(element, parent);
            if (!explicitParentChoice) // not a context click, so center in sceneview
                SetPositionVisibleinSceneView(parent.GetComponent<RectTransform>(), element.GetComponent<RectTransform>());

            Selection.activeGameObject = element;
        }

        // Graphic elements

        private static Vector2 s_ImageElementSize = new Vector2(100f, 100f);

        [MenuItem("GameObject/UI/Raw Image(Extend)", false, 2003)]
        static public void AddRawImage(MenuCommand menuCommand)
        {
            GameObject go = CreateRawImageExtend(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        private static GameObject CreateUIElementRoot(string name, Vector2 size, params Type[] components)
        {
            GameObject child = DefaultControls.factory.CreateGameObject(name, components);
            RectTransform rectTransform = child.GetComponent<RectTransform>();
            rectTransform.sizeDelta = size;
            return child;
        }

        public static GameObject CreateRawImageExtend(DefaultControls.Resources resources)
        {
            GameObject go = CreateUIElementRoot("RawImage(Extend)", s_ImageElementSize, typeof(RawImageExtend));
            return go;
        }

        // Helper methods

        static public GameObject CreateNewUI()
        {
            // Root for the UI
            var root = new GameObject("Canvas");
            root.layer = LayerMask.NameToLayer(kUILayerName);
            Canvas canvas = root.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            root.AddComponent<CanvasScaler>();
            root.AddComponent<GraphicRaycaster>();

            // Works for all stages.
            StageUtility.PlaceGameObjectInCurrentStage(root);
            bool customScene = false;
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                root.transform.SetParent(prefabStage.prefabContentsRoot.transform, false);
                customScene = true;
            }

            Undo.RegisterCreatedObjectUndo(root, "Create " + root.name);

            // If there is no event system add one...
            // No need to place event system in custom scene as these are temporary anyway.
            // It can be argued for or against placing it in the user scenes,
            // but let's not modify scene user is not currently looking at.
            if (!customScene)
                CreateEventSystem(false);
            return root;
        }


        public static void CreateEventSystem(MenuCommand menuCommand)
        {
            GameObject parent = menuCommand.context as GameObject;
            CreateEventSystem(true, parent);
        }

        private static void CreateEventSystem(bool select)
        {
            CreateEventSystem(select, null);
        }

        private static void CreateEventSystem(bool select, GameObject parent)
        {
            StageHandle stage = parent == null ? StageUtility.GetCurrentStageHandle() : StageUtility.GetStageHandle(parent);
            var esys = stage.FindComponentOfType<EventSystem>();
            if (esys == null)
            {
                var eventSystem = new GameObject("EventSystem");
                if (parent == null)
                    StageUtility.PlaceGameObjectInCurrentStage(eventSystem);
                else
                    GameObjectUtility.SetParentAndAlign(eventSystem, parent);
                esys = eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();

                Undo.RegisterCreatedObjectUndo(eventSystem, "Create " + eventSystem.name);
            }

            if (select && esys != null)
            {
                Selection.activeGameObject = esys.gameObject;
            }
        }

        // Helper function that returns a Canvas GameObject; preferably a parent of the selection, or other existing Canvas.
        static public GameObject GetOrCreateCanvasGameObject()
        {
            GameObject selectedGo = Selection.activeGameObject;

            // Try to find a gameobject that is the selected GO or one if its parents.
            Canvas canvas = (selectedGo != null) ? selectedGo.GetComponentInParent<Canvas>() : null;
            if (IsValidCanvas(canvas))
                return canvas.gameObject;

            // No canvas in selection or its parents? Then use any valid canvas.
            // We have to find all loaded Canvases, not just the ones in main scenes.
            Canvas[] canvasArray = StageUtility.GetCurrentStageHandle().FindComponentsOfType<Canvas>();
            for (int i = 0; i < canvasArray.Length; i++)
                if (IsValidCanvas(canvasArray[i]))
                    return canvasArray[i].gameObject;

            // No canvas in the scene at all? Then create a new one.
            return MenuOptionsExtend.CreateNewUI();
        }

        static bool IsValidCanvas(Canvas canvas)
        {
            if (canvas == null || !canvas.gameObject.activeInHierarchy)
                return false;

            // It's important that the non-editable canvas from a prefab scene won't be rejected,
            // but canvases not visible in the Hierarchy at all do. Don't check for HideAndDontSave.
            if (EditorUtility.IsPersistent(canvas) || (canvas.hideFlags & HideFlags.HideInHierarchy) != 0)
                return false;

            if (StageUtility.GetStageHandle(canvas.gameObject) != StageUtility.GetCurrentStageHandle())
                return false;

            return true;
        }
    }

    class SpriteDrawUtilityExtend
    {
        static Texture2D s_ContrastTex;

        // Returns a usable texture that looks like a high-contrast checker board.
        static Texture2D contrastTexture
        {
            get
            {
                if (s_ContrastTex == null)
                    s_ContrastTex = CreateCheckerTex(
                        new Color(0f, 0.0f, 0f, 0.5f),
                        new Color(1f, 1f, 1f, 0.5f));
                return s_ContrastTex;
            }
        }

        // Create a checker-background texture.
        static Texture2D CreateCheckerTex(Color c0, Color c1)
        {
            Texture2D tex = new Texture2D(16, 16);
            tex.name = "[Generated] Checker Texture";
            tex.hideFlags = HideFlags.DontSave;

            for (int y = 0; y < 8; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c1);
            for (int y = 8; y < 16; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c0);
            for (int y = 0; y < 8; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c0);
            for (int y = 8; y < 16; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c1);

            tex.Apply();
            tex.filterMode = FilterMode.Point;
            return tex;
        }

        // Create a gradient texture.
        static Texture2D CreateGradientTex()
        {
            Texture2D tex = new Texture2D(1, 16);
            tex.name = "[Generated] Gradient Texture";
            tex.hideFlags = HideFlags.DontSave;

            Color c0 = new Color(1f, 1f, 1f, 0f);
            Color c1 = new Color(1f, 1f, 1f, 0.4f);

            for (int i = 0; i < 16; ++i)
            {
                float f = Mathf.Abs((i / 15f) * 2f - 1f);
                f *= f;
                tex.SetPixel(0, i, Color.Lerp(c0, c1, f));
            }

            tex.Apply();
            tex.filterMode = FilterMode.Bilinear;
            return tex;
        }

        // Draws the tiled texture. Like GUI.DrawTexture() but tiled instead of stretched.
        static void DrawTiledTexture(Rect rect, Texture tex)
        {
            float u = rect.width / tex.width;
            float v = rect.height / tex.height;

            Rect texCoords = new Rect(0, 0, u, v);
            TextureWrapMode originalMode = tex.wrapMode;
            tex.wrapMode = TextureWrapMode.Repeat;
            GUI.DrawTextureWithTexCoords(rect, tex, texCoords);
            tex.wrapMode = originalMode;
        }

        // Draw the specified Image.
        public static void DrawSprite(Sprite sprite, Rect drawArea, Color color)
        {
            if (sprite == null)
                return;

            Texture2D tex = sprite.texture;
            if (tex == null)
                return;

            Rect outer = sprite.rect;
            Rect inner = outer;
            inner.xMin += sprite.border.x;
            inner.yMin += sprite.border.y;
            inner.xMax -= sprite.border.z;
            inner.yMax -= sprite.border.w;

            Vector4 uv4 = UnityEngine.Sprites.DataUtility.GetOuterUV(sprite);
            Rect uv = new Rect(uv4.x, uv4.y, uv4.z - uv4.x, uv4.w - uv4.y);
            Vector4 padding = UnityEngine.Sprites.DataUtility.GetPadding(sprite);
            padding.x /= outer.width;
            padding.y /= outer.height;
            padding.z /= outer.width;
            padding.w /= outer.height;

            DrawSprite(tex, drawArea, padding, outer, inner, uv, color, null);
        }

        // Draw the specified Image.
        public static void DrawSprite(Texture tex, Rect drawArea, Rect outer, Rect uv, Color color)
        {
            DrawSprite(tex, drawArea, Vector4.zero, outer, outer, uv, color, null);
        }

        // Draw the specified Image.
        private static void DrawSprite(Texture tex, Rect drawArea, Vector4 padding, Rect outer, Rect inner, Rect uv, Color color, Material mat)
        {
            // Create the texture rectangle that is centered inside rect.
            Rect outerRect = drawArea;
            outerRect.width = Mathf.Abs(outer.width);
            outerRect.height = Mathf.Abs(outer.height);

            if (outerRect.width > 0f)
            {
                float f = drawArea.width / outerRect.width;
                outerRect.width *= f;
                outerRect.height *= f;
            }

            if (drawArea.height > outerRect.height)
            {
                outerRect.y += (drawArea.height - outerRect.height) * 0.5f;
            }
            else if (outerRect.height > drawArea.height)
            {
                float f = drawArea.height / outerRect.height;
                outerRect.width *= f;
                outerRect.height *= f;
            }

            if (drawArea.width > outerRect.width)
                outerRect.x += (drawArea.width - outerRect.width) * 0.5f;

            // Draw the background
            EditorGUI.DrawTextureTransparent(outerRect, null, ScaleMode.ScaleToFit, outer.width / outer.height);

            // Draw the Image
            GUI.color = color;

            Rect paddedTexArea = new Rect(
                outerRect.x + outerRect.width * padding.x,
                outerRect.y + outerRect.height * padding.w,
                outerRect.width - (outerRect.width * (padding.z + padding.x)),
                outerRect.height - (outerRect.height * (padding.w + padding.y))
            );

            if (mat == null)
            {
                GUI.DrawTextureWithTexCoords(paddedTexArea, tex, uv, true);
            }
            else
            {
                // NOTE: There is an issue in Unity that prevents it from clipping the drawn preview
                // using BeginGroup/EndGroup, and there is no way to specify a UV rect...
                EditorGUI.DrawPreviewTexture(paddedTexArea, tex, mat);
            }

            // Draw the border indicator lines
            GUI.BeginGroup(outerRect);
            {
                tex = contrastTexture;
                GUI.color = Color.white;

                if (inner.xMin != outer.xMin)
                {
                    float x = (inner.xMin - outer.xMin) / outer.width * outerRect.width - 1;
                    DrawTiledTexture(new Rect(x, 0f, 1f, outerRect.height), tex);
                }

                if (inner.xMax != outer.xMax)
                {
                    float x = (inner.xMax - outer.xMin) / outer.width * outerRect.width - 1;
                    DrawTiledTexture(new Rect(x, 0f, 1f, outerRect.height), tex);
                }

                if (inner.yMin != outer.yMin)
                {
                    // GUI.DrawTexture is top-left based rather than bottom-left
                    float y = (inner.yMin - outer.yMin) / outer.height * outerRect.height - 1;
                    DrawTiledTexture(new Rect(0f, outerRect.height - y, outerRect.width, 1f), tex);
                }

                if (inner.yMax != outer.yMax)
                {
                    float y = (inner.yMax - outer.yMin) / outer.height * outerRect.height - 1;
                    DrawTiledTexture(new Rect(0f, outerRect.height - y, outerRect.width, 1f), tex);
                }
            }

            GUI.EndGroup();
        }
    }

}
