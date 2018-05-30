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
	

	input = aie::Input::getInstance();
	//setBackgroundColour(0.25f, 0.25f, 0.25f);

	//// initialise gizmo primitive counts
	Gizmos::create(10000, 10000, 10000, 10000);

	//// create simple camera transforms
	m_view = glm::lookAt(vec3(10), vec3(0), vec3(0, 1, 0));
	m_projection = glm::perspective(glm::pi<float>() * 0.25f,
										  getWindowWidth() / (float)getWindowHeight(),
										  0.1f, 1000.f);
	//void processInput(GLFWwindow *window);
	//void mouse_callback(GLFWwindow* window, double xpos, double ypos);
	//void scroll_callback(GLFWwindow* window, double xoffset, double yoffset);


	//if (glfwInit() == false)
	//{
	//	return -1;
	//}
	////window = glfwCreateWindow(1280, 720, "Computer Graphics", nullptr, nullptr);
	//if (window == nullptr) 
	//{
	//	glfwTerminate();
	//	return -2;
	//}
	//glfwMakeContextCurrent(window);
	//// the rest of our code goes here!
	//if (ogl_LoadFunctions() == ogl_LOAD_FAILED) {
	//	aie::Gizmos::destroy();
	//	glfwDestroyWindow(window);
	//	glfwTerminate();
	//	return -3;
	//}	aie::Gizmos::create(100000, 100000, 100000, 100000);





	/*glm::mat4 view;
	view = glm::lookAt(glm::vec3(0.0f, 0.0f, 3.0f),
	glm::vec3(0.0f, 0.0f, 0.0f),
	glm::vec3(0.0f, 1.0f, 0.0f));*/


	////mat4 view = glm::lookAt(vec3(15, 15, 15), vec3(0), vec3(0, 1, 0));
	//m_projection = glm::perspective(glm::pi<float>() * 0.25f,
	//	16 / 9.f, 0.1f, 1000.f);	//auto major = ogl_GetMajorVersion();
	//auto minor = ogl_GetMinorVersion();
	//printf("GL: %i.%i\n", major, minor);


	////glClearColor(0.75f, 0.1f, 0.1f, 1);
	//glClearColor(0.25f, 0.25f, 0.25f, 1);
	//glEnable(GL_DEPTH_TEST); // enables the depth buffer
	//glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	//
	////glfwSetCursorPosCallback(window, mouse_callback);
	return true;
}

void Application3D::shutdown() {

	Gizmos::destroy();
}

void Application3D::update(float deltaTime) {

	//// query time since application started
	float time = getTime();

	// rotate camera
	m_view = glm::lookAt(vec3(glm::sin(time) * 10, 10, glm::cos(time) * 10),
							   vec3(0), vec3(0, 1, 0));

	// wipe the gizmos clean for this frame
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

	//// add a transform so that we can see the axis
	//Gizmos::addTransform(mat4(1));

	//// demonstrate a few shapes
	//Gizmos::addAABBFilled(vec3(0), vec3(1), vec4(0, 0.5f, 1, 0.25f));
	//Gizmos::addSphere(vec3(5, 0, 5), 1, 8, 8, vec4(1, 0, 0, 0.5f));
	//Gizmos::addRing(vec3(5, 0, -5), 1, 1.5f, 8, vec4(0, 1, 0, 1));
	//Gizmos::addDisk(vec3(-5, 0, 5), 1, 16, vec4(1, 1, 0, 1));
	//Gizmos::addArc(vec3(-5, 0, -5), 0, 2, 1, 8, vec4(1, 0, 1, 1));

	//mat4 t = glm::rotate(mat4(1), time, glm::normalize(vec3(1, 1, 1)));
	//t[3] = vec4(-2, 0, 0, 1);
	//Gizmos::addCylinderFilled(vec3(0), 0.5f, 1, 5, vec4(0, 1, 1, 1), &t);

	//// demonstrate 2D gizmos
	//Gizmos::add2DAABB(glm::vec2(getWindowWidth() / 2, 100),
	//				  glm::vec2(getWindowWidth() / 2 * (fmod(getTime(), 3.f) / 3), 20),
	//				  vec4(0, 1, 1, 1));

	/*while (glfwWindowShouldClose(window) == false &&
		glfwGetKey(window, GLFW_KEY_ESCAPE) != GLFW_PRESS)
	{*/
		float test1 = 0;
		float test2 = 0;

		/*float currentFrame = glfwGetTime();*/
	/*	deltaTime = currentFrame - lastFrame;
		lastFrame = currentFrame;*/




	//
	//	aie::Gizmos::addTransform(glm::mat4(1));
	//	vec4 white(1);
	//	vec4 black(0, 0, 0, 1);
	//	for (int i = 0; i < 21; ++i) {
	//		aie::Gizmos::addLine(vec3(-10 + i, 0, 10),
	//			vec3(-10 + i, 0, -10),
	//			i == 10 ? white : black);
	//		aie::Gizmos::addLine(vec3(10, 0, -10 + i),
	//			vec3(-10, 0, -10 + i),
	//			i == 10 ? white : black);
	//	}
	//	float radius = 10.0f;
	//	/*float camX = sin(glfwGetTime()) * radius;
	//	float camZ = cos(glfwGetTime()) * radius;*/
	//	glm::mat4 view;


	//	view = glm::lookAt(cameraPos, cameraPos + cameraFront, { 0,1, 0 });
	//	m_projection = glm::perspective(glm::radians(fov), 800.0f / 600.0f, 0.1f, 100.0f);
	//	processInput();
	//	//glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_DISABLED);
	//	
	//	//GLFWcursorposfun mousecallBack = mouse_callback();

	//	//glfwSetWindowUserPointer(window, reinterpret_cast<void*>(this));
	//	//glfwSetCursorPosCallback(window, cursor_pos_callback);
	//	//glfwSetScrollCallback(window, scroll_callback);
	//	//mouse_callback(window, lastX, lastY);
	//	cursor_position_callback(input->getMouseX(), input->getMouseY());
	//	aie::Gizmos::draw(m_projection * view);
	////	glfwSwapBuffers(window);
	////	glfwPollEvents();
	/////*}*/

	///*glfwSwapBuffers(window);
	//glfwPollEvents();
	//*/
	////// quit if we press escape
	//aie::Input* input = aie::Input::getInstance();

	if (input->isKeyDown(aie::INPUT_KEY_ESCAPE))
		quit();
}

void Application3D::processInput()
{
	//float cameraSpeed = 2.5f * deltaTime;
	//if (glfwGetKey(window, GLFW_KEY_W) == GLFW_PRESS)
	//	cameraPos += cameraSpeed * cameraFront;
	//if (glfwGetKey(window, GLFW_KEY_S) == GLFW_PRESS)
	//	cameraPos -= cameraSpeed * cameraFront;
	//if (glfwGetKey(window, GLFW_KEY_A) == GLFW_PRESS)
	//	cameraPos -= glm::normalize(glm::cross(cameraFront, { 0,1, 0 })) * cameraSpeed;
	//if (glfwGetKey(window, GLFW_KEY_D) == GLFW_PRESS)
	//	cameraPos += glm::normalize(glm::cross(cameraFront, { 0,1, 0 })) * cameraSpeed;
	//
	//float currentMouseX = input->getMouseX();
	//float currentMouseY = input->getMouseY();

	//cursor_position_callback(window, currentMouseX, currentMouseY);
	//mouse_callback(window, currentMouseX, currentMouseY);

}


//
//void Application3D::cursor_pos_callback(GLFWwindow * window, double xposition, double yposition)
//{
//
//}
//
//void Application3D::cursor_position_callback(double xpos, double ypos)
//{
//
//
//	//float xPos = (float)xpos;
//	//float yPos = (float)ypos;
//	//float negativePitch = -89.0f;
//	//float positivePitch = 89.0f;
//
//
//	//if (firstMouse)
//	//{
//	//	lastX = xPos;
//	//	lastY = yPos;
//	//	firstMouse = false;
//	//}
//
//	//float xoffset = xPos - lastX;
//	//float yoffset = lastY - yPos;
//	//lastX = xPos;
//	//lastY = yPos;
//
//	//float sensitivity = 0.05;
//	//xoffset *= sensitivity;
//	//yoffset *= sensitivity;
//
//	//yaw += xoffset;
//	//pitch += yoffset;
//
//	//if (pitch > positivePitch)
//	//{
//	//	pitch = positivePitch;
//	//}
//	//if (pitch < negativePitch)
//	//{
//	//	pitch = negativePitch;
//	//}
//	//glm::vec3 front;
//	//front.y = 0;
//	//front.x = cos(glm::radians(yaw)) * cos(glm::radians(pitch));
//	//front.y = sin(glm::radians(pitch));
//	//front.z = sin(glm::radians(yaw))* cos(glm::radians(pitch));
//	//cameraFront = glm::normalize(front);
//}
//
//void Application3D::scroll_callback(GLFWwindow * window, double xoffset, double yoffset)
//{
//	//if (fov >= 1.0f && fov <= 45.0f)
//	//	fov -= yoffset;
//	//if (fov <= 1.0f)
//	//	fov = 1.0f;
//	//if (fov >= 45.0f)
//	//	fov = 45.0f;
//}

void Application3D::draw() {

		// wipe the screen to the background colour
		//clearScreen();
	
		// update perspective in case window resized
		m_projection = glm::perspective(glm::pi<float>() * 0.25f,
											  getWindowWidth() / (float)getWindowHeight(),
											  0.1f, 1000.f);
	
		// draw 3D gizmos
		Gizmos::draw(m_projection * m_view);
	
		// draw 2D gizmos using an orthogonal projection matrix (or screen dimensions)
		//Gizmos::draw2D((float)getWindowWidth(), (float)getWindowHeight());
	}
