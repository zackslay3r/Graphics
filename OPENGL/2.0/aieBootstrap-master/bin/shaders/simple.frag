// a simple flat colour shader
#version 410
out vec4 FragColour;

uniform vec3 Kd;

void main() {

FragColour = vec4(Kd, 1);
}
