namespace MVC_Project.Models.MenuModel
{
    public class MenuListModel
    {
        //public string MENU_NAME { get; set; }=string.Empty;
        public List<MenuNameModel> M_NAME { get; set; }
        public List<SubMenuNameModel> S_NAME { get; set; }
        public string idcheck { get; set; }

        public string EmpCode { get; set; } = string.Empty;
        public string EMP_NAME { get; set; }=string.Empty;

    }

    public class MenuNameModel

    {
        public string MENU_NAME { get; set; }
        public string MENU_ID { get; set; }



    }

    public class SubMenuNameModel

    {
         public string SUBMENU_NAME { get; set; }
        //public string SUB_MENU { get; set; }
        
        public string MAIN_MENU_ID { get; set; }
        public string LINK { get; set; }
    }
}
