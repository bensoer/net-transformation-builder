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
            
        }

        public double[,] GetNetTransformation()
        {
            return netTransformation;
        }

        public void Translate(double xValue, double yValue){
            if(netTransformation == null){
                netTransformation = new double[4,4]{ {1,0,0,0}, {0,1,0,0}, {0,0,1,0}, {xValue,yValue,0,1}};
            }else{
                multiply(new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { xValue, yValue, 0, 1 } });
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

        public void Scale(double xValue, double yValue)
        {
            if (netTransformation == null)
            {
                netTransformation = new double[4, 4] { { xValue, 0, 0, 0 }, { 0, yValue, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            }
            else
            {
                multiply(new double[4, 4] { { xValue, 0, 0, 0 }, { 0, yValue, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } });
            }
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
