﻿using System.Collections.Generic;

namespace ReportsDAL
{
    public class ReportModel
    {
        public int Id { get; set; }
        public List<TaskModel> Tasks { get; set; } 
        public ReportStatus Status { get; set; }
        public int Employee { get; set; }
        public enum ReportStatus
        {
            Active,
            Close
        }
    }
}