#version 330

in vec3 ex_colour; 
void main() { 
	gl_FragColor = vec4(ex_colour.rgb, 1.0);
} 