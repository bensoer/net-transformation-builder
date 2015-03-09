using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asgn5v1
{
    /// <summary>
    /// NetTransformationBuilder calculates and builds a net transformation matrix which can be retrieved at any time. The idea is to make building a netTranformation Matrix
    /// easier with simple method calls and avoids any extra math needed by the user. It also allows the user to easily debug thier program by removing and adding steps at
    /// any point of the builders process to see if the input is actualy creating the result they desire
    /// 
    /// NOTE: NetTransformationBuilder builds netTransformation Matrix's for Right-Handed Co-ordinate Systems
    /// </summary>
    class NetTransformationBuilder
    {
        /// <summary>
        /// The net Transformation
        /// </summary>
        private double[,] netTransformation;

        public NetTransformationBuilder(){
            netTransformation = new double[4,4]{{ 1, 0, 0, 0},
                                                { 0, 1, 0, 0},
                                                { 0, 0, 1, 0},
                                                { 0, 0, 0, 1}
                                               };
        }

        public NetTransformationBuilder(double[,] netTransformation){
            this.netTransformation = netTransformation;
        }
        /// <summary>
        /// Fetches the netTransformation
        /// </summary>
        /// <returns>a double array of type double of the net transformation matrix</returns>
        public double[,] GetNetTransformation()
        {
            return netTransformation;
        }
        /// <summary>
        /// Translates the shape in the desired direction. Negative values will cause the distance from 0 to shrink. Posotive
        /// will cause the distance to grow
        /// </summary>
        /// <param name="xValue"> the number of units to move in the x direction</param>
        /// <param name="yValue"> the number of units to move in the y direction</param>
        /// <param name="zValue"> the number of units to move in the z direction</param>
        public void Translate(double xValue, double yValue, double zValue){
            
                multiply(new double[4, 4] { { 1, 0, 0, 0 }, 
                                            { 0, 1, 0, 0 }, 
                                            { 0, 0, 1, 0 }, 
                                            { xValue, yValue, zValue, 1 } });
            
        }
        /// <summary>
        /// Mirrors the object on the X-axis. Note this method had no safety and does not check that the matrix has been translated appropriatly
        /// </summary>
        public void MirrorOnX()
        {
            multiply(new double[4, 4] { { 1, 0, 0, 0 }, 
                                        { 0, -1, 0, 0 }, 
                                        { 0, 0, 1, 0 }, 
                                        { 0, 0, 0, 1 } });   
        }
        /// <summary>
        /// Mirrors the object on the Y-axis. Note this method had no safety and does not check that the matrix has been translated appropriatly
        /// </summary>
        public void MirrorOnY()
        {            
            multiply(new double[4, 4] { { -1, 0, 0, 0 }, 
                                        { 0, 1, 0, 0 }, 
                                        { 0, 0, 1, 0 }, 
                                        { 0, 0, 0, 1 } });
        }
        /// <summary>
        /// Mirrors the object on the Z-axis. Note this method had no safety and does not check that the matrix has been translated appropriatly
        /// </summary>
        public void MirrorOnZ()
        {
            multiply(new double[4, 4] { { 1, 0, 0, 0 }, 
                                        { 0, 1, 0, 0 }, 
                                        { 0, 0, -1, 0 }, 
                                        { 0, 0, 0, 1 } });
        }
        /// <summary>
        /// Scales the object in the parameter passed directions. The value passed is how many times larger the shape will be made in the given axis
        /// </summary>
        /// <param name="xFactor"> number of time larger the shape will be in the x-axis</param>
        /// <param name="yFactor"> number of times larger the shape will be in the y-axis</param>
        /// <param name="zFactor"> number of times larger the shape will be in the z-axis</param>
        public void Scale(double xFactor, double yFactor, double zFactor)
        {
            multiply(new double[4, 4] { { xFactor, 0, 0, 0 }, 
                                        { 0, yFactor, 0, 0 }, 
                                        { 0, 0, zFactor, 0 }, 
                                        { 0, 0, 0, 1 } });
        }
        /// <summary>
        /// Rotates the object on the Z-axis. Note this method has no safety so it will not check if the matrix has been translated to the appropriate axis.
        /// </summary>
        /// <param name="radians"> how many radians to rotate the object by </param>
        /// <param name="clockwise"> whether to turn the shape clockwise or counter-clockwise as determined by viewing 0,0,0 from 0,0,1 on the Z-axis </param>
        public void RotateOnZ(double radians, bool clockwise = true)
        {
            double[,] temp = new double[4, 4] { { Math.Cos(radians), Math.Sin(radians), 0, 0 }, 
                                                { Math.Sin(radians), Math.Cos(radians), 0, 0 }, 
                                                { 0, 0, 1, 0 }, 
                                                { 0, 0, 0, 1 } };

            multiply(alterForRotationDirection(temp, 1, 0, clockwise));
        }
        /// <summary>
        /// Rotates the object on the Y-axis. Note this method has no safety so it will not check if the matrix has been translated to the appropriate axis.
        /// </summary>
        /// <param name="radians"> how many radians to rotate the object by </param>
        /// <param name="clockwise"> whether to turn the shape clockwise or counter-clockwise as determined by viewing 0,0,0 from 0,1,0 on the Y-axis </param>
        public void RotateOnY(double radians, bool clockwise = true)
        {
            double[,] temp = new double[4, 4] { { Math.Cos(radians), 0, Math.Sin(radians), 0 }, 
                                                { 0, 1, 0, 0 }, 
                                                { Math.Sin(radians), 0, Math.Cos(radians), 0 }, 
                                                { 0, 0, 0, 1 } };

            multiply(alterForRotationDirection(temp, 2, 0, clockwise));

        }
        /// <summary>
        /// Rotates the object on the X-axis. Note this method has no safety so it will not check if the matrix has been translated to the appropriate axis.
        /// </summary>
        /// <param name="radians"> how many radians to rotate the object by </param>
        /// <param name="clockwise"> whether to turn the shape clockwise or counter-clockwise as determined by viewing 0,0,0 from 1,0,0 on the X-axis </param>
        public void RotateOnX(double radians, bool clockwise = true)
        {
            double[,] temp = new double[4, 4] { { 1, 0, 0, 0 }, 
                                                { 0, Math.Cos(radians), Math.Sin(radians), 0 }, 
                                                { 0, Math.Sin(radians), Math.Cos(radians), 0 }, 
                                                { 0, 0, 0, 1 } };


            multiply(alterForRotationDirection(temp, 1, 2, clockwise));

        }
        /// <summary>
        /// Shears the object in the Horizontal direction effecting the X-Axis. Note this function has no safety and the object must translate appropriatly for shear to work
        /// </summary>
        /// <param name="factor"> the factor of shearing </param>
        public void ShearHorizontal(double factor)
        {
            double[,] temp = new double[4, 4] { { 1, 0, 0, 0},
                                                { factor, 1, 0, 0},
                                                { 0, 0, 1, 0},
                                                { 0, 0, 0, 1} };
            multiply(temp);
        }

        /// <summary>
        /// Shears the boject in the Vertical direciton effecting the Y-Axis. Note this function has no safety and the object must be translated appropriatly for shear to work
        /// </summary>
        /// <param name="factor"> the factor of shearing </param>
        public void ShearVertical(double factor)
        {
            double[,] temp = new double[4, 4] { { 1, factor, 0, 0},
                                                { 0, 1, 0, 0},
                                                { 0, 0, 1, 0},
                                                { 0, 0, 0, 1} };
            multiply(temp);
        }

        //row and col assume what row and colum to apply negative to for clockwise rotation. method will compensate if not clockwise
        private double[,] alterForRotationDirection(double[,] matrix, int row, int col, bool clockwise)
        {
            if (clockwise)
            {
                matrix[row, col] *= -1;
            }
            else
            {
                matrix[col, row] *= -1;
            }
            return matrix;
        }

        // the multiplication engine, takes in a matrix and multiplies the netMatrix by it
        private void multiply(double[,] matrix)
        {
            if (netTransformation == null)
            {
                netTransformation = matrix;
                return;
            }
            else
            {
                double[,] copy = createCopy();

                for(int h = 0; h < 4; h++){
               
                    for (int i = 0; i < 4; i++)
                    {
                        double total = 0;
                        for (int j = 0; j < 4; j++)
                        {
                            total += copy[i, j] * matrix[j, h];
                        }
                        netTransformation[i,h] = total;
                    }
                }
            }
        }
        // creates a copy of the netTransformation matrix. Is a helper for the multiply method
        private double[,] createCopy()
        {
            double[,] copy = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    copy[i, j] = netTransformation[i, j];
                }
            }
            return copy;
        }


    }
}
