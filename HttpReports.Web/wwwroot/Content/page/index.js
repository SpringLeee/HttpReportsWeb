

GoTop();


function InitPage() {

    laydate.render({ elem: '.start', theme: '#67c2ef' });
    laydate.render({ elem: '.end', theme: '#67c2ef' });
    laydate.render({ elem: '.day', theme: '#67c2ef' });   
  
}  

InitPage();


var global = {};

global.top = localStorage.getItem("TopCount") == null ? 10 : localStorage.getItem("TopCount");

InitChart();

getTopCount();

GetData();

QueryClick();

GetDayChart();

function getTopCount() {

    var top = localStorage.getItem("TopCount");

    if (top == null || top == "") {
        return;
    }

    $(".topCount").val(top);
}

function changeTopCount(item) {

    localStorage.setItem("TopCount", $(item).val());

    location.reload();

}

//初始化百度Echart
function InitChart() {

    // 状态码
    global.StatusCodePie = echarts.init(document.getElementById('StatusCodePie'), 'macarons');

    global.StatusCodePieOption = {
        title: {
            text: '请求状态码',
            subtext: "",
            x: "left",
            y: "2%"
        },
        legend: {
            orient: 'vertical',
            left: 'right',
            data: []
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br />{b} : {c} ({d}%)"
        },
        series: [
            {
                name: '状态码',
                type: 'pie',
                radius: '55%',
                center: ['50%', '60%'],
                data: [],
                itemStyle: {
                    emphasis: {
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                }
            }
        ]
    };

    global.StatusCodePie.setOption(global.StatusCodePieOption);

    global.StatusCodePie.showLoading("default", {
        text: '',
        color: '#FFF',
        textColor: '#FFF',
        maskColor: 'rgba(0, 0, 0, 0.01)',
        zlevel: 0
    });



    // 响应时间
    global.ResponseTimePie = echarts.init(document.getElementById('ResponseTimePie'), 'macarons');

    global.ResponseTimePieOption = {
        title: {
            text: '请求响应时间(ms)',
            x: "left",
            y: "2%"
        },
        legend: {
            orient: 'vertical',
            left: 'right',
            data: []
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br />{b} ms : {c}次 <br /> {d}%"
        },
        series: [
            {
                name: '响应时间',
                type: 'pie',
                radius: '55%',
                center: ['50%', '60%'],
                data: [],
                itemStyle: {
                    emphasis: {
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                }
            }
        ]
    };

    global.ResponseTimePie.setOption(global.ResponseTimePieOption);

    global.ResponseTimePie.showLoading("default", {
        text: '',
        color: '#FFF',
        textColor: '#FFF',
        maskColor: 'rgba(0, 0, 0, 0.01)',
        zlevel: 0
    });

    // 24小时请求次数
    global.DayStateTimesBar = echarts.init(document.getElementById('DayStateTimesBar'), 'macarons');

    global.DayStateTimesBarOption = {
        tooltip: {},
        legend: {
            data: ['请求次数']
        },
        grid: {
            left: '3%',
            right: '3%'
        },
        title: {
            text: '每小时请求次数',
            x: "left",
            y: "2%"
        },
        xAxis: {
            data: []
        },
        yAxis: {},
        series: [{
            type: 'line',
            name: "请求次数",
            data: []
        }]
    };

    global.DayStateTimesBar.setOption(global.DayStateTimesBarOption);

    global.DayStateTimesBar.showLoading("default", {
        text: '',
        color: '#FFF',
        textColor: '#FFF',
        maskColor: 'rgba(0, 0, 0, 0.01)',
        zlevel: 0
    });

    // 24小时平均响应时间 ms
    global.DayStateAvgBar = echarts.init(document.getElementById('DayStateAvgBar'), 'macarons');

    global.DayStateAvgBarOption = {
        color: ['#af91e1'],
        tooltip: {},
        legend: {
            data: ['响应时间']
        },
        grid: {
            left: '3%',
            right: '3%'
        },
        title: {
            text: '每小时平均响应时间 ms',
            x: "left",
            y: "2%"
        },
        xAxis: {
            data: []
        },
        yAxis: {},
        series: [{
            type: 'line',
            name: "响应时间",
            data: []
        }]
    };

    global.DayStateAvgBar.setOption(global.DayStateAvgBarOption);

    global.DayStateAvgBar.showLoading("default", {
        text: '',
        color: '#FFF',
        textColor: '#FFF',
        maskColor: 'rgba(0, 0, 0, 0.01)',
        zlevel: 0
    });



    global.MostRequestChart = echarts.init(document.getElementById('MostRequestChart'), 'macarons');

    global.MostRequestChartOption = {
        title: {
            text: '最多请求 TOP' + global.top,
            x: "left",
            y: "2%"
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        grid: {
            left: '3%',
            right: '3%',
            top: '15%',
            bottom: '5%',
            containLabel: true
        },
        xAxis: {
            type: 'value',
            position: "top",
            boundaryGap: [0, 0.01]
        },
        yAxis: {
            type: 'category',
            data: [],
            axisTick: { show: false },
            xisLine: { show: false },
            axisLabel: {
                textStyle: {
                    fontSize: 12,
                    align: "right",
                    fontWeight: "bolder"
                }
            },
        },
        series: [
            {
                name: '请求次数',
                type: 'bar',
                barWidth: 12,
                itemStyle: {
                    color: "#87CEFA"
                },
                data: []
            }
        ]
    };

    global.MostRequestChart.setOption(global.MostRequestChartOption);



    global.Code500RequestChart = echarts.init(document.getElementById('Code500RequestChart'), 'macarons');

    global.Code500RequestChartOption = {
        title: {
            text: '请求错误率 TOP' + global.top,
            x: "left",
            y: "2%"
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        grid: {
            left: '3%',
            right: '3%',
            top: '15%',
            bottom: '5%',
            containLabel: true
        },
        xAxis: {
            type: 'value',
            position: "top",
            boundaryGap: [0, 0.01]
        },
        yAxis: {
            type: 'category',
            data: [],
            axisTick: { show: false },
            xisLine: { show: false },

            axisLabel: {
                textStyle: {
                    fontSize: 12,
                    align: "right",
                    fontWeight: "bolder"
                }
            },
        },
        series: [
            {
                name: '请求次数',
                type: 'bar',
                barWidth: 12,
                itemStyle: {
                    color: "#F08080"
                },
                data: []
            }
        ]
    };

    global.Code500RequestChart.setOption(global.Code500RequestChartOption);




    global.FastARTChart = echarts.init(document.getElementById('FastARTChart'), 'macarons');

    global.FastARTChartOption = {
        title: {
            text: '平均响应时间最快 TOP' + global.top,
            x: "left",
            y: "2%"
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        grid: {
            left: '3%',
            right: '3%',
            top: '15%',
            bottom: '5%',
            containLabel: true
        },
        xAxis: {
            type: 'value',
            position: "top",
            boundaryGap: [0, 0.01]
        },
        yAxis: {
            type: 'category',
            data: [],
            axisTick: { show: false },
            xisLine: { show: false },

            axisLabel: {
                textStyle: {
                    fontSize: 12,
                    align: "right",
                    fontWeight: "bolder"
                }
            },
        },
        series: [
            {
                name: '平均响应时间',
                type: 'bar',
                barWidth: 12,
                itemStyle: {

                },
                data: []
            }
        ]
    };

    global.FastARTChart.setOption(global.FastARTChartOption);


    global.SlowARTChart = echarts.init(document.getElementById('SlowARTChart'), 'macarons');

    global.SlowARTChartOption = {
        title: {
            text: '平均响应时间最慢 TOP' + global.top,
            x: "left",
            y: "2%"
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        grid: {
            left: '3%',
            right: '3%',
            top: '15%',
            bottom: '5%',
            containLabel: true
        },
        xAxis: {
            type: 'value',
            position: "top",
            boundaryGap: [0, 0.01]
        },
        yAxis: {
            type: 'category',
            data: [],
            axisTick: { show: false },
            xisLine: { show: false },

            axisLabel: {
                textStyle: {
                    fontSize: 12,
                    align: "right",
                    fontWeight: "bolder"
                }
            },
        },
        series: [
            {
                name: '平均响应时间',
                type: 'bar',
                barWidth: 12,
                itemStyle: {
                    color: "#af91e1"
                },
                data: []
            }
        ]
    };

    global.SlowARTChart.setOption(global.SlowARTChartOption);




    // 最近每天请求数量
    global.LatelyDayChart = echarts.init(document.getElementById('LatelyDayChart'), 'macarons');

    global.LatelyDayChartOption = {
        tooltip: {},
        legend: {
            data: ['每天请求数量']
        },
        grid: {
            left: '3%',
            top: '20%',
            right: '3%'
        },
        title: {
            text: '每天请求数量',
            x: "left",
            y: "2%",
            subtext: ""
        },
        xAxis: {
            data: []
        },
        yAxis: {},
        series: [{
            type: 'line',
            name: "请求次数",
            data: []
        }]
    };

    global.LatelyDayChart.setOption(global.LatelyDayChartOption);
}

//Ajax获取页面数据
function GetData() {
    $.ajax({
        url: "/Data/GetNodes",
        success: function (result) {

            $(".node-row").html("");

            $.each(result.data, function (i, item) {

                $(".node-row").append(' <button onclick="check_node(this)" style="width:120px;margin-left:20px;" class="btn btn-info">' + item + '</button>');

            });
        }
    })
}

function GetDayChart() {

    var node = [];

    var day = $(".day").val();

    $(".node-row").find(".btn-info").each(function (i, item) {
        node.push($(item).text());
    });


    $.ajax({
        url: "/Data/GetDayStateBar",
        type: "POST",
        data: {
            day: day,
            node: node.join(",")
        },
        success: function (result) {

            // 24 小时请求次数
            global.DayStateTimesBar.hideLoading();
            global.DayStateTimesBarOption.xAxis.data = result.data.hours;
            global.DayStateTimesBarOption.series[0].data = result.data.timesList;
            global.DayStateTimesBar.setOption(global.DayStateTimesBarOption);


            // 24小时请求平均时长
            global.DayStateAvgBar.hideLoading();
            global.DayStateAvgBarOption.xAxis.data = result.data.hours;
            global.DayStateAvgBarOption.series[0].data = result.data.avgList;
            global.DayStateAvgBar.setOption(global.DayStateAvgBarOption);
        }
    })

}

function GetLatelyChart() {

    var node = [];

    var start = $(".start").val();
    var end = $(".end").val();

    $(".node-row").find(".btn-info").each(function (i, item) {
        node.push($(item).text());
    });

    $.ajax({
        url: "/Data/GetLatelyDayChart",
        type: "POST",
        data: {
            start: start,
            end: end,
            node: node.join(",")
        },
        success: function (result) {

            global.LatelyDayChartOption.title.subtext = result.data.range;
            global.LatelyDayChartOption.xAxis.data = result.data.time;
            global.LatelyDayChartOption.series[0].data = result.data.value;
            global.LatelyDayChart.setOption(global.LatelyDayChartOption);

        }
    })

}

function GetTOPRequestChart() {

    var start = $(".start").val();
    var end = $(".end").val();

    var node = [];

    $(".node-row").find(".btn-info").each(function (i, item) {
        node.push($(item).text());
    });


    $.ajax({
        url: "/Data/GetTopRequest",
        type: "POST",
        data: {
            start: start,
            end: end,
            node: node.join(","),
            top: global.top
        },
        success: function (result) {


            // 最多 TOP15
            global.MostRequestChartOption.yAxis.data = [];
            global.MostRequestChartOption.series[0].data = [];

            $.each(result.data.most, function (i, item) {
                global.MostRequestChartOption.yAxis.data.push(item.url + "    ");
                global.MostRequestChartOption.series[0].data.push(item.total);
            });

            global.MostRequestChartOption.yAxis.data.reverse();
            global.MostRequestChartOption.series[0].data.reverse();

            global.MostRequestChart.setOption(global.MostRequestChartOption);
        }
    });
}

function GetTopCode500Chart() {

    var start = $(".start").val();
    var end = $(".end").val();

    var node = [];

    $(".node-row").find(".btn-info").each(function (i, item) {
        node.push($(item).text());
    });


    $.ajax({
        url: "/Data/GetTopCode500",
        type: "POST",
        data: {
            start: start,
            end: end,
            node: node.join(","),
            top: global.top
        },
        success: function (result) {

            // 错误率最高 TOP15
            global.Code500RequestChartOption.yAxis.data = [];
            global.Code500RequestChartOption.series[0].data = [];

            $.each(result.data, function (i, item) {
                global.Code500RequestChartOption.yAxis.data.push(item.url + "    ");
                global.Code500RequestChartOption.series[0].data.push(item.total);
            });

            global.Code500RequestChartOption.yAxis.data.reverse();
            global.Code500RequestChartOption.series[0].data.reverse();

            global.Code500RequestChart.setOption(global.Code500RequestChartOption);
        }
    });




}


// 获取首页请求状态码图表
function GetStatusCodePie() {

    var start = $(".start").val();
    var end = $(".end").val();

    var node = [];

    $(".node-row").find(".btn-info").each(function (i, item) {
        node.push($(item).text());
    });

    global.StatusCodePie.showLoading("default", {
        text: '',
        color: '#FFF',
        textColor: '#FFF',
        maskColor: 'rgba(0, 0, 0, 0.01)',
        zlevel: 0
    });

    $.ajax({
        url: "/Data/GetStatusCodePie",
        type: "POST",
        data: {
            start: start,
            end: end,
            node: node.join(",")
        },
        success: function (result) {

            global.StatusCodePie.hideLoading();

            global.StatusCodePieOption.series[0].data = result.data;

            global.StatusCodePieOption.legend.data = [];

            $.each(result.data, function (i, item) {
                global.StatusCodePieOption.legend.data.push(item.name);
            });

            global.StatusCodePie.setOption(global.StatusCodePieOption);
        }
    })

}

// 获取首页请求平均时间图表
function GetResponseTimePie() {

    var start = $(".start").val();
    var end = $(".end").val();

    var node = [];

    $(".node-row").find(".btn-info").each(function (i, item) {
        node.push($(item).text());
    });

    global.ResponseTimePie.showLoading("default", {
        text: '',
        color: '#FFF',
        textColor: '#FFF',
        maskColor: 'rgba(0, 0, 0, 0.01)',
        zlevel: 0
    });

    $.ajax({
        url: "/Data/GetResponseTimePie",
        type: "POST",
        data: {
            start: start,
            end: end,
            node: node.join(",")
        },
        success: function (result) {

            global.ResponseTimePie.hideLoading();

            global.ResponseTimePieOption.series[0].data = result.data;

            global.ResponseTimePieOption.legend.data = [];

            $.each(result.data, function (i, item) {
                global.ResponseTimePieOption.legend.data.push(item.name);
            });

            global.ResponseTimePie.setOption(global.ResponseTimePieOption);

        }
    })
}


// 获取首页面板数据
function GetBoardData() {

    var start = $(".start").val();
    var end = $(".end").val();

    var node = [];

    $(".node-row").find(".btn-info").each(function (i, item) {
        node.push($(item).text());
    });

    $.ajax({
        url: "/Data/GetIndexData",
        type: "POST",
        data: {
            start: start,
            end: end,
            node: node.join(",")
        },
        success: function (result) {   

        
            $(".board-row").find("span").eq(0).text(result.data.total);
            $(".board-row").find("span").eq(1).text(result.data.art);
            $(".board-row").find("span").eq(2).text(result.data.code404);
            $(".board-row").find("span").eq(3).text(result.data.code500);
            $(".board-row").find("span").eq(4).text(result.data.errorPercent);
            $(".board-row").find("span").eq(5).text(result.data.apiCount); 
        }
    });
}


function GetARTChart() {

    var start = $(".start").val();
    var end = $(".end").val();

    var node = [];

    $(".node-row").find(".btn-info").each(function (i, item) {
        node.push($(item).text());
    });


    $.ajax({
        url: "/Data/GetTOPART",
        type: "POST",
        data: {
            start: start,
            end: end,
            node: node.join(","),
            top: global.top
        }, 
        success: function (result) {

            // 最快平均响应 TOP15
            global.FastARTChartOption.yAxis.data = [];
            global.FastARTChartOption.series[0].data = [];

            $.each(result.data.fast, function (i, item) {
                global.FastARTChartOption.yAxis.data.push(item.name + "    ");
                global.FastARTChartOption.series[0].data.push(item.value);
            });

            global.FastARTChartOption.yAxis.data.reverse();
            global.FastARTChartOption.series[0].data.reverse();

            global.FastARTChart.setOption(global.FastARTChartOption);


            // 最慢平均响应 TOP15
            global.SlowARTChartOption.yAxis.data = [];
            global.SlowARTChartOption.series[0].data = [];

            $.each(result.data.slow, function (i, item) {
                global.SlowARTChartOption.yAxis.data.push(item.name + "    ");
                global.SlowARTChartOption.series[0].data.push(item.value);
            });

            global.SlowARTChartOption.yAxis.data.reverse();
            global.SlowARTChartOption.series[0].data.reverse();

            global.SlowARTChart.setOption(global.SlowARTChartOption);

        }
    });




}


// 查询按钮点击
function QueryClick() { 




    ReSetTag();

    GetBoardData();
    GetStatusCodePie();
    GetResponseTimePie();


    GetARTChart();
    GetTopCode500Chart();
    GetTOPRequestChart();

    GetLatelyChart();    


    //ReSetTag();  
    ////GetStatusCodePie();
    ////GetResponseTimePie();

    //GetBoardData();  

    //GetARTChart(); 

    ////GetTopCode500Chart();
    ////GetTOPRequestChart();

    ////GetLatelyChart(); 

}

function ReSetTag() {  


    var start = $(".start").val();
    var end = $(".end").val();

    var tag = $(".board-row").find("p").find("b");

    if (start.length == 0 && end.length == 0) { 
        tag.text('今天');
    }
    else {
        tag.text((start.length > 0 ? start.substr(0, 10) : "null") + " - " + (end.length > 0 ? end.substr(0, 10) : "null"));
    } 

    var tagId = $(".timeSelect").find(".btn-info").attr("data-id");   

    var tagValue = tagId == undefined ? 0 : tagId;   

    $.ajax({
        url: "/Data/GetTimeTag",
        type: "POST",
        data: {
            start: start,
            end: end,
            tagValue: tagValue
        },
        success: function (result) {  

            if (result.data == -1) {
                return;
            }   

            $(".timeSelect").find("button").each(function (i, item) { 

                var tag = $(item).attr("data-id"); 

                if (tag == result.data) {

                    if ($(item).hasClass("btn-default")) {  

                        $(item).removeClass("btn-default");
                        $(item).addClass("btn-info");
                    }   
                }
                else {

                    if ($(item).hasClass("btn-info")) {

                        $(item).removeClass("btn-info");
                        $(item).addClass("btn-default");
                    }  
                }  

            }); 

        }     
    });  
}


//选择服务节点
function check_node(item) {

    if ($(item).hasClass("btn-info")) {
        $(item).removeClass("btn-info");
        $(item).addClass("btn-default");
    }
    else {
        $(item).removeClass("btn-default");
        $(item).addClass("btn-info");
    }
}

//全选
function select_all(item) {

    $(item).parent().next().find("button").each(function (i, k) {

        if ($(k).hasClass("btn-default")) {
            $(k).removeClass("btn-default");
            $(k).addClass("btn-info");
        }
    });
}

//反选
function select_reverse(item) {

    $(item).parent().next().find("button").each(function (i, k) {

        if ($(k).hasClass("btn-info")) {
            $(k).removeClass("btn-info");
            $(k).addClass("btn-default");
        }
        else {
            $(k).removeClass("btn-default");
            $(k).addClass("btn-info");
        }

    });
}  




 