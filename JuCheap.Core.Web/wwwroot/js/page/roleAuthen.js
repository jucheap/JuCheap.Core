﻿var roleSetting = {
    async: {
        enable: true,
        url: "/Role/AuthenRoleDatas",
        autoParam: ["id", "name=n", "level=lv"],
        otherParam: { "otherParam": "tree" }
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    check: {
        enable: true,
        chkStyle: "radio",
        radioType: "level"
    },
    callback: {
        onCheck: onCheck
    }
};
var menuSetting = {
    async: {
        enable: true,
        url: "/Role/AuthenMenuDatas",
        autoParam: ["id", "name=n", "level=lv"],
        otherParam: { "otherParam": "tree" }
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    check: {
        enable: true
    }
};

function onCheck(e, treeId, treeNode) {
    var zTree = $.fn.zTree.getZTreeObj("menuTree");
    zTree.checkAllNodes(false);
    if (treeNode.checked) {
        $.ajax({
            url: "/Role/AuthenRoleMenus/" + treeNode.id,
            type: "get",
            dataType: "json",
            success: function (res) {
                for (var i = 0, id; id = res[i]; i++) {
                    var node = zTree.getNodeByParam("id", id);
                    zTree.checkNode(node, true, true, false);
                }
            }
        });
    }
}

function saveData() {
    var roleTree = $.fn.zTree.getZTreeObj("roleTree");
    var roles = roleTree.getCheckedNodes(true);
    if (roles != null && roles.length > 0) {
        var menuTree = $.fn.zTree.getZTreeObj("menuTree");
        var menus = menuTree.getCheckedNodes(true);
        if (menus == null || menus.length === 0) {
            alert("请选择要授权的菜单");
        } else {
            var datas = [];
            var roleId = roles[0].id;
            for (var i = 0, menu; menu = menus[i]; i++) {
                datas.push({ RoleId: roleId, MenuId: menu.id });
            }
            var postData = JSON.stringify(datas);
            $.ajax({
                url: "/Role/SetRoleMenus",
                type: "POST",
                dataType: "json",
                data: postData,
                contentType: "application/json, charset=utf-8",
                success: function (res) {
                    if (res.flag) {
                        alert("授权成功");
                    } else {
                        alert("授权失败：" + res.msg);
                    }
                }
            });
        }
    }
}

$(document).ready(function () {
    $.fn.zTree.init($("#roleTree"), roleSetting);
    $.fn.zTree.init($("#menuTree"), menuSetting);
    $("#btnSave").click(saveData);
});