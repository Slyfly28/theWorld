/*site.js*/
(function () {

//var ele = $("#username");
//ele.text("Ivan Salamat");

//var main = $("#main");
//main.on("mouseenter", function () {
//    main.style = "background-color: #888;";
//});

//main.on("mouseleave", function() {
//    main.style = "";
//});

//var menuItems = $("ul.menu li a");
//menuItems.on("click", function () {
//    var me = $(this);
//        alert(me.text());
//    });

    var $sidebarAndWrapper = $("#sidebar,#wrapper");

    $("#showsidebar").on("click", function() {
        $sidebarAndWrapper.toggleClass("hidesidebar");
        if ($sidebarAndWrapper.hasClass("hidesidebar")) {
            $(this).text("Show Menu");
        } else {
            $(this).text("Hide Menu");
        }
    });
    

})();
