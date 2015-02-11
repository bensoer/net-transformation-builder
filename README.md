# Easily generate Net Transformation Matrixes in C#.

#Usage
Download the file and adjust the namesapce as appropriate

In your C# project create a NetTransformationBuilder object and start applying actions to it. The object will automaticaly manage the net matrix for you

````
NetTransformationBuilder matrix = new NetTransformationBuilder();
matrix.Translate(50,20);
matrix.MirrorOnX();
matrix.Translate(-50,-20);
````

Get the completed matrix as a double[,] array
````
doube[,] myNextTransformation = matrix.GetNetTransformation();
````

## Warning
The NetTransformationBuilder does not apply any safeties on its transformations. Example mirrors can only work if the shape is against the appropriate axis it is mirroring on. You will have to make sure you have translated your net matrix approrpiatly so that the mirror does work. Unless of course the matrix of the original shape is already on the axis you are mirroring on

#API
Create a new transformation matrix by instantiating one
````
NetTransformationBuilder matrix = new NetTransformationBuilder();
````
Apply translations by passing how much you want to translate by in the appropriate axis
````
matrix.Translate(xAxis,yAxis);
````
Apply mirroring by calling the appropriate functions.
````
matrix.MirrorOnX(); //mirror on x-axis (flip all y values)
matrix.MirrorOnY(); //mirror on y-axis (flip all x values)
````
Fetch your results as a double[,] array
````
matrix.GetNetTransformation();
````
