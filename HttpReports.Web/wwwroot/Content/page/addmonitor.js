 
function Save() {

    var Id = $(".id").val();

    var title = $(".title").val().trim();

    var rate = $(".rate").val().trim();

    var email = $(".email").val().trim();

    var status = $(".status").val().trim(); 

    var nodes = [];

    $(".node-row").find(".btn-info").each(function (i, item) {
        nodes.push($(item).text());
    });

    var node = nodes.join(",");

    var RtStatus = $(".RtStatus").val().trim();

    var RtTime = $(".RtTime").val().trim();

    var RtRate = $(".RtRate").val().trim();

    var HttpStatus = $(".HttpStatus").val().trim();

    var HttpCodes = $(".HttpCodes").val().trim();

    var HttpRate = $(".HttpRate").val().trim();

    var IPStatus = $(".IPStatus").val().trim();

    var IPWhiteList = $(".IPWhiteList").val().trim();

    var IPRate = $(".IPRate").val().trim();  

    $.ajax({

        url: "/Data/EditJob",
        type: "POST",
        data: {
            Id, title, rate, email, status, node, RtStatus, RtTime, RtRate, HttpStatus, HttpCodes, HttpRate, IPStatus, IPWhiteList, IPRate
        },
        success: function (result) {

            if (result.code == 1) {
                alert("保存成功");
            }
            else {
                alert(result.msg)
            } 

        } 
    });  

}
