/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

var webroot = "./wwwroot/";

var paths = {
    layoutJs: [
        webroot + "js/jquery.min.js",
        webroot + "js/bootstrap.min.js",
        webroot + "js/plugins/metisMenu/jquery.metisMenu.js",
        webroot + "js/plugins/slimscroll/jquery.slimscroll.min.js",
        webroot + "js/jucheap.js",
        webroot + "js/contabs.js",
        "!" + webroot + "js/site.min.js"
    ],
    layoutCss:[
        webroot + "css/bootstrap.css",
        webroot + "css/style.css",
        webroot + "css/font-awesome.css",
        "!" + webroot + "css/**/*.min.css"
    ],
    contentCss: [
        webroot + "css/bootstrap.css",
        webroot + "css/style.css",
        webroot + "css/font-awesome.css",
        webroot + "css/animate.css",
        "!" + webroot + "css/**/*.min.css"
    ],
    concatJsDest: webroot + "js/site.min.js",
    concatCssDest: webroot + "css/site.min.css",
    concatContentCssDest: webroot + "css/content.min.css"
};

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src(paths.layoutJs, { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src(paths.layoutCss)
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src(paths.contentCss)
        .pipe(concat(paths.concatContentCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);
