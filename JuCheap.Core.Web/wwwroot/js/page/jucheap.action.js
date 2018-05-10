(function () {
	//返回按钮
    $("#btnBack")
        .bind("click",
            function (e) {
                var type = $(this).data("type");
				if (type === "window") {//表示关闭弹框
					parent.layer.closeAll();
				}else if (type === "url") {//表示url跳转
				    window.history.go(-1);
				}
            });
    $("#btnSave")
        .bind("click",
            function() {
                if ($("form:first").valid()) {
                    $(this).button("loading");
                }
            });
})();