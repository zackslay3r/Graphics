#pragma once

#include "Application.h"
#include <glm/mat4x4.hpp>
#include "FlyCamera.h"
#include "OBJMesh.h"
#include "tiny_obj_loader.h"
#include "Shader.h"

class Application3D : public aie::Application {
public:

	Application3D();
	virtual ~Application3D();

	virtual bool startup();
	virtual void shutdown();

	virtual void update(float deltaTime);
	virtual void draw();


	float dt;
protected:

	// A camera object.
	Camera camera;
	// The view and projection matrix of the camera.
	glm::mat4	m_viewMatrix;
	glm::mat4	m_projectionMatrix;
	
	// A standard phong shader, a shader with Normal Mapping, Oran-Nayar and Cook-Torrance and a Toon shader.
	aie::ShaderProgram m_shader;
	aie::ShaderProgram m_phongShader;
	aie::ShaderProgram m_toonShader;

	
	// THese are all the Models that are in my scene.
	aie::OBJMesh m_FoxMesh;
	aie::OBJMesh m_appleMesh;
	aie::OBJMesh m_toonbunny;
	aie::OBJMesh m_dragonMesh;
	aie::OBJMesh m_buddaMesh;
	aie::OBJMesh m_LucyMesh;
	aie::OBJMesh m_spearMesh;

	// And their respective transforms.
	glm::mat4 m_FoxTransform;
	glm::mat4 m_appleTransform;
	glm::mat4 m_bunnyTransform;
	glm::mat4 m_spearTransform;
	glm::mat4 m_dragonTransform;
	glm::mat4 m_LucyTransform;
	glm::mat4 m_buddaTransform;

	// A struct for the directional light within the scene
	struct Light
	{
		glm::vec3 direction;
		glm::vec3 diffuse;
		glm::vec3 specular;
	
	};



	// A light position and colour of the point light within the scene.
	glm::vec3 lightPos;
	glm::vec3 colour;

	float roughness;
	float reflectionCoefficent;
	Light m_light;
	
	// a vector 3 used to represent ambient light.
	glm::vec3 m_ambientLight;

};