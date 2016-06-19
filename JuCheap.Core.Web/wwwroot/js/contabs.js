$(function () {
    function getWidth(l) {
        var k = 0;
        $(l).each(function () {
            k += $(this).outerWidth(true);
        });
        return k;
    }

    function activeTab(n) {
        var o = getWidth($(n).prevAll()),
            q = getWidth($(n).nextAll());
        var l = getWidth($(".content-tabs").children().not(".J_menuTabs"));
        var k = $(".content-tabs").outerWidth(true) - l;
        var widthNum = 0;
        if ($(".page-tabs-content").outerWidth() < k) {
            widthNum = 0;
        } else {
            if (q <= (k - $(n).outerWidth(true) - $(n).next().outerWidth(true))) {
                if ((k - $(n).next().outerWidth(true)) > q) {
                    widthNum = o;
                    var m = n;
                    while ((widthNum - $(m).outerWidth()) > ($(".page-tabs-content").outerWidth() - k)) {
                        widthNum -= $(m).prev().outerWidth();
                        m = $(m).prev();
                    }
                }
            } else {
                if (o > (k - $(n).outerWidth(true) - $(n).prev().outerWidth(true))) {
                    widthNum = o - $(n).prev().outerWidth(true);
                }
            }
        }
        $(".page-tabs-content").animate({
            marginLeft: 0 - widthNum + "px"
        }, "fast");
    }

    function moveLeft() {
        var o = Math.abs(parseInt($(".page-tabs-content").css("margin-left")));
        var l = getWidth($(".content-tabs").children().not(".J_menuTabs"));
        var k = $(".content-tabs").outerWidth(true) - l;
        var p = 0;
        if ($(".page-tabs-content").width() < k) {
            return false;
        } else {
            var m = $(".J_menuTab:first");
            var n = 0;
            while ((n + $(m).outerWidth(true)) <= o) {
                n += $(m).outerWidth(true);
                m = $(m).next();
            }
            n = 0;
            if (getWidth($(m).prevAll()) > k) {
                while ((n + $(m).outerWidth(true)) < (k) && m.length > 0) {
                    n += $(m).outerWidth(true);
                    m = $(m).prev();
                }
                p = getWidth($(m).prevAll());
            }
        }
        $(".page-tabs-content").animate({
            marginLeft: 0 - p + "px"
        }, "fast");
        return true;
    }

    function moveRight() {
        var o = Math.abs(parseInt($(".page-tabs-content").css("margin-left")));
        var l = getWidth($(".content-tabs").children().not(".J_menuTabs"));
        var k = $(".content-tabs").outerWidth(true) - l;
        var p;
        if ($(".page-tabs-content").width() < k) {
            return false;
        } else {
            var m = $(".J_menuTab:first");
            var n = 0;
            while ((n + $(m).outerWidth(true)) <= o) {
                n += $(m).outerWidth(true);
                m = $(m).next();
            }
            n = 0;
            while ((n + $(m).outerWidth(true)) < (k) && m.length > 0) {
                n += $(m).outerWidth(true);
                m = $(m).next();
            }
            p = getWidth($(m).prevAll());
            if (p > 0) {
                $(".page-tabs-content").animate({
                    marginLeft: 0 - p + "px"
                }, "fast");
            }
        }
        return true;
    }
    $(".J_menuItem").each(function (k) {
        if (!$(this).attr("data-index")) {
            $(this).attr("data-index", k);
        }
    });

    function menuClick() {
        var o = $(this).attr("href"),
            m = $(this).data("index"),
            l = $.trim($(this).text()),
            k = true;
        if (o == undefined || $.trim(o).length === 0) {
            return false;
        }
        $(".J_menuTab").each(function () {
            if ($(this).data("id") === o) {
                if (!$(this).hasClass("active")) {
                    $(this).addClass("active").siblings(".J_menuTab").removeClass("active");
                    activeTab(this);
                    $(".J_mainContent .J_iframe").each(function() {
                        if ($(this).data("id") === o) {
                            $(this).show().siblings(".J_iframe").hide();
                            return false;
                        }
                        return true;
                    });
                }
                k = false;
                return false;
            }
            return true;
        });
        if (k) {
            var p = '<a href="javascript:;" class="active J_menuTab" data-id="' + o + '">' + l + ' <i class="fa fa-times-circle"></i></a>';
            $(".J_menuTab").removeClass("active");
            var n = '<iframe class="J_iframe" name="iframe' + m + '" width="100%" height="100%" src="' + o + '" frameborder="0" data-id="' + o + '" seamless></iframe>';
            $(".J_mainContent").find("iframe.J_iframe").hide().parents(".J_mainContent").append(n);
            $(".J_menuTabs .page-tabs-content").append(p);
            activeTab($(".J_menuTab.active"));
        }
        return false;
    }
    $(".J_menuItem").on("click", menuClick);

    function closeTab() {
        var m = $(this).parents(".J_menuTab").data("id");
        var l = $(this).parents(".J_menuTab").width();
        if ($(this).parents(".J_menuTab").hasClass("active")) {
            if ($(this).parents(".J_menuTab").next(".J_menuTab").size()) {
                var dataId = $(this).parents(".J_menuTab").next(".J_menuTab:eq(0)").data("id");
                $(this).parents(".J_menuTab").next(".J_menuTab:eq(0)").addClass("active");
                $(".J_mainContent .J_iframe").each(function () {
                    if ($(this).data("id") === dataId) {
                        $(this).show().siblings(".J_iframe").hide();
                        return false;
                    }
                    return true;
                });
                var n = parseInt($(".page-tabs-content").css("margin-left"));
                if (n < 0) {
                    $(".page-tabs-content").animate({
                        marginLeft: (n + l) + "px"
                    }, "fast");
                }
                $(this).parents(".J_menuTab").remove();
                $(".J_mainContent .J_iframe").each(function() {
                    if ($(this).data("id") === m) {
                        $(this).remove();
                        return false;
                    }
                    return true;
                });
            }
            if ($(this).parents(".J_menuTab").prev(".J_menuTab").size()) {
                var k = $(this).parents(".J_menuTab").prev(".J_menuTab:last").data("id");
                $(this).parents(".J_menuTab").prev(".J_menuTab:last").addClass("active");
                $(".J_mainContent .J_iframe").each(function () {
                    if ($(this).data("id") === k) {
                        $(this).show().siblings(".J_iframe").hide();
                        return false;
                    }
                    return true;
                });
                $(this).parents(".J_menuTab").remove();
                $(".J_mainContent .J_iframe").each(function() {
                    if ($(this).data("id") === m) {
                        $(this).remove();
                        return false;
                    }
                    return true;
                });
            }
        } else {
            $(this).parents(".J_menuTab").remove();
            $(".J_mainContent .J_iframe").each(function () {
                if ($(this).data("id") === m) {
                    $(this).remove();
                    return false;
                }
                return true;
            });
            activeTab($(".J_menuTab.active"));
        }
        return false;
    }
    $(".J_menuTabs").on("click", ".J_menuTab i", closeTab);

    function closeOtherTab() {
        $(".page-tabs-content").children("[data-id]").not(":first").not(".active").each(function () {
            $('.J_iframe[data-id="' + $(this).data("id") + '"]').remove();
            $(this).remove();
        });
        $(".page-tabs-content").css("margin-left", "0");
    }
    $(".J_tabCloseOther").on("click", closeOtherTab);

    function gotoCurrentTab() {
        activeTab($(".J_menuTab.active"));
    }
    $(".J_tabShowActive").on("click", gotoCurrentTab);

    function tabClick() {
        if (!$(this).hasClass("active")) {
            var k = $(this).data("id");
            $(".J_mainContent .J_iframe").each(function () {
                if ($(this).data("id") === k) {
                    $(this).show().siblings(".J_iframe").hide();
                    return false;
                }
                return true;
            });
            $(this).addClass("active").siblings(".J_menuTab").removeClass("active");
            activeTab(this);
        }
    }
    $(".J_menuTabs").on("click", ".J_menuTab", tabClick);

    function refreshPage() {
        var frame = $('.J_iframe[data-id="' + $(this).data("id") + '"]');
        var url = frame.attr("src");
        frame.attr("src", url);
    }
    $(".J_menuTabs").on("dblclick", ".J_menuTab", refreshPage);
    $(".J_tabLeft").on("click", moveLeft);
    $(".J_tabRight").on("click", moveRight);
    $(".J_tabCloseAll").on("click", function() {
        $(".page-tabs-content").children("[data-id]").not(":first").each(function() {
            $('.J_iframe[data-id="' + $(this).data("id") + '"]').remove();
            $(this).remove();
        });
        $(".page-tabs-content").children("[data-id]:first").each(function() {
            $('.J_iframe[data-id="' + $(this).data("id") + '"]').show();
            $(this).addClass("active");
        });
        $(".page-tabs-content").css("margin-left", "0");
    });
});