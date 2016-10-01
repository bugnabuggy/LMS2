/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var sass = require('gulp-sass');
var concatCss = require('gulp-concat-css');
var shell = require('gulp-shell');


var libs = ['./node_modules/core-js/client/shim.min.js',
            './node_modules/zone.js/dist/zone.js',
            './node_modules/reflect-metadata/Reflect.js',
            './node_modules/systemjs/dist/system.src.js' ];


gulp.task("libs", function () {

});

gulp.task('default', function () {
    // place code for your default task here
});

gulp.task('watch', function () {
    gulp.watch([
        './Assets/Styles/**/*.scss',
        './wwwroot/app/**/*.ts'],
        [   'sass',
            'shell.tsc']);
});


// make it in console to avoid any errors
gulp.task('shell.tsc',
    shell.task([
        'tsc'
    ]));

gulp.task('tsc:watch', function () {
    gulp.watch('./wwwroot/app/**/*.ts', ['shell.tsc']);
});


gulp.task('sass', function () {
    return gulp.src([   
                        './Assets/**/*.scss'
    ])
      .pipe(sass().on('error', sass.logError))
      .pipe(concatCss("site.css"))
      .pipe(gulp.dest('./wwwroot/css'));
});

gulp.task('sass:watch', function () {
    gulp.watch('./Assets/Styles/**/*.scss', ['sass']);
});
