#pragma once
#include <glm\glm.hpp>
#include <glm\ext.hpp>
#include <Input.h>
class Camera
{
public:
	Camera() : theta(-90), phi(0), position(0, 4, 20) {}
	~Camera();

	void Update(unsigned int width, unsigned int height, float deltaTime);

	glm::mat4 GetProjectionMatrix();
	glm::mat4 GetViewMatrix();
	glm::mat4 GetCameraMatrix();

	glm::vec3 GetPos();

	// set the field of view for the camera.
	float m_fov = glm::pi<float>() * 0.25f;
private:
	float theta;
	float phi;

	// have the limit for the pitch we are allowed for viewing.
	float pitchLimit = 80.0f;

	// the position of the camera.
	glm::vec3 position;

	unsigned int m_width = 0;
	unsigned int m_height = 0;
};

