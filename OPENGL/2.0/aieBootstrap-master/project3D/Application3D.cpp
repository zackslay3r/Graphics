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

									
	camera = new FlyCamera();

	camera->setPerspective(glm::pi<float>() * 0.25f, 16.0f/9.0f, 0.1f, 1000.0f);
	camera->setLookAt(vec3(10, 10, 10), vec3(0), vec3(0, 1, 0));

	m_shader.loadShader(aie::eShaderStage::VERTEX,
		"./shaders/simple.vert");
	m_shader.loadShader(aie::eShaderStage::FRAGMENT,
		"./shaders/simple.frag");
	if (m_shader.link() == false) {
		printf("Shader Error: %s\n", m_shader.getLastError());
		return false;
	}
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

	
	

	// create simple camera transforms
//	m_viewMatrix = camera->getView();
//	m_projectionMatrix = camera->getProjection();
	return true;
}

void Application3D::shutdown() {

	Gizmos::destroy();
}

void Application3D::update(float deltaTime) {

	// query time since application started
	dt = deltaTime;
	float time = getTime();

	// rotate camera
	//m_viewMatrix = glm::lookAt(vec3(glm::sin(time) * 10, 10, glm::cos(time) * 10),
	//						   vec3(0), vec3(0, 1, 0));
	//camera->update(time);
	// wipe the gizmos clean for this frame
	
	// rotate light
	m_light.direction = glm::normalize(vec3(glm::cos(time * 2),
		glm::sin(time * 2), 0));

	camera->update(deltaTime);
	// update perspective in case window resized
	//m_projectionMatrix = glm::perspective(glm::pi<float>() * 0.25f,
	//								  getWindowWidth() / (float)getWindowHeight(),
	//								  0.1f, 1000.f);
	//camera->setPerspective(glm::pi<float>() * 0.25f,
	//	getWindowWidth() / (float)getWindowHeight(),
	//	0.1f, 1000.f);

	// draw 3D gizmos
	Gizmos::draw(camera->getProjectionView());
	
	Gizmos::clear();

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

	camera->update(dt);
	// update perspective in case window resized
	//m_projectionMatrix = glm::perspective(glm::pi<float>() * 0.25f,
		//								  getWindowWidth() / (float)getWindowHeight(),
		//								  0.1f, 1000.f);
	camera->setPerspective(glm::pi<float>() * 0.25f,
		getWindowWidth() / (float)getWindowHeight(),
		0.1f, 1000.f);

	// draw 3D gizmos
	Gizmos::draw(camera->getProjectionView());

	// bind shader
	m_shader.bind();
	// bind transform
	auto pvm = camera->projectionTransform * camera->viewTransform * m_spearTransform;
	m_shader.bindUniform("ProjectionViewModel", pvm);
	//bind transforms for lighting
	m_shader.bindUniform("NormalMatrix", glm::inverseTranspose(glm::mat3(m_spearTransform)));

	
	//m_texturedShader.bindUniform("ProjectViewModel", pvm);
	// draw mesh
	//m_bunnyMesh.draw();
	// draw mesh
	m_spearMesh.draw();
	
	// draw 2D gizmos using an orthogonal projection matrix (or screen dimensions)
	Gizmos::draw2D((float)getWindowWidth(), (float)getWindowHeight());
}