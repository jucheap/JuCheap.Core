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
})();