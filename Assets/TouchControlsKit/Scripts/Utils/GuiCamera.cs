/*******************************************************
 * 													   *
 * Asset:		 Touch Controls Kit         		   *
 * Script:		 GuiCamera.cs                          *
 * 													   *
 * Copyright(c): Victor Klepikov					   *
 * Support: 	 http://bit.ly/vk-Support			   *
 * 													   *
 * mySite:       http://vkdemos.ucoz.org			   *
 * myAssets:     http://u3d.as/5Fb                     *
 * myTwitter:	 http://twitter.com/VictorKlepikov	   *
 * 													   *
 *******************************************************/


using UnityEngine;

namespace TouchControlsKit.Utils
{
    [RequireComponent( typeof( Camera ) )]
    public sealed class GuiCamera : MonoBehaviour
    {
        public static Camera m_Camera { get; private set; }
        public static Transform m_Transform { get; private set; }

        // Awake
        void Awake()
        {
            m_Transform = transform;
            m_Camera = this.GetComponent<Camera>();
        }                 

        // ScreenToWorldPoint
        public static Vector2 ScreenToWorldPoint( Vector2 position )
        {
            return m_Camera.ScreenToWorldPoint( position );
        }


#if UNITY_EDITOR
        // CreateCamera
        public static void CreateCamera( Transform parent, int cullingMask, float orthographicSize )
        {
            m_Transform = new GameObject( "_guiCamera", typeof( GuiCamera ) ).transform;
            m_Transform.parent = parent;
            m_Transform.localPosition = Vector3.zero;

            m_Camera = m_Transform.GetComponent<Camera>();
            m_Camera.clearFlags = CameraClearFlags.Depth;
            m_Camera.cullingMask = cullingMask;
            m_Camera.orthographic = true;
            m_Camera.orthographicSize = orthographicSize;
            m_Camera.nearClipPlane = -.25f;
            m_Camera.farClipPlane = .25f;
            m_Camera.depth = 1f;
            m_Camera.useOcclusionCulling = false;
            m_Camera.hdr = false;
        }
#endif
    }
}