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

	FlyCamera* camera;
	glm::mat4	m_viewMatrix;
	glm::mat4	m_projectionMatrix;
	/*aie::ShaderProgram m_shader;*/
	aie::ShaderProgram m_phongShader;
	
	aie::OBJMesh m_bunnyMesh;
	aie::OBJMesh m_spearMesh;
	glm::mat4 m_bunnyTransform;
	glm::mat4 m_spearTransform;

	struct Light
	{
		glm::vec3 direction;
		glm::vec3 diffuse;
		glm::vec3 specular;
	
	};

	Light m_light;
	glm::vec3 m_ambientLight;

};