using System;
//using System.Data;
using System.Collections;
//using MySql.Data;
//using MySql.Data.MySqlClient;

namespace Database
{
	public class Para
	{
		public int level;
		public float force = 0;
		public int patience = 0;
		public float sensitivity = 0;
		public int depth = 0;

	}

	public class DataAccess
	{
		private Para[] paras = new Para[3];
		private int[] scores = { 15, 30, 70};
		private int[] user = { 1, 14 }; //first is level;second is score;
		//private MySqlConnection conn;

		public void DbAccess()

		{
			string M_str_sqlcon = "Server=localhost;port=3306;" +
					"User ID=47;" +
					"Password=1111;" +
					"Database=FishGame;";
			//conn = new MySqlConnection(M_str_sqlcon);

		}

		public Para getPara(int level)
	    {
			Para result = new Para();
			result.level = level;
			String M_str_sqlstr = "SELECT * FROM Fish where level=" + level.ToString();
			/*try{
				MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, conn);
				conn.Open();
         		MySqlDataReader rdr = mysqlcom.ExecuteReader();
				if (rdr.Read())
				{
					result.force = (float)rdr[1];
					result.patience = (int)rdr[2];
					result.sensitivity = (float)rdr[3];
					result.depth = (int)rdr[4];
				}
				conn.Close();
			}catch(Exception e){*/
				result = paras [level - 1];
			//}
			return result;
	     }

		public int getScore(int level)
		{
			int score = 0;
			String M_str_sqlstr = "SELECT * FROM Scene where level=" + level.ToString();
			/*try{
				MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, conn);
				conn.Open();
				MySqlDataReader rdr = mysqlcom.ExecuteReader();
				if (rdr.Read())
				{
					score = (int)rdr[1];
				}
				conn.Close();
			}catch(Exception e){
			}*/
			score = scores [level - 1];
			return score;
		}

		public int getUserLevel(String username)
		{
			int level = 1;
			String M_str_sqlstr = "SELECT * FROM User where name=" + username;
			/*try{
				MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, conn);
				conn.Open();
				MySqlDataReader rdr = mysqlcom.ExecuteReader();
				if (rdr.Read())
				{
					level = (int)rdr[1];
				}
				conn.Close();
			}catch(Exception e){*/
				level = user [0];
			//}
			return level;
		}

		public int getUserScore(String username)
		{
			int score= 0;
			/*String M_str_sqlstr = "SELECT * FROM User where name=" + username;
			try{
				MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, conn);
				conn.Open();
				MySqlDataReader rdr = mysqlcom.ExecuteReader();
				if (rdr.Read())
				{
					score = (int)rdr[2];
				}
				conn.Close();
			}catch(Exception e){*/
				score = user [1];
			//}
			return score;
		}

		public void updateUser(String username,int level,int score){
			user [0] = level;
			user [1] = score;
		}

		public DataAccess()
		{
			DbAccess();
			Para p1 = new Para ();
			Para p2 = new Para ();
			Para p3 = new Para ();
			p1.level = 1;p1.force = 2;p1.patience = 15;p1.sensitivity = 7;p1.depth = 35;
			p2.level = 2;p2.force = 2.5f;p2.patience = 14;p2.sensitivity = 6;p2.depth = 40;
			p3.level = 3;p3.force = 4;p3.patience = 13;p3.sensitivity = 5;p3.depth = 45;
			paras [0] = p1;paras [1] = p2;paras [2] = p3;
		}

		public static void main(string[] args){
			DataAccess d = new DataAccess();
			d.getScore(1);
		}
	}
}
