#pragma once
#include <../dependencies/glfw/include/GLFW/glfw3.h>
#include <stdio.h>

#include <../bootstrap/Gizmos.h>
#include <glm/glm.hpp>
#include <glm/ext.hpp>
class Camera
{
public:
	Camera();
	glm::mat4 worldTransform;
	glm::mat4 viewTransform;
	glm::mat4 projectionTransform;
	glm::mat4 projectionViewTransform;
	
	virtual void update(float deltatime) = 0;
	virtual void getInput() = 0;
	void setPerspective(float fieldOfView, float aspectRatio, float near, float far);
	void setLookAt(glm::vec3 form, glm::vec3 to, glm::vec3 up);
	void setPosition(glm::vec3 position);
	glm::mat4 getWorldTransform();
	glm::mat4 getView();
	glm::mat4 getProjection();
	glm::mat4 getProjectionView();
	void updateProjectionViewTransform();
	~Camera();
};

