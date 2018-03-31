1.如何获取token
	token获取地址：http://localhost:63230/connect/token
	方式：post
	body参数：
		client_id:api.client
		client_secret:28365BC74137474DA6986B86836B4468
		grant_type:password
		username:jucheap
		password:qwaszx12
		scope:openid profile jucheap
	返回成功参数格式:
	{
	  "access_token": "token",
	  "expires_in": 3600,
	  "token_type": "Bearer"
	}
	返回失败时的格式：
	{
	  "error": "invalid_scope"
	}
2.如何访问需要认证的api接口
	需要认证的api地址为：http://localhost:63230/api/menu/my
	添加认证信息到Header里面,Header的key/value如下：
	Authorization:Bearer {access_token}
	ps:其中{access_token}需要替换成第一步拿到的access_token，另外注意Bearer后面有一个空格