/// <binding BeforeBuild='sass' Clean='autoprefixer' />
module.exports = function (grunt) {
    'use strict';

    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-autoprefixer');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        // Sass
        sass: {
            options: {
                implementation: require('sass'),
                sourceMap: true,
                outputStyle: 'compressed'
            },
            dist: {
                files: [
                    {
                        expand: true,
                        cwd: "sass",
                        src: ["**/*.scss"],
                        dest: "stylesheets",
                        ext: ".css"
                    }
                ]
            }
        },
        // Autoprefixer
        autoprefixer: {
            options: {
                browsers: ['last 2 versions'],
                map: true // Update source map (creates one if it can't find an existing map)
            },

            // Prefix all files
            multiple_files: {
                src: 'stylesheets/**/*.css'
            },
        },

        // Watch
        watch: {
            css: {
                files: ['sass/**/*.scss'],
                tasks: ['sass', 'autoprefixer'],
                options: {
                    spawn: false
                }
            }
        }
    });

    grunt.registerTask('dev', ['watch']);
    grunt.registerTask('prod', ['sass', 'autoprefixer']);
};