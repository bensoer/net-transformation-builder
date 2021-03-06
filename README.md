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
doube[,] myNetTransformation = matrix.GetNetTransformation();
````

See the API section of the README for a full breakdown of all the functions

## Warning
<b>NetTransofmratonBuilder Is Designed To Build NetTransformation Matrixes For Right-Handed Co-ordinate Systems</b>

The NetTransformationBuilder does not apply any safeties on any of its transformations. Example mirrors can only work if the shape is against the appropriate axis it is mirroring on. You will have to make sure you have translated your net matrix approrpiatly so that the mirror does work. Unless of course the matrix of the original shape is already on the axis you are mirroring on. Rotations are the same

#API
Create a new transformation matrix by instantiating one. If you already have a 4x4 net transformation you can pass it as part of the constructor for the  NetTransformationBuilder to manage for you
````csharp
NetTransformationBuilder matrix = new NetTransformationBuilder();

NetTransformationBuilder matrix = new NetTransformationBuilder(double[4,4] netMatrix);
````
Apply translations by passing how much you want to translate by in the appropriate axis
````csharp
matrix.Translate(double xAxis, double yAxis, double zAxis);
````
Apply mirroring by calling the appropriate functions. Z mirror is currently partialy supported but untested
````csharp
matrix.MirrorOnX(); //mirror on x-axis (flip all y values)
matrix.MirrorOnY(); //mirror on y-axis (flip all x values)
matrix.MirrorOnZ(); //mirror on z-axis (flip all z values)
````
Apply scaling by passing the factor to scale by in each axis
````csharp
matrix.Scale(double xFactor, double yFactor, double zFactor);
````
Apply rotation by calling the appropriate function passing how many radians and optionaly directions. By default the method assumes clockwise rotation. Clockwise rotation is determined by looking at 0,0,0 from the axis you are rotating on (eg. for RotateOnZ, clockwise is determined if you were standing at 0,0,1 and looking at 0,0,0)
````csharp
matrix.RotateOnZ(double radians, bool clockwise = true);
matrix.RoateOnY(double radians, bool clockwise = true);
matrix.RotateOnX(double radians, bool clockwise = true);
````
Apply A Horizontal or Vertical shear with the following functions. Currently only 2D shears along the X and Y axis are supported.
````csharp
matrix.ShearHorizontal(double factor);
matrix.ShearVertical(double factor);
````
Fetch your results as a double[,] array
````csharp
double[,] myNetMatrix = matrix.GetNetTransformation();
````
