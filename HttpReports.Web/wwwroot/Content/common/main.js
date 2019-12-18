 

tippy('.serviceTip', {
    content: "<div class='tipbox'>服务节点是WebAPI请求的服务节点，点击选中和取消节点</div>",
    arrow: true,
    size: "large",
    inertia: true,
    placement: "right"
})  

function timeChange(k) {

    $(k).parent().find("button").each(function (i, item) {

        if ($(item).hasClass("btn-info")) {

            $(item).removeClass("btn-info");

            $(item).addClass("btn-default");
        }
    });


    if (!$(k).hasClass("btn-info")) {

        $(k).removeClass("btn-default");

        $(k).addClass("btn-info");

    }

    var tag = $(k).attr("data-id");

    $.ajax({
        url: "/Data/GetTimeRange?Tag=" + tag,
        success: function (result) {

            $(".start").val(result.data.start);

            $(".end").val(result.data.end);

            QueryClick();

        }
    });

}
