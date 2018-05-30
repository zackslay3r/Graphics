#include "Camera.h"



Camera::Camera()
{
}

void Camera::setPerspective(float fieldOfView, float aspectRatio, float near, float)
{
}

void Camera::setLookAt(glm::vec3 form, glm::vec3 to, glm::vec3 up)
{
}

void Camera::setPosition(glm::vec3 position)
{
}

glm::mat4 Camera::getWorldTransform()
{
	return glm::mat4();
}

glm::mat4 Camera::getView()
{
	return glm::mat4();
}

glm::mat4 Camera::getProjection()
{
	return glm::mat4();
}

glm::mat4 Camera::getProjectionView()
{
	return glm::mat4();
}

void Camera::updateProjectionViewTransform()
{
}


Camera::~Camera()
{
}
