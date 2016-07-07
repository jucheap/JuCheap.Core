/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

var webroot = "./wwwroot/";

var paths = {
    baseJs: [
        webroot + "js/base.js",
        "!" + webroot + "js/base.min.js"
    ],
    layoutJs: [
        webroot + "js/jquery.js",
        webroot + "js/bootstrap.js",
        webroot + "js/plugins/metisMenu/jquery.metisMenu.js",
        webroot + "js/plugins/slimscroll/jquery.slimscroll.js",
        webroot + "js/jucheap.js",
        webroot + "js/contabs.js",
        "!" + webroot + "js/site.min.js"
    ],
    contentJs: [
        webroot + "js/jquery.js",
        webroot + "js/bootstrap.js",
        "!" + webroot + "js/content.min.js"
    ],
    validateJs:[
        webroot + "js/jquery.validate.js",
        webroot + "js/jquery.validate.unobtrusive.js",
        "!" + webroot + "js/validate.min.js"
    ],
    gridJs: [
        webroot + "js/plugins/jqgrid/jquery.jqGrid.js",
        webroot + "js/plugins/jqgrid/i18n/grid.locale-cn.js",
        webroot + "js/jqgrid-list.js",
        webroot + "js/base.js",
        "!" + webroot + "js/grid.min.js"
    ],
    menuJs: [
        webroot + "js/jquery.validate.js",
        webroot + "js/jquery.validate.unobtrusive.js",
        webroot + "js/plugins/suggest/bootstrap-suggest.js",
        webroot + "js/page/menu.js",
        webroot + "js/base.js",
        "!" + webroot + "js/menu.min.js"
    ],
    ztreeJs: [
        webroot + "js/plugins/ztree/jquery.ztree.all.js",
        webroot + "js/page/roleAuthen.js",
        webroot + "js/json2.js",
        "!" + webroot + "js/ztree.min.js"
    ],

    layoutCss:[
        webroot + "css/bootstrap.css",
        webroot + "css/style.css",
        webroot + "css/font-awesome.css",
        "!" + webroot + "css/site.min.css"
    ],
    contentCss: [
        webroot + "css/bootstrap.css",
        webroot + "css/style.css",
        webroot + "css/font-awesome.css",
        webroot + "css/animate.css",
        "!" + webroot + "css/content.min.css"
    ],
    loginCss: [
        webroot + "css/bootstrap.css",
        webroot + "css/font-awesome.css",
        webroot + "css/login-theme.css",
        webroot + "css/login.css",
        "!" + webroot + "css/login.min.css"
    ],
    gridCss:[
        webroot + "css/plugins/jqgrid/ui.jqgrid.css",
        "!" + webroot + "css/grid.min.css"
    ],
    ztreeCss: [
        webroot + "css/plugins/ztree/metroStyle.css",
        "!" + webroot + "css/ztree.min.css"
    ],
    concatBaseJsDest: webroot + "js/base.min.js",
    concatJsDest: webroot + "js/site.min.js",
    concatContentJsDest: webroot + "js/content.min.js",
    concatValidateJsDest: webroot + "js/validate.min.js",
    concatGridJsDest: webroot + "js/grid.min.js",
    concatMenuJsDest: webroot + "js/menu.min.js",
    concatZTreeJsDest: webroot + "js/ztree.min.js",
    concatCssDest: webroot + "css/site.min.css",
    concatContentCssDest: webroot + "css/content.min.css",
    concatLoginCssDest: webroot + "css/login.min.css",
    concatGridCssDest: webroot + "css/grid.min.css",
    concatZTreeCssDest: webroot + "css/ztree.min.css"
};

gulp.task("clean:js_base", function (cb) {
    rimraf(paths.concatBaseJsDest, cb);
});
gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});
gulp.task("clean:js_content", function (cb) {
    rimraf(paths.concatContentJsDest, cb);
});
gulp.task("clean:js_validate", function (cb) {
    rimraf(paths.concatValidateJsDest, cb);
});
gulp.task("clean:js_grid", function (cb) {
    rimraf(paths.concatGridJsDest, cb);
});
gulp.task("clean:js_menu", function (cb) {
    rimraf(paths.concatMenuJsDest, cb);
});
gulp.task("clean:js_ztree", function (cb) {
    rimraf(paths.concatZTreeJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});
gulp.task("clean:css_content", function (cb) {
    rimraf(paths.concatContentCssDest, cb);
});
gulp.task("clean:css_login", function (cb) {
    rimraf(paths.concatLoginCssDest, cb);
});
gulp.task("clean:css_grid", function (cb) {
    rimraf(paths.concatGridCssDest, cb);
});
gulp.task("clean:css_ztree", function (cb) {
    rimraf(paths.concatZTreeCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:js_base", "clean:js_content", "clean:js_validate", "clean:js_grid", "clean:js_menu", "clean:js_ztree", "clean:css", "clean:css_content", "clean:css_login", "clean:css_grid", "clean:css_ztree"]);

gulp.task("min:js_base", function () {
    return gulp.src(paths.baseJs, { base: "." })
        .pipe(concat(paths.concatBaseJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:js_layout", function () {
    return gulp.src(paths.layoutJs, { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:js_content", function () {
    return gulp.src(paths.contentJs, { base: "." })
        .pipe(concat(paths.concatContentJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:js_validate", function () {
    return gulp.src(paths.validateJs, { base: "." })
        .pipe(concat(paths.concatValidateJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:js_grid", function () {
    return gulp.src(paths.gridJs, { base: "." })
        .pipe(concat(paths.concatGridJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:js_menu", function () {
    return gulp.src(paths.menuJs, { base: "." })
        .pipe(concat(paths.concatMenuJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:js_ztree", function () {
    return gulp.src(paths.ztreeJs, { base: "." })
        .pipe(concat(paths.concatZTreeJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css_layout", function () {
    return gulp.src(paths.layoutCss)
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min:css_content", function () {
    return gulp.src(paths.contentCss)
        .pipe(concat(paths.concatContentCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min:css_login", function () {
    return gulp.src(paths.loginCss)
        .pipe(concat(paths.concatLoginCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min:css_grid", function () {
    return gulp.src(paths.gridCss)
        .pipe(concat(paths.concatGridCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min:css_ztree", function () {
    return gulp.src(paths.ztreeCss)
        .pipe(concat(paths.concatZTreeCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js_base", "min:js_layout", "min:js_content", "min:js_validate", "min:js_grid", "min:js_menu", "min:js_ztree", "min:css_layout", "min:css_content", "min:css_login", "min:css_grid", "min:css_ztree"]);
