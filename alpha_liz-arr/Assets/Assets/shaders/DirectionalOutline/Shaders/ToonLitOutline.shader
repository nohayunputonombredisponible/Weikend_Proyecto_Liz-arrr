Shader "Toon/Lit Outline" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_Brightness("Outline Brightness", Range(1,5)) = 2
		_OutlineZ("Outline Z", Range(0.01,0.25)) = 0.2
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		UsePass "Toon/Lit/FORWARD"
		UsePass "Toon/Rim Outline/OUTLINE"

	} 
	
	Fallback "Toon/Lit"
}
