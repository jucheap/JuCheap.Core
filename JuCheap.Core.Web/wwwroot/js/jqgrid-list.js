var JucheapGrid = {
    Load: function (config) {
        var jqGrid = !config.id ? $("#table_list") : $("#" + config.id);
        var defaultConfig = {
            caption: config.title,
            url: config.url,
            mtype: "GET",
            styleUI: 'Bootstrap',
            datatype: "json",
            colNames: config.colNames,
            colModel: config.colModel,
            viewrecords: true,
            multiselect: true,
            rownumbers: true,
            autowidth: true,
            height: "100%",
            rowNum: 15,
            rownumWidth: 35,
            emptyrecords: "没有相关数据",
            loadComplete: function (xhr) {
                if (xhr && xhr.code === 401) {
                    alert(xhr.msg);
                }
            },
            loadError: function (xhr, status, error) {
                console.log(xhr);
                console.log(status);
                console.log(error);
            },
            pager: !config.pagerId ? "#pager_list" : "#" + config.pagerId,
            subGrid: config.subGrid ? true : false,
            subGridRowExpanded: config.subGridRowExpanded ? config.subGridRowExpanded : null,
            gridComplete: config.gridComplete ? config.gridComplete : function () { }
        };
        $.extend(defaultConfig, config);
        jqGrid.jqGrid(defaultConfig);

        // Add responsive to jqGrid
        $(window).bind('resize', function () {
            var width = $('.jqGrid_wrapper').width();
            jqGrid.setGridWidth(width);
        });
    },
    //获取jqgrid的编辑内容
    GetData: function () {
        var id = $('#table_list').jqGrid('getGridParam', 'selrow');
        if (id)
            return $('#table_list').jqGrid("getRowData", id);
        else
            return null;
    },
    //获取jqgrid要删除的内容
    GetDataTableDeleteData: function () {
        return JucheapGrid.GetAllSelected("table_list");
    },
    //获取所有选择项
    GetAllSelected: function (id) {
        var res = {
            Len: 0,
            Data: []
        };
        var grid = $("#" + id);
        var rowKey = grid.getGridParam("selrow");

        if (rowKey) {
            var selectedIDs = grid.getGridParam("selarrrow");
            for (var i = 0; i < selectedIDs.length; i++) {
                res.Data.push(selectedIDs[i]);
            }
        }
        res.Len = res.Data.length;
        return res;
    }
};

