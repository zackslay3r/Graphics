#include "Camera.h"



Camera::Camera()
{
}

void Camera::setPerspective(float fieldOfView, float aspectRatio, float near, float far)
{
	projectionTransform = glm::perspective(fieldOfView, aspectRatio, near, far);
}

void Camera::setLookAt(glm::vec3 from, glm::vec3 to, glm::vec3 up)
{
	viewTransform = glm::lookAt(from, to, up);
}

void Camera::setPosition(glm::vec3 position)
{
}

glm::mat4 Camera::getWorldTransform()
{
	return worldTransform;
}

glm::mat4 Camera::getView()
{
	return viewTransform;
}

glm::vec4 Camera::getPosition()
{
	return viewTransform[3];
}

glm::mat4 Camera::getProjection()
{
	return projectionTransform;
}

glm::mat4 Camera::getProjectionView()
{
	return projectionViewTransform;
}

void Camera::updateProjectionViewTransform()
{
	
	projectionViewTransform = projectionTransform * viewTransform;
	//projectionViewTransform = viewTransform * projectionTransform;
}


Camera::~Camera()
{
}
