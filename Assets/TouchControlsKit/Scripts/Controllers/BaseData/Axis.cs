/*******************************************************
 * 													   *
 * Asset:		 Touch Controls Kit         		   *
 * Script:		 Axes.cs                               *
 * 													   *
 * Copyright(c): Victor Klepikov					   *
 * Support: 	 http://bit.ly/vk-Support			   *
 * 													   *
 * mySite:       http://vkdemos.ucoz.org			   *
 * myAssets:     http://u3d.as/5Fb                     *
 * myTwitter:	 http://twitter.com/VictorKlepikov	   *
 * 													   *
 *******************************************************/


namespace TouchControlsKit
{
    [System.Serializable]
    public sealed class Axis
    {
        public bool enabled = true;
        public bool inverse = false;

        [UnityEngine.SerializeField]
        private string name = string.Empty;

        internal float value { get; private set; }
        

        // Constructor
        public Axis( string m_Name )
        {
            name = ( name == string.Empty ) ? m_Name : name;
        }

        // Name
        public string Name
        {
            get { return name; }
            set
            {
                if( name == value || value == string.Empty )
                    return;

                name = value;
            }
        }

        // SetValue
        internal void SetValue( float m_value )
        {
            value = ( inverse ) ? -m_value : m_value;
        }
    }
}