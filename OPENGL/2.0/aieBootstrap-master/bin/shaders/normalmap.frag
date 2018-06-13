// a normal map fragment shader
#version 410
in vec2 vTexCoord;
in vec3 vNormal;
in vec3 vTangent;
in vec3 vBiTangent;
in vec4 vPosition;
out vec4 FragColour;
uniform sampler2D diffuseTexture;
uniform sampler2D specularTexture;
uniform sampler2D normalTexture;
uniform vec3 Ka; // material ambient
uniform vec3 Kd; // material diffuse
uniform vec3 Ks; // material specular
uniform float specularPower;
uniform vec3 Ia; // light ambient
uniform vec3 Id; // light diffuse
uniform vec3 Is; // light specular
uniform vec3 lightDirection;
uniform vec3 lightPosition;
uniform vec3 cameraPosition;
uniform vec3 colour;

void main() {
vec3 N = normalize(vNormal);
vec3 T = normalize(vTangent);
vec3 B = normalize(vBiTangent);
vec3 L = normalize(lightDirection);
vec3 P = normalize(vPosition.xyz - lightPosition);

mat3 TBN = mat3(T,B,N);
//N = TBN * (texNormal * 2 - 1);
vec3 texDiffuse = texture( diffuseTexture, vTexCoord ).rgb;
vec3 texSpecular = texture( specularTexture, vTexCoord ).rgb;
vec3 texNormal = texture( normalTexture, vTexCoord ).rgb;
// calculate lambert term
float lambertTerm = max( 0, dot( N, -L ) );
// calculate view vector and reflection vector
vec3 V = normalize(cameraPosition - vPosition.xyz);
vec3 R = reflect( L, N );
// calculate specular term
float specularTerm = pow( max( 0, dot( R, V ) ), specularPower );

vec3 diffuse = Id * Kd * texDiffuse * lambertTerm;
vec3 specular = Is * Ks * texSpecular * specularTerm;

//POINT LIGHT STUFF


// calculate lambert term
float pointlambertTerm = max( 0, dot( N, -P ) );
// calculate view vector and reflection vector
vec3 pointV = normalize(vPosition.xyz - cameraPosition);
vec3 pointR = reflect( P, N );
// calculate specular term
float pointspecularTerm = pow( max( 0, dot( pointR, pointV ) ), specularPower );

vec3 pointdiffuse = colour * Kd * texDiffuse * pointlambertTerm;
vec3 pointspecular = colour * Ks * texSpecular * pointspecularTerm;
// calculate each light property




vec3 ambient = Ia * Ka;
FragColour = vec4(ambient + diffuse + pointdiffuse + pointspecular + specular, 1);
}