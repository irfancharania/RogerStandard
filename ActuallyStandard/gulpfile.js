'use strict';   // Enable strict mode for JavaScript (See https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Strict_mode).

// Set up imported packages.
var gulp = require('gulp'),
    gulpif = require('gulp-if'),                // If statement (https://www.npmjs.com/package/gulp-if/)
    del = require("del"),
    concat = require('gulp-concat'),            // Concatenate files (https://www.npmjs.com/package/gulp-concat/)
    csslint = require('gulp-csslint'),          // CSS linter (https://www.npmjs.com/package/gulp-csslint/)
    plumber = require('gulp-plumber'),          // Handles Gulp errors (https://www.npmjs.com/package/gulp-plumber)
    rename = require('gulp-rename'),            // Renames file paths (https://www.npmjs.com/package/gulp-rename/)
    size = require('gulp-size'),                // Prints size of files to console (https://www.npmjs.com/package/gulp-size/)
    sourcemaps = require('gulp-sourcemaps'),    // Creates source map files (https://www.npmjs.com/package/gulp-sourcemaps/)
    merge = require('merge-stream'),            // Merges one or more gulp streams into one (https://www.npmjs.com/package/merge-stream/)
    uglify = require('gulp-uglify')             // Minifies JavaScript (https://www.npmjs.com/package/gulp-uglify/)
    ;

// Initialize directory paths.
var paths = {
    // Source Directory Paths
    nodeModules: './node_modules/',
    scripts: 'Assets/Scripts/',
    styles: 'Assets/Styles/',
    tests: 'Tests/',
    semantic: 'Assets/semantic/',

    // Destination Directory Paths
    wwwroot: './wwwroot/',
    css: './wwwroot/css/',
    fonts: './wwwroot/fonts/',
    img: './wwwroot/img/',
    js: './wwwroot/js/'
};


// Initialize the mappings between the source and output files.
var sources = {
    // An array containing objects required to build a single CSS file.
    css: [
        {
            name: 'semantic.css',
            copy: true,
            paths: paths.semantic + '/dist/semantic.min.css'
        },
        {
            name: 'site.css',
            // paths - An array of paths to CSS or SASS files which will be compiled to CSS, concatenated and minified
            // to create a file with the above file name.
            paths: [
                paths.styles + 'site.scss'
            ]
        }
    ],
    // An array of paths to images to be optimized.
    img: [
        paths.img + '**/*.{png,jpg,jpeg,gif,svg}'
    ],
    // An array containing objects required to build a single JavaScript file.
    js: [
        {
            name: 'jquery.js',
            copy: true,
            paths: paths.nodeModules + 'jquery/dist/jquery.min.js'
        },
        {
            name: 'semantic.js',
            copy: true,
            paths: paths.semantic + '/dist/semantic.min.js'
        },
        {
            name: 'jquery-validate.js',
            copy: true,
            paths: paths.nodeModules + 'jquery-validation/dist/jquery.validate.min.js'
        },
        {
            name: 'jquery-validate-unobtrusive.js',
            paths: paths.nodeModules + 'jquery-validation-unobtrusive/jquery.validate.unobtrusive.js'
        },
        {
            name: 'all.js',
            paths: [
                paths.scripts + 'site.js'
            ]
        }
    ]
};

// Calls and returns the result from the gulp-size plugin to print the size of the stream. Makes it more readable.
function sizeBefore(title) {
    return size({
        title: 'Before: ' + title
    });
}
function sizeAfter(title) {
    return size({
        title: 'After: ' + title
    });
}

/*
 * Deletes all files and folders within the js directory.
 */
gulp.task('clean-js', function () {
    return del(paths.js);
});


/*
 * Builds the JavaScript files for the site.
 */
gulp.task('build-js', 
function () {
    var tasks = sources.js.map(function (source) {  // For each set of source files in the sources.
        if (source.copy) {                          // If we are only copying files.
            return gulp
                .src(source.paths)                  // Start with the source paths.
                .pipe(rename({                      // Rename the file to the source name.
                    basename: source.name,
                    extname: ''
                }))
                .pipe(gulp.dest(paths.js));         // Saves the JavaScript file to the specified destination path.
        }
        else {
            return gulp                             // Return the stream.
                .src(source.paths)                  // Start with the source paths.
                .pipe(plumber())                    // Handle any errors.
                .pipe(concat(source.name))          // Concatenate JavaScript files into a single file with the specified name.
                .pipe(sizeBefore(source.name))      // Write the size of the file to the console before minification.
                .pipe(uglify())                     // Minifies the JavaScript.
                .pipe(sizeAfter(source.name))       // Write the size of the file to the console after minification.
                .pipe(sourcemaps.write('.'))        // Generates source .map files for the JavaScript.
                .pipe(gulp.dest(paths.js));         // Saves the JavaScript file to the specified destination path.
        }
    });
    return merge(tasks);                            // Combine multiple streams to one and return it so the task can be chained.
});


/*
 * Deletes all files and folders within the css directory.
 */
gulp.task('clean-css', function () {
    return del(paths.css);
});

gulp.task('default', function(){
    var tasks = sources.css.map(function (source) {  // For each set of source files in the sources.
        return gulp
            .src(source.paths)                  // Start with the source paths.
            .pipe(rename({                      // Rename the file to the source name.
                basename: source.name,
                extname: ''
            }))
            .pipe(gulp.dest(paths.css)); 
    });
    return merge(tasks);                            // Combine multiple streams to one and return it so the task can be chained.
})
