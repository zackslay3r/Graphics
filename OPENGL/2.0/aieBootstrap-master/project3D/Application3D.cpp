#include "Application3D.h"
#include "Gizmos.h"
#include "Input.h"
#include <glm/glm.hpp>
#include <glm/ext.hpp>

using glm::vec3;
using glm::vec4;
using glm::mat4;
using aie::Gizmos;

Application3D::Application3D() {

}

Application3D::~Application3D() {

}

bool Application3D::startup() {
	
	setBackgroundColour(0.25f, 0.25f, 0.25f);

	// initialise gizmo primitive counts
	Gizmos::create(10000, 10000, 10000, 10000);

	reflectionCoefficent = 0.1f;
	

	
	// This is all for the simple shader.
	m_shader.loadShader(aie::eShaderStage::VERTEX,
		"./shaders/normalmap.vert");
	m_shader.loadShader(aie::eShaderStage::FRAGMENT,
		"./shaders/OrenNayar.frag");
	if (m_shader.link() == false) {
		printf("Shader Error: %s\n", m_shader.getLastError());
		return false;
	}

	// This is all for the simple shader.
	m_toonShader.loadShader(aie::eShaderStage::VERTEX,
		"./shaders/toonshader.vert");
	m_toonShader.loadShader(aie::eShaderStage::FRAGMENT,
		"./shaders/toonshader.frag");
	if (m_toonShader.link() == false) {
		printf("Shader Error: %s\n", m_shader.getLastError());
		return false;
	}



	/*m_phongShader.loadShader(aie::eShaderStage::VERTEX, "shaders/phong.vert");*/
	/*m_phongShader.loadShader(aie::eShaderStage::FRAGMENT, "shaders/phong.frag");*/
	m_phongShader.loadShader(aie::eShaderStage::VERTEX, "shaders/normalmap.vert");
	//m_phongShader.loadShader(aie::eShaderStage::FRAGMENT, "shaders/normalmap.frag");
	m_phongShader.loadShader(aie::eShaderStage::FRAGMENT, "shaders/normalmap.frag");
	if (m_phongShader.link() == false) {
		printf("Shader Error: %s\n", m_phongShader.getLastError());
		return false;
	}
	
	roughness = 0.3f;


	//if (m_bunnyMesh.load("./stanford/bunny.obj") == false) 
	//{
	//	printf("Bunny Mesh Error!\n");
	//	return false;
	//}
	//m_bunnyTransform = {
	//	0.5f,0,0,0,
	//	0,0.5f,0,0,
	//	0,0,0.5f,0,
	//	0,0,0,1
	//};

	if (m_spearMesh.load("./stanford/soulspear.obj",
		true, true) == false) {
		printf("Soulspear Mesh Error!\n");
		return false;
	}
	m_spearTransform = {
		1,0,0,0,
		0,1,0,0,
		0,0,1,0,
		0,0,0,1
	};

	if (m_toonSpear.load("./stanford/soulspear.obj",
		true, true) == false) {
		printf("Soulspear Mesh Error!\n");
		return false;
	}
	m_toonSpearTransform = {
		1,0,0,0,
		0,1,0,0,
		0,0,1,0,
		-2.5,0,0,1
	};
	
	//Set the light variables.
	m_light.diffuse = { 1,1,1 };
	m_light.specular = { 1,1,1 };
	m_ambientLight = { 1.0f,1.0f,1.0f };

	if (m_phongSpear.load("./stanford/soulspear.obj",
		true, true) == false) {
		printf("Soulspear Mesh Error!\n");
		return false;
	}
	m_spearPhongTransform = {
		1,0,0,0,
		0,1,0,0,
		0,0,1,0,
		2.5,0,0,1
	};


	// create simple camera transforms
//	m_viewMatrix = camera->getView();
//	m_projectionMatrix = camera->getProjection();
	return true;
}

void Application3D::shutdown() {

	Gizmos::destroy();
}

void Application3D::update(float deltaTime) {

	//// query time since application started
	////dt = deltaTime;
	float time = getTime();

	//// rotate camera
	//camera.Update(aie::Application::getWindowWidth(), aie::Application::getWindowHeight(), time);
	/*camera->viewTransform = glm::lookAt(vec3(glm::sin(time) * 5, 5, glm::cos(time) * 5),vec3(0), vec3(0, 1, 0));*/
	//camera->update(time);
	// wipe the gizmos clean for this frame
	camera.Update(aie::Application::getWindowWidth(), aie::Application::getWindowHeight(), deltaTime);
	
	Gizmos::clear();
	// rotate light
	m_light.direction = glm::normalize(vec3(glm::cos(time * 2),
		glm::sin(time * 2), 0));

	
	// update perspective in case window resized
	//m_projectionMatrix = glm::perspective(glm::pi<float>() * 0.25f,
	//								  getWindowWidth() / (float)getWindowHeight(),
	//								  0.1f, 1000.f);
	//camera->setPerspective(glm::pi<float>() * 0.25f,
	//	getWindowWidth() / (float)getWindowHeight(),
	//	0.1f, 1000.f);

	// draw 3D gizmos
	/*Gizmos::draw(camera.GetProjectionMatrix() * camera.GetViewMatrix());*/
	
	

	// draw a simple grid with gizmos
	vec4 white(1);
	vec4 black(0, 0, 0, 1);
	for (int i = 0; i < 21; ++i) {
		Gizmos::addLine(vec3(-10 + i, 0, 10),
						vec3(-10 + i, 0, -10),
						i == 10 ? white : black);
		Gizmos::addLine(vec3(10, 0, -10 + i),
						vec3(-10, 0, -10 + i),
						i == 10 ? white : black);
	}

	// add a transform so that we can see the axis
	Gizmos::addTransform(mat4(1));

	// demonstrate a few shapes
	//Gizmos::addAABBFilled(vec3(0), vec3(1), vec4(0, 0.5f, 1, 0.25f));
	//Gizmos::addSphere(vec3(5, 0, 5), 1, 8, 8, vec4(1, 0, 0, 0.5f));
	//Gizmos::addRing(vec3(5, 0, -5), 1, 1.5f, 8, vec4(0, 1, 0, 1));
	//Gizmos::addDisk(vec3(-5, 0, 5), 1, 16, vec4(1, 1, 0, 1));
	//Gizmos::addArc(vec3(-5, 0, -5), 0, 2, 1, 8, vec4(1, 0, 1, 1));

	mat4 t = glm::rotate(mat4(1), time, glm::normalize(vec3(1, 1, 1)));
	t[3] = vec4(-2, 0, 0, 1);
	Gizmos::addCylinderFilled(vec3(0), 0.5f, 1, 5, vec4(0, 1, 1, 1), &t);

	// demonstrate 2D gizmos
	Gizmos::add2DAABB(glm::vec2(getWindowWidth() / 2, 100),
					  glm::vec2(getWindowWidth() / 2 * (fmod(getTime(), 3.f) / 3), 20),
					  vec4(0, 1, 1, 1));

	
	
	// quit if we press escape
	aie::Input* input = aie::Input::getInstance();
	
	

	if (input->isKeyDown(aie::INPUT_KEY_ESCAPE))
		quit();
	
}

void Application3D::draw() {

	// wipe the screen to the background colour
	clearScreen();
	setBackgroundColour(0.25, 0.25, 0.25, 1);

	float time = getTime();

	// rotate camera
	
	//camera->update(dt);
	//// update perspective in case window resized
	////m_projectionMatrix = glm::perspective(glm::pi<float>() * 0.25f,
	//	//								  getWindowWidth() / (float)getWindowHeight(),
	//	//								  0.1f, 1000.f);
	//camera->setPerspective(glm::pi<float>() * 0.25f,
	//	getWindowWidth() / (float)getWindowHeight(),
	//	0.1f, 1000.f);

	//// draw 3D gizmos
	//Gizmos::draw(camera->getProjectionView());

	// draw 3D gizmos
	Gizmos::draw(camera.GetCameraMatrix());

	/////SIMPLE SHADER.
	//// bind shader
	//m_shader.bind();
	//// bind transform
	//auto pvm = camera->projectionTransform * camera->viewTransform * m_spearTransform;
	//m_shader.bindUniform("ProjectionViewModel", pvm);
	////draw mesh.
	//m_spearMesh.draw();
	

	//Phong Shader + Light.
	//bind shader
	m_phongShader.bind();

	//bind transform
	auto pvm = camera.GetProjectionMatrix() * camera.GetViewMatrix() * m_spearPhongTransform;
	m_phongShader.bindUniform("ProjectionViewModel", pvm);

	//bind transforms for lighting
	m_phongShader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_spearPhongTransform)));
	m_phongShader.bindUniform("Ia", m_ambientLight);
	m_phongShader.bindUniform("Id", m_light.diffuse);
	m_phongShader.bindUniform("Is", m_light.specular);
	m_phongShader.bindUniform("lightDirection", m_light.direction);
	m_phongShader.bindUniform("cameraPosition",
		vec3(glm::inverse(camera.GetViewMatrix())[3]));
	m_phongShader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_spearPhongTransform)));
	



	// draw mesh


	m_phongSpear.draw();
	
	m_shader.bind();
	
	pvm = camera.GetProjectionMatrix() * camera.GetViewMatrix() * m_spearTransform;
	m_shader.bindUniform("ProjectionViewModel", pvm);
	//m_shader.bindUniform("ProjectionViewModel", pvm);


	//bind transforms for lighting
	m_shader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_spearTransform)));
	m_shader.bindUniform("Ia", m_ambientLight);
	m_shader.bindUniform("Id", m_light.diffuse);
	m_shader.bindUniform("Is", m_light.specular);
	m_shader.bindUniform("lightDirection", m_light.direction);
	m_shader.bindUniform("cameraPosition",
		vec3(glm::inverse(camera.GetViewMatrix())[3]));
	m_shader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_spearTransform)));
	m_shader.bindUniform("roughness", roughness);
	m_shader.bindUniform("reflectionCoefficient", reflectionCoefficent);
	// bind transform
//	auto pvm = camera->projectionTransform * camera->viewTransform * m_spearTransform;
//	m_phongShader.bindUniform("ProjectionViewModel", pvm);
	//bind transforms for lighting
	
	//bind transform
	
	m_spearMesh.draw();
	
	
	m_toonShader.bind();
	pvm = camera.GetProjectionMatrix() * camera.GetViewMatrix() * m_toonSpearTransform;
	m_toonShader.bindUniform("ProjectionViewModel", pvm);
	m_toonShader.bindUniform("lightDir", m_light.direction);
	m_toonShader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_toonSpearTransform)));
	m_toonSpear.draw();


	//m_texturedShader.bindUniform("ProjectViewModel", pvm);
	// draw mesh
	//m_bunnyMesh.draw();
	// draw mesh
	
	
	// draw 2D gizmos using an orthogonal projection matrix (or screen dimensions)
	Gizmos::draw2D((float)getWindowWidth(), (float)getWindowHeight());
}