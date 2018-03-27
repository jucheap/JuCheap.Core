(function() {
    $("#ParentName").bsSuggest({
        allowNoKeyword: true,
        multiWord: true,
        showHeader: true,
        effectiveFieldsAlias: { id: "主键", name: "名称", typeName: "类型", url: "URL地址" },
        effectiveFields: ["id", "name","typeName", "url"],
        getDataMethod: "url",
        url: "/Menu/GetListWithKeywords?keywords=",
        idField: "id",
        keyField: "name"
    }).on('onSetSelectValue', function(e, data) {
        $("#ParentId").val(data.id);
    }).on('onUnsetSelectValue', function() {
        $("#ParentId").val("");
    });

    $("#Icon").focus(function() {
        parent.layer.open({
            title: '选择Icon图标',
            type: 2,
            content: "/Menu/FontAwesome",
            area: ['90%', '80%'],
            btn: ['确定', '关闭'],
            btnclass: ['btn btn-primary', 'btn btn-danger'],
            yes: function(index, layero) {
                var icon = $(layero).find("iframe")[0].contentWindow.getData();
                $("#Icon").val(icon);
                parent.layer.close(index);
            },
            cancel: function() {
                return true;
            }
        });
    });
})();