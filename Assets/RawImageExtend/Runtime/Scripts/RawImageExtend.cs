using UnityEngine.Serialization;

namespace UnityEngine.UI
{
    
    //public enum MaskType
    //{
    //    NONE = 0,
    //    TEXTURE,
    //    ELLIPSE,
    //    POLYGON,
    //    RECTANGLE,
    //    ROUNDEDPOLYGON,
    //    ROUNDEDRECTANGLE
    //}

    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu("UI/Raw Image(Extend)", 50)]
    public class RawImageExtend : MaskableGraphic
    {
        [FormerlySerializedAs("m_Tex")]
        [SerializeField] Texture m_Texture;
        [SerializeField] Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);

        //#region Mask Field
        //[FormerlySerializedAs("m_Tex")]
        //[SerializeField] Texture m_MaskMap;
        //[SerializeField] MaskType m_MaskType = MaskType.NONE;
        //[SerializeField] float m_MaskWidth = 0.5f;
        //[SerializeField] float m_MaskHeight = 0.5f;
        //[SerializeField] int m_MaskSides = 3;
        //[SerializeField] float m_MaskRoundness = 3;
        //[SerializeField] float m_MaskRotationUV = 0;
        //[SerializeField] float m_MaskTwirlStrength = 0;
        //[SerializeField] Vector2 m_MaskTwirlOffset = Vector2.zero;
        //[SerializeField] Vector2 m_MaskSmoothLeft = Vector2.zero;
        //[SerializeField] Vector2 m_MaskSmoothRight = Vector2.one;

        //#endregion

        //#region Mask ShaderProperty
        [HideInInspector] public static readonly int _UVRect = Shader.PropertyToID("_UVRect");
        //[HideInInspector] public static readonly int _MaskMap = Shader.PropertyToID("_MaskMap");
        //[HideInInspector] public static readonly int _MaskWidth = Shader.PropertyToID("_MaskWidth");
        //[HideInInspector] public static readonly int _MaskHeight = Shader.PropertyToID("_MaskHeight");
        //[HideInInspector] public static readonly int _MaskSides = Shader.PropertyToID("_MaskSides");
        //[HideInInspector] public static readonly int _MaskRoundness = Shader.PropertyToID("_MaskRoundness");
        //[HideInInspector] public static readonly int _MaskSmooth = Shader.PropertyToID("_MaskSmooth");
        //[HideInInspector] public static readonly int _MaskPolygonUV = Shader.PropertyToID("_MaskPolygonUV");
        //[HideInInspector] public static readonly int _OVERLAY = Shader.PropertyToID("_OVERLAY");

        //#endregion

        protected RawImageExtend()
        {
            useLegacyMeshGeneration = false;
   
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
        }

        public override Texture mainTexture
        {
            get
            {
                if (m_Texture == null)
                {
                    if (material != null && material.mainTexture != null)
                    {
                        return material.mainTexture;
                    }
                    return s_WhiteTexture;
                }

                return m_Texture;
            }
        }

        public Texture texture
        {
            get
            {
                return m_Texture;
            }
            set
            {
                if (m_Texture == value)
                    return;

                m_Texture = value;
                material.SetVector(_UVRect, new Vector4(m_UVRect.x, m_UVRect.y, m_UVRect.width, m_UVRect.height));
                SetVerticesDirty();
                SetMaterialDirty();
            }
        }

        public Rect uvRect
        {
            get
            {
                return m_UVRect;
            }
            set
            {
                if (m_UVRect == value)
                    return;
                m_UVRect = value;
                SetVerticesDirty();
            }
        }

        //#region Mask Property

        //public Texture maskMap
        //{
        //    get
        //    {
        //        return m_MaskMap;
        //    }
        //    set
        //    {
        //        if (m_MaskMap == value)
        //            return;

        //        m_MaskMap = value;
        //        material.SetTexture(_MaskMap, m_MaskMap);
        //        SetMaterialDirty();
        //    }
        //}

        //public MaskType maskType
        //{
        //    get
        //    {
        //        return m_MaskType;
        //    }
        //    set
        //    {
        //        if (m_MaskType == value)
        //            return;

        //        m_MaskType = value;
        //        material.SetFloat(_OVERLAY, (int)value);
        //        SetMaterialDirty();
        //    }
        //}

        //public float maskWidth
        //{
        //    get
        //    {
        //        return m_MaskWidth;
        //    }
        //    set
        //    {
        //        if (m_MaskWidth == value)
        //            return;
        //        m_MaskWidth = value;
        //        SetVerticesDirty();
        //    }
        //}

        //public float maskHeight
        //{
        //    get
        //    {
        //        return m_MaskHeight;
        //    }
        //    set
        //    {
        //        if (m_MaskHeight == value)
        //            return;
        //        m_MaskHeight = value;
        //        SetVerticesDirty();
        //    }
        //}

        //public int maskSides
        //{
        //    get
        //    {
        //        return m_MaskSides;
        //    }
        //    set
        //    {
        //        if (m_MaskSides == value)
        //            return;
        //        m_MaskSides = value;
        //        SetVerticesDirty();
        //    }
        //}

        //public float maskRoundness
        //{
        //    get
        //    {
        //        return m_MaskRoundness;
        //    }
        //    set
        //    {
        //        if (m_MaskRoundness == value)
        //            return;
        //        m_MaskRoundness = value;
        //        SetVerticesDirty();
        //    }
        //}

        //public Vector4 maskSmooth
        //{
        //    get
        //    {
        //        return new Vector4(m_MaskSmoothLeft.x, m_MaskSmoothLeft.y, m_MaskSmoothRight.x, m_MaskSmoothRight.y);
        //    }
        //    set
        //    {
        //        if (m_MaskSmoothLeft.x == value.x && m_MaskSmoothLeft.x == value.y 
        //            && m_MaskSmoothRight.x == value.z && m_MaskSmoothRight.x == value.w)
        //            return;
        //        m_MaskSmoothLeft.x = value.x; m_MaskSmoothLeft.y = value.y;
        //        m_MaskSmoothRight.x = value.z; m_MaskSmoothRight.y = value.w;
        //        SetVerticesDirty();
        //    }
        //}

        //public Vector4 maskPolygonUV
        //{
        //    get
        //    {
        //        return new Vector4(m_MaskRotationUV, m_MaskTwirlStrength, m_MaskTwirlOffset.x, m_MaskTwirlOffset.y);
        //    }
        //    set
        //    {
        //        if (m_MaskRotationUV == value.x && m_MaskTwirlStrength == value.y
        //            && m_MaskTwirlOffset.x == value.z && m_MaskTwirlOffset.x == value.w)
        //            return;
        //        m_MaskRotationUV = value.x; m_MaskTwirlStrength = value.y;
        //        m_MaskTwirlOffset.x = value.z; m_MaskTwirlOffset.y = value.w;
        //        SetVerticesDirty();
        //    }
        //}

        //#endregion

        public override void SetNativeSize()
        {
            Texture tex = mainTexture;
            if (tex != null)
            {
                int w = Mathf.RoundToInt(tex.width * uvRect.width);
                int h = Mathf.RoundToInt(tex.height * uvRect.height);
               
                rectTransform.anchorMax = rectTransform.anchorMin;
                rectTransform.sizeDelta = new Vector2(w, h);
            }
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            material.SetVector(_UVRect, new Vector4(m_UVRect.x, m_UVRect.y, m_UVRect.width, m_UVRect.height));
            Texture tex = mainTexture;
            vh.Clear();
            if (tex != null)
            {
                var r = GetPixelAdjustedRect();
                var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
                var scaleX = tex.width * tex.texelSize.x;
                var scaleY = tex.height * tex.texelSize.y;
                {
                    var color32 = color;
                    vh.AddVert(new Vector3(v.x, v.y), color32, new Vector2(m_UVRect.xMin * scaleX, m_UVRect.yMin * scaleY));
                    vh.AddVert(new Vector3(v.x, v.w), color32, new Vector2(m_UVRect.xMin * scaleX, m_UVRect.yMax * scaleY));
                    vh.AddVert(new Vector3(v.z, v.w), color32, new Vector2(m_UVRect.xMax * scaleX, m_UVRect.yMax * scaleY));
                    vh.AddVert(new Vector3(v.z, v.y), color32, new Vector2(m_UVRect.xMax * scaleX, m_UVRect.yMin * scaleY));

                    vh.AddTriangle(0, 1, 2);
                    vh.AddTriangle(2, 3, 0);
                }
            }

        }

        protected override void OnDidApplyAnimationProperties()
        {
            SetMaterialDirty();
            SetVerticesDirty();
        }

    }

}