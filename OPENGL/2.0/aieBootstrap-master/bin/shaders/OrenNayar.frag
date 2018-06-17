#version 410


in vec2 vTexCoord;
in vec3 vNormal;
in vec3 vTangent;
in vec3 vBiTangent;
in vec4 vPosition;
out vec4 FragColour;

uniform vec4 uLight;
uniform vec3 uLightIntensity;
uniform vec4 uDiffuseRefl;
uniform vec3 uViewVec;
uniform float uStdDev;
uniform sampler2D diffuseTexture;
uniform sampler2D specularTexture;
uniform sampler2D normalTexture;
uniform vec3 Ka; // material ambient
uniform vec3 Kd; // material diffuse
uniform vec3 Ks; // material specular
uniform float specularPower;
uniform float roughness;
uniform float reflectionCoefficient;
uniform vec3 Ia; // light ambient
uniform vec3 Id; // light diffuse
uniform vec3 Is; // light specular
uniform vec3 lightDirection;
uniform vec3 cameraPosition;
uniform int useTexture = 0;

void main() {
vec3 N = normalize(vNormal);
vec3 T = normalize(vTangent);
vec3 B = normalize(vBiTangent);
vec3 L = normalize(lightDirection);
vec3 E = normalize(cameraPosition - vPosition.xyz);


float NdL = max(0.0f, dot(N,L));
float NdE = max(0.0f,dot(N,E));
float R2 = roughness * roughness;

vec3 H = E + L / 2; 

float NdH = max( 0.0f, dot( N, H ) );
float NdH2 = NdH * NdH;
float e = 2.71828182845904523536028747135f;
float pi = 3.1415926535897932384626433832f;

// Beckman's Distribution Function D
float exponent = -(1 - NdH2) / (NdH2 * R2);
float D = pow( e, exponent ) / (R2 * NdH2 * NdH2);
// Fresnel Term F
float F = reflectionCoefficient + (1 - reflectionCoefficient) * pow(1- NdE,5);


// Geometric Attenuation Factor G
float X = 2.0f * NdH / dot( E, H );
float G = min(1, min(X * NdL, X * NdE));
// Calculate Cook-Torrance
float CookTorrance = max( (D*G*F) / (NdE * pi), 0.0f );



float ora = 1.0f - 0.5f * R2 / (R2 + 0.33f);
float orb = 0.45f * R2 / (R2 + 0.09f);

vec3 lightProjected = normalize(L - N * NdL);
vec3 viewProjected = normalize( E - N * NdE);
float CX = max( 0.0f, dot( lightProjected, viewProjected ) );

 


// DX = sin(alpha) * tan(beta)
float alpha = sin( max( acos( NdE ), acos( NdL ) ) );
float beta = tan( min( acos( NdE ), acos( NdL ) ) );
float DX = alpha * beta;
// Calculate Oren-Nayar, replaces the Phong Lambertian Term
float OrenNayar = NdL * (ora + orb * CX * DX);


mat3 TBN = mat3(T,B,N);
//N = TBN * (texNormal * 2 - 1);
int inverse = 1 - useTexture;
vec3 texDiffuse = (texture( diffuseTexture, vTexCoord ).rgb + vec3(inverse,inverse,inverse)) * Kd;
vec3 texSpecular = (texture( specularTexture, vTexCoord ).rgb + vec3(inverse,inverse,inverse)) * Ks;
vec3 texNormal = texture( normalTexture, vTexCoord ).rgb;
// calculate lambert term
float lambertTerm = OrenNayar;
// calculate view vector and reflection vector
vec3 V = normalize(cameraPosition - vPosition.xyz);
vec3 R = reflect( L, N );
// calculate specular term
float specularTerm = CookTorrance;
// calculate each light property
vec3 ambient = Ia * Ka;
vec3 diffuse = Id * Kd * texDiffuse * lambertTerm;
vec3 specular = Is * Ks * texSpecular * specularTerm;
FragColour = vec4(ambient + diffuse + specular, 1);
}