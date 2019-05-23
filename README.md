# JuCheap.Core

#### 介绍
Jucheap.Core 使用asp.net core和ef core构建了一个跨平台的快速开发框架.

项目线上地址：  
net 4.5版本：http://www.jucheap.com  
net core版本：http://core.jucheap.com  
账号/密码：admin/qwaszx


#### 项目运行效果图

![JuCheap登陆界面](https://images.gitee.com/uploads/images/2019/0522/204550_1b6e782d_422345.png "JuCheap登陆界面")

![JuCheap菜单管理](https://img-blog.csdn.net/20160722103223754?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQv/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/Center "JuCheap菜单管理")

![JuCheap菜单管理](https://images.gitee.com/uploads/images/2019/0522/204638_ed9f84c9_422345.png "JuCheap菜单管理")




#### 技术栈
net core  
ef core  
hangfire  
log4net  
automapper  
simpleinjector(关于simpleinjector的api注入方式，请切换到api分支查看)  
sso(identity server 4，请切换到sso分支)  

#### 安装教程

1. 配置appsettings.json里面的数据库连接字符串
2. 在startup.cs里面找到自己使用的数据库类型，配置相应的代码
3. 代码里面默认使用的sql server数据，如果要使用mysql，则打开“程序包管理控制台”，选中“JuCheap.Core.Data”项目，执行 Add-Migration Init(这个是迁移脚本的名称)
4.初始化数据，都放在JuCheap.Core.Services/AppServices/DatabaseInitService.cs文件里面，程序启动的时候时候，会自动执行InitAsync方法，创建数据库和数据表，并初始化原始数据

#### 参与贡献

1. Fork 本仓库
2. 新建 Feat_xxx 分支
3. 提交代码
4. 新建 Pull Request!

#### 联系方式
QQ邮箱:359484089@qq.com