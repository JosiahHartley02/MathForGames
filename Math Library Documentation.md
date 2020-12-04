
| Josiah Hartley|
| :---          	|
| s208046     	|
| Math For Games |
| MathLibrary Documentation |

## I. Requirements

1. Description of Problem

	- **Name**: Math Library

	- **Problem Statement**: 
	During this subject you will be required to create maths classes implementing Vector and Matrix objects for use in real-time applications.
	- **Problem Specifications**:  
    Your first task is to write a collection of classes in C#. You must implement the required classes and functions listed below. To ensure that your code functions correctly you will be given a Unit Test Application that you must use to test the accuracy of your mathematical methods.

## I. Design


### Math Information

   **File**: Vector2.cs

     
  **Attributes**

         Name: _x
             Description: x value
             Type:float
         Name: _y
             Description: y value
             Type:float
         Name: X
             Description: Getter for _x
             Type:float
         Name: Y
             Description: Getter for _y
             Type:float
         Name: Magnitude
             Description: returns pythagorean theorum of x and y
             Type:float
         Name: Normalized
             Description: Adapts x and y value to fit in basic unit circle (ie) divides all values by magnitude
             Type:Vector2
         Name: Vector2
             Description: creates a new vector2
             Type:Vector2.cs
         Name: Normalize(Vector2 vector)
             Description: Adapts x and y value to fit in basic unit circle (ie) divides all values by magnitude
             Type:static Vector2
         Name: DotProduct(Vector2 lhs, Vector2 rhs)
             Description: returns lhs • rhs
             Type:static float
         Name: +(Vector2 lhs, Vector2 rhs)
             Description: adds vectors
             Type: operator
         Name: -(Vector2 lhs, Vector2 rhs)
             Description: subtracts vectors
             Type: operator
         Name: *(Vector2 lhs, Vector2 rhs)
             Description: multplies vectors
             Type: operator
         Name: /(Vector2 lhs, Vector2 rhs)
             Description: divides vectors
             Type: operator

   **File**: Vector3.cs

     
  **Attributes**

         Name: _x
             Description: x value
             Type:float
         Name: _y
             Description: y value
             Type:float
         Name: _z
             Description: z value
             Type:float
         Name: X
             Description: Getter for _x
             Type:float
         Name: Y
             Description: Getter for _y
             Type:float
         Name: Z
             Description: Getter for _z
             Type:float
         Name: Magnitude
             Description: returns pythagorean theorum of x and y
             Type:float
         Name: Normalized
             Description: Adapts x and y and z value to fit in basic unit circle (ie) divides all values by magnitude
             Type:Vector3
         Name: Vector3
             Description: creates a new vector3
             Type:Vector3.cs
         Name: Vector3(float x,float y, float z)
             Description: sets values to above 
             Type:Vector3
         Name: Normalize(Vector3 vector)
             Description: Adapts x and y and z value to fit in basic unit circle (ie) divides all values by magnitude
             Type:static Vector3
         Name: DotProduct(Vector3 lhs, Vector3 rhs)
             Description: returns lhs • rhs
             Type:static float
         Name: CrossProduct(Vector3 lhs, Vector3 rhs)
             Description: returns lhs x rhs
             Type:float
         Name: +(Vector3 lhs, Vector3 rhs)
             Description: adds vectors
             Type: operator
         Name: -(Vector3 lhs, Vector3 rhs)
             Description: subtracts vectors
             Type: operator
         Name: *(Vector3 lhs, Vector3 rhs)
             Description: multplies vectors
             Type: operator
         Name: /(Vector3 lhs, Vector3 rhs)
             Description: divides vectors
             Type: operator

   **File**: Vector4.cs

     
  **Attributes**

         Name: _x
             Description: x value
             Type:float
         Name: _y
             Description: y value
             Type:float
         Name: _z
             Description: z value
             Type:float
         Name: _w
             Description: w value
             Type:float
         Name: X
             Description: Getter for _x
             Type:float
         Name: Y
             Description: Getter for _y
             Type:float
         Name: Z
             Description: Getter for _z
             Type:float
         Name: W
             Description: Getter for _w
             Type:float
         Name: Magnitude
             Description: returns pythagorean theorum of x and y
             Type:float
         Name: Normalized
             Description: Adapts x and y and z value to fit in basic unit circle (ie) divides all values by magnitude
             Type:Vector4
         Name: Vector4
             Description: creates a new vector4
             Type:Vector4.cs
         Name: Vector4(float x,float y, float z, float w)
             Description: sets values to above 
             Type:Vector4
         Name: Normalize(Vector4 vector)
             Description: Adapts x and y and z value to fit in basic unit circle (ie) divides all values by magnitude
             Type:static Vector4
         Name: DotProduct(Vector4 lhs, Vector4 rhs)
             Description: returns lhs • rhs
             Type:static float
         Name: CrossProduct(Vector4 lhs, Vector4 rhs)
             Description: returns lhs x rhs
             Type:float
         Name: +(Vector4 lhs, Vector4 rhs)
             Description: adds vectors
             Type: operator
         Name: -(Vector4 lhs, Vector4 rhs)
             Description: subtracts vectors
             Type: operator
         Name: *(Vector4 lhs, Vector4 rhs)
             Description: multplies vectors
             Type: operator
         Name: /(Vector4 lhs, Vector4 rhs)
             Description: divides vectors
             Type: operator

   **File**: Natrix3.cs

     
  **Attributes**

         Name: m11
             Description: m11 value
             Type:float
         Name: m12
             Description: m12 value
             Type:float
         Name: m13
             Description: m13 value
             Type:float
         Name: m21
             Description: m21 value
             Type:float
         Name: m22
             Description: m22 value
             Type:float
         Name: m23
             Description: m23 value
             Type:float
         Name: m31
             Description: m31 value
             Type:float
         Name: m32
             Description: m32 value
             Type:float
         Name: m33
             Description: m33 value
             Type:float
         Name: Matrix3
             Description: creates a new matrix3 variable
             Type:Matrix3.cs
         Name: CreateRotation(radians)
             Description: Creates a rotation of radians around z vector
             Type:Matrix3.cs
         Name: CreateTranslation(Vector2 Position)
             Description: adds m13 + position.x and m23 + position.y
             Type:Matrix3.cs
         Name: CreateScale(float x, float y)
             Description: multiplies m11 and m21 by x and multiplies m12 and m22 by y
             Type:Matrix3.cs
         Name: +(Matrix3 lhs, Matrix3 rhs)
             Description: adds each individual float
             Type:operator
         Name: -(Matrix3 lhs, Matrix3 rhs)
             Description: subtracts each individual float
             Type:operator
         Name: *(Matrix3 lhs, Matrix3 rhs)
             Description: multiplies each individual float
             Type:operator

   **File**: Natrix4.cs

     
  **Attributes**

         Name: m11
             Description: m11 value
             Type:float
         Name: m12
             Description: m12 value
             Type:float
         Name: m13
             Description: m13 value
             Type:float
         Name: m14
             Description: m14 value
             Type:float
         Name: m21
             Description: m21 value
             Type:float
         Name: m22
             Description: m22 value
             Type:float
         Name: m23
             Description: m23 value
             Type:float
         Name: m24
             Description: m24 value
             Type:float
         Name: m31
             Description: m31 value
             Type:float
         Name: m32
             Description: m32 value
             Type:float
         Name: m33
             Description: m33 value
             Type:float
         Name: m34
             Description: m34 value
             Type:float
         Name: m41
             Description: m41 value
             Type:float
         Name: m42
             Description: m42 value
             Type:float
         Name: m43
             Description: m43 value
             Type:float
         Name: m44
             Description: m44 value
             Type:float
         Name: Matrix4
             Description: creates a new matrix4 variable
             Type:Matrix4.cs
         Name: CreateRotationX(radians)
             Description: Creates a rotation of radians around x vector
             Type:Matrix4.cs
         Name: CreateRotationY(radians)
             Description: Creates a rotation of radians around y vector
             Type:Matrix4.cs
         Name: CreateRotationZ(radians)
             Description: Creates a rotation of radians around z vector
             Type:Matrix4.cs
         Name: CreateTranslation(Vector3 Position)
             Description: adds m13 + position.x and m23 + position.y and m33 + position.z
             Type:Matrix4.cs
         Name: CreateScale(float x, float y, float z)
             Description: multiplies m11 and m21 by x and multiplies m12 and m22 by y and multiplies m31 and m32 by z
             Type:Matrix4.cs
         Name: +(Matrix4 lhs, Matrix4 rhs)
             Description: adds each individual float
             Type:operator
         Name: -(Matrix4 lhs, Matrix4 rhs)
             Description: subtracts each individual float
             Type:operator
         Name: *(Matrix4 lhs, Matrix4 rhs)
             Description: multiplies each individual float
             Type:operator