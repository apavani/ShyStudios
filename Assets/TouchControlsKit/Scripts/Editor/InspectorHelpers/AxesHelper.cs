/*******************************************************
 * 													   *
 * Asset:		 Touch Controls Kit         		   *
 * Script:		 AxesHelper.cs                         *
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
using UnityEditor;

namespace TouchControlsKit.Inspector
{
    public sealed class AxesHelper
    {
        private static AxesBasedController myTarget = null;


        // HelperSetup
        public static void HelperSetup( AxesBasedController target )
        {
            myTarget = target;
        }

        /// <summary>
        /// ShowAxes
        /// </summary>
        /// <param name="size"></param>
        /// <param name="hideVert"></param>
        public static void ShowAxes( bool hideVert = false )
        {
            StyleHelper.StandardSpace();
            GUILayout.BeginVertical( "Box" );
            GUILayout.Label( "Axes", StyleHelper.labelStyle );
            StyleHelper.StandardSpace();

            ShowAxis( ref myTarget.axisX, "X" );

            if( hideVert )
            {
                StyleHelper.StandardSpace();
                GUILayout.EndVertical();
                return;
            }

            ShowAxis( ref myTarget.axisY, "Y" );

            StyleHelper.StandardSpace();
            GUILayout.EndVertical();
        }

        // ShowAxis
        private static void ShowAxis( ref Axis axis, string label )
        {
            GUILayout.BeginHorizontal();
            axis.enabled = EditorGUILayout.Toggle( axis.enabled, GUILayout.Width( 15f ) );
            GUILayout.Label( "Axis " + label, GUILayout.Width( 50f ) );
            GUI.enabled = axis.enabled;
            axis.Name = EditorGUILayout.TextField( axis.Name, GUILayout.Width( 100f ) );
            GUILayout.Space( 10f );
            axis.inverse = EditorGUILayout.Toggle( axis.inverse, GUILayout.Width( 15f ) );
            GUILayout.Label( "Inverse", GUILayout.Width( 55f ) );
            GUI.enabled = true;
            GUILayout.EndHorizontal(); 
        }
    }
}