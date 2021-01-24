using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;



namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public delegate double Calc();

        private static string begin = @"using System;
namespace MyNamespace
{
    public delegate double Calc();
    public static class LambdaCreator 
    {
        public static Calc Create()
        {
            return ()=>";
        private static string end = @";
        }
    }
}";
        int RowsCount = 0;
        string[] MasColumns = { "I","J","K","L","M","N","O","P","Q","R", "S", "T", "V", "W", "X", "Y", "Z" };
        char[] Chisla = { '1', '2', '3', '4', '5', '6', '7', '8', '9','0'};
        char[] masTabliza = {  'A', 'B', 'C', 'D','E','F','G','H', 'I', 'J', 'K', 'L', 'M' };
       
      
       

        int ColumnsCount = 0;
        int RowIndex=0;
        int ColumnIndex = 0;
        string expression;
        int j = 0;
        char operatorr;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(1);
            dataGridView1.Rows[RowsCount].HeaderCell.Value = (RowsCount).ToString();
            RowsCount+=1;
           

        }
     
       
       
        private void button2_Click(object sender, EventArgs e)
        {
            
                dataGridView1.Columns.Add("column-6", MasColumns[ColumnsCount]);
            ColumnsCount += 1;
         /*   for(int k = 0; k < 4; k++)
            {
                for(int j = 0; j < 4; j++)
                {
                    textBox1.Text = masTablizaValue[k,j,1]+textBox1.Text;
                }
            }
          */
   

        }

        private void dataGridView1_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            j = 0;
            try
            {
                if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
                {
                    expression = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    // msg += "4";
                    
                    textBox1.Text = expression;
                    dataGridView1.Rows[RowIndex].Cells[ColumnIndex].Value = textBox1.Text;
                    //  MessageBox.Show(expression);
                }
            }
            catch (NullReferenceException ex)
            {
                                                    
            }
            if (expression != null)
            { int[] maszifra=new int[10];
                char[] masoperator = new char[10];
                string zifra="";
                int j = 0;
                int BukvaTest=0;
                int IndexX=1;
                int IndexY =1;
                string silka="";
                for (int i = 0; i < expression.Length; i++)
                {
                    if (masTabliza.Any(str => str == expression[i])) {
                        IndexX = (int)expression[i]-65;
                        IndexY = Convert.ToInt32(new string(expression[i+1], 1));
                       string znachenia = (dataGridView1.Rows[IndexY].Cells[IndexX].Value).ToString();
                        char[] chr = { expression[i],expression[i+1] };
                        silka =new string(chr);
                        expression = expression.Replace(silka, znachenia);
                    }


                }
                string middle = expression;
                  CSharpCodeProvider provider = new CSharpCodeProvider();
                  CompilerParameters parameters = new CompilerParameters();
                  parameters.GenerateInMemory = true;
                  parameters.ReferencedAssemblies.Add("System.dll");
                  CompilerResults results = provider.CompileAssemblyFromSource(parameters, begin + middle + end);
                  var cls = results.CompiledAssembly.GetType("MyNamespace.LambdaCreator");
                  var method = cls.GetMethod("Create", BindingFlags.Static | BindingFlags.Public);
                  var calc = (method.Invoke(null, null) as Delegate);
                  Console.WriteLine(calc.DynamicInvoke());
                  textBox1.Text = calc.DynamicInvoke().ToString();
                  //textBox3.Text = result(masoperator2, maszifra2).ToString();
                  dataGridView1.Rows[RowIndex].Cells[ColumnIndex].Value= calc.DynamicInvoke().ToString();
                  expression = "";
                  
                // textBox2.Text = (dataGridView1.Rows[IndexX].Cells[IndexY].Value).ToString();
               // textBox2.Text = expression;



            }

        }

            private void textBox1_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows[RowIndex].Cells[ColumnIndex].Value= textBox1.Text;
           // MessageBox.Show(textBox1.Text);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            {
                dataGridView1.Rows[RowIndex].Cells[ColumnIndex].Value = textBox1.Text;
           // MessageBox.Show(textBox1.Text);
            if (e.KeyCode == Keys.Enter) {
                
                }
          
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(textBox1.Text);
            RowIndex = e.RowIndex;
            ColumnIndex = e.ColumnIndex;
        }
      }
    }

