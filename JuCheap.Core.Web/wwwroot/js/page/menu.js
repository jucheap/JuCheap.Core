(function() {
    $("#ParentName").bsSuggest({
        allowNoKeyword: true,
        multiWord: true,
        showHeader: true,
        effectiveFieldsAlias: { Id: "主键", Name: "名称", TypeName: "类型", Url: "URL地址" },
        effectiveFields: ["Id", "Name","TypeName", "Url"],
        getDataMethod: "url",
        url: "/Menu/GetListWithKeywords?keywords=",
        idField: "Id",
        keyField: "Name"
    }).on('onSetSelectValue', function(e, data) {
        $("#ParentId").val(data.id);
    }).on('onUnsetSelectValue', function() {
        $("#ParentId").val("");
    });
})();