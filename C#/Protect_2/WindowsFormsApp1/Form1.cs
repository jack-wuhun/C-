using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public const int Add = 1;//加
        public const int Sub = 2;//减
        public const int Mul = 3;//乘
        public const int Div = 4;//除
        public const int Rec = 5;//倒数
        public const int Sqrt = 6;//开根
        public const int Opp = 7;//正负
        public const int Sqr = 8;//平方
        public const int Dep = 9;//小数点
        private double temp = 0;//equ功能中的临时数
        private double temp1 = 0;//showNumTxt1中的临时数
        private double num1 = 0;
        private double num2 = 0;
        private double res = 0;//结果
        private int flag = 0;/*动态标识输入类型,0为输入数字，1为第一次输入，
                              2为非第一次输入，3为得出结果后不输入新数并重复按等号
                              4为退格到尽头，归零的状态*/
        private Boolean backFlag = false;//决定退格的状态
        private int way;//标识四种符号方法（+ - * /）

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt1.Enabled = false;
            txt2.Enabled = false;
        }

        public void showNumTxt1(object str)
        {
            backFlag = true;
            if (str.ToString() == "0" && txt2.Text == "")//防止数的开头为0
            {
                return;
            }
            else if (flag == 0 || flag == Dep)//输入的是数字或者小数点
            {
                if(str.ToString() != ".")//输入的是数字
                {
                    backFlag = false;//保证可以退格
                    txt1.Text += str;
                    showNumTxt2(Int32.Parse(str.ToString()));
                }
                else//输入的是小数点
                {
                    backFlag = false;//保证可以退格
                    txt1.Text += str;
                    txt2.Text = txt2.Text.Substring(0, txt2.Text.Length) + ".";
                }
            }
            else if (flag == Rec)//输入的是1/x 运算符
            {
                txt1.Text += str;
                temp1 = 1 / double.Parse(txt2.Text.ToString());
                txt2.Text = temp1.ToString();
            }
            else if (flag == Sqrt)//输入的是开根号
            {
                txt1.Text += str; 
                temp1 = System.Math.Sqrt(double.Parse(txt2.Text.ToString()));
                txt2.Text = temp1.ToString();
            }
            else if(flag == Opp)//输入的是正负号
            {
                temp1 = double.Parse(txt2.Text.ToString());
                txt2.Text = (temp1 - temp1 * 2).ToString();
            }
            else if (flag == Sqr)//输入的是平方
            {
                txt1.Text += str;
                temp1 = double.Parse(txt2.Text.ToString());
                txt2.Text = (temp1 * temp1).ToString();
            }
            else if (txt1.Text != "" && flag == 1)//第一次输入的数字
            {
                txt1.Text += str;
                setNum1();
            }
            else if(txt1.Text == "" && flag == 2)//按完等号算完结果后继续按运算符
            {
                txt1.Text = num1.ToString() + str;
            }
            else if(txt1.Text != "")//没有按等号，直接继续按运算符
            {
                txt1.Text += str;
            }
        }

        public void showNumTxt2(int num)
        {
            txt2.Text += num.ToString();
        }

        /*public Boolean checkOperator()//检查是否已经处于运算符状态
        {
            Boolean tempBool = false;
        }*/

        public void clearTxt1()//模仿计算机按了等号清空上面的运算过程
        {
            txt1.Text = "";
        }

        public void clearTxt2()//清空下面的屏幕
        {
            txt2.Text = "";
        }

        public void setNum1()//设置第一次运算的第一个数
        {
            num1 = double.Parse(txt2.Text);
            clearTxt2();
        }

        public void setNum2()//设置第二个数
        {
            num2 = double.Parse(txt2.Text);
            equ();
        }

        public void showDivDefault()//展示除法错误
        {
            txt2.Text = "除数不能为0";
            object[] bt = { btn_0, btn_1, btn_2, btn_3, btn_4, btn_5, btn_6, btn_7,
            btn_8, btn_9, btn_dep, btn_add, btn_sub, btn_mul, btn_div, btn_equ, btn_rec,
            btn_sqr, btn_sqrt, btn_opp, btn_ce, btn_back};
            for (int i = 0; i <= 21; i++)//只开放按钮C，清零按钮
            {
                ((Button)bt[i]).Enabled = false;
            }
        }

        public void equ()//计算功能
        {
            if (txt2.Text == "")//只有采用了clearTxt2的方法才符合，即按了运算符way肯定不等于0
            {
                num2 = num1;
                temp = num2;
                flag = 3;
            }
            else if (way == 0)//只输入一个数后直接按等号
            {
                num1 = double.Parse(txt2.Text);
                res = num1;
                txt2.Text = res.ToString();
                clearTxt1();
            }
            else if(flag == 3)//运算完一直只按等号
            {
                switch (way)
                {
                    case Add:
                        res += temp;
                        break;
                    case Sub:
                        res -= temp;
                        break;
                    case Mul:
                        res *= temp;
                        break;
                    case Div:
                        res /= temp;
                        break;
                    default:
                        return;
                }
                txt2.Text = res.ToString();
                num1 = res;
                num2 = 0;
                return;
            }
            else//正常计算
            {
                num2 = double.Parse(txt2.Text);
                temp = num2;
                flag = 3;
            }               
            switch (way)
            {
                case Add:
                    res = num1 + num2;
                    break;
                case Sub:
                    res = num1 - num2;
                    break;
                case Mul:
                    res = num1 * num2;
                    break;
                case Div:
                    if (temp != 0)
                    {
                        res = num1 / temp;
                    }
                    else
                    {
                        showDivDefault();
                        return;
                    }
                    break;
                default:
                    return;
            }
            txt2.Text = res.ToString();
            num1 = res;           
            num2 = 0;
        }

        public void initTxt2()//重置显示屏2的内容
        {
            if(flag == 2 || flag == 4)
            {
                clearTxt2();
            }
            if(flag == 3)//计算完结果，输入新的数字进行重置
            {
                initCalculate();
            }
            backFlag = false;
        }

        public void initCalculate()//初始化
        {
            clearTxt1();
            clearTxt2();
            num1 = num2 = 0;
            temp = temp1 = 0;
            res = 0;
            flag = 0;
            way = 0;
            object[] bt = { btn_0, btn_1, btn_2, btn_3, btn_4, btn_5, btn_6, btn_7,
            btn_8, btn_9, btn_dep, btn_add, btn_sub, btn_mul, btn_div, btn_equ, btn_rec,
            btn_sqr, btn_sqrt, btn_opp, btn_ce, btn_back};
            for (int i = 0; i <= 21; i++)//开放上锁按钮
            {
                ((Button)bt[i]).Enabled = true;
            }
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            initTxt2();
            flag = 0;
            showNumTxt1(0);
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            initTxt2();
            flag = 0;
            showNumTxt1(1);
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            initTxt2();
            flag = 0;
            showNumTxt1(2); 
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            initTxt2();
            flag = 0;
            showNumTxt1(3);
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            initTxt2();
            flag = 0;
            showNumTxt1(4);
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            initTxt2();
            flag = 0;
            showNumTxt1(5);
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            initTxt2();
            flag = 0;
            showNumTxt1(6);
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            initTxt2();
            flag = 0;
            showNumTxt1(7);
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            initTxt2();
            flag = 0;
            showNumTxt1(8);
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            initTxt2();
            flag = 0;
            showNumTxt1(9);
        }

        private void btn_equ_Click(object sender, EventArgs e)//等号
        {
            equ();
            clearTxt1();
        }

        private void btn_add_Click(object sender, EventArgs e)//加法
        {

            if (way == 0)//第一次运算，第一个数的确定
            {
                flag = 1;
                way = Add;
                showNumTxt1("+");                              
            }
            else if (txt1.Text == "")//按了等号非第一次运算
            {
                flag = 2;
                way = Add;
                showNumTxt1("+");              
            }
            else//没按等号非第一次运算
            {
                setNum2();//必须先进行计算后改变运算符号
                flag = 2;
                way = Add;
                showNumTxt1("+");
            }
        }

        private void btn_sub_Click(object sender, EventArgs e)//减法
        {
            if (way == 0)//第一次运算，第一个数的确定
            {
                flag = 1;
                way = Sub;
                showNumTxt1("-");
            }
            else if (txt1.Text == "")//按了等号非第一次运算
            {
                flag = 2;
                way = Sub;
                showNumTxt1("-");
            }
            else//没按等号非第一次运算
            {
                setNum2();//必须先进行计算后改变运算符号
                flag = 2;
                way = Sub;
                showNumTxt1("-");
            }
        }

        private void btn_mul_Click(object sender, EventArgs e)//乘法
        {
            if (way == 0)//第一次运算，第一个数的确定
            {
                flag = 1;
                way = Mul;
                showNumTxt1("*");
            }
            else if (txt1.Text == "")//按了等号非第一次运算
            {
                flag = 2;
                way = Mul;
                showNumTxt1("*");
            }
            else//没按等号非第一次运算
            {
                setNum2();//必须先进行计算后改变运算符号
                flag = 2;
                way = Mul;
                showNumTxt1("*");
            }
        }

        private void btn_div_Click(object sender, EventArgs e)//除法
        {
            if (way == 0)//第一次运算，第一个数的确定
            {
                flag = 1;
                way = Div;
                showNumTxt1("/");
            }
            else if (txt1.Text == "")//按了等号非第一次运算
            {
                flag = 2;
                way = Div;
                showNumTxt1("/");
            }
            else//没按等号非第一次运算
            {
                setNum2();//必须先进行计算后改变运算符号
                flag = 2;
                way = Div;
                showNumTxt1("/");
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)//按钮C,全体清零,初始化
        {
            initCalculate();
        }

        private void btn_opp_Click(object sender, EventArgs e)//正反数
        {
            flag = Opp;
            showNumTxt1("opp");
        }

        private void btn_ce_Click(object sender, EventArgs e)//按钮CE
        {
            initCalculate();
        }

        private void btn_rec_Click(object sender, EventArgs e)//倒数
        {
            flag = Rec;
            showNumTxt1("in");
        }

        private void btn_sqrt_Click(object sender, EventArgs e)//开根
        {
            flag = Sqrt;
            showNumTxt1("sqrt");
        }

        private void btn_back_Click(object sender, EventArgs e)//退格
        {
            if(txt2.Text == txt1.Text || (txt1.Text != "" && flag == 0))//双屏相同的时候，或者输入的数字。退格同时退
            {               
                txt2.Text = txt2.Text.Substring(0, txt2.Text.Length - 1);
                txt1.Text = txt1.Text.Substring(0, txt1.Text.Length - 1);
            }
            else if (!backFlag)
            {
                txt2.Text = txt2.Text.Substring(0, txt2.Text.Length - 1);
            }
            if (txt2.Text == "")
            {
                txt2.Text = "0";//模拟计算机退完全部数字会变成零
                flag = 4;
            }
        }

        private void btn_dep_Click(object sender, EventArgs e)//小数点
        {
            if(txt2.Text == "")
            {
                flag = Dep;
                showNumTxt1("0");
                showNumTxt1(".");
            }
            else if (txt2.Text.IndexOf(".") < 0)
            {
                showNumTxt1(".");
            }
        }

        private void btn_sqr_Click(object sender, EventArgs e)//平方
        {
            flag = Sqr;
            showNumTxt1("sqr");
        }
    }
}