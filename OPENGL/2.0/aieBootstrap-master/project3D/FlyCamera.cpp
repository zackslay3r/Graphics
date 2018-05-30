#include "FlyCamera.h"



FlyCamera::FlyCamera()
{
	
}
void FlyCamera::update(float deltaTime)
{
	getInput();
	updateProjectionViewTransform();
	
}

void FlyCamera::getInput()
{
}

void FlyCamera::setSpeed(float newSpeed)
{
	speed = newSpeed;
}

FlyCamera::~FlyCamera()
{
}
