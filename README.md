# Easily Generate Net Transformation Matrixes in C#.

#Usage
Download the file and adjust the namesapce as appropriate

In your C# project create a NetTransformationBuilder object and start applying actions to it. The object will automaticaly manage the net matrix for you

````csharp
NetTransformationBuilder matrix = new NetTransformationBuilder();
matrix.Translate(50,20, 0);
matrix.MirrorOnX();
matrix.Translate(-50,-20, 0);
````

Get the completed matrix as a double[,] array
````csharp
doube[,] myNextTransformation = matrix.GetNetTransformation();
````

## Warning
The NetTransformationBuilder does not apply any safeties on its transformations. Example mirrors can only work if the shape is against the appropriate axis it is mirroring on. You will have to make sure you have translated your net matrix approrpiatly so that the mirror does work. Unless of course the matrix of the original shape is already on the axis you are mirroring on

#API
Create a new transformation matrix by instantiating one. If you already have a 4x4 net transformation you can pass it as part of the constructor for the  NetTransformationBuilder to manage for you
````csharp
NetTransformationBuilder matrix = new NetTransformationBuilder();

NetTransformationBuilder matrix = new NetTransformationBuilder(double[4,4] netMatrix);
````
Apply translations by passing how much you want to translate by in the appropriate axis
````csharp
matrix.Translate(xAxis,yAxis, zAxis);
````
Apply mirroring by calling the appropriate functions. Z mirror is currently not supported
````csharp
matrix.MirrorOnX(); //mirror on x-axis (flip all y values)
matrix.MirrorOnY(); //mirror on y-axis (flip all x values)
````
Apply scaling by passing the factor to scale by in each axis
````csharp
matrix.Scale(xFactor, yFactor, zFactor);
````
Apply rotation by calling the appropriate function passing how many radians and optionaly directions. By default the method assumes clockwise rotation
````csharp
matrix.RotateOnZ(radians, clockwise?);
matrix.RoateOnY(radians, clockwise?);
matrix.RotateOnX(radians, clockwise?);

//shortcut for clockwise rotation
matrix.RotateOnZ(radians);
matrix.RotateOnY(radians);
matrix.RotateOnX(radians);
````
Fetch your results as a double[,] array
````csharp
matrix.GetNetTransformation();
````
