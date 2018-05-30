#pragma once
#include "Camera.h"
class FlyCamera :
	public Camera
{
public:
	FlyCamera();

	float speed;
	glm::vec3 up;

	void update(float deltaTime);
	void getInput();
	void setSpeed(float speed);
	~FlyCamera();
};

