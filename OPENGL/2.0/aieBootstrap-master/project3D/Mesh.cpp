//#include "Mesh.h"
//
//
//
////Mesh::Mesh()
////{
////}
//
//
//Mesh::~Mesh()
//{
//	glDeleteVertexArrays(1, &vao);
//	glDeleteBuffers(1, &vbo);
//	glDeleteBuffers(1, &ibo);
//}
//
//void Mesh::initialiseQuad()
//{
//	// check that the mesh is not initalized already
//	assert(vao == 0);
//
//	// Generate Buffers.
//	glGenBuffers(1, &vbo);
//	glGenVertexArrays(1, &vao);
//
//	//bind vertex array aka a mesh wrapper
//	glBindVertexArray(vao);
//
//	//bind vertex buffer
//	glBindBuffer(GL_ARRAY_BUFFER, vbo);
//
//	// define 6 vertices for 2 triangles
//
//	Vertex vertices[6];
//
//	vertices[0].position = { -0.5f, 0, 0.5f, 1 };
//	vertices[1].position = { 0.5f, 0, 0.5f, 1 };
//	vertices[2].position = { -0.5f, 0, -0.5f, 1 };
//	
//	
//	vertices[3].position = { -0.5f, 0, -0.5f, 1 };
//	vertices[4].position = { 0.5f, 0, 0.5f, 1 };
//	vertices[5].position = { 0.5f, 0, -0.5f, 1 };
//
//
//
//	vertices[0].texCoord = { 0,1 };
//	vertices[1].texCoord = { 1,1 };
//	vertices[2].texCoord = { 0,0 };
//
//	vertices[3].texCoord = { 0,0 };
//	vertices[4].texCoord = { 1,1 };
//	vertices[5].texCoord = { 1,0 };
//
//	// fill vertex buffer
//	glBufferData(GL_ARRAY_BUFFER, 6 * sizeof(Vertex), vertices, GL_STATIC_DRAW);
//
//	// enable first element as position
//	glEnableVertexAttribArray(0);
//	glVertexAttribPointer(0, 4, GL_FLOAT, GL_FALSE, sizeof(Vertex), 0);
//
//	//unbind buffers
//	glBindVertexArray(0);
//	glBindBuffer(GL_ARRAY_BUFFER, 0);
//
//	// quad has 2 triangles
//	triCount = 2;
//
//}
