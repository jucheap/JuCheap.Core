namespace JuCheap.Core.Infrastructure.Menu
{
    /// <summary>
    /// 菜单数据配置
    /// </summary>
    public class Menu
    {
        #region 模块参数配置

        /// <summary>
        /// 系统设置模块
        /// </summary>
        public static (string Id, string Name) System = (SystemId, "系统设置");

        /// <summary>
        /// 日志查看模块
        /// </summary>
        public static (string Id, string Name) Logs = (LogsId, "日志查看");

        /// <summary>
        /// 示例页面模块
        /// </summary>
        public static (string Id, string Name) Pages = (PagesId, "示例页面");

        #endregion

        #region 菜单常量配置

        /// <summary>
        /// 系统设置模块Id
        /// </summary>
        public const string SystemId = "00001";

        /// <summary>
        /// 日志查看模块Id
        /// </summary>
        public const string LogsId = "00002";

        /// <summary>
        /// 示例页面模块Id
        /// </summary>
        public const string PagesId = "00003";

        /// <summary>
        /// 系统配置页面Id
        /// </summary>
        public const string SystemConfigId = "00004";

        /// <summary>
        /// 重置路劲码Id
        /// </summary>
        public const string ResetPathCodeId = "00005";

        /// <summary>
        /// 重置省市区Id
        /// </summary>
        public const string ResetAreaId = "00006";

        /// <summary>
        /// 省市区页面Id
        /// </summary>
        public const string AreaPageId = "00007";

        /// <summary>
        /// 省市区添加Id
        /// </summary>
        public const string AreaPageAddId = "00008";

        /// <summary>
        /// 省市区编辑Id
        /// </summary>
        public const string AreaPageEditId = "00009";

        /// <summary>
        /// 省市区删除Id
        /// </summary>
        public const string AreaPageDeleteId = "00010";

        /// <summary>
        /// 部门管理页面Id
        /// </summary>
        public const string DepartmentPageId = "00011";

        /// <summary>
        /// 部门管理添加Id
        /// </summary>
        public const string DepartmentAddId = "00012";

        /// <summary>
        /// 部门管理编辑Id
        /// </summary>
        public const string DepartmentEditId = "00013";

        /// <summary>
        /// 部门管理删除Id
        /// </summary>
        public const string DepartmentDeleteId = "00014";

        /// <summary>
        /// 菜单管理页面Id
        /// </summary>
        public const string MenuPageId = "00015";

        /// <summary>
        /// 菜单管理添加Id
        /// </summary>
        public const string MenuAddId = "00016";

        /// <summary>
        /// 菜单管理编辑Id
        /// </summary>
        public const string MenuEditId = "00017";

        /// <summary>
        /// 菜单管理删除Id
        /// </summary>
        public const string MenuDeleteId = "00018";

        /// <summary>
        /// 角色管理页面Id
        /// </summary>
        public const string RolePageId = "00019";

        /// <summary>
        /// 角色管理添加Id
        /// </summary>
        public const string RoleAddId = "00020";

        /// <summary>
        /// 角色管理编辑Id
        /// </summary>
        public const string RoleEditId = "00021";

        /// <summary>
        /// 角色管理删除Id
        /// </summary>
        public const string RoleDeleteId = "00022";

        /// <summary>
        /// 角色授权页面Id
        /// </summary>
        public const string RoleAuthorizeId = "00023";

        /// <summary>
        /// 设置角色权限Id
        /// </summary>
        public const string RoleSetAuthorizeId = "00024";

        /// <summary>
        /// 取消角色授权Id
        /// </summary>
        public const string RoleCancelAuthorizeId = "00025";

        /// <summary>
        /// 用户管理页面Id
        /// </summary>
        public const string UserPageId = "00026";

        /// <summary>
        /// 用户管理添加Id
        /// </summary>
        public const string UserAddId = "00027";

        /// <summary>
        /// 用户管理编辑Id
        /// </summary>
        public const string UserEditId = "00028";

        /// <summary>
        /// 用户管理删除Id
        /// </summary>
        public const string UserDeleteId = "00029";

        /// <summary>
        /// 用户角色设置页面Id
        /// </summary>
        public const string UserRoleSetPageId = "00030";

        /// <summary>
        /// 用户角色设置Id
        /// </summary>
        public const string UserRoleSetId = "00031";

        /// <summary>
        /// 用户角色取消Id
        /// </summary>
        public const string UserRoleCancelId = "00032";

        /// <summary>
        /// 登陆日志页面Id
        /// </summary>
        public const string LoginLogId = "00033";

        /// <summary>
        /// 访问日志页面Id
        /// </summary>
        public const string PageViewId = "00034";

        /// <summary>
        /// 统计图表页面Id
        /// </summary>
        public const string ChartId = "00035";

        /// <summary>
        /// 按钮页面Id
        /// </summary>
        public const string PageButtonId = "00036";

        /// <summary>
        /// 字体页面Id
        /// </summary>
        public const string PageFontId = "00037";

        /// <summary>
        /// 表单页面Id
        /// </summary>
        public const string PageFormId = "00038";

        /// <summary>
        /// 高级表单页面Id
        /// </summary>
        public const string PageFormAdvanceId = "00039";

        /// <summary>
        /// 表格页面Id
        /// </summary>
        public const string PageTableId = "00040";

        /// <summary>
        /// Tab页面Id
        /// </summary>
        public const string PageTabId = "00041";

        /// <summary>
        /// 站内信页面Id
        /// </summary>
        public const string MessagePageId = "00042";

        /// <summary>
        /// 发送站内信页面Id
        /// </summary>
        public const string MessageAddId = "00043";

        /// <summary>
        /// 编辑站内信页面Id
        /// </summary>
        public const string MessageEditId = "00044";

        /// <summary>
        /// 删除站内信页面Id
        /// </summary>
        public const string MessageDeleteId = "00045";
        #endregion

        #region 任务模板常量配置

        /// <summary>
        /// 任务模板首页页面Id
        /// </summary>
        public const string TaskTemplatePageId = "00046";

        /// <summary>
        /// 任务模板添加页面Id
        /// </summary>
        public const string TaskTemplateAddId = "00047";

        #endregion
    }
}
