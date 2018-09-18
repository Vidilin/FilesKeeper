module.exports = function (grunt) {
    grunt.initConfig({
        concat: {
            build: {
                src: ['wwwroot/js/*'],
                dest: 'wwwroot/js/app.js'
            }
        },
        uglify: {
            build: {
                files: {
                    'wwwroot/bundles/jquery.js': ['wwwroot/lib/jquery/dist/jquery.js'],
                    'wwwroot/bundles/jqueryval.js': ['wwwroot/lib/jquery-validation/dist/jquery.validate.js', 'lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js'],
                    'wwwroot/bundles/bootstrap.js': ['wwwroot/lib/bootstrap/dist/js/bootstrap.min.js'],
                    'wwwroot/bundles/dropzone.js': ['wwwroot/lib/dropzone/dist/min/dropzone.min.js'],
                    'wwwroot/bundles/app.js': ['wwwroot/js/app.js']
                }
            }
        },
        clean: ["wwwroot/js/app.js"],
        cssmin: {
            build: {
                files: {
                    'wwwroot/bundles/main.css': ['wwwroot/lib/bootstrap/dist/css/bootstrap.css', 'wwwroot/css/site.css']
                }
            }
        },
        copy: {
            build: {
                expand: true,
                cwd: 'wwwroot/lib/bootstrap/dist/fonts/',
                src: '**',
                dest: 'wwwroot/fonts/',
                flatten: true,
                filter: 'isFile'
            },
        }
    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-uglify');

    grunt.registerTask('build', ['clean', 'copy', 'concat', 'uglify', 'cssmin']);
};