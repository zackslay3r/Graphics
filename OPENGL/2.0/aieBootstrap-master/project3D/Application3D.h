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

	Camera camera;
	glm::mat4	m_viewMatrix;
	glm::mat4	m_projectionMatrix;
	aie::ShaderProgram m_shader;
	aie::ShaderProgram m_phongShader;
	aie::ShaderProgram m_toonShader;

	aie::OBJMesh m_bunnyMesh;
	aie::OBJMesh m_spearMesh;
	aie::OBJMesh m_phongSpear;
	aie::OBJMesh m_toonSpear;
	glm::mat4 m_bunnyTransform;
	glm::mat4 m_spearTransform;
	glm::mat4 m_spearPhongTransform;
	glm::mat4 m_toonSpearTransform;

	struct Light
	{
		glm::vec3 direction;
		glm::vec3 diffuse;
		glm::vec3 specular;
	
	};


	float roughness;
	float reflectionCoefficent;
	Light m_light;
	glm::vec3 m_ambientLight;

};