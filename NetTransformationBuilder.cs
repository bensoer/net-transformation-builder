using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asgn5v1
{
    class NetTransformationBuilder
    {
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

        public double[,] GetNetTransformation()
        {
            return netTransformation;
        }

        public void Translate(double xValue, double yValue, double zValue){
            if(netTransformation == null){
                netTransformation = new double[4,4]{ {1,0,0,0}, {0,1,0,0}, {0,0,1,0}, {xValue,yValue,zValue,1}};
            }else{
                multiply(new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { xValue, yValue, zValue, 1 } });
            }
        }

        public void MirrorOnX()
        {
            if (netTransformation == null)
            {
                netTransformation = new double[4, 4] { { 1, 0, 0, 0 }, { 0, -1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            }
            else
            {
                multiply(new double[4, 4] { { 1, 0, 0, 0 }, { 0, -1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } });
            }
        }

        public void MirrorOnY()
        {
            if (netTransformation == null)
            {
                netTransformation = new double[4, 4] { { -1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            }
            else
            {
                multiply(new double[4, 4] { { -1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } });
            }
        }

        public void Scale(double xValue, double yValue, double zValue)
        {
            if (netTransformation == null)
            {
                netTransformation = new double[4, 4] { { xValue, 0, 0, 0 }, { 0, yValue, 0, 0 }, { 0, 0, zValue, 0 }, { 0, 0, 0, 1 } };
            }
            else
            {
                multiply(new double[4, 4] { { xValue, 0, 0, 0 }, { 0, yValue, 0, 0 }, { 0, 0, zValue, 0 }, { 0, 0, 0, 1 } });
            }
        }

        public void RotateOnZ(double radians, bool clockwise = true)
        {
            if (netTransformation == null)
            {
                netTransformation = new double[4, 4] { { Math.Cos(radians), Math.Sin(radians), 0, 0 }, 
                                                       { Math.Sin(radians), Math.Cos(radians), 0, 0 }, 
                                                       { 0, 0, 1, 0 }, 
                                                       { 0, 0, 0, 1 } };
                this.alterForRotationDirection(netTransformation, clockwise);
            }
            else
            {
                multiply(alterForRotationDirection(new double[4,4] { { Math.Cos(radians), Math.Sin(radians), 0, 0 }, { Math.Sin(radians), Math.Cos(radians), 0, 0 }, {0, 0, 1, 0}, {0, 0, 0, 1} }, clockwise));
            }
        }

        public void RotateOnY(double radians, bool clockwise = true)
        {
            double[,] temp = new double[4, 4] { { Math.Cos(radians), 0, Math.Sin(radians), 0 }, 
                                                { 0, 1, 0, 0 }, 
                                                { Math.Sin(radians), 0, Math.Cos(radians), 0 }, 
                                                { 0, 0, 0, 1 } };

            if (clockwise)
            {
                temp[0, 2] *= -1;
            }
            else
            {
                temp[2, 0] *= -1;
            }


            if (netTransformation == null)
            {
                this.netTransformation = temp;
            }
            else
            {
                multiply(temp);
            }
        }

        public void RotateOnX(double radians, bool clockwise = true)
        {
            double[,] temp = new double[4, 4] { { 1, 0, 0, 0 }, 
                                                { 0, Math.Cos(radians), Math.Sin(radians), 0 }, 
                                                { 0, Math.Sin(radians), Math.Cos(radians), 0 }, 
                                                { 0, 0, 0, 1 } };
            if (clockwise)
            {
                temp[1, 2] *= -1;
            }
            else
            {
                temp[2, 1] *= -1;
            }

            if (netTransformation == null)
            {
                this.netTransformation = temp;
            }
            else
            {
                multiply(temp);
            }
        }

        private double[,] alterForRotationDirection(double[,] matrix, bool clockwise)
        {
            if (clockwise)
            {
                matrix[1, 0] *= -1;
                //matrix[1, 0] *= -1;
                return matrix;
            }
            return matrix;
        }

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
