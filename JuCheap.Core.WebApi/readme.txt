1.如何获取token
	token获取地址：http://localhost:63230/connect/token
	方式：post
	body参数：
		client_id:api.client
		client_secret:28365BC74137474DA6986B86836B4468
		grant_type:password
		username:jucheap
		password:qwaszx12
		scope:jucheap
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