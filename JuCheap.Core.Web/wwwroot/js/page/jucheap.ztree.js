var JuCheapTree = function (config) {
    this.id = config.id;
    this.title = config.title;//弹框的标题
    this.height = config.height;//弹框的高度
    this.setting = {
        view: {
            selectedMulti: false
        },
        async: {
            enable: true,
            url: config.loadUrl,
            autoParam: ["id", "name=n", "level=lv"]
        },
        callback: {
            beforeClick: function(treeId, treeNode) {
                $("#txtParentId").val(treeNode.id);
            }
        }
    };

    this.addPage = function (e) {
        parent.layer.open({
            title: '添加' + e.data.scope.title,
            type: 2,
            content: config.addUrl + '/' + $("#txtParentId").val(),
            area: ['800px', e.data.scope.height],
            end: function () {
                var zTree = $.fn.zTree.getZTreeObj(e.data.scope.id),
                    type = "refresh",
                    nodes = zTree.getSelectedNodes();
                if (nodes.length === 0) {
                    zTree.reAsyncChildNodes(null, type);
                    return;
                }
                var node = nodes[0];
                node.isParent = true;
                zTree.updateNode(node, false);
                zTree.reAsyncChildNodes(node, type, false);
            }
        });
    }

    this.editPage = function(e) {
        var id = $("#txtParentId").val();
        if (id === "") {
            parent.layer.msg("请选择要编辑的节点");
            return;
        }
        parent.layer.open({
            title: '编辑' + e.data.scope.title,
            type: 2,
            content: config.editUrl + '/' + id,
            area: ['800px', e.data.scope.height],
            end: function () {
                var zTree = $.fn.zTree.getZTreeObj(e.data.scope.id),
                    type = "refresh",
                    nodes = zTree.getSelectedNodes();
                if (nodes.length === 0) {
                    return;
                }
                var node = nodes[0];
                zTree.reAsyncChildNodes(node.getParentNode(), type, false);
            }
        });
    }

    this.deletePage = function(e) {
        var id = $("#txtParentId").val();
        if (id === "") {
            parent.layer.msg("请选择要删除的数据");
            return;
        }
        var scope = e.data.scope;
        var onDeleted = scope.onDeleted;
        parent.layer.confirm('确定删除吗?',
            { icon: 3, title: '提示' },
            function(index) {
                //loading
                parent.layer.load(2, { shade: 0.3 });
                $.post(config.deleteUrl,
                    { id: id },
                    function(res) {
                        parent.layer.close(index);
                        parent.layer.closeAll();
                        if (res && res.flag) {
                            parent.layer.alert("删除成功");
                            onDeleted.call(scope, null);
                        } else {
                            parent.layer.alert("删除失败:" + res.msg);
                        }
                    });
            });
    }
    //删除本地的节点
    this.onDeleted = function () {
        var zTree = $.fn.zTree.getZTreeObj(this.id),
            nodes = zTree.getSelectedNodes();
        if (nodes.length === 0) {
            return;
        }
        zTree.removeNode(nodes[0], null);
    }
};

JuCheapTree.prototype.load = function () {
    $.fn.zTree.init($("#" + this.id), this.setting);
    var data = {
        scope: this
    };
    $("#btnAdd").unbind("click").bind("click", data, this.addPage);
    $("#btnEdit").unbind("click").bind("click", data, this.editPage);
    $("#btnDel").unbind("click").bind("click", data, this.deletePage);
}