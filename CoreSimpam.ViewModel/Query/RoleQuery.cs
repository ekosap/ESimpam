using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSimpam.ViewModel.Query
{
    public class RoleQuery
    {
        public string searchValue { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 25;
        public string SortBy { get; set; }
    }
    public class JqueryDatatableParam    
{    
        public string sEcho { get; set; }    
        public string sSearch { get; set; }    
        public int iDisplayLength { get; set; }    
        public int iDisplayStart { get; set; }    
        public int iColumns { get; set; }    
        public int iSortCol_0 { get; set; }    
        public string sSortDir_0 { get; set; }    
        public int iSortingCols { get; set; }    
        public string sColumns { get; set; }    
 } 
}
