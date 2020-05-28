Shader "Unlit/MyShader"
{
    Properties
    {		
         _MainTex( "2D Texture", 2D ) = "white" {}
	     _Color("Main Color", Color) = (1,1,1,1)
    }


    SubShader
	{
		Tags {"Queue" = "Geometry-1"}


		Pass{
			Zwrite On
			ColorMask 0
		}
    }
}
