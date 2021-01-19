#version 330

uniform mat4 mvp; 
in vec3 in_pos; 
uniform vec3 in_colour; 
out vec3 ex_colour; 

void main() { 
	ex_colour = in_colour; 
	gl_Position = mvp * vec4(in_pos, 1.0); 
} 