﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySqlHelper;
using System.Data;

namespace DemoForMyHelper
{
    class DemoForMySql
    {
        MySqlDrive mySql = new MySqlDrive("Server=192.168.200.152;UserId=root;Password=xpsh;Database=huala_test");
           
        public DemoForMySql()
        {
            mySql.OnDriveStateChange += mySql_OnDriveStateChange;
            mySql.OnGetErrorMessage += mySql_OnGetErrorMessage;
            mySql.OnGetInfoMessage += mySql_OnGetInfoMessage;
        }

        public void RunTest()
        {
            Console.WriteLine("ConnectDataBase");
            Console.WriteLine(mySql.ConnectDataBase().ToString());
            DataTable myTable = mySql.ExecuteQuery("select * from h_order limit 10");
            //foreach(DataColumn column in table.Columns)
            foreach (DataRow rows in myTable.Rows)
            {
                Console.WriteLine("");
                foreach(var filed in rows.ItemArray)
                {
                    Console.Write(filed.ToString());
                }
            }

            Console.WriteLine("__________________________________________________");
            myTable = mySql.ExecuteQuery("select * from h_order where seller_id=?id and order_amount>?amt limit 10", new Dictionary<string, string> { { "?id", "562" },{ "?amt", "100" } });
            //foreach(DataColumn column in table.Columns)
            foreach (DataRow rows in myTable.Rows)
            {
                Console.WriteLine("");
                foreach (var filed in rows.ItemArray)
                {
                    Console.Write(filed.ToString());
                }
            }

            Console.WriteLine("++++++++++++++++++++++++++select null data++++++++++++++++++++++++++++");
            myTable = mySql.ExecuteQuery("select * from h_order where seller_id=?id and order_amount>?amt limit 10", new Dictionary<string, string> { { "?id", "56200000"}, {"?amt", "100" } });
            //foreach(DataColumn column in table.Columns)
            foreach (DataRow rows in myTable.Rows)
            {
                Console.WriteLine("");
                foreach (var filed in rows.ItemArray)
                {
                    Console.Write(filed.ToString());
                }
            }

            Console.WriteLine("++++++++++++++++++++++++++updata data++++++++++++++++++++++++++++");
            myTable = mySql.ExecuteQuery("update  h_order set order_status = 'no_pay' where order_sn = 170724185537912003");
            //foreach(DataColumn column in table.Columns)
            foreach (DataRow rows in myTable.Rows)
            {
                Console.WriteLine("");
                foreach (var filed in rows.ItemArray)
                {
                    Console.Write(filed.ToString());
                }
            }

            Console.WriteLine("__________________________________________________");
            Console.WriteLine("__________________________________________________");
            Console.WriteLine("__________________________________________________");
            mySql.CreateNewAliveTask("TestIn", string.Format("select * from h_order  where mobile ='{0}' and order_status='no_pay' order by add_time desc", "15158155511"), 2000, new delegateGetAliveTaskDataTableInfoEventHandler((obj,table) => {
                foreach (DataRow rows in table.Rows)
                {
                    Console.WriteLine("");
                    foreach (var filed in rows.ItemArray)
                    {
                        Console.Write(filed.ToString());
                    }
                }
            }));

            Console.WriteLine("__________________________________________________");
            Console.WriteLine("__________________________________________________");
            Console.WriteLine("__________________________________________________");
            mySql.CreateNewMonitorTask("m1","select* from h_sso_log order by id desc limit 20 ",1,2,4000,new delegateGetMonitorTaskDataTableInfoEventHandler((sender,str)=>{
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine(str);
            }));


            
            //mySql.DelAliveTask("TestIn");
            //GC.Collect();
            //return;
            mySql.StartAliveTask("TestIn");
            mySql.StartMonitorTask("m1");

        }

        void mySql_OnGetInfoMessage(object sender, string InfoMessage)
        {
            Console.WriteLine("mySql_OnGetInfoMessage");
            Console.WriteLine(InfoMessage);
        }

        void mySql_OnGetErrorMessage(object sender, string ErrorMessage)
        {
            Console.WriteLine("mySql_OnGetErrorMessage");
            Console.WriteLine(ErrorMessage);
        }

        void mySql_OnDriveStateChange(object sender, bool isCounect)
        {
            Console.WriteLine("mySql_OnDriveStateChange");
            Console.WriteLine(isCounect.ToString());
        }

    }
}
