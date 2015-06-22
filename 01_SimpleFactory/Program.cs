using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_SimpleFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("计算A+-*/B");
            Console.Write("输入数字A：");
            string strNumberA = Console.ReadLine();
            Console.Write("输入运算符：");
            string strOperate = Console.ReadLine();
            Console.Write("输入数字B：");
            string strNumberB = Console.ReadLine();

            Operation oper = OperationFactory.createOperation(strOperate);
            oper.NumberA = Convert.ToDouble(strNumberA);
            oper.NumberB = Convert.ToDouble(strNumberB);
            Console.WriteLine(oper.GetResult());
            Console.ReadLine();
        }
    }

    public class Operation
    {
        private double _numberB = 0;
        private double _numberA = 0;

        public double NumberA
        {
            get { return _numberA; }
            set { _numberA = value; }
        }

        public double NumberB
        {
            get { return _numberB; }
            set { _numberB = value; }
        }

        public virtual double GetResult()
        {
            return 0;
        }
    }

    class OperationAdd:Operation
    {
        public override double GetResult()
        {
            return NumberA + NumberB;
        }
    }

    class OperationSub : Operation
    {
        public override double GetResult()
        {
            return NumberA - NumberB;
        }
    }

    class OperationMul : Operation
    {
        public override double GetResult()
        {
            return NumberA * NumberB;
        }
    }

    class OperationDiv : Operation
    {
        public override double GetResult()
        {
            if (NumberB == 0)
                throw new Exception("除数不能为0");
            else
                return NumberA / NumberB;
        }
    }

    public class OperationFactory
    {
        public static Operation createOperation(string operate)
        {
            Operation oper = null;

            switch(operate)
            {
                case "+":
                    oper = new OperationAdd();
                    break;
                case "-":
                    oper = new OperationSub();
                    break;
                case "*":
                    oper = new OperationMul();
                    break;
                case "/":
                    oper = new OperationDiv();
                    break;
                default:
                    throw new Exception("不支持的运算");
            }

            return oper;
        }
    }
}
