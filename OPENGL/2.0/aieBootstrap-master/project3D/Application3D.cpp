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
	


	// These values are for the point light, as well as loading in the normal-mapped shader.
	lightPos = { 0,-10,1 };
	colour = { 1,0,0 };
	m_shader.loadShader(aie::eShaderStage::VERTEX,
		"./shaders/normalmap.vert");
	m_shader.loadShader(aie::eShaderStage::FRAGMENT,
		"./shaders/OrenNayar.frag");
	if (m_shader.link() == false) {
		printf("Shader Error: %s\n", m_shader.getLastError());
		return false;
	}

	// These shaders being loaded in are for the toon shader.
	m_toonShader.loadShader(aie::eShaderStage::VERTEX,
		"./shaders/toonshader.vert");
	m_toonShader.loadShader(aie::eShaderStage::FRAGMENT,
		"./shaders/toonshader.frag");
	if (m_toonShader.link() == false) {
		printf("Shader Error: %s\n", m_shader.getLastError());
		return false;
	}



	// These shaders being loaded in are for the phong shader.
	m_phongShader.loadShader(aie::eShaderStage::VERTEX, "shaders/normalmap.vert");

	m_phongShader.loadShader(aie::eShaderStage::FRAGMENT, "shaders/normalmap.frag");
	if (m_phongShader.link() == false) {
		printf("Shader Error: %s\n", m_phongShader.getLastError());
		return false;
	}
	


	roughness = 0.3f;


	
	// Load the buddha mesh and set its position.
	if (m_buddaMesh.load("./stanford/Buddha.obj",
		true, true) == false) {
		printf("Buddha Mesh Error!\n");
		return false;
	}
	m_buddaTransform = {
		1,0,0,0,
		0,1,0,0,
		0,0,1,0,
		-15,0,0,1
	};



	// Load the bunny mesh and set its position.
	if (m_toonbunny.load("./stanford/bunny.obj",
		true, true) == false) {
		printf("Bunny Mesh Error!\n");
		return false;
	}
	m_bunnyTransform = {
		1,0,0,0,
		0,1,0,0,
		0,0,1,0,
		-2,0,-6,1
	};
	
	//Set the light variables.
	m_light.diffuse = { 1,1,1 };
	m_light.specular = { 1,1,1 };
	m_ambientLight = { 0.2f,0.2f,0.2f };

	// Load the apple mesh and set its position.
	if (m_appleMesh.load("./stanford/apple.obj",
		true, true) == false) {
		printf("Apple Mesh Error!\n");
		return false;
	}
	m_appleTransform = {
		1,0,0,0,
		0,1,0,0,
		0,0,1,0,
		2.5,0,0,1
	};

	// Load the spear mesh and set its position.
	if (m_spearMesh.load("./stanford/soulspear.obj",
		true, true) == false) {
		printf("Fox Mesh Error!\n");
		return false;
	}
	m_spearTransform = {
		1,0,0,0,
		0,1,0,0,
		0,0,1,0,
		15,0,0,1
	};

	// Load the dragon mesh and set its position.
	if (m_dragonMesh.load("./stanford/dragon.obj",
		true, true) == false) {
		printf("Dragon Mesh Error!\n");
		return false;
	}
	m_dragonTransform = {
		1,0,0,0,
		0,1,0,0,
		0,0,1,0,
		10,10,0,1
	};

	// Load the Lucy mesh and set its position.
	if (m_LucyMesh.load("./stanford/Lucy.obj",
		true, true) == false) {
		printf("Lucy Mesh Error!\n");
		return false;
	}
	m_LucyTransform = {
		1,0,0,0,
		0,1,0,0,
		0,0,1,0,
		10,4,-10,1
	};


	// return true if all the shaders and model have been loaded correctly.
	return true;
}

void Application3D::shutdown() {

	Gizmos::destroy();
}

void Application3D::update(float deltaTime) {

	//// query time since application started
	////dt = deltaTime;
	float time = getTime();

	
	// wipe the gizmos clean for this frame
	camera.Update(aie::Application::getWindowWidth(), aie::Application::getWindowHeight(), deltaTime);
	
	Gizmos::clear();
	// rotating the directional light.
	m_light.direction = glm::normalize(vec3(glm::cos(time * 2),
		glm::sin(time * 2), 0));

	
	
	
	

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

	mat4 t = glm::rotate(mat4(1), time, glm::normalize(vec3(1, 1, 1)));
	t[3] = vec4(-2, 0, 0, 1);


	
	
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



	// draw 3D gizmos
	Gizmos::draw(camera.GetCameraMatrix());

	
	//bind shader
	m_phongShader.bind();

	//bind transform of the buddha
	auto pvm = camera.GetProjectionMatrix() * camera.GetViewMatrix() * m_buddaTransform;
	m_phongShader.bindUniform("ProjectionViewModel", pvm);

	//bind transforms for lighting the buddha model
	// Buddha is not multitextured.
	m_phongShader.bindUniform("useTexture", 0);
	m_phongShader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_buddaTransform)));
	m_phongShader.bindUniform("Ia", m_ambientLight);
	m_phongShader.bindUniform("Id", m_light.diffuse);
	m_phongShader.bindUniform("Is", m_light.specular);
	m_phongShader.bindUniform("lightDirection", m_light.direction);
	m_phongShader.bindUniform("cameraPosition",
		vec3(glm::inverse(camera.GetViewMatrix())[3]));
	m_phongShader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_buddaTransform)));
	m_phongShader.bindUniform("lightPosition", lightPos);
	m_phongShader.bindUniform("colour", colour);
	
	// draw the buddha.
	m_buddaMesh.draw();



	//bind transform of the lucy.
	pvm = camera.GetProjectionMatrix() * camera.GetViewMatrix() * m_LucyTransform;
	m_phongShader.bindUniform("ProjectionViewModel", pvm);

	//bind transforms for lighting
	
	m_phongShader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_LucyTransform)));
	m_phongShader.bindUniform("Ia", m_ambientLight);
	m_phongShader.bindUniform("Id", m_light.diffuse);
	m_phongShader.bindUniform("Is", m_light.specular);
	m_phongShader.bindUniform("lightDirection", m_light.direction);
	m_phongShader.bindUniform("cameraPosition",
		vec3(glm::inverse(camera.GetViewMatrix())[3]));
	m_phongShader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_LucyTransform)));
	m_phongShader.bindUniform("lightPosition", lightPos);
	m_phongShader.bindUniform("colour", colour);
	m_LucyMesh.draw();




	// open the normalmapped shader for binding.
	m_shader.bind();
	
	// Get and bind the spear transform.
	pvm = camera.GetProjectionMatrix() * camera.GetViewMatrix() * m_spearTransform;
	m_shader.bindUniform("ProjectionViewModel", pvm);
	


	//bind transforms for lighting the spear model.
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
	// make sure that multitextures are turned on.
	m_shader.bindUniform("useTexture", 1);

	// draw the spear.
	m_spearMesh.draw();

	// Get and bind the dragon transform.
	pvm = camera.GetProjectionMatrix() * camera.GetViewMatrix() * m_dragonTransform;
	m_shader.bindUniform("ProjectionViewModel", pvm);



	//bind transforms for lighting the dragon model.
	m_shader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_dragonTransform)));
	m_shader.bindUniform("Ia", m_ambientLight);
	m_shader.bindUniform("Id", m_light.diffuse);
	m_shader.bindUniform("Is", m_light.specular);
	m_shader.bindUniform("lightDirection", m_light.direction);
	m_shader.bindUniform("cameraPosition",
		vec3(glm::inverse(camera.GetViewMatrix())[3]));
	m_shader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_dragonTransform)));
	m_shader.bindUniform("roughness", roughness);
	m_shader.bindUniform("reflectionCoefficient", reflectionCoefficent);
	// the dragon is not multitextured. so dont enable it.
	m_shader.bindUniform("useTexture", 0);

	// Draw the dragon.
	m_dragonMesh.draw();
	
	// Open the toon shader for binding.
	m_toonShader.bind();

	// bind the transforms for toon shading the bunny.
	pvm = camera.GetProjectionMatrix() * camera.GetViewMatrix() * m_bunnyTransform;
	m_toonShader.bindUniform("ProjectionViewModel", pvm);
	m_toonShader.bindUniform("lightDir", m_light.direction);
	m_toonShader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_bunnyTransform)));
	// draw the bunny.
	m_toonbunny.draw();

	// bind the transforms for toon shading the apple.
	pvm = camera.GetProjectionMatrix() * camera.GetViewMatrix() * m_appleTransform;
	m_toonShader.bindUniform("ProjectionViewModel", pvm);
	m_toonShader.bindUniform("lightDir", m_light.direction);
	m_toonShader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_appleTransform)));
	// draw the apple.
	m_appleMesh.draw();

	

	
	
	// draw 2D gizmos using an orthogonal projection matrix (or screen dimensions)
	Gizmos::draw2D((float)getWindowWidth(), (float)getWindowHeight());
}