﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class AccountComment
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public int Likes { get; set; }
        public string Date { get; set; }

        public AccountComment(string data)
        {
            string[] arr = data.Split('~');
            for (int i = 0; i < arr.Length; i += 2)
            {
                switch (arr[i])
                {
                    case "2":
                        Comment = Encoding.ASCII.GetString(Convert.FromBase64String(arr[i + 1]));
                        break;
                    case "4":
                        Likes = int.Parse(arr[i + 1]);
                        break;
                    case "6":
                        ID = int.Parse(arr[i + 1]);
                        break;
                    case "9":
                        Date = arr[i + 1];
                        break;
                }
            }
        }
    }
}
