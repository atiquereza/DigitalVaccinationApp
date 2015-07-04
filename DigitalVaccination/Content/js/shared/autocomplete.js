function GetAutoSuggestion(geturl, postdata) {
    $(document).click(function () {
        $("#ajax_response").fadeOut('slow');
    });
    $("#keyword").focus();
    var offset = $("#keyword").offset();
    var width = $("#keyword").width() - 2;
    
    
    $("#ajax_response").css("left", offset.left);
    $("#ajax_response").css("top", offset.top);
    $("#ajax_response").css("width", width);




    $("#keyword").keyup(function (event) {
        var xhr;
        console.log($("#keyword").val());
        var keyword = $("#keyword").val();
        setTimeout(function () {
            if (keyword.length > 3) {
                //alert(keyword);
                //alert(geturl);
                if (event.keyCode != 40 && event.keyCode != 38 && event.keyCode != 13) {
                    xhr = $.ajax({
                        type: "POST",
                        url: geturl,
                        data: postdata + keyword,
                        success: function (msg) {
                            //alert(msg);
                            if (msg != 0) {
                                $("#ajax_response").fadeIn("slow").html(msg);
                                $("#ajax_response").css("border", "1px solid lightgray");
                                $("#ajax_response").css("background", "#FFFFFF");
                                $("#ajax_response").css("position", "absolute");
                                $("#ajax_response").css("padding", "2px 0px");
                                $("#ajax_response").css("top", "auto");
                                $(".list").css("padding", "0px 0px");
                                $(".list").css("margin", "0px");
                                $(".list").css("list-style", "none");
                                $(".list li a").css("text-align", "left");
                                $(".list li a").css("padding", "2px");
                                $(".list li a").css("cursor", "pointer");
                                $(".list li a").css("display", "block");
                                $(".list li a").css("text-decoration", "none");
                                $(".list li a").css("color", "#000000");
                                $(".selected").css("background", "#CCCFF2");
                            }
                            else {
                                $("#ajax_response").fadeIn("slow");
                                $("#ajax_response").html('<div style="text-align:left;">No Matches Found</div>');
                            }
                        }
                    });


                }
                else {
                    switch (event.keyCode) {
                        case 40:
                            {
                                found = 0;
                                $("li").each(function () {
                                    if ($(this).attr("class") == "selected")
                                        found = 1;
                                });
                                if (found == 1) {
                                    var sel = $("li[class='selected']");
                                    sel.next().addClass("selected");
                                    sel.removeClass("selected");
                                }
                                else
                                    $("li:first").addClass("selected");
                            }
                            break;
                        case 38:
                            {
                                found = 0;
                                $("li").each(function () {
                                    if ($(this).attr("class") == "selected")
                                        found = 1;
                                });
                                if (found == 1) {
                                    var sel = $("li[class='selected']");
                                    sel.prev().addClass("selected");
                                    sel.removeClass("selected");
                                }
                                else
                                    $("li:last").addClass("selected");
                            }
                            break;
                        case 13:
                            $("#ajax_response").fadeOut("slow");
                            $("#keyword").val($("li[class='selected'] a").text());
                            break;
                    }
                }
            }
            else
                $("#ajax_response").fadeOut("slow");


        }, 0000);
        xhr.abort();



    });

    $("#ajax_response").mouseover(function () {
        $(this).find("li a:first-child").click(function () {
            $("#keyword").val($(this).text());
            $("#ajax_response").fadeOut("slow");
        });
    });

}