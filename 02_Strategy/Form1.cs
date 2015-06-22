using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _02_Strategy
{
    public partial class Form1 : Form
    {
        double total = 0.0d;

        public Form1()
        {
            InitializeComponent();
            cbxType.Items.AddRange(new object[] { "正常收费", "打8折", "满300返100", });
            cbxType.SelectedIndex = 0;
            textPrice.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CashContext csuper = new CashContext(cbxType.SelectedItem.ToString());
            double price = Convert.ToDouble(textPrice.Text);
            double num = Convert.ToDouble(textNum.Text);
            double totalPrices = csuper.GetResult(price * num);

            total += totalPrices;
            lbxList.Items.Add("单价：" + textPrice.Text + "    数量：" + textNum.Text +
                "   小计：" + totalPrices);
            lblResult.Text = total.ToString();
            textPrice.SelectAll();
            textPrice.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lbxList.Items.Clear();
            cbxType.SelectedIndex = 0;
            total = 0.0d;
            lblResult.Text = total.ToString();
            textPrice.Text = "";
            textNum.Text = "";
        }
    }

    //现金收费抽象类
    abstract class CashSuper
    {
        public abstract double acceptCash(double money);
    }

    //正常收费子类
    class CashNormal:CashSuper
    {
        public override double acceptCash(double money)
        {
            return money;
        }
    }

    //打折收费子类
    class CashRebate : CashSuper
    {
        private double moneyRebate = 1d;
        public CashRebate(string mondyRebate)
        {
            this.moneyRebate = double.Parse(mondyRebate);
        }

        public override double acceptCash(double money)
        {
            return money * moneyRebate;
        }
    }

    //返利收费子类
    class CashReturn:CashSuper
    {
        private double moneyCondition = 0.0d;
        private double moneyReturn = 0.0d;

        public CashReturn(string moneyCondition, string moneyReturn)
        {
            this.moneyCondition = double.Parse(moneyCondition);
            this.moneyReturn = double.Parse(moneyReturn);
        }

        public override double acceptCash(double money)
        {
            double result = money;
            if (money > moneyCondition)
                result = money - Math.Floor(money / moneyCondition) * moneyReturn;
            return result;
        }
    }

    class CashContext
    {
        CashSuper cs = null;
        public CashContext(string type)
        {
            switch(type)
            {
                case "正常收费":
                    cs = new CashNormal();
                    break;
                case "满300返100":
                    cs = new CashReturn("300", "100");
                    break;
                case "打8折":
                    cs = new CashRebate("0.8");
                    break;
            }
        }

        public double GetResult(double money)
        {
            return cs.acceptCash(money);
        }
    }
}
