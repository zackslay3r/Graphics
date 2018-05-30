#pragma once

#include "Application.h"
#include <glm/mat4x4.hpp>
#include <gl_core_4_4.h>
#include <../dependencies/glfw/include/GLFW/glfw3.h>
#include <stdio.h>

#include <../bootstrap/Gizmos.h>
#include <glm/glm.hpp>
#include <glm/ext.hpp>
#include "Input.h"

using glm::vec3;
using glm::vec4;
using glm::mat4;

class Application3D : public aie::Application {
public:
	static Application3D& getInstance()
	{
		static Application3D instance;
		return instance;
	}


	Application3D();
	virtual ~Application3D();

	virtual bool startup();
	virtual void shutdown();

	virtual void update(float deltaTime);
	void processInput();

	//void cursor_pos_callback(GLFWwindow* window, double xpos, double ypos);
	//void cursor_position_callback(double xpos, double ypos);
	//void scroll_callback(GLFWwindow* window, double xoffset, double yoffset);
	virtual void draw();
// Testing camera...
//glm::vec3 cameraPos = glm::vec3(0.0f, 5.0f, 3.0f);
//glm::vec3 cameraTarget = glm::vec3(0.0f, 0.0f, 0.0f);
//glm::vec3 cameraDirection = glm::normalize(cameraPos - cameraTarget);
//glm::vec3 up = glm::vec3(0.0f, 1.0f, 0.0f);
//glm::vec3 cameraRight = glm::normalize(glm::cross(up, cameraDirection));
//glm::vec3 cameraUp = glm::cross(cameraDirection, cameraRight);
//glm::vec3 cameraFront = glm::vec3(0.0f, 0.0f, -1.0f);
//
//float deltaTime = 0.0f;	// Time between current frame and last frame
//float lastFrame = 0.0f; // Time of last frame
//bool firstMouse;
//
//float lastX = 640;
//float lastY = 360;
//
//float yaw = 0.0f;
//float pitch = 0.0f;
//float fov = 45.0f;
protected:
	aie::Input* input;
	//GLFWwindow* window;
	glm::mat4	m_view;
	glm::mat4	m_projection;
};