var barChart = echarts.init(document.getElementById('barChart'));
var lineChart = echarts.init(document.getElementById('lineChart'));
var pieChart = echarts.init(document.getElementById('pieChart'));

var loadPageData = function () {
    // 基于准备好的dom，初始化echarts实例
    barChart.showLoading();
    lineChart.showLoading();
    pieChart.showLoading();

    $.get("/Log/ChartsDatas", function (response) {
        // 指定图表的配置项和数据
        var barOption = {
            color: ['#19aa8d'],
            tooltip: {
                trigger: 'axis',
                axisPointer: { // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow' // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            xAxis: [
                {
                    type: 'category',
                    name: '日期',
                    data: response.categoryDatas,
                    axisTick: {
                        alignWithLabel: true
                    }
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    name: '访问量'
                }
            ],
            series: [
                {
                    name: '访问量',
                    type: 'bar',
                    barWidth: '60%',
                    data: response.datas
                }
            ]
        };

        var lineOption = {
            title: {
                text: '访问量',
                left: 'center'
            },
            tooltip: {
                trigger: 'item',
                formatter: '{b} {a} : {c}'
            },
            xAxis: {
                type: 'category',
                name: '日期',
                splitLine: { show: false },
                data: response.categoryDatas
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            yAxis: {
                type: 'log',
                name: '访问量'
            },
            series: [
                {
                    name: '日访问量',
                    type: 'line',
                    data: response.datas
                }
            ]
        };

        var pieOption = {
            tooltip: {
                trigger: 'item',
                formatter: "{b} {a} : {c}<br/>占比：{d}%"
            },
            series: [
                {
                    name: '访问量',
                    type: 'pie',
                    radius: ['50%', '70%'],
                    avoidLabelOverlap: false,
                    label: {
                        normal: {
                            show: false,
                            position: 'center'
                        },
                        emphasis: {
                            show: true,
                            textStyle: {
                                fontSize: '30',
                                fontWeight: 'bold'
                            }
                        }
                    },
                    labelLine: {
                        normal: {
                            show: false
                        }
                    },
                    data: response.pieDatas
                }
            ]
        };

        barChart.hideLoading();
        lineChart.hideLoading();
        pieChart.hideLoading();
        // 使用刚指定的配置项和数据显示图表。
        barChart.setOption(barOption, true);
        lineChart.setOption(lineOption, true);
        pieChart.setOption(pieOption, true);
    });
}

var resizeCharts = function () {
    barChart.resize();
    lineChart.resize();
    pieChart.resize();
}

window.onload= function() {
    loadPageData();
    window.onresize = resizeCharts;
}