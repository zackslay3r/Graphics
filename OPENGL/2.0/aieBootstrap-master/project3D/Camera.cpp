#include "Camera.h"

// The conversion from degrees to Radians
const float deg2Rad = 3.14159f / 180.0f;

// the last know mouse x,y coords.
int lastMouseX = 0;
int lastMouseY = 0;

// movement values.
float moveSpeed = 6.0f;
float cameraSpeed = 1.0f;




Camera::~Camera()
{
}

void Camera::Update(unsigned int width, unsigned int height, float deltaTime)
{
	m_width = width;
	m_height = height;

	//Get input instance
	aie::Input* input = aie::Input::getInstance();

	//Convert angles to radians
	float thetaRadians = theta * deg2Rad;
	float phiRadians = phi * deg2Rad;

	//Calculate forward and right axis and up axis for camera
	glm::vec3 forward(cos(thetaRadians) * cos(thetaRadians), sin(phiRadians), cos(phiRadians) * sin(thetaRadians));
	glm::vec3 right(-sin(thetaRadians), 0, cos(thetaRadians));
	glm::vec3 up(0, 1, 0);

	//Use WASD, QE keys to move around
	// W = Forward
	// A = Left
	// S = Backward
	// D = Right
	// Q = Down
	// E = Up
	if (input->isKeyDown(aie::INPUT_KEY_E))
	{
		position += up * deltaTime * moveSpeed;
	}
	if (input->isKeyDown(aie::INPUT_KEY_Q))
	{
		position += -up * deltaTime * moveSpeed;
	}
	if (input->isKeyDown(aie::INPUT_KEY_A))
	{
		position += -right * deltaTime * moveSpeed;
	}
	if (input->isKeyDown(aie::INPUT_KEY_D))
	{
		position += right * deltaTime * moveSpeed;
	}
	if (input->isKeyDown(aie::INPUT_KEY_W))
	{
		position += forward * deltaTime * moveSpeed;
	}
	
	if (input->isKeyDown(aie::INPUT_KEY_S))
	{
		position += -forward * deltaTime * moveSpeed;
	}
	//Get current mouse position
	int mx = input->getMouseX();
	int my = input->getMouseY();

	//If right mouse button is down, increment theta and pi by mouse movement
	if (input->isMouseButtonDown(aie::INPUT_MOUSE_BUTTON_RIGHT))
	{
		theta += 0.05f * (mx - lastMouseX) * cameraSpeed;
		phi += 0.05f * (my - lastMouseY) * cameraSpeed;

		//Clamp pitch the prevent camera turning upside-down
		if (phi < -pitchLimit)
			phi = -pitchLimit;
		else if (phi >= pitchLimit)
			phi = pitchLimit;
	}

	//Store this frame's mouse pos for next frame
	lastMouseX = mx;
	lastMouseY = my;
}

glm::mat4 Camera::GetProjectionMatrix()
{
	return glm::perspective(m_fov, (float)m_width / m_height, 0.1f, 1000.0f);
}

glm::mat4 Camera::GetViewMatrix()
{
	//Calculate angles in radians
	float thetaRadians = theta * deg2Rad;
	float phiRadians = phi * deg2Rad;

	//Calculate local forward vector
	glm::vec3 forward(cos(phiRadians) * cos(thetaRadians), sin(phiRadians), cos(phiRadians) * sin(thetaRadians));

	//Return view matrix
	return glm::lookAt(position, position + forward, glm::vec3(0, 1, 0));
}

glm::mat4 Camera::GetCameraMatrix()
{
	return GetProjectionMatrix() * GetViewMatrix();
}

glm::vec3 Camera::GetPos()
{
	return position;
}