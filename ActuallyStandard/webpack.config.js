module.exports = {
    entry: [
        './Assets/src/index.tsx',
        './Assets/sass/index.scss',
        './node_modules/bootstrap/dist/css/bootstrap.min.css'
    ],
    output: {
      filename: 'bundle.js',
      path: __dirname + '/wwwroot/dist'
    },
    
    devtool: "source-map",
    resolve: {
        extensions: [".ts", ".tsx", ".js", ".json"],
        /*
        alias: {
            'vue$': 'vue/dist/vue.esm.js' // 'vue/dist/vue.common.js' for webpack 1
        }
        */
    },

    module: {
        rules: [
            // All files with a '.ts' or '.tsx' extension will be handled by 'awesome-typescript-loader'.
            {
                test: /\.tsx?$/,
                loader: "awesome-typescript-loader",
                /*
                options: {
                    appendTsSuffixTo: [/\.vue$/],
                }
                */
            },

            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            { enforce: "pre", test: /\.js$/, loader: "source-map-loader" },

            { test: /\.scss$/, use: ["style-loader", "css-loader", "sass-loader"] },
            { test: /\.css$/, use: ['style-loader', 'css-loader'] }
            /*
            {
                test: /\.vue$/,
                loader: 'vue-loader'
              },
              */
        ]
    },
  };