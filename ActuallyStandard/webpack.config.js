module.exports = {
    entry: [
        './Assets/src/index.ts',
        './Assets/sass/index.scss',
        './node_modules/bootstrap/dist/css/bootstrap.min.css'
    ],
    output: {
      filename: 'bundle.js',
      path: __dirname + '/wwwroot/dist'
    },
    
    devtool: "source-map",
    resolve: {
        // Add '.ts' and '.tsx' as resolvable extensions.
        extensions: [".ts", ".tsx", ".js", ".json"]
    },

    module: {
        rules: [
            // All files with a '.ts' or '.tsx' extension will be handled by 'awesome-typescript-loader'.
            { test: /\.tsx?$/, loader: "awesome-typescript-loader" },

            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            //{ enforce: "pre", test: /\.js$/, loader: "source-map-loader" },

            { test: /\.scss$/, use: ["style-loader", "css-loader", "sass-loader"] },
            { test: /\.css$/, use: ['style-loader', 'css-loader'] }
        ]
    },
  };