function tabletr(c) {
    $("tr").hover(function() {
         $(this).addClass(c);
    }, function() {
         $(this).removeClass(c);
    });
}

function CpsUrl(obj) {
    if (obj.indexOf("pc") >= 0) {
        $(".urlpc textarea").attr("class", "qm_textareaon");
        $(".urlwap textarea").attr("class", "qm_textarea");
    } else if (obj.indexOf("wap") >= 0) {
        $(".urlwap textarea").attr("class", "qm_textareaon");
        $(".urlpc textarea").attr("class", "qm_textarea");
    } else {
        $(".urlwap textarea").attr("class", "qm_textarea");
        $(".urlpc textarea").attr("class", "qm_textarea");
    }
}

function pmurl(id, b, w, h) {
    var str = unescape($("#pm" + id).html());
    var sbid_o = "bid=" + str.split("&amp;bid=")[1].split("&")[0] + "&";
    var sbid_n = "bid=" + b + "&";
    var swid_o = 'width="' + str.split('width="')[1].split('"')[0] + '"';
    var swid_n = 'width="' + w + '"';
    var shei_o = 'height="' + str.split('height="')[1].split('"')[0] + '"';
    var shei_n = 'height="' + h + '"';
    var str_n = str.replace(sbid_o, sbid_n).replace(swid_o, swid_n).replace(shei_o, shei_n);
    var href_n = str_n.split('src="')[1].split('"')[0].replace(/&amp;/g, '&');
    $("#pm" + id).html(str_n);
    $("#pm" + id).select();
    $("#a" + id).attr("href", href_n);

    //<iframe src="http://cps.gome.com.cn/home/UnionAd?sid=3236&wid=&category=48&bid=304&feedback=" width="468" height="60" scrolling="no" border="0" marginwidth="0" style="border: none;" frameborder="0"></iframe>
}

function QM_Press(obj, on, ot) {
    $(obj).click(function() {
            $(this).removeClass();
            $(this).siblings().removeClass();
            $(this).addClass(on);
            $(this).siblings().addClass(ot);
        }
    );
}
//function QM_Press(obj,val,a, on, ot) {
//    //$(obj).click(function () {
//    obj +=' '+ val + ' ' + a;
//    alert(obj);
//        $(this).removeClass();
//        $(this).siblings().removeClass();
//        $(this).addClass(on);
//        $(this).siblings().addClass(ot);
//    //}
//	//)
//}