using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Linq;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		private void button2_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderDlg = new FolderBrowserDialog();
			DialogResult result = folderDlg.ShowDialog();
			if(result == DialogResult.OK)
			{
				string[] subdirs = Directory.GetDirectories(folderDlg.SelectedPath, "*", SearchOption.AllDirectories);
				textBox1.Text = folderDlg.SelectedPath;
				ArrayList list = new ArrayList();
				foreach(string i in subdirs)
				{
					list.Add(i);
				}
				list.Sort();
				list.Reverse();
				listBox1.Items.Clear();
				listBox2.Items.Clear();
				foreach(string i in list)
				{
					listBox2.Items.Add(Path.GetFileName(i).Trim());
					listBox1.Items.Add(i);
				}
			}
		}
		private void button3_Click(object sender, EventArgs e)
		{
			DateTime dt = new DateTime();
			bool error = true;
			foreach(string str in listBox1.Items)
			{
				string listDate = Path.GetFileName(str);
				if(DateTime.TryParse(listDate, out dt)) 
		        {
					try
		            {
		            	dt = Convert.ToDateTime(listDate);
		            	string newFileName = dt.ToString("yyyy-MM-dd");
		            	string FileName = Path.GetFileName(str);
		            	string NewFilePath = str.Replace(FileName, newFileName);
		            	Directory.Move(str, NewFilePath);
		            	error = false;
		            }
		            catch(Exception e1)
		            {
		            	error = true;
		            }
				}
				else
				{
					try
					{

						/*string strspite = "[a-z]+";
						string[] result = Regex.Split(listDate, strspite, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500));

						Regex extractDateRegex = new Regex("[0-9]{1,2}\\/[0-9]{1,2}\\/[0-9]{4}");
						string[] extracted = extractDateRegex.Matches(listDate).Cast<Match>().Select(m => m.Value).ToArray();
						DateTime dateformat = Convert.ToDateTime(extracted[0]);
						
							listBox2.Items.Add("demo :  " + dateformat);*/
						
						


						string[] parts = listDate.Split(' ');
						DateTime dateformat = Convert.ToDateTime(parts[0]);
						string newFileName = dateformat.ToString("yyyy-MM-dd");
						string stringformat = parts[1];
						string datestring = newFileName + " " + stringformat;
						string FileName = Path.GetFileName(str);
						string NewFilePath = str.Replace(FileName, datestring);
						Directory.Move(str, NewFilePath);
						error = false;
					}
					catch(Exception e1)
					{ 
						error = true;
					}
				}
			}
			if(error == false)
			{
				MessageBox.Show("Name format changed");
			}
			else
			{
				MessageBox.Show("Name changing failed  :(");
			}
		}
		private void button4_Click(object sender, EventArgs e)
		{
			textBox1.Clear();
			listBox1.Items.Clear();
			listBox2.Items.Clear();
		}
		public Form1()
		{
			InitializeComponent();
		}
	
	}
}
