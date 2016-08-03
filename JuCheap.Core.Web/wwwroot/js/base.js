(function () {
    //基础扩展Start
    //日期扩展
    Date.prototype.format = function (fmt) {
        var o = {
            "M+": this.getMonth() + 1, //月份   
            "d+": this.getDate(), //日   
            "H+": this.getHours(), //小时   
            "m+": this.getMinutes(), //分   
            "s+": this.getSeconds(), //秒   
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
            "S": this.getMilliseconds() //毫秒   
        };
        if (/(y+)/.test(fmt))
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o) {
            if (o.hasOwnProperty(k)) {
                if (new RegExp("(" + k + ")").test(fmt))
                    fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return fmt;
    };
    //字符串扩展
    String.prototype.getDate = function () {
        var res = /\d{13}/.exec(this);
        var date = new Date(parseInt(res));
        return date.format("yyyy-MM-dd HH:mm:ss");
    }
})();
var XPage = {
    DelData: function (url) {
        var delDatas = JucheapGrid.GetDataTableDeleteData();
        if (delDatas.Len > 0 && delDatas.Data.length > 0) {
            if (confirm("确认要删除这" + delDatas.Len + "条数据？")) {
                var btn = $("#btnDel");
                btn.button('loading');
                $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(delDatas.Data),
                    contentType: "application/json, charset=utf-8",
                    success: function (res) {
                        btn.button('reset');
                        if (res.flag) {
                            alert("删除成功");
                            $("#table_list").trigger("reloadGrid");
                        } else {
                            alert("删除失败：" + res.msg);
                        }
                    }
                });
            }
        } else {
            alert("请选择要删除的数据！");
        }
    },

    GoTo: function (btn, url) {
        $(btn).button("loading");
        window.location.href = url;
    },

    //搜索jqGrid
    Search: function (json) {
        var postData = $("#table_list").jqGrid("getGridParam", "postData");
        $.extend(postData, json);
        $("#table_list").setGridParam({ search: true }).trigger("reloadGrid", [{ page: 1 }]);
    },

    DoPost: function (btn, url, data, suc_callback, fail_callback) {
        var hasBtn = btn != null;
        if (hasBtn) {
            $(btn).button("loading");
        }
        var myRequestAjax = $.ajax({
            url: url,
            type: "POST",
            data: data,
            dataType: "JSON",
            success: function (res) {
                if (hasBtn) {
                    $(btn).button("reset");
                }
                if (res.flag) {
                    if (suc_callback == null || typeof (suc_callback) == 'undefined')
                        alert("操作成功" );
                    else
                        suc_callback.call(this, res);
                } else {
                    if (fail_callback == null || typeof (fail_callback) == 'undefined')
                        alert("操作失败：" + res.msg);
                    else
                        fail_callback.call(this, res);
                }
            },
            complete: function (XMLHttpRequest, status) { //请求完成后最终执行参数
                if (hasBtn) {
                    $(btn).button("reset");
                }
                if (status === "timeout") {//超时,status还有success,error等值的情况
                    myRequestAjax.abort();
                    alert("请求超时，请刷新页面重试");
                }
                if (status === "error") {
                    myRequestAjax.abort();
                    alert("请求失败，请刷新页面重试");
                }
            }
        });
    }
}