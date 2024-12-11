/*
Template Name: Admin Template
Author: Wrappixel

File: js
*/
// ==============================================================
// Auto select left navbar
// ==============================================================



document.addEventListener("DOMContentLoaded", function () {
    "use strict";

    var url = window.location.href;
    var path = url.replace(window.location.protocol + "//" + window.location.host + "/", "");

    var sidebarLinks = document.querySelectorAll("ul#sidebarnav a");

    // Find and activate the matching link
    sidebarLinks.forEach(function (link) {
        if (link.href === url || link.href.endsWith(path)) {
            link.classList.add("active");
        }
    });

    // Add click event listener to each sidebar link
    sidebarLinks.forEach(function (link) {
        link.addEventListener("click", function (e) {
            e.preventDefault(); // Prevent default link behavior

            if (!this.classList.contains("active")) {
                // Remove active class from all links in the parent list
                sidebarLinks.forEach(function (item) {
                    item.classList.remove("active");
                });

                // Add active class to the clicked link
                this.classList.add("active");
            } else {
                // If the clicked link is already active, remove the active class
                this.classList.remove("active");
            }
        });
    });
});




/*
$(function () {
    "use strict";
    var url = window.location + "";
    var path = url.replace(
      window.location.protocol + "//" + window.location.host + "/",
      ""
    );
    var element = $("ul#sidebarnav a").filter(function () {
      return this.href === url || this.href === path; // || url.href.indexOf(this.href) === 0;
    });

  
    element.addClass("active");
    $("#sidebarnav a").on("click", function (e) {
      if (!$(this).hasClass("active")) {
        // hide any open menus and remove all other classes
        $("a", $(this).parents("ul:first")).removeClass("active");
  
        // open our new menu and add the open class
        $(this).addClass("active");
      } else if ($(this).hasClass("active")) {
        $(this).removeClass("active");
      }
    });
  });*/